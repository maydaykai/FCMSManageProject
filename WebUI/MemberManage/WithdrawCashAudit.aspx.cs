using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System.Text;

namespace WebUI.MemberManage
{
    public partial class WithdrawCashAudit : BasePage
    {
        public int Id;
        private int _cashMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            //线上提现审核按妞
            Btn_Audit.Visible = false;
            Btn_Audit.Enabled = false;

            //线下提现审核按钮
            cashAudit_Btn.Visible = false;
            cashAudit_Btn.Enabled = false;
            Id = ConvertHelper.QueryString(Request, "ID", 0);
            _cashMode = ConvertHelper.QueryString(Request, "CashMode", 0);
            if (!IsPostBack)
            {
                if (Id > 0)
                {
                    bankListTr.Visible = true;
                    bankTypeTr.Visible = false;
                    bankAccountTr.Visible = false;
                    txtAccountHolder.Disabled = true;
                    txtAccountHolder.Attributes.Remove("class");
                    txtAccountHolder.Attributes.Add("class", "input_Disabled fl");
                    cashApply_Btn.Visible = false;
                    cashApply_Btn.Enabled = false;

                    Btn_Audit.Visible = true;
                    Btn_Audit.Enabled = true;
                    cashAudit_Btn.Visible = true;
                    cashAudit_Btn.Enabled = true;

                    selCurrStatus_TR.Visible = true;
                    AuditStatus_TR.Visible = true;
                    AuditRemark_TR.Visible = true;
                    AuditRecords_TR.Visible = true;
                    txtCashAmount.Disabled = true;
                    txtreason.Disabled = true;

                    txtCashAmount.Attributes.Remove("class");
                    txtCashAmount.Attributes.Add("class", "input_Disabled fl");
                    selectSpan.Disabled = true;
                    selectSpan.Visible = false;
                    var memberBll = new MemberBll();
                    var memberInfoBll = new MemberInfoBll();
                    var cashRecordBll = new CashRecordBLL();
                    var cashRecordModel = cashRecordBll.getCashRecordModel(Id);

                    var bankAccountBll = new BankAccountBll();//线上
                    var bankAccountBlBll = new BankAccount_BLBLL();//线下
                    var bankApply = new StringBuilder();
                    var bankAccountModel = new BankAccountModel();
                    var bankAccountAuthentModel = new BankAccountAuthentModel();
                    var bankAccountBlModel = new BankAccount_BLModel();
                    txtreason.Visible = cashRecordModel.CashMode != 0;

                    if (_cashMode == 0)
                    {
                        if (cashRecordModel.BankAccountType == 1)
                            bankAccountModel = bankAccountBll.getBankAccountModel(cashRecordModel.BankAccountID);
                        else
                            bankAccountAuthentModel = bankAccountBll.GetBankAccountAuthentModel("BA.ID=" + cashRecordModel.BankAccountID);
                    }
                    else
                        bankAccountBlModel = bankAccountBlBll.GetModel(cashRecordModel.BankAccountID);

                    txtMemberName.Value = memberBll.GetModel(cashRecordModel.MemberID).MemberName;
                    txtAccountHolder.Value = (_cashMode == 0 ? memberInfoBll.GetModel(cashRecordModel.MemberID).RealName : bankAccountBlModel.AccountHolder);
                    txtCashAmount.Value = cashRecordModel.CashAmount.ToString("F2");
                    txtreason.Value = cashRecordModel.ApplyReason ?? "";
                    txtMemberID.Value = cashRecordModel.MemberID.ToString(CultureInfo.InvariantCulture);
                    selCurrStatus.Value = cashRecordModel.Status.ToString(CultureInfo.InvariantCulture);
                    litAuditReco.InnerHtml = !string.IsNullOrEmpty(cashRecordModel.AuditRecords) ? (cashRecordModel.AuditRecords.Contains("|")
                             ? (cashRecordModel.AuditRecords.Split('|')[0] + "</br>" + cashRecordModel.AuditRecords.Split('|')[1])
                             : cashRecordModel.AuditRecords) : "";

                    bankApply.Append("<p class=\"clear\" style=\"padding: 5px;\">");
                    bankApply.Append("<span class=\"fl\" style=\"line-height: 36px;\">");
                    bankApply.Append("<input id=\"rdBank\" name=\"rdBank\" type=\"radio\" checked=\"checked\" value=\"" + cashRecordModel.BankAccountID + "\" />");
                    bankApply.Append("</span>");
                    bankApply.Append("<span class=\"icon_box fl\">");
                    bankApply.Append("<span class=\"icon_b " + (_cashMode == 1 ? bankAccountBlModel.EnglishName : (cashRecordModel.BankAccountType == 1 ? bankAccountModel.EnglishName : bankAccountAuthentModel.EnglishName)) + "\"></span>");
                    bankApply.Append("</span>");
                    bankApply.Append("<span class=\"fl\" style=\"line-height: 36px; margin-left: 5px; font-size: 14px; font-family: Verdana;\">");
                    bankApply.Append((_cashMode == 1 ? bankAccountBlModel.BankCardNo : (cashRecordModel.BankAccountType == 1 ? bankAccountModel.BankCardNo : bankAccountAuthentModel.BankCardNo)));
                    bankApply.Append("</span>");
                    bankApply.Append("</p>");
                    bankList.InnerHtml = bankApply.ToString();

                    if (cashRecordModel.CashPayType == 1)
                        tlpay.Attributes["checked"] = "checked";
                    //else if (cashRecordModel.CashPayType == 2)
                    //    llpay.Attributes["checked"] = "checked";
                    else if (cashRecordModel.CashPayType == 3)
                        tlsspay.Attributes["checked"] = "checked";


                    if (selCurrStatus.Value == "2" || selCurrStatus.Value == "3" || selCurrStatus.Value == "4")
                    {
                        cashAudit_Btn.Visible = false;
                        cashAudit_Btn.Enabled = false;
                        Btn_Audit.Visible = false;
                        Btn_Audit.Enabled = false;
                    }

                    if (!(RightArray.IndexOf("|9|", StringComparison.Ordinal) >= 0 && selCurrStatus.Value == "0") && !(RightArray.IndexOf("|11|", StringComparison.Ordinal) >= 0 && selCurrStatus.Value == "1"))
                    {
                        cashAudit_Btn.Visible = false;
                        cashAudit_Btn.Enabled = false;
                        Btn_Audit.Visible = false;
                        Btn_Audit.Enabled = false;
                    }

                    if (_cashMode == 0)
                    {
                        cashAudit_Btn.Visible = false;
                        cashAudit_Btn.Enabled = false;
                    }
                    else
                    {
                        Btn_Audit.Visible = false;
                        Btn_Audit.Enabled = false;
                    }
                    //balance.InnerText = cashRecordBll.GetLLBalance().amt_balance;
                }
            }
        }


        //线上提现审核
        protected void Audit_Click(object sender, EventArgs e)
        {
            string auditStatus = Request.Form["AuditStatus"];
            string cashPayType = Request.Form["rdPayment"];
            if (string.IsNullOrEmpty(auditStatus))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择审核状态。','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(cashPayType))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择支付平台类型。','warning', '');", true);
                return;
            }
            bool flag = false;
            string errorMsg = "";
            string note = auditRemark.Value.Trim();
            CashRecordBLL _bll = new CashRecordBLL();
            CashRecordModel oldCashRecordModel = _bll.getCashRecordModel(Id);
            if (oldCashRecordModel.Status >= 2)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('该笔提现已" + oldCashRecordModel.StatusStr + ",请不要重复审核！','warning', '');", true);
                return;
            }
            var cashRecordModel = new CashRecordModel
            {
                ID = Id,
                Remark = "",
                CashPayType = Convert.ToInt32(cashPayType),
                UpdateTime = DateTime.Now
            };
            switch (selCurrStatus.Value)//当前状态
            {
                case "0"://初审中
                    cashRecordModel.AuditRecords = (auditStatus == "1" ? "初审通过—" : "初审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）|";
                    if (Convert.ToInt32(auditStatus) == 1)//通过                                                                                                                                                                                                                                                                                                                                                                                
                    {
                        cashRecordModel.Status = 1;
                        flag = _bll.updateCashRecord(cashRecordModel);
                    }else{
                        cashRecordModel.Status = 2;
                        flag = _bll.withdrawCashAuditFail(cashRecordModel);
                    }
                    break;
                case "1":
                    cashRecordModel.AuditRecords = (auditStatus == "1" ? "复审通过—" : "复审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                    if (Convert.ToInt32(auditStatus) == 1)//通过                                                                                                                                                                                                                                                                                                                                                                                
                    {
                        cashRecordModel.Status = 4;
                        flag = _bll.withdrawCashAuditSuccess(cashRecordModel, ref errorMsg);
                    }
                    else
                    {
                        cashRecordModel.Status = 3;
                        flag = _bll.withdrawCashAuditFail(cashRecordModel);
                    }
                    break;
            }
            if (flag)
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核成功','success', location.href);", true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核失败   " + errorMsg + "','error', location.href);", true);
        }

        //线下提现申请
        protected void cashApply_Btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMemberID.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择会员。','warning', '');", true);
                return;
            }

            int memberId = ConvertHelper.ToInt(txtMemberID.Value.Trim());
            string accountHolder = txtAccountHolder.Value.Trim();
            int bankId = ConvertHelper.ToInt(Request.Form["sel_BankType"].Trim());
            string bankAccount = txtBankAccount.Value.Trim();
            decimal cashAmount = ConvertHelper.ToDecimal(txtCashAmount.Value.Trim());

            var cashRecordBll = new CashRecordBLL();
            var obj = cashRecordBll.BelowCashApply(memberId, accountHolder, bankId, bankAccount, cashAmount);
            var resultStr = (obj != null) ? obj.ToString() : "";
            ClientScript.RegisterClientScriptBlock(GetType(), "", (resultStr.Split('|')[0] == "1") ? "MessageAlert('" + resultStr.Split('|')[1] + "','success', '/MemberManage/WithdrawCashManage.aspx?columnId=" + ColumnId + "');" : "MessageAlert('" + resultStr.Split('|')[1] + "','error', '');", true);
        }

        //线下提现审核
        protected void cashAudit_Btn_Click(object sender, EventArgs e)
        {
            string auditStatus = Request.Form["AuditStatus"];
            string cashPayType = Request.Form["rdPayment"];

            if (string.IsNullOrEmpty(auditStatus))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择审核状态。','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(cashPayType))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择支付平台类型。','warning', '');", true);
                return;
            }
            string note = auditRemark.Value.Trim();
            var cashRecordBll = new CashRecordBLL();
            var cashRecordModel = cashRecordBll.getCashRecordModel("ID=" + Id);
            cashRecordModel.CashPayType = Convert.ToInt32(cashPayType);
            switch (selCurrStatus.Value)//当前状态
            {
                case "0"://初审中
                    cashRecordModel.Status = ConvertHelper.ToInt(auditStatus);
                    cashRecordModel.AuditRecords = (auditStatus == "1" ? "初审通过—" : "初审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）|";
                    cashRecordModel.UpdateTime = DateTime.Now;
                    if (auditStatus == "1")//初审通过
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", cashRecordBll.BelowCashCheckPass(cashRecordModel)
                                                                   ? "MessageAlert('操作成功。','success', '/MemberManage/WithdrawCashAudit.aspx?" + HttpContext.Current.Request.QueryString + "');"
                                                                   : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
                        break;
                    }
                    //初审不通过
                    var resultStr = cashRecordBll.BelowCashNotPass(cashRecordModel).ToString();
                    ClientScript.RegisterClientScriptBlock(GetType(), "", resultStr.Split('|')[0] == "1"
                                                               ? "MessageAlert('" + resultStr.Split('|')[1] + "','success', '/MemberManage/WithdrawCashAudit.aspx?" + HttpContext.Current.Request.QueryString + "');"
                                                               : "MessageAlert('" + resultStr.Split('|')[1] + "','error', '');", true);
                    break;
                case "1"://复审中
                    switch (auditStatus)
                    {
                        case "1"://复审通过
                            cashRecordModel.AuditRecords = "复审通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            var errorMsg = "";
                            var resultFt = cashRecordBll.withdrawCashAuditSuccess(cashRecordModel, ref errorMsg);
                            ClientScript.RegisterClientScriptBlock(GetType(), "", resultFt
                                                                                      ? "MessageAlert('审核成功','success', '/MemberManage/WithdrawCashAudit.aspx?" +
                                                                                        HttpContext.Current.Request
                                                                                                   .QueryString + "');"
                                                                                      : "MessageAlert('审核失败，" + errorMsg +
                                                                                        "','error', '');", true);
                            break;
                        case "2"://复审不通过
                            cashRecordModel.Status = 3;
                            cashRecordModel.AuditRecords = cashRecordModel.AuditRecords + "复审不通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）";
                            //复审不通过
                            var result = cashRecordBll.BelowCashNotPass(cashRecordModel).ToString();
                            ClientScript.RegisterClientScriptBlock(GetType(), "", result.Split('|')[0] == "1"
                                                                       ? "MessageAlert('" + result.Split('|')[1] + "','success', '/MemberManage/WithdrawCashAudit.aspx?" + HttpContext.Current.Request.QueryString + "');"
                                                                       : "MessageAlert('" + result.Split('|')[1] + "','error', '');", true);
                            break;
                    }
                    break;
            }

        }

        protected void Query_Click(object sender, EventArgs e)
        {
            var model = new CashRecordBLL().getCashRecordModel(Id);
            if (model != null)
            {
                string resultStr = ManageFcmsCommon.TongLian.TradeQuery.query(model.REQ_SN);
                lab_result.InnerText = resultStr;
            }
        }
    }
}