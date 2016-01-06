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

namespace WebUI.FinanceSettlement
{
    public partial class ReturnCashFeeAdd : BasePage
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
                if (Id > 0)
                {
                    CurrorStatus.Visible = true;
                    AuditStatus.Visible = true;
                    AuditReco.Visible = true;
                    AreaNote.Visible = true;
                    Operate_Btn.Visible = false;
                    selectSpan.Visible = false;

                    txtAmount.Disabled = true;
                    txtAmount.Attributes.Remove("class");
                    txtAmount.Attributes.Add("class", "input_Disabled fl");

                    var returnCashFeeBll = new ReturnCashFeeBll();
                    var memberBll = new MemberBll();
                    var memberInfoBll = new MemberInfoBll();
                    var returnCashFeeModel = returnCashFeeBll.GetModel(Id);
                    if (returnCashFeeModel != null)
                    {
                        txtMemberName.Value = memberBll.GetModel(returnCashFeeModel.MemberId).MemberName;
                        txtRealName.Value = memberInfoBll.GetModel(returnCashFeeModel.MemberId).RealName;
                        txtMemberID.Value = returnCashFeeModel.MemberId.ToString(CultureInfo.InvariantCulture);


                        txtAmount.Value = returnCashFeeModel.Amount.ToString("f2");
                        txtDescription.Value = returnCashFeeModel.Description;
                        selIncreasingMode.Value = returnCashFeeModel.AuditStatus.ToString(CultureInfo.InvariantCulture);
                        litAuditReco.InnerHtml = !string.IsNullOrEmpty(returnCashFeeModel.AuditRecords) ? (returnCashFeeModel.AuditRecords.Contains("|")
                                                     ? (returnCashFeeModel.AuditRecords.Split('|')[0] + "</br>" + returnCashFeeModel.AuditRecords.Split('|')[1])
                                                     : returnCashFeeModel.AuditRecords) : "";
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

            }

        }

        protected void Operate_Btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMemberID.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择会员。','warning', '');", true);
                return;
            }

            if (!RegexHelper.IsDecimal(txtAmount.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('申请发放奖励金额输入错误。','warning', '');", true);
                return;
            }

            int memberId = ConvertHelper.ToInt(txtMemberID.Value.Trim());
            decimal Amount = ConvertHelper.ToDecimal(txtAmount.Value.Trim());
            string description = txtDescription.Value;
            var returnCashFeeModel = new ReturnCashFeeModel
            {
                MemberId = memberId,
                FeeType = 31,
                Amount = Amount,
                AuditStatus = 0,
                Description = description
            };
            var returnCashFeeBll = new ReturnCashFeeBll();
            ClientScript.RegisterClientScriptBlock(GetType(), "", returnCashFeeBll.Add(returnCashFeeModel) > 0 ? "MessageAlert('申请成功，请耐心等待审核。','success', '/FinanceSettlement/ReturnCashFeeManage.aspx?columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
        }

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
                    var returnCashFeeBll = new ReturnCashFeeBll();
                    var returnCashFeeModel = returnCashFeeBll.GetModel(Id);
                    returnCashFeeModel.AuditStatus = ConvertHelper.ToInt(auditStatus);
                    returnCashFeeModel.AuditRecords = (auditStatus == "1" ? "初审通过—" : "初审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）|";
                    returnCashFeeModel.UpdateTime = DateTime.Now;
                    ClientScript.RegisterClientScriptBlock(GetType(), "", returnCashFeeBll.Update(returnCashFeeModel) ? "MessageAlert('操作成功。','success', '/FinanceSettlement/ReturnCashFeeAdd.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                    break;
                case "1"://复审
                    switch (auditStatus)
                    {
                        case "1"://复审通过
                            var returnCashFeeBllFt = new ReturnCashFeeBll();
                            var itemFt = returnCashFeeBllFt.GetModel(Id);
                            itemFt.Id = Id;
                            itemFt.AuditRecords = itemFt.AuditRecords + "复审通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            ClientScript.RegisterClientScriptBlock(GetType(), "", returnCashFeeBllFt.ReturnCashFeeAudit(itemFt) ? "MessageAlert('发放奖励操作成功。','success', '/FinanceSettlement/ReturnCashFeeAdd.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                            break;
                        case "2"://复审不通过
                            var returnCashFeeBllNo = new ReturnCashFeeBll();
                            var item = returnCashFeeBllNo.GetModel(Id);
                            item.AuditStatus = 3;
                            item.AuditRecords = item.AuditRecords + "复审不通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            item.UpdateTime = DateTime.Now;
                            ClientScript.RegisterClientScriptBlock(GetType(), "", returnCashFeeBllNo.Update(item) ? "MessageAlert('操作成功。','success', '/FinanceSettlement/ReturnCashFeeAdd.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                            break;
                    }
                    break;
            }
        }
    }
}