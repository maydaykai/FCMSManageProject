/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-27
Description: 日期总表界面
Update: 
**************************************************************/

using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.ReportStatistics
{
    public partial class DateDataTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-7-27</remarks>
        public void ExcelExport_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtDateStart.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateStart.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtDateEnd.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateEnd.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/DateDataTotalTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetDateDataTotalList(dateStart, dateEnd, 0, 50000, out total);
            int i = 3;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["DataDate"].ToString(), style);
                writer.PasteText("B" + i, dr["Register"].ToString(), style);
                writer.PasteText("C" + i, dr["Authent"].ToString(), style);
                writer.PasteText("D" + i, dr["RegInvest"].ToString(), style);
                writer.PasteText("E" + i, dr["RegPayAmount"].ToString(), style);
                writer.PasteText("F" + i, dr["ConversionRate"].ToString(), style);
                writer.PasteText("G" + i, dr["InvestAmount"].ToString(), style);
                writer.PasteText("H" + i, dr["PayAmount"].ToString(), style);
                writer.PasteText("I" + i, dr["Invest"].ToString(), style);
                writer.PasteText("J" + i, dr["TransferSuccessAmount"].ToString(), style);
                writer.PasteText("K" + i, dr["TransferCanceledAmount"].ToString(), style);
                writer.PasteText("L" + i, dr["TransferAmount"].ToString(), style);
                writer.PasteText("M" + i, dr["RepaymentAmount"].ToString(), style);
                writer.PasteText("N" + i, dr["WithdrawAmount"].ToString(), style);
                writer.PasteText("O" + i, dr["WithdrawCount"].ToString(), style);
                writer.PasteText("P" + i, dr["LoginCount"].ToString(), style); ;
                writer.PasteText("Q" + i, dr["PayCount"].ToString(), style);
                writer.PasteText("R" + i, dr["NewPayCount"].ToString(), style);
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