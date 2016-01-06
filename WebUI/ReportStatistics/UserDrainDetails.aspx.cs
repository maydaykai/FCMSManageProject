/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-20
Description: 客户流失情况界面
Update: 
**************************************************************/
using System;
using System.Data;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;

namespace WebUI.ReportStatistics
{
    public partial class UserDrainDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-7-20</remarks>
        public void ExcelExport_Click(object sender, EventArgs e)
        {
            DateTime? dateStart = null;
            DateTime? dateEnd = null;
            if (this.txtDateStart.Value != "")
                dateStart = Convert.ToDateTime(this.txtDateStart.Value);
            if (this.txtDateEnd.Value != "")
                dateEnd = Convert.ToDateTime(this.txtDateEnd.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/UserDrainDetailsTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetUserDrainDetailsList(dateStart, dateEnd, 0, 50000, out total);
            int i = 3;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["DrainTime"].ToString(), style);
                writer.PasteText("B" + i, dr["RegisterCount"].ToString(), style);
                writer.PasteText("C" + i, dr["MarchGoatInvest"].ToString(), style);
                writer.PasteText("D" + i, dr["MarchGoatInvest2"].ToString(), style);
                writer.PasteText("E" + i, dr["JanuaryGoatInvest"].ToString(), style);
                writer.PasteText("F" + i, dr["JanuaryGoatInvest2"].ToString(), style);
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