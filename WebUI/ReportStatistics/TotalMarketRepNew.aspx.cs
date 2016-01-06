//using DocumentFormat.OpenXml.Extensions;
//using DocumentFormat.OpenXml.Packaging;
//using ManageFcmsBll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.ReportStatistics
{
    public partial class TotalMarketRepNew : BasePage
    {
        protected int _btnOutPut = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsRole("6"))
            {
                _btnOutPut = 0;
            }
        }

        /// <summary>
        /// 判断是否包含权限
        /// </summary>
        /// <param name="role"></param>要判断的权限
        /// <returns></returns>
        public static bool IsRole(string role)
        {
            return (RightArray.IndexOf("|" + role + "|", StringComparison.Ordinal) >= 0);
        }

        ///// <summary>
        ///// 导出全部数据
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void ExcelExport1_Click(object sender, EventArgs e)
        //{
        //     TotalMarketRepBll _bll=new TotalMarketRepBll();
            
        //    string filter = string.Empty;
        //    var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/MarkingTemp.xlsx"));
        //    var doc = SpreadsheetDocument.Open(stream, true);
        //    var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
        //    var writer = new WorksheetWriter(doc, worksheetPart);
        //    SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
        //    string next_days = Convert.ToDateTime(txtDate1.Value).AddMonths(-1).ToString("yyyy-MM-dd");
        //    var dt = _bll.GetExcelTable(txtDate1.Value, txtDate2.Value, next_days).Tables[0];
        //    int i = 2;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        writer.PasteText("A" + i, dr["RealName"].ToString(), style);
        //        writer.PasteText("B" + i, dr["MemberName"].ToString(), style);
        //        writer.PasteText("C" + i, dr["SumBidNum"].ToString(), style);
        //        writer.PasteText("D" + i, dr["SumBidNum"].ToString(), style);
        //        writer.PasteText("E" + i, dr["SumBidAmount"].ToString(), style);
        //        writer.PasteText("F" + i, dr["BidNumContinued"].ToString(), style);
        //        writer.PasteText("G" + i, dr["curr_mouth_money"].ToString(), style);
        //        writer.PasteText("H" + i, dr["next_mouth_money"].ToString(), style);
        //        writer.PasteText("I" + i, dr["mouth_money"].ToString(), style);
        //        writer.PasteText("J" + i, dr["BidAmountContinued"].ToString(), style);
        //        i++;
        //    }
        //    SpreadsheetWriter.Save(doc);

        //    Response.Clear();
        //    Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "yx_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    stream.WriteTo(Response.OutputStream);
        //    Response.End();
        //}
    }
}