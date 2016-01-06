using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// MemberProjectHandler 的摘要说明
    /// </summary>
    public class MemberProjectHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _uName;
        private string _uType;
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
            var filter = "";
            _uName = HttpContext.Current.Server.UrlDecode(_uName);
            if (!string.IsNullOrEmpty(_uName))
            {
                string field = (_uType.Equals("0")) ? "M.MemberName" : "MI.RealName";
                filter += " AND charindex('" + _uName.Trim() + "'," + field + ") >0";
            }
            context.Response.Write(GetPageList(_currentPage, _pageSize, filter));

        }

        //获得分页数据
        public object GetPageList(int currentPage, int pageSize, string filter)
        {
            int total;
            var memberBll = new MemberBll();
            var dt = memberBll.InvestorProjectSummary(currentPage, pageSize, filter, out total);
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