/*************************************************************
Author:		 卢侠勇
Create date: 2015-09-14
Description: 访问报表
Update: 
**************************************************************/
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System;
using System.Data;

namespace WebUI.ReportStatistics
{
    public partial class UserSourceTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dt = new MemberBll().GetChannel();
                DataRow row = dt.NewRow();
                row["Id"] = "-1";
                row["Channel"] = "全部";
                dt.Rows.InsertAt(row, 0);
                this.selChannel.DataSource = dt;
                this.selChannel.DataValueField = "Id";
                this.selChannel.DataTextField = "Channel";
                this.selChannel.DataBind();
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>add 卢侠勇,2015-09-14</remarks>
        public void ExcelExport_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtDateStart.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateStart.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtDateEnd.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateEnd.Value);
            string memberName = string.IsNullOrEmpty(this.txtName.Value) ? "" : Convert.ToString(this.txtName.Value);
            var channel = int.Parse(selChannel.SelectedValue);
            var channelRemark = string.IsNullOrEmpty(this.txtchannelRemark.Value) ? "" : Convert.ToString(this.txtchannelRemark.Value);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/UserSourceTotalTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetUserSourceTotalList(dateStart, dateEnd,memberName,channel,channelRemark, 1, 50000, out total);
            int i = 3;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["VisitTime"].ToString(), style);
                writer.PasteText("B" + i, dr["MemberName"].ToString(), style);
                writer.PasteText("C" + i, dr["HostIP"].ToString(), style);
                writer.PasteText("D" + i, dr["VisitPreUrl"].ToString(), style);
                writer.PasteText("E" + i, dr["VisitUrl"].ToString(), style);
                writer.PasteText("F" + i, dr["Channel"].ToString(), style);
                writer.PasteText("G" + i, dr["ChannelRemark"].ToString(), style);
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