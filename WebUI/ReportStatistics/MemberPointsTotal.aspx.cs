/*************************************************************
Author:		 卢侠勇
Create date: 2015-11-27
Description: 积分报表
Update: 
**************************************************************/
using System;
using ManageFcmsBll;
using System.Data;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;

namespace WebUI.ReportStatistics
{
    public partial class MemberPointsTotal : System.Web.UI.Page
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
        protected void ExcelExport1_Click(object sender, EventArgs e)
        {
            string checkStatus = this.sel_checkStatus.Value;
            string startDate = this.txtStartDate.Value.Trim();
            string endDate = this.txtEndDate.Value.Trim();
            string name = this.txtName.Value.Trim();

            string filter = string.Empty;
            if (!string.IsNullOrEmpty(startDate))
                filter += " AND MPD.CreateTime>='" + DateTime.Parse(startDate).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(endDate))
                filter += " AND MPD.CreateTime<'" + DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(name))
                filter += " AND M.MemberName='" + name + "'";

            if (!string.IsNullOrEmpty(checkStatus))
                filter += " AND DM.ID=" + checkStatus;

            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/MemberPointsTotalTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetMemberPointsList(filter, 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["MemberName"].ToString(), style);
                writer.PasteText("B" + i, dr["Name"].ToString(), style);
                writer.PasteNumber("C" + i, dr["PointsVal"].ToString(), style);
                writer.PasteText("D" + i, dr["CreateTime"].ToString(), style);
                writer.PasteText("E" + i, dr["Notes"].ToString(), style);
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