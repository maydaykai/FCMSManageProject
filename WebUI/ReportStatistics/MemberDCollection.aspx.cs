using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.ReportStatistics
{
    public partial class MemberDCollection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ExcelExport_Click(object sender, EventArgs e)
        {
            string uName = txtName.Value.Trim();
            var filter = "";
            if (!string.IsNullOrEmpty(uName))
            {
                string field = (selSUserType.Value.Trim().Equals("0")) ? "M.MemberName" : "MI.RealName";
                filter += " AND charindex('" + uName.Trim() + "', " + field + ") >0";
            }
            int total;
            var memberBll = new MemberBll();
            var dt = memberBll.MemberDataCollection(0, 65535, filter, out total);
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dt, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls", Server.MapPath("ExcelTeml/mdTemp.xls"), 1, Title, 3);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
            }
        }

        public void MemberFundSummaryExcel_Click(object sender, EventArgs e)
        {
            string nowDateTime = txtDownloadDate.Value.Trim();
            var memberBll = new MemberBll();
            var dt = memberBll.MemberFundSummary(ConvertHelper.ToDateTime(nowDateTime));
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dt, "mdTotal" + DateTime.Now.ToString("yyyyMMdd") + ".xls", Server.MapPath("ExcelTeml/mdTotalTemp.xls"), 1, Title, 1);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('抱歉，系统查不到该日期的数据。','warning', '');", true);
            }
        }

        public void PlatformCapitalStockExcel_Click(object sender, EventArgs e)
        {
            string nowDateTime = txtDownloadDate.Value.Trim();
            var memberBll = new MemberBll();
            var dt = memberBll.PlatformCapitalStock(ConvertHelper.ToDateTime(nowDateTime));
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dt, "pcs" + DateTime.Now.ToString("yyyyMMdd") + ".xls", Server.MapPath("ExcelTeml/pcsTemp.xls"), 1, Title, 1);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('抱歉，系统查不到该日期的数据。','warning', '');", true);
            }
        }

    }
}