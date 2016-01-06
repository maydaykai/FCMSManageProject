using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// ReCommendRewardHandler 的摘要说明
    /// </summary>
    public class ReCommendRewardHandler : IHttpHandler
    {

        private int _sign, _currentPage = 1, _pageSize, _sYear, _sMonth, _eYear, _eMonth, _memberId, _bType;
        private string _uName, _uType, _lowerLevel;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _sign = ConvertHelper.QueryString(context.Request, "_sign", 0);
            _memberId = ConvertHelper.QueryString(context.Request, "memberId", 0);
            
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _uName = ConvertHelper.QueryString(context.Request, "uName", "");
            _uType = ConvertHelper.QueryString(context.Request, "uType", "0");
            _sYear = ConvertHelper.QueryString(context.Request, "sYear", 0);
            _sMonth = ConvertHelper.QueryString(context.Request, "sMonth", 0);
            _eYear = ConvertHelper.QueryString(context.Request, "eYear", 0);
            _eMonth = ConvertHelper.QueryString(context.Request, "eMonth", 0);
            _bType = ConvertHelper.QueryString(context.Request, "bType", 0);
            _lowerLevel = GetlowerLevel(_memberId, _sYear, _sMonth);
            var filter = " 1=1 ";
            _uName = HttpContext.Current.Server.UrlDecode(_uName);
            if (!string.IsNullOrEmpty(_uName))
            {
                string field = (_uType.Equals("0")) ? "M.MemberName" : "MI.RealName";
                filter += " and " + field + " like '%" + _uName.Trim() + "%'";
            }
            if (_sign == -1)
            {
                filter += " and CHARINDEX( '|' + CAST(P.MemberID AS VARCHAR(20)) + '|','" + _lowerLevel + "')>0 ";
            }
            filter += getDateWhere() + " GROUP BY P.MemberId,M.MemberName,MI.RealName,P.SelfInterestRate";
            if (_sign == 1)
            {
                context.Response.Write(GetData());
            }
            else if (_bType == 1)
            {
                filter = " AND B.createTime>='" + _sYear + "-" + _sMonth + "-01' AND B.createTime<'" + Convert.ToDateTime(+_eYear + "/" + _eMonth + "/01").AddMonths(1).ToString("yyyy-MM-dd") + "'";
                if (!string.IsNullOrEmpty(_uName))
                    filter += _uType == "0" ? " AND M.MemberName LIKE '%" + _uName + "%'" : " AND MI.RealName LIKE '%" + _uName + "%'";
                context.Response.Write(GetPageList1(_currentPage, _pageSize, filter));
            }
            else
            {
                context.Response.Write(GetPageList(_currentPage, _pageSize, filter));
            }
        }

        private string GetlowerLevel(int memberId, int year, int month)
        {
            return new ReCommendRewardBll().GetLowerLevel(memberId, year, month);
        }

        private string GetData()
        {
            var bll = new ReCommendRewardBll();
            bool flag = bll.GetData();
            return "{\"result\":" + (flag ? "1" : "0") + "}";
        }

        //获得分页数据
        public object GetPageList(int currentPage, int pageSize, string filter)
        {
            currentPage += 1;
            int total;
            var bll = new ReCommendRewardBll();
            string eDate = _eMonth == 12 ? (_eYear + 1) + "-01-01" : _eYear + "-" + (_eMonth + 1) + "-01";
            var dt =
                bll.GetPageList(" P.SelfInterestRate,sum(P.DirectReward) as DirectReward,Sum(P.IndirectReward) as IndirectReward,Sum(P.SelfInterest) as SelfInterest," +
                    "P.MemberId,SUM(P.SumInterest) SumInterest,SUM(P.RewardRate) RewardRate,SUM(P.Reward) Reward,SUM(P.SumSubReward) SumSubReward,MAX(P.UpdateTime) TotalDate," +
                    "(SELECT TOP 1 LowerLevel FROM dbo.ReCommendRewardHistory WHERE MemberId=P.MemberId ORDER BY UpdateTime DESC) LowerLevel,M.MemberName,MI.RealName," +
                    "dbo.GetUserRecommendedCount(P.MemberId,'" + _sYear + "-" + _sMonth + "-01','" + eDate + "') as SumRec," +
                    "isnull(SUM(P.SelfBidAmount),0) as BidAmount,isnull(SUM(P.SumBidAmount),0) as LowerBidAmount", filter, "SUM(P.SumInterest) desc",
                    currentPage, pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }

        //获得分页数据
        public object GetPageList1(int currentPage, int pageSize, string filter)
        {
            currentPage += 1;
            int total;
            var bll = new ReCommendRewardBll();
            var dt = bll.GetPageList1(filter, currentPage, pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }

        private string getDateWhere()
        {
            string where;
            if (_sYear == _eYear)
            {
                where = "AND (P.TotalYear=" + _sYear + " AND P.TotalMonth>=" + _sMonth + " AND P.TotalMonth<=" + _eMonth + ")";
            }
            else
            {
                where = "AND (P.TotalYear=" + _sYear + " AND P.TotalMonth>=" + _sMonth + " AND P.TotalMonth<=12)";
                if ((_eYear - _sYear) > 2)
                {
                    for (int i = 0; i < (_eYear - _sYear - 1); i++)
                    {
                        where += " OR (P.TotalYear=" + (_sYear + i + 1) + " AND P.TotalMonth>=1 AND P.TotalMonth<=12)";
                    }
                }
                where += " OR (P.TotalYear=" + _eYear + " AND P.TotalMonth>=1 AND P.TotalMonth<=" + _eMonth + ")";
            }

            return where;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}