using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.MemberManage
{
    public partial class RechargeUnderLine : BasePage
    {
        public int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            selIncreasingMode.Disabled = true;
            if (ConvertHelper.QueryString(Request, "ID", 0) > 0)
            {
                Id = ConvertHelper.QueryString(Request, "ID", 0);
            }
            if (!IsPostBack)
            {
                Audit_Btn.Attributes.Add("onclick", "this.value='提交中...';this.disabled = true;this.style='width: 100px;';");
                if (Id > 0)
                {
                    CurrorStatus.Visible = true;
                    AuditStatus.Visible = true;
                    AuditReco.Visible = true;
                    AreaNote.Visible = true;
                    Operate_Btn.Visible = false;
                    selectSpan.Visible = false;
                    txtOrderNumber.Disabled = true;
                    txtOrderNumber.Attributes.Remove("class");
                    txtOrderNumber.Attributes.Add("class", "input_Disabled fl");
                    txtAmount.Disabled = true;
                    txtAmount.Attributes.Remove("class");
                    txtAmount.Attributes.Add("class", "input_Disabled fl");
                    txtCreateTime.Disabled = true;
                    txtCreateTime.Attributes.Remove("class");
                    txtCreateTime.Attributes.Add("class", "input_Disabled fl");
                    var rechargeRecordBll = new RechargeRecordBll();
                    var memberBll = new MemberBll();
                    var memberInfoBll = new MemberInfoBll();
                    var rechargeModel = rechargeRecordBll.GetModel(Id);
                    if (rechargeModel != null)
                    {
                        txtMemberName.Value = memberBll.GetModel(rechargeModel.MemberID).MemberName;
                        txtRealName.Value = memberInfoBll.GetModel(rechargeModel.MemberID).RealName;
                        txtMemberID.Value = rechargeModel.MemberID.ToString(CultureInfo.InvariantCulture);

                        txtOrderNumber.Value = rechargeModel.OrderNumber;
                        txtAmount.Value = rechargeModel.Amount.ToString("f2");
                        txtCreateTime.Value = rechargeModel.ApplicationTime.ToString("yyyy-MM-dd HH:mm:ss");
                        selIncreasingMode.Value = rechargeModel.AuditStatus.ToString(CultureInfo.InvariantCulture);
                        litAuditReco.InnerHtml = !string.IsNullOrEmpty(rechargeModel.AuditRecords) ? (rechargeModel.AuditRecords.Contains("|")
                                                     ? (rechargeModel.AuditRecords.Split('|')[0] + "</br>" + rechargeModel.AuditRecords.Split('|')[1])
                                                     : rechargeModel.AuditRecords) : "";
                        if (selIncreasingMode.Value == "2" || selIncreasingMode.Value == "3" || selIncreasingMode.Value == "4")
                        {
                            Audit_Btn.Visible = false;
                        }


                        if ((RightArray.IndexOf("|9|", StringComparison.Ordinal) >= 0 && selIncreasingMode.Value == "0") || (RightArray.IndexOf("|11|", StringComparison.Ordinal) >= 0 && selIncreasingMode.Value == "1"))
                        {
                            Audit_Btn.Visible = true;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('初始化数据时发生异常，请联系系统管理员。','error', '');", true);
                    }
                }
                else
                {
                    if (!Role.IsRoleNew(RightArray, "2"))
                    {
                        Audit_Btn.Visible = false;
                        Operate_Btn.Visible = false;
                    }
                }

            }
        }

        //线下充值申请
        protected void Operate_Btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMemberID.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择会员。','warning', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(txtOrderNumber.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请填写银行交易流水号。','warning', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(txtCreateTime.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择交易时间。','warning', '');", true);
                return;
            }

            if (!RegexHelper.IsDecimal(txtAmount.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('申请充值金额输入错误。','warning', '');", true);
                return;
            }

            int memberId = ConvertHelper.ToInt(txtMemberID.Value.Trim());
            string orderNumber = txtOrderNumber.Value.Trim();
            decimal rechargeAmount = ConvertHelper.ToDecimal(txtAmount.Value.Trim());
            var rechargeRecordModel = new RechargeRecordModel
                {
                    MemberID = memberId,
                    Type = 1,
                    RechargeChannel = 0,
                    OrderNumber = orderNumber,
                    MerchantOrderNo = "",
                    Amount = rechargeAmount,
                    ApplicationTime = ConvertHelper.ToDateTime(txtCreateTime.Value.Trim()),
                    RechargeFee = 0
                };
            var rechargeRecordBll = new RechargeRecordBll();
            ClientScript.RegisterClientScriptBlock(GetType(), "", rechargeRecordBll.Add(rechargeRecordModel) > 0 ? "MessageAlert('申请成功，请耐心等待审核。','success', '/MemberManage/RechargeManage.aspx?columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);

        }


        //线下充值审核
        protected void Audit_Btn_Click(object sender, EventArgs e)
        {
            string auditStatus = Request.Form["AuditStatus"];

            if (string.IsNullOrEmpty(auditStatus))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择审核状态。','error', '');", true);
                return;
            }
            string note = txtAreaNote.Value.Trim();
            switch (selIncreasingMode.Value)
            {
                case "0"://初审
                    var rechargeRecordBll = new RechargeRecordBll();
                    var rechargeModel = rechargeRecordBll.GetModel(Id);
                    rechargeModel.AuditStatus = ConvertHelper.ToInt(auditStatus);
                    rechargeModel.AuditRecords = (auditStatus == "1" ? "初审通过—" : "初审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）|";
                    rechargeModel.UpdateTime = DateTime.Now;
                    ClientScript.RegisterClientScriptBlock(GetType(), "", rechargeRecordBll.Update(rechargeModel) ? "MessageAlert('操作成功。','success', '/MemberManage/RechargeUnderLine.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                    break;
                case "1"://复审
                    switch (auditStatus)
                    {
                        case "1"://复审通过
                            var rechargeBllFt = new RechargeRecordBll();
                            var itemFt = rechargeBllFt.GetModel(Id);
                            itemFt.ID = Id;
                            itemFt.AuditRecords = itemFt.AuditRecords + "复审通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            int resultVal = rechargeBllFt.RechargeBelowLine(itemFt);
                            switch (resultVal)
                            {
                                case 1:
                                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('充值操作成功。','success', '/MemberManage/RechargeUnderLine.aspx?ID=" + Id + "&columnId=" + ColumnId + "');", true); break;
                                case -1:
                                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('操作失败，状态已不是最新的了，请刷新页面。','error', '')", true); break;
                                case 0:
                                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('操作异常，请联系系统管理员。','error', '');", true); break;
                            }
                            break;
                        case "2"://复审不通过
                            var rechargeBll = new RechargeRecordBll();
                            var item = rechargeBll.GetModel(Id);
                            item.AuditStatus = 3;
                            item.AuditRecords = item.AuditRecords + "复审不通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            item.UpdateTime = DateTime.Now;
                            ClientScript.RegisterClientScriptBlock(GetType(), "", rechargeBll.Update(item) ? "MessageAlert('操作成功。','success', '/MemberManage/RechargeUnderLine.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                            break;
                    }
                    break;
            }

            Audit_Btn.Attributes.Add("style", "width: 60px;");
        }
    }
}