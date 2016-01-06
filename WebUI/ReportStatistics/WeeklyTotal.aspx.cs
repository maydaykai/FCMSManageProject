/*************************************************************
Author:		 卢侠勇
Create date: 2015-8-20
Description: 周报报表界面
Update: 
**************************************************************/
using System;
using System.Data;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;

namespace WebUI.ReportStatistics
{
    public partial class WeeklyTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-7-14</remarks>
        public void ExcelExport_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtDateStart.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateStart.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtDateEnd.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateEnd.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/WeeklyTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            var dt = ReportBll.Instance.GetWeekly(dateStart, dateEnd);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["A"].ToString(), style);
                writer.PasteText("B" + i, dr["B"].ToString(), style);
                writer.PasteText("C" + i, dr["C"].ToString(), style);
                writer.PasteText("D" + i, dr["D"].ToString(), style);
                writer.PasteText("E" + i, dr["E"].ToString(), style);
                writer.PasteText("F" + i, dr["F"].ToString(), style);
                writer.PasteText("G" + i, dr["J"].ToString(), style);
                writer.PasteText("H" + i, dr["H"].ToString(), style);
                writer.PasteNumber("I" + i, dr["I"].ToString(), style);
                writer.PasteText("J" + i, dr["J"].ToString(), style);
                writer.PasteNumber("K" + i, dr["K"].ToString(), style);
                writer.PasteNumber("L" + i, dr["L"].ToString(), style);
                writer.PasteNumber("M" + i, dr["M"].ToString(), style);
                writer.PasteNumber("N" + i, dr["N"].ToString(), style);
                writer.PasteNumber("O" + i, dr["O"].ToString(), style);
                writer.PasteNumber("P" + i, dr["P"].ToString(), style);
                writer.PasteNumber("Q" + i, dr["Q"].ToString(), style);
                writer.PasteNumber("R" + i, dr["R"].ToString(), style);
                writer.PasteNumber("S" + i, dr["S"].ToString(), style);
                writer.PasteNumber("T" + i, dr["T"].ToString(), style);
                writer.PasteNumber("U" + i, dr["U"].ToString(), style);
                writer.PasteNumber("V" + i, dr["V"].ToString(), style);
                writer.PasteNumber("W" + i, dr["W"].ToString(), style);
                writer.PasteNumber("X" + i, dr["X"].ToString(), style);
                writer.PasteNumber("Y" + i, dr["Y"].ToString(), style);
                writer.PasteNumber("Z" + i, dr["Z"].ToString(), style);
                writer.PasteNumber("AA" + i, dr["AA"].ToString(), style);

                writer.PasteText("AB" + i, dr["AB"].ToString(), style);
                writer.PasteText("AC" + i, dr["AC"].ToString(), style);
                writer.PasteText("AD" + i, dr["AD"].ToString(), style);
                writer.PasteText("AE" + i, dr["AE"].ToString(), style);
                writer.PasteText("AF" + i, dr["AF"].ToString(), style);
                writer.PasteText("AG" + i, dr["AG"].ToString(), style);
                writer.PasteText("AH" + i, dr["AH"].ToString(), style);
                writer.PasteText("AI" + i, dr["AI"].ToString(), style);
                writer.PasteText("AJ" + i, dr["AJ"].ToString(), style);
                writer.PasteText("AK" + i, dr["AK"].ToString(), style);
                writer.PasteText("AL" + i, dr["AL"].ToString(), style);
                writer.PasteText("AM" + i, dr["AM"].ToString(), style);
                writer.PasteText("AN" + i, dr["AN"].ToString(), style);
                writer.PasteText("AO" + i, dr["AO"].ToString(), style);
                writer.PasteText("AP" + i, dr["AP"].ToString(), style);
                writer.PasteText("AQ" + i, dr["AQ"].ToString(), style);
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