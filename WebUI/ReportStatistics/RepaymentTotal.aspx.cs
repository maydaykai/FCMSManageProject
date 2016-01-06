/*************************************************************
Author:		 卢侠勇
Create date: 2015-09-25
Description: 还款报表界面
Update: 
**************************************************************/
using System;
using System.Data;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using ManageFcmsCommon;
using System.Web.Services;


namespace WebUI.ReportStatistics
{
    public partial class RepaymentTotal : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-09-25</remarks>
        public void ExcelExport1_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtStartDate.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtStartDate.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtEndDate.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtEndDate.Value);
            string checkStatus = this.sel_checkStatus.Value;

            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/repaymentTotalTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetRepaymentTotalList(dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"), checkStatus, 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["RePayTime"].ToString(), style);
                writer.PasteText("B" + i, dr["UpdateTime"].ToString(), style);
                writer.PasteText("C" + i, dr["Agency"].ToString(), style);
                writer.PasteText("D" + i, dr["RealName"].ToString(), style);
                writer.PasteText("E" + i, dr["LoanAmount"].ToString(), style);
                writer.PasteText("F" + i, dr["LoanTerm"].ToString(), style);
                writer.PasteText("G" + i, dr["ReAmount"].ToString(), style);
                writer.PasteText("H" + i, dr["RePrincipal"].ToString(), style);
                writer.PasteText("I" + i, dr["ReInterest"].ToString(), style);
                writer.PasteText("J" + i, dr["ReInterest"].ToString(), style);
                writer.PasteText("K" + i, dr["ReOverInterest"].ToString(), style);
                writer.PasteText("L" + i, dr["PenaltyAmount"].ToString(), style);
                writer.PasteText("M" + i, dr["LoanNumber"].ToString(), style);
                i++;
            }
            SpreadsheetWriter.Save(doc);

            Response.Clear();
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "bp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            stream.WriteTo(Response.OutputStream);
            Response.End();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-09-25</remarks>
        public void ExcelExport2_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtStartDate2.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtStartDate2.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtEndDate2.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtEndDate2.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/repaymentTotalTemp2.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetRepaymentTotalList(dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"), 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["repDate"].ToString(), style);
                writer.PasteText("B" + i, dr["principal"].ToString(), style);
                writer.PasteText("C" + i, dr["interest"].ToString(), style);
                writer.PasteText("D" + i, dr["amount"].ToString(), style);
                writer.PasteText("E" + i, dr["reppCount"].ToString(), style);
                writer.PasteText("F" + i, dr["repCount"].ToString(), style);
                i++;
            }
            SpreadsheetWriter.Save(doc);

            Response.Clear();
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "bp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            stream.WriteTo(Response.OutputStream);
            Response.End();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-09-25</remarks>
        public void ExcelExport3_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtStartDate3.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtStartDate3.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtEndDate3.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtEndDate3.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/repaymentTotalTemp3.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            var dt = ReportBll.Instance.GetRepaymentTotalList(dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["Agency"].ToString(), style);
                writer.PasteText("B" + i, dr["Amount"].ToString(), style);
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