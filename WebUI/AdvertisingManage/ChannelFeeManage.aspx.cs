/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 渠道费用列表
Update: 
**************************************************************/
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System;
using System.Data;
using ManageFcmsCommon;

namespace WebUI.AdvertisingManage
{
    public partial class ChannelFeeManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ExcelExport1_Click(object sender, EventArgs e)
        {
            var filter = string.Empty;
            if (!string.IsNullOrEmpty(this.txtStartDate.Value.Trim()))
                filter += " AND CONVERT(CHAR(10),C.createTime,120)>='" + this.txtStartDate.Value.Trim() + "'";
            if (!string.IsNullOrEmpty(this.txtEndDate.Value.Trim()))
                filter += " AND CONVERT(CHAR(10),C.createTime,120)<='" + this.txtEndDate.Value.Trim() + "'";

            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/ChannelFeeTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = AdvertisementBLL.Instance.GetChannelFeeList(filter, 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["Channel"].ToString(), style);
                writer.PasteNumber("B" + i, dr["dayFee"].ToString(), style);
                writer.PasteNumber("C" + i, dr["zhouFee"].ToString(), style);
                writer.PasteNumber("D" + i, dr["monthFee"].ToString(), style);
                writer.PasteNumber("E" + i, dr["feeSum"].ToString(), style);
                writer.PasteText("F" + i, dr["periodFee"].ToString(), style);
                writer.PasteNumber("G" + i, dr["createTime"].ToString(), style);
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