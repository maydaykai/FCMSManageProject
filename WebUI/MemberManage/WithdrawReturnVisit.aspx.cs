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
    public partial class WithdrawReturnVisit : BasePage
    {
        public int Id;
        private int _cashMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            //线下提现审核按钮
            Id = ConvertHelper.QueryString(Request, "ID", 0);
            _cashMode = ConvertHelper.QueryString(Request, "CashMode", 0);
            if (!IsPostBack)
            {
                if (Id > 0)
                {
                    txtMemberName.Disabled = true;
                    txtMemberName.Attributes.Remove("class");
                    txtMemberName.Attributes.Add("class", "input_Disabled fl");

                    txtAccountHolder.Disabled = true;
                    txtAccountHolder.Attributes.Remove("class");
                    txtAccountHolder.Attributes.Add("class", "input_Disabled fl");

                    txtMobile.Disabled = true;
                    txtMobile.Attributes.Remove("class");
                    txtMobile.Attributes.Add("class", "input_Disabled fl");

                    txtCashAmount.Disabled = true;
                    txtCashAmount.Attributes.Remove("class");
                    txtCashAmount.Attributes.Add("class", "input_Disabled fl");

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

                    if (_cashMode == 0)
                    {
                        if (cashRecordModel.BankAccountType == 1)
                            bankAccountModel = bankAccountBll.getBankAccountModel(cashRecordModel.BankAccountID);
                        else
                            bankAccountAuthentModel = bankAccountBll.GetBankAccountAuthentModel("BA.ID=" + cashRecordModel.BankAccountID);
                    }
                    else
                        bankAccountBlModel = bankAccountBlBll.GetModel(cashRecordModel.BankAccountID);

                    var member = memberBll.GetModel(cashRecordModel.MemberID);
                    txtMemberName.Value = member.MemberName;
                    txtAccountHolder.Value = (_cashMode == 0 ? memberInfoBll.GetModel(cashRecordModel.MemberID).RealName : bankAccountBlModel.AccountHolder);
                    txtCashAmount.Value = cashRecordModel.CashAmount.ToString("F2");
                    txtCashID.Value = cashRecordModel.ID.ToString();
                    txtMemberID.Value = cashRecordModel.MemberID.ToString(CultureInfo.InvariantCulture);
                    txtMobile.Value = member.Mobile;
                    spWarningRecord.InnerHtml = cashRecordModel.WarningRecord == null ? "" : cashRecordModel.WarningRecord.Replace("|", "</br>");
                    if (cashRecordModel.WarningStatus == 3 || cashRecordModel.WarningStatus == 4)
                    {
                        Radio1.Disabled = true;
                        Radio2.Disabled = true;
                        Radio3.Disabled = true;
                        btnSave.Visible = false;
                    }
                    

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
                }
            }
        }

        //提交数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int status = 2;
            if (Radio1.Checked)
                status = 2;
            if (Radio2.Checked)
                status = 4;
            if (Radio3.Checked)
                status = 3;

            if (new CashRecordBLL().ModifyWithdrawWarning(int.Parse(txtCashID.Value), status, txtNote.Value.Trim(), this.MemberId))
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('操作成功','success', location.href);", true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('操作失败','error', location.href);", true);
        }
    }
}