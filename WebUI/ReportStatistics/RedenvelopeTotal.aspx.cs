/*************************************************************
Author:		 卢侠勇
Create date: 2015-11-24
Description: 红包报表
Update: 
**************************************************************/
using System;
using ManageFcmsBll;
using System.Data;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;

namespace WebUI.ReportStatistics
{
    public partial class RedenvelopeTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = ReportBll.Instance.GetRedenvelopeType();
                DataRow dr = data.NewRow(); 
                dr["ID"] = -1;
                dr["RedenvelopeName"] = "--红包类型--";
                data.Rows.InsertAt(dr,0);
                this.sel_AmountType.DataSource = data;
                this.sel_AmountType.DataTextField = "RedenvelopeName";
                this.sel_AmountType.DataValueField = "ID";
                this.sel_AmountType.DataBind();
            }
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
            string amountType = this.sel_AmountType.SelectedValue;
            string startDate = this.txtStartDate.Value.Trim();
            string endDate = this.txtEndDate.Value.Trim();
            string name = this.txtName.Value.Trim();

            string filter = string.Empty;
            if (!string.IsNullOrEmpty(startDate))
                filter += " AND R.CreateTime>='" + DateTime.Parse(startDate).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(endDate))
                filter += " AND R.CreateTime<'" + DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(name))
                filter += " AND M.MemberName='" + name + "'";

            if (!string.IsNullOrEmpty(checkStatus))
                filter += " AND R.Status=" + checkStatus;

            if (amountType != "-1" && !string.IsNullOrEmpty(amountType))
                filter += " AND R.RedenvelopeID=" + amountType;

            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/RedenvelopeTotalTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetRedenvelopeDetailsList(filter, 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["MemberName"].ToString(), style);
                writer.PasteText("B" + i, dr["RedenvelopeName"].ToString(), style);
                writer.PasteText("C" + i, dr["Amount"].ToString(), style);
                writer.PasteText("D" + i, dr["ActivateAmount"].ToString(), style);
                writer.PasteText("E" + i, dr["StatusStr"].ToString(), style);
                writer.PasteText("F" + i, dr["StartTime"].ToString(), style);
                writer.PasteText("G" + i, dr["EndTime"].ToString(), style);
                writer.PasteText("H" + i, dr["UseTime"].ToString(), style);
                writer.PasteText("I" + i, dr["CreateTime"].ToString(), style);
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