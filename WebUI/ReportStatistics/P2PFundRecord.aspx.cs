/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-16
Description: 平台资金情况界面
Update: 
**************************************************************/
using System;
using System.Data;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;

namespace WebUI.ReportStatistics
{
    public partial class P2PFundRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-7-16</remarks>
        public void ExcelExport_Click(object sender, EventArgs e)
        {

            DateTime dateStart = string.IsNullOrEmpty(this.txtDateStart.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateStart.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtDateEnd.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateEnd.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/P2PFundRecordTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetFundRecordList(dateStart, dateEnd, 0, 50000, out total);
            int i = 3;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["CreateTime"].ToString(), style);
                writer.PasteText("B" + i, dr["InvestorPayAmount"].ToString(), style);
                writer.PasteText("C" + i, dr["BorrowerPayAmount"].ToString(), style);
                writer.PasteText("E" + i, dr["InvestorWithdrawAmount"].ToString(), style);
                writer.PasteText("F" + i, dr["BorrowerWithdrawAmount"].ToString(), style);
                writer.PasteText("H" + i, dr["InvestorRetainAmount"].ToString(), style);
                writer.PasteText("I" + i, dr["BorrowerRetainAmount"].ToString(), style);
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