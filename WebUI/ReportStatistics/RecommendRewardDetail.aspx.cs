using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.ReportStatistics
{
    public partial class RecommendRewardDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExcelExport_Click(object sender, EventArgs e)
        {
            var filter = string.Empty;
            var filter2 = "接";
            string uName = txtName.Value.Trim();
            string uType = selSUserType.Value.Trim();
            int sYear = 0, sMonth = 0, eYear = 0, eMonth = 0;
            int _bType = int.Parse(selType.Value.Trim());
            if (!string.IsNullOrEmpty(startDate.Value))
            {
                sYear = ConvertHelper.ToInt(startDate.Value.Substring(0, 4));
                sMonth = ConvertHelper.ToInt(startDate.Value.Substring(5, 2));
            }
            if (!string.IsNullOrEmpty(endDate.Value))
            {
                eYear = ConvertHelper.ToInt(startDate.Value.Substring(0, 4));
                eMonth = ConvertHelper.ToInt(startDate.Value.Substring(5, 2));
            }

            filter = " AND B.createTime>='" + sYear + "-" + sMonth + "-01' AND B.createTime<'" + Convert.ToDateTime(+eYear + "/" + eMonth + "/01").AddMonths(1).ToString("yyyy-MM-dd") + "'";
            if (!string.IsNullOrEmpty(uName))
                filter += uType == "0" ? " AND M.MemberName = '" + uName + "'" : " AND MI.RealName = '" + uName + "'";

            if (_bType != 0)
                filter2 = _bType == 1 ? "直接" : "间接";

            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/rrNewTemp2.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = new ReCommendRewardBll().GetRecommendDetalis(filter, filter2, 1, 50000, out total);
            int i = 4;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["dateDay"].ToString(), style);
                writer.PasteText("B" + i, dr["MemberName"].ToString(), style);
                writer.PasteText("C" + i, dr["RealName"].ToString(), style);
                writer.PasteNumber("D" + i, dr["monthInterst"].ToString(), style);
                writer.PasteText("E" + i, dr["Proportion"].ToString(), style);
                writer.PasteNumber("F" + i, dr["Reward"].ToString(), style);
                writer.PasteText("G" + i, dr["dType"].ToString(), style);
                writer.PasteText("H" + i, dr["recMemberName"].ToString(), style);
                writer.PasteText("I" + i, dr["recRealName"].ToString(), style);
                i++;
            }
            SpreadsheetWriter.Save(doc);

            Response.Clear();
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "bp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            stream.WriteTo(Response.OutputStream);
            Response.End();
        }

        private string getDateWhere(int sYear, int sMonth, int eYear, int eMonth)
        {
            string where;
            if (sYear == eYear)
            {
                where = "AND (P.TotalYear=" + sYear + " AND P.TotalMonth>=" + sMonth + " AND P.TotalMonth<=" + eMonth + ")";
            }
            else
            {
                where = "AND (P.TotalYear=" + sYear + " AND P.TotalMonth>=" + sMonth + " AND P.TotalMonth<=12)";
                if ((eYear - sYear) > 2)
                {
                    for (int i = 0; i < (eYear - sYear - 1); i++)
                    {
                        where += " OR (P.TotalYear=" + (sYear + i + 1) + " AND P.TotalMonth>=1 AND P.TotalMonth<=12)";
                    }
                }
                where += " OR (P.TotalYear=" + eYear + " AND P.TotalMonth>=1 AND P.TotalMonth<=" + eMonth + ")";
            }

            return where;
        }
    }
}