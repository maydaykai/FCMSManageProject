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
    public partial class ApplyAbandonLoanAudit : BasePage
    {
        private int _id;
        private int _loanID;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            _loanID = ConvertHelper.QueryString(Request, "loanID", 0);
            if (!IsPostBack)
            {
                ApplyAbandonLoanModel model = new ApplyAbandonLoanBLL().getApplyAbandonLoanModel(_id);
                if (model != null)
                {
                    LoanModel loanModel = new LoanBll().GetLoanModel(model.LoanID);
                    lab_loanNum.InnerText = loanModel.LoanNumber;
                    lab_loanTerm.InnerText = loanModel.LoanTerm + loanModel.BorrowModeStr;
                    lab_loanAmount.InnerText = "￥" + loanModel.LoanAmount.ToString("F2");
                    applyRemark.InnerText = model.ApplyReason;
                    auditRemark.InnerText = model.AuditRemark;
                    lab_auditRecords.InnerHtml = model.AuditRecords;
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
                        auditRemark.Disabled = true;
                        Rad_Check.Enabled = false;
                        Btn_Audit.Enabled = false;
                        Btn_Audit.CssClass = "inputButton_BDisabled";
                    }
                }
            }
        }

        protected void Audit_Click(object sender, EventArgs e)
        {
            int Status = Rad_Check.SelectedValue == "" ? 0 : Convert.ToInt32(Rad_Check.SelectedValue);
            ApplyAbandonLoanModel model = new ApplyAbandonLoanModel
            {
                ID = _id,
                LoanID = _loanID,
                Status = Status,
                AuditRemark = auditRemark.InnerText
            };
            model.AuditRecords = model.StatusStr + DateTime.Now.ToString("(yyyy-MM-dd HH:mm)");
            bool flag = new ApplyAbandonLoanBLL().auditAbandonLoan(model);
            if (flag)
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核成功','success', location.href);", true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核失败','error', location.href);", true);
        }
    }
}