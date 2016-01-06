using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class DeferLoanAudit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            if (!IsPostBack)
            {
                ApplyDeferLoanModel model = new ApplyDeferLoanBLL().getApplyDeferLoanModel(_id);
                if (model != null)
                {
                    LoanModel loanModel = new LoanBll().GetLoanModel(model.LoanID);
                    lab_loanNum.InnerText = loanModel.LoanNumber;
                    lab_loanTerm.InnerText = model.ExtensionTerm + loanModel.BorrowModeStr;
                    applyRemark.InnerText = model.ExtensionReason;
                    auditRemark.InnerText = model.Remark;
                    if (model.Status == 0)
                    {
                        Rad_Check.Items[2].Enabled = false;
                        Rad_Check.Items[3].Enabled = false;
                    }
                    else if (model.Status == 2)
                    {
                        Rad_Check.Items[0].Enabled = false;
                        Rad_Check.Items[1].Enabled = false;
                    }
                    else
                    {
                        Rad_Check.Enabled = false;
                        Btn_Audit.Enabled = false;
                    }
                }
            }
        }

        protected void Audit_Click(object sender, EventArgs e)
        {
            decimal GuaranteeFee = txt_guaranteeFee.Value == "" ? 0 : Convert.ToDecimal(txt_guaranteeFee.Value);
            decimal LoanServiceCharges = txt_loanServiceCharges.Value == "" ? 0 : Convert.ToDecimal(txt_loanServiceCharges.Value);
            int Status = Rad_Check.SelectedValue == "" ? 0 : Convert.ToInt32(Rad_Check.SelectedValue);
            ApplyDeferLoanModel model = new ApplyDeferLoanModel
            {
                ID = _id,
                GuaranteeFee = GuaranteeFee,
                LoanServiceCharges = LoanServiceCharges,
                Status = Status,
                Remark = auditRemark.InnerText,
            };
            model.AuditRecords = model.StatusStr + DateTime.Now.ToString("(yyyy-MM-dd HH:mm)");
            bool flag = new ApplyDeferLoanBLL().auditDeferLoan(model);
            if (flag)
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核成功','success', '');", true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核失败','error', '');", true);
        }
    }
}