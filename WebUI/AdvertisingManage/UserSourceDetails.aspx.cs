/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 用户来源明细
Update: 
**************************************************************/
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System;
using System.Data;

namespace WebUI.AdvertisingManage
{
    public partial class UserSourceDetails : System.Web.UI.Page
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
            var _visitPreUrl = this.txtVisitPreUrl.Value.Trim();
            var _visitUrl = this.txtVisitUrl.Value.Trim();
            var _isRecharge = this.boxIsRecharge.Checked;
            var _isBinBank = this.boxIsBinBank.Checked;
            var _isRealName = this.boxIsRealName.Checked;
            var _regSource = this.sel_RegSource.Value.Trim();

            var filter = "AND M.RegTime>='" + dateStart.ToString("yyyy-MM-dd") + "' AND M.RegTime<='" + dateEnd.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(_secondary))
            {
                filter += " AND M.ChannelRemark LIKE '%" + _secondary + "%'";
            }
            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " AND M.MemberName LIKE '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_visitPreUrl))
            {
                filter += " AND US.VisitPreUrl LIKE '%" + _visitPreUrl + "%'";
            }
            if (!string.IsNullOrEmpty(_visitUrl))
            {
                filter += " AND US.VisitUrl LIKE '%" + _visitUrl + "%'";
            }
            if (!string.IsNullOrEmpty(_regSource))
            {
                filter += " AND M.RegSource=" + _regSource;
            }
            if (_isRecharge)
            {
                filter += " AND ISNULL(RR.Amount,0)>0";
            }
            if (_isBinBank)
            {
                filter += " AND EXISTS (SELECT 1 FROM BankAccount BA WHERE BA.Status=1 AND BA.MemberID=M.ID)";
            }
            if (_isRealName)
            {
                filter += " AND M.CompleStatus>=2";
            }


            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/UserSourceDetailsTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = AdvertisementBLL.Instance.GetUserSourceDetails(filter, 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, Convert.ToDateTime(dr["RegTime"]).ToString("yyyy-MM-dd"), style);
                writer.PasteText("B" + i, Convert.ToDateTime(dr["RegTime2"]).ToString("HH:mm:ss"), style);
                writer.PasteText("C" + i, dr["MemberName"].ToString(), style);
                writer.PasteText("D" + i, dr["Channel"].ToString(), style);
                //writer.PasteText("E" + i, dr["Mobile"].ToString(), style);
                //writer.PasteText("F" + i, dr["RegisterDate"].ToString(), style);
                writer.PasteNumber("G" + i, dr["RechargeAmount"].ToString(), style);
                writer.PasteNumber("H" + i, dr["CashAmount"].ToString(), style);
                writer.PasteText("I" + i, dr["BankStatus"].ToString(), style);
                writer.PasteText("J" + i, dr["CompleStatus"].ToString(), style);
                writer.PasteText("K" + i, dr["RegSource"].ToString(), style);
                writer.PasteText("L" + i, dr["HostIP"].ToString(), style);
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