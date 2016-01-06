/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-11
Description: 投资用户明细
Update: 
**************************************************************/
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System;
using System.Data;

namespace WebUI.AdvertisingManage
{
    public partial class BiddingUserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExcelExport1_Click(object sender, EventArgs e)
        {
            DateTime dateStart = string.IsNullOrEmpty(this.txtStartDate.Value) ? DateTime.Now : Convert.ToDateTime(this.txtStartDate.Value);
            DateTime dateEnd = string.IsNullOrEmpty(this.txtEndDate.Value) ? DateTime.Now.AddDays(1) : Convert.ToDateTime(this.txtEndDate.Value);

            var _secondary = this.txtSecondary.Value.Trim();
            var _memberName = this.txtName.Value.Trim();
            var _payTerminal = this.sel_PayTerminal.Value.Trim();
            var _investTerminal = this.sel_InvestTerminal.Value.Trim();
            var _regSource = this.sel_RegSource.Value.Trim();

            var filter = "AND B.CreateTime>='" + dateStart.ToString("yyyy-MM-dd") + "' AND B.CreateTime<='" + dateEnd.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(_secondary))
            {
                filter += " AND M.ChannelRemark LIKE '%" + _secondary + "%'";
            }
            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " AND M.MemberName LIKE '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_payTerminal))
            {
                filter += " AND R.RechargeChannel=" + _payTerminal;
            }
            if (!string.IsNullOrEmpty(_investTerminal))
            {
                filter += " AND B.BidType=" + _investTerminal;
            }
            if (!string.IsNullOrEmpty(_regSource))
            {
                filter += " AND M.RegSource=" + _regSource;
            }


            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/BiddingUserDetailsTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = AdvertisementBLL.Instance.GetBiddingUserDetailsList(filter, 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, Convert.ToDateTime(dr["CreateTime"]).ToString("yyyy-MM-dd"), style);
                writer.PasteText("B" + i, Convert.ToDateTime(dr["CreateTime2"]).ToString("HH:mm:ss"), style);
                writer.PasteText("C" + i, dr["MemberName"].ToString(), style);
                writer.PasteText("D" + i, dr["Channel"].ToString(), style);
                //writer.PasteText("E" + i, dr["Mobile"].ToString(), style);
                //writer.PasteText("F" + i, dr["RegisterDate"].ToString(), style);
                writer.PasteNumber("G" + i, dr["BidAmount"].ToString(), style);
                writer.PasteText("H" + i, dr["payGap"].ToString(), style);
                writer.PasteText("I" + i, dr["investGap"].ToString(), style);
                writer.PasteText("J" + i, dr["RechargeChannel"].ToString(), style);
                writer.PasteText("K" + i, dr["BidType"].ToString(), style);
                writer.PasteText("L" + i, dr["RegSource"].ToString(), style);
                writer.PasteText("M" + i, dr["VisitPreUrl"].ToString(), style);
                writer.PasteText("N" + i, dr["VisitUrl"].ToString(), style);
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