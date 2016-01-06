/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-14
Description: 广告统计界面
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
    public partial class Advertising : BasePage
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
        /// <remarks>add 卢侠勇,2015-7-14</remarks>
        public void ExcelExport_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtDateStart.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateStart.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtDateEnd.Value) ? DateTime.Now.Date : Convert.ToDateTime(this.txtDateEnd.Value);
            var minBalance = txtMinBalance.Value.Trim() == "" ? 0 : int.Parse(txtMinBalance.Value.Trim());
            var maxBalance = txtMaxBalance.Value.Trim() == "" ? 0 : int.Parse(txtMaxBalance.Value.Trim());
            var revStatus = int.Parse(selCurrStatus.Value);
            var channelRemark = txtChannelRemark.Value.Trim();
            var channel = int.Parse(selChannel.SelectedValue);
            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/AdvertisingTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = ReportBll.Instance.GetAdvertisingList(dateStart, dateEnd, minBalance, maxBalance, channelRemark, channel, revStatus, 0, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                var _regSource = "-";
                switch (Convert.ToInt32(dr["RegSource"]))
                {
                    case 0:
                        _regSource = "PC";
                        break;
                    case 1:
                        _regSource = "WAP";
                        break;
                    case 2:
                        _regSource = "IOS";
                        break;
                    case 3:
                        _regSource = "Android";
                        break;
                }
                writer.PasteText("A" + i, dr["rowNum"].ToString(), style);
                writer.PasteText("B" + i, _regSource, style);
                writer.PasteText("C" + i, dr["ChannelRemark"].ToString(), style);
                writer.PasteText("D" + i, dr["Channel"].ToString(), style);
                writer.PasteText("E" + i, dr["MemberName"].ToString(), style);
                writer.PasteText("F" + i, dr["Mobile"].ToString(), style);
                writer.PasteText("G" + i, dr["RegisterDate"].ToString(), style);
                writer.PasteText("H" + i, dr["RegisterTime"].ToString(), style);
                writer.PasteText("I" + i, dr["Sex"].ToString(), style);
                writer.PasteText("J" + i, dr["Birthday"].ToString(), style);
                writer.PasteNumber("K" + i, dr["Balance"].ToString(), style);
                writer.PasteText("L" + i, dr["Payable"].ToString(), style);
                writer.PasteNumber("M" + i, dr["Pay"].ToString(), style);
                writer.PasteNumber("N" + i, dr["FirstWithdraw"].ToString(), style);
                writer.PasteNumber("O" + i, dr["FirstWithdrawAmount"].ToString(), style);
                writer.PasteNumber("P" + i, dr["FirstPay"].ToString(), style);
                writer.PasteNumber("Q" + i, dr["FirstPayAmount"].ToString(), style);
                writer.PasteNumber("R" + i, dr["IsComple"].ToString(), style);
                writer.PasteNumber("S" + i, dr["IsBank"].ToString(), style);
                writer.PasteNumber("T" + i, dr["BidAmount"].ToString(), style);
                writer.PasteNumber("U" + i, dr["VisitRecord"].ToString(), style);
                writer.PasteNumber("V" + i, dr["VisitCreateTime"].ToString(), style);
                writer.PasteNumber("W" + i, dr["VisitMemberName"].ToString(), style);
                writer.PasteNumber("X" + i, dr["Amount"].ToString(), style);
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