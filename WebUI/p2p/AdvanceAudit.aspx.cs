using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class AdvanceAudit : BasePage
    {
        public static int _loanId;
        private static LoanModel _loanInfoModel = new LoanModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            _loanId = Convert.ToInt16(HttpContext.Current.Request["LoanID"]);

            _loanInfoModel = new LoanBll().GetLoanInfoModel(" id =" + _loanId);
            DataSet ds = new RepaymentDetailBll().GetList(_loanId, 0);
            DataTable dt = ds.Tables[0];

            lab_LoanNumber.InnerText = "RJB" + _loanInfoModel.LoanNumber;
            lab_LoanAmount.InnerText = dt.Rows[0]["LoanAmount"].ToString();
            lab_LoanRate.InnerText = dt.Rows[0]["LoanRate"].ToString();
            lab_LoanTerm.InnerText = dt.Rows[0]["LoanTerm"].ToString();
            lab_ReceivedPrincipalAndInterest.InnerText = dt.Rows[0]["ReceivedPrincipalAndInterest"].ToString();
            lab_OverInterest.InnerText = dt.Rows[0]["OverInterest"].ToString();
            lab_ReInterest.InnerText = dt.Rows[0]["ReInterest"].ToString();
            lab_RePrincipal.InnerText = dt.Rows[0]["RePrincipal"].ToString();
            lab_LoanServiceFee.InnerText = dt.Rows[0]["LoanServiceFee"].ToString();
            lab_RepaymentAmount.InnerText = dt.Rows[0]["RepaymentAmount"].ToString();
            lab_GuaranteeBalance.InnerText = (new MemberBll().GetBalance(_loanInfoModel.GuaranteeID)).ToString();

        }

        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {

                string message = "";
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       new ProcRepaymentBll().Add(_loanId, 3, ref message)
                                                           ? "refreshParent();"
                                                           : "alert('" + message + "');", true);
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
            }

        }
    }
}