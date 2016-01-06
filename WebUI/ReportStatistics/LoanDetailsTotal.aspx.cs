/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-15
Description: 发标统计界面
Update: 
**************************************************************/
using System;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System.Data;

namespace WebUI.ReportStatistics
{
    public partial class LoanDetailsTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-7-15</remarks>
        public void ExcelExport_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtDateStart.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateStart.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtDateEnd.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateEnd.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/LoanDetailsTotalTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetLoanDetailsList(dateStart, dateEnd, 0, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["LoanNumber"].ToString(), style);
                writer.PasteText("B" + i, dr["LoanAmount"].ToString(), style);
                writer.PasteText("C" + i, dr["FullScaleAmount"].ToString(), style);
                writer.PasteText("D" + i, dr["CreateTime"].ToString(), style);
                writer.PasteText("E" + i, dr["FullScaleTime"].ToString(), style);
                writer.PasteNumber("F" + i, dr["LoanRate"].ToString(), style);
                writer.PasteText("G" + i, dr["LoanTerm"].ToString(), style);
                writer.PasteNumber("H" + i, dr["RepaymentMethod"].ToString(), style);
                writer.PasteNumber("I" + i, dr["ComputeTime"].ToString(), style);
                writer.PasteNumber("J" + i, dr["BidCount"].ToString(), style);
                writer.PasteNumber("K" + i, dr["RePayTime"].ToString(), style);
                writer.PasteNumber("L" + i, dr["ReAmount"].ToString(), style);
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