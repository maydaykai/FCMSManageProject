using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.FinanceSettlement
{
    public partial class ReCommendRewardGrantAdd : BasePage
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

                    txtYear.Disabled = true;
                    txtYear.Attributes.Remove("class");
                    txtYear.Attributes.Add("class", "input_Disabled fl");

                    txtMonth.Disabled = true;
                    txtMonth.Attributes.Remove("class");
                    txtMonth.Attributes.Add("class", "input_Disabled fl");

                    txtAmount.Disabled = true;
                    txtAmount.Attributes.Remove("class");
                    txtAmount.Attributes.Add("class", "input_Disabled fl");

                    var bll = new ReCommendRewardGrantBll();
                    var model = bll.GetModel(Id);
                    if (model != null)
                    {
                        txtYear.Value = model.Year.ToString();
                        txtMonth.Value = model.Month.ToString();
                        txtAmount.Value = model.Amount.ToString("f2");
                        selIncreasingMode.Value = model.AuditStatus.ToString();
                        litAuditReco.InnerHtml = !string.IsNullOrEmpty(model.AuditRecords) ? (model.AuditRecords.Contains("|")
                                                     ? (model.AuditRecords.Split('|')[0] + "</br>" + model.AuditRecords.Split('|')[1])
                                                     : model.AuditRecords) : "";
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
            string year = txtYear.Value.Trim();
            string month = txtMonth.Value.Trim();
            string amount = txtAmount.Value.Trim();
            if (string.IsNullOrEmpty(year))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入年。','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(month))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入月份。','warning', '');", true);
                return;
            }
            if (!RegexHelper.IsDecimal(amount))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('申请发放奖励金额输入错误。','warning', '');", true);
                return;
            }
            var bll = new ReCommendRewardGrantBll();
            ReCommendRewardGrantModel modelCount = bll.GetModel("AuditStatus IN (0,2,4) AND [Year]=" + year + " AND [Month]=" + month);

            if (modelCount != null)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('该月奖励已申请，请勿重复申请。','warning', '');", true);
                return;
            }
            var model = new ReCommendRewardGrantModel
            {
                Year = ConvertHelper.ToInt(year),
                Month = ConvertHelper.ToInt(month),
                Amount = ConvertHelper.ToDecimal(amount),
                ApplyUserID = MemberId
            };
            ClientScript.RegisterClientScriptBlock(GetType(), "", bll.Add(model) > 0 ? "MessageAlert('申请成功，请耐心等待审核。','success', '/FinanceSettlement/RecommendRewardGrantManage.aspx?columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
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
            var bll = new ReCommendRewardGrantBll();
            var model = bll.GetModel(Id);
            model.UpdateTime = DateTime.Now;
            switch (selIncreasingMode.Value)
            {
                case "0"://初审
                    model.AuditStatus = ConvertHelper.ToInt(auditStatus);
                    model.AuditRecords = (auditStatus == "1" ? "初审通过—" : "初审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）|";
                    ClientScript.RegisterClientScriptBlock(GetType(), "", bll.Update(model) ? "MessageAlert('操作成功。','success', '/FinanceSettlement/RecommendRewardGrantAdd.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                    break;
                case "1"://复审
                    switch (auditStatus)
                    {
                        case "1"://复审通过
                            model.AuditRecords = model.AuditRecords + "复审通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            ClientScript.RegisterClientScriptBlock(GetType(), "", bll.ReCommendRewardGrantSuccessHandler(model) ? "MessageAlert('发放奖励操作成功。','success', '/FinanceSettlement/RecommendRewardGrantAdd.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常或无当月数据，请联系系统管理员。','error', '');", true);
                            break;
                        case "2"://复审不通过
                            model.AuditStatus = 3;
                            model.AuditRecords = model.AuditRecords + "复审不通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            ClientScript.RegisterClientScriptBlock(GetType(), "", bll.Update(model) ? "MessageAlert('操作成功。','success', '/FinanceSettlement/RecommendRewardGrantAdd.aspx?ID=" + Id + "&columnId=" + ColumnId + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                            break;
                    }
                    break;
            }
        }
    }
}