using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.p2p
{
    public partial class LocalCreditManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ExcelExport_Click(object sender, EventArgs e)
        {
            string uName = txtName.Value.Trim();
            string status = sel_status.Value;
            string startDate = txtStartTime.Value;
            string endDate = txtEndTime.Value;
            string loanNumber = txtLoanNumber.Value;

            string where = " 1=1 ";
            if (uName != "")
            {
                where += " and MemberName like '" + uName + "%'";
            }
            if (status != "-1")
            {
                where += " and ExamStatus = " + status;
            }
            if (startDate != "")
            {
                where += " and CreateTime >= '" + startDate + "'";
            }
            if (endDate != "")
            {
                where += " and CreateTime<=DATEADD(DAY,1,'" + endDate + "')";
            }
            if (loanNumber != "")
            {
                where += " and LoanNumber = '" + loanNumber + "'";
            }

            var dt = new LocalCreditBll().GetPagedLoanApplyList(where);
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dt, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls", Server.MapPath("/ReportStatistics/ExcelTeml/LocalCreditTemp.xls"), 1, Title, 3);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
                
            }
        }
    }
}