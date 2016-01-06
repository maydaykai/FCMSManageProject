using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// RecommendRewardDetailHandler 的摘要说明
    /// </summary>
    public class RecommendRewardDetailHandler : IHttpHandler
    {
        private int _currentPage = 1, _pageSize, _sYear, _sMonth, _eYear, _eMonth, _bType;
        private string _uName, _uType;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _uName = ConvertHelper.QueryString(context.Request, "uName", "");
            _uType = ConvertHelper.QueryString(context.Request, "uType", "0");
            _sYear = ConvertHelper.QueryString(context.Request, "sYear", 0);
            _sMonth = ConvertHelper.QueryString(context.Request, "sMonth", 0);
            _eYear = ConvertHelper.QueryString(context.Request, "eYear", 0);
            _eMonth = ConvertHelper.QueryString(context.Request, "eMonth", 0);
            _bType = ConvertHelper.QueryString(context.Request, "bType", 0);
            var filter = string.Empty;
            var filter2 = "接";
            _uName = HttpContext.Current.Server.UrlDecode(_uName);

            filter = " AND B.createTime>='" + _sYear + "-" + _sMonth + "-01' AND B.createTime<'" + Convert.ToDateTime(+_eYear + "/" + _eMonth + "/01").AddMonths(1).ToString("yyyy-MM-dd") + "'";
            if (!string.IsNullOrEmpty(_uName))
                filter += _uType == "0" ? " AND M.MemberName = '" + _uName + "'" : " AND MI.RealName = '" + _uName + "'";

            if (_bType != 0)
                filter2 = _bType == 1 ? "直接" : "间接";

            context.Response.Write(GetPageList(_currentPage, _pageSize, filter, filter2));

        }

        public object GetPageList(int currentPage, int pageSize, string filter, string filter2)
        {
            currentPage += 1;
            int total;
            var bll = new ReCommendRewardBll();
            var dt = bll.GetRecommendDetalis(filter, filter2, currentPage, pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
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