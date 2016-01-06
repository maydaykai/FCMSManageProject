using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using System.Data;

namespace WebUI.ReportStatistics
{
    public partial class RecommendRewardReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ExcelExport_Click(object sender, EventArgs e)
        {
            string uName = txtName.Value.Trim();
            int sYear = 0, sMonth = 0, eYear = 0, eMonth = 0;
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
            var filter = " 1=1 and Reward > 0 ";
            if (!string.IsNullOrEmpty(uName))
            {
                string field = (selSUserType.Value.Trim().Equals("0")) ? "M.MemberName" : "MI.RealName";
                filter += " and " + field + " like '%" + uName + "%'";
            }
            filter += getDateWhere(sYear, sMonth, eYear, eMonth) + " GROUP BY P.MemberId,M.MemberName,MI.RealName";
            string eDate = eMonth == 12 ? (eYear + 1) + "-01-01" : eYear + "-" + (eMonth + 1) + "-01";
            int total;
            var bll = new ReCommendRewardBll();
            //var dt = bll.GetPageList(
            //        "M.MemberName,MI.RealName,(SELECT ISNULL(SUM(RePrincipal),0) FROM dbo.RepaymentPlan WHERE CHARINDEX( '|' + CAST(BidMemberID AS VARCHAR(20)) + '|',(SELECT TOP 1 Relation FROM dbo.ReCommendRewardHistory WHERE MemberId=P.MemberId ORDER BY UpdateTime DESC))>0 AND CreateTime>= CONVERT(DATETIME,'" + sYear + "-" +
            //        sMonth + "-01') AND CreateTime < CONVERT(DATETIME,'" + eDate + "')) LowerBidAmount,(SELECT ISNULL(SUM(RePrincipal),0) FROM dbo.RepaymentPlan WHERE BidMemberID=P.MemberId AND CreateTime>= CONVERT(DATETIME,'" + sYear + "-" +
            //        sMonth + "-01') AND CreateTime < CONVERT(DATETIME,'" + eDate + "')) BidAmount,SUM(P.SumInterest) SumInterest,SUM(P.RewardRate) RewardRate,SUM(P.Reward) Reward,SUM(P.SumSubReward) SumSubReward,MAX(P.UpdateTime) TotalDate," +
            //        "(SELECT ISNULL(SUM(1),0) FROM dbo.MemberRecommended WHERE CHARINDEX( '|' + CAST(RecMemberID AS VARCHAR(20)) + '|',(SELECT TOP 1 Relation FROM dbo.ReCommendRewardHistory WHERE MemberId=P.MemberId ORDER BY UpdateTime DESC))>0 AND CreateTime>= CONVERT(DATETIME,'" + sYear + "-" +
            //        sMonth + "-01') AND CreateTime < CONVERT(DATETIME,'" + eDate + "')) SumRec", filter, "SUM(P.SumInterest) desc",
            //        1, 65535, out total);
            var dt = bll.GetPageList(
                    "M.MemberName,MI.RealName,isnull(SUM(P.SumBidAmount),0) as LowerBidAmount,isnull(SUM(P.SelfBidAmount),0) as BidAmount,SUM(P.SumInterest) SumInterest,SUM(P.RewardRate) RewardRate,SUM(P.Reward) Reward,SUM(P.SumSubReward) SumSubReward,MAX(P.UpdateTime) TotalDate," +
                    "dbo.GetUserRecommendedCount(P.MemberId,'" + sYear + "-" + sMonth + "-01','" + eDate + "') as  SumRec", filter, "SUM(P.SumInterest) desc",
                    1, 65535, out total);
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dt, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls",
                                                       Server.MapPath("ExcelTeml/rrTemp.xls"), 1, Title,3);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
            }
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

        protected void ExcelExport2_Click(object sender, EventArgs e)
        {
            var filter=string.Empty;
            string uName = txtName.Value.Trim();
            string uType = selSUserType.Value.Trim();
            int sYear = 0, sMonth = 0, eYear = 0, eMonth = 0;
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

            filter = " AND B.createTime>'" + sYear + "-" + sMonth + "-01' AND B.createTime<'" + Convert.ToDateTime(+ eYear + "/" + eMonth + "/01").AddMonths(1).ToString("yyyy-MM-dd") + "'";
            if (!string.IsNullOrEmpty(uName))
                filter += uType == "0" ? " AND M.MemberName LIKE '%" + uName + "%'" : " AND MI.RealName LIKE '%" + uName + "%'";

            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/rrNewTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = new ReCommendRewardBll().GetPageList1(filter, 1, 50000, out total);
            int i = 4;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["x_createTime"].ToString(), style);
                writer.PasteText("B" + i, dr["x_MemberName"].ToString(), style);
                writer.PasteText("C" + i, dr["x_RealName"].ToString(), style);
                writer.PasteText("D" + i, dr["x_level"].ToString(), style);
                writer.PasteNumber("E" + i, dr["x_amount"].ToString(), style);
                writer.PasteNumber("F" + i, dr["x_directInterest"].ToString(), style);
                writer.PasteText("G" + i, dr["x_directProportion"].ToString(), style);
                writer.PasteNumber("H" + i, dr["x_directReward"].ToString(), style);
                writer.PasteNumber("I" + i, dr["x_IndirectInterest"].ToString(), style);
                writer.PasteText("J" + i, dr["x_IndirectProportion"].ToString(), style);
                writer.PasteNumber("K" + i, dr["x_IndirectReward"].ToString(), style);
                writer.PasteText("L" + i, dr["x_investCount"].ToString(), style);
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