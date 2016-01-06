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

namespace WebUI.ReportStatistics
{
    public partial class MemberProjectSummary : System.Web.UI.Page
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
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/bpTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var memberBll = new MemberBll();
            var dt = memberBll.InvestorProjectSummary(0, 50000, filter, out total);
            int i = 4;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["BidMemberName"].ToString(), style);
                writer.PasteText("B" + i, dr["BidRealName"].ToString(), style);
                writer.PasteText("C" + i, dr["LoanNumber"].ToString(), style);
                writer.PasteText("D" + i, dr["LoanRealName"].ToString(), style);
                writer.PasteText("E" + i, dr["ExamStatus"].ToString(), style);
                writer.PasteNumber("F" + i, dr["LoanRate"].ToString(), style);
                writer.PasteText("G" + i,   dr["LoanTerm"].ToString(), style);
                writer.PasteNumber("H" + i, dr["BidStratTime"].ToString(), style);
                writer.PasteNumber("I" + i,ConvertHelper.ToDateTime(dr["ReviewTime"].ToString()).Year == 1970 ? "—" : ConvertHelper.ToDateTime(dr["ReviewTime"].ToString()).ToString("yyyy-MM-dd"), style);
                writer.PasteNumber("J" + i, dr["BiddingFreezing"].ToString(), style);
                writer.PasteNumber("K" + i, dr["AR_Principal"].ToString(), style);
                writer.PasteNumber("L" + i, dr["RE_Principal"].ToString(), style);
                writer.PasteNumber("M" + i, dr["DI_Principal"].ToString(), style);
                writer.PasteNumber("N" + i, dr["AR_Interest"].ToString(), style);
                writer.PasteNumber("O" + i, dr["RE_Interest"].ToString(), style);
                writer.PasteNumber("P" + i, dr["DI_Interest"].ToString(), style);
                writer.PasteNumber("Q" + i, dr["BO_Interest"].ToString(), style);
                writer.PasteNumber("R" + i, dr["BidServiceCharges"].ToString(), style);
                writer.PasteNumber("S" + i, dr["RecoIncentive"].ToString(), style);
                writer.PasteNumber("T" + i, dr["ActivitiesAward"].ToString(), style);
                writer.PasteNumber("U" + i, dr["ProjectIncome"].ToString(), style);
                i++;
            }
            SpreadsheetWriter.Save(doc);

            Response.Clear();
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "bp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            stream.WriteTo(Response.OutputStream);
            Response.End();
        }
    }
}