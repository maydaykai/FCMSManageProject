using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using ManageFcmsCommon;
using System.Web.Services;

namespace WebUI.ReportStatistics
{
    public partial class SysProjectSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void ExcelExport_Click(object sender, EventArgs e)
        {
            string loanNumber = txtLoanNumber.Value.Trim();
            var filter = "";
            if (!string.IsNullOrEmpty(loanNumber))
            {
                filter += " AND charindex('" + loanNumber + "', LoanNumber) >0";
            }

            if (int.Parse(selPrepaymentStatus.Value) == 1)
                filter += " AND EXISTS (SELECT 1 FROM RepaymentPlan R WHERE R.LoanID=Loan.ID AND R.OverStatus=4)";

            if (int.Parse(selLoanStatus.Value) > 0)
                filter += " AND ExamStatus =" + int.Parse(selLoanStatus.Value);

            int total;
            var loanBll = new LoanBll();
            var dt = loanBll.SysProjectSummary(0, 99999, filter, out total);
            if (dt == null || dt.Rows.Count <= 0) return;
            try
            {
                ExcelHelper.ExportExcelForDtByNpoi(dt, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls", Server.MapPath("ExcelTeml/spTemp.xls"), 1, Title, 3);
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
            }
        }

        /// <summary>
        /// 查看提前还款信息ajax
        /// </summary>
        /// <param name="loanId">借款ID</param>
        /// <param name="type">类型:2</param>
        /// <returns>还款信息数据</returns>
        /// <remarks>add 卢侠勇,2015-08-26</remarks>
        [WebMethod]
        public  static string getRepaymentAmount(int loanId, int type)
        {
            var dt = new RepaymentPlanBll().GetRepaymentAmount(loanId, type);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + dt.Rows.Count + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }
    }
}