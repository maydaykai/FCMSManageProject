using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// CouponCodeHandler 的摘要说明
    /// </summary>
    public class CouponCodeHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _uName = "";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "DESC");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "C.ID");

            _uName = ConvertHelper.QueryString(context.Request, "uName", "");

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_uName))
            {
                filter += " and M.RealName like '%" + _uName + "%'";
            }

            var sortColumn = _sortField + " " + _sort;

            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var memberBll = new MemberBll();
            var dt = memberBll.GetPageList("C.*,M.MemberName", "dbo.Coupon C LEFT JOIN dbo.Member M ON C.UseID=M.ID", filter, sortField, pagenum, pagesize, out total);
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