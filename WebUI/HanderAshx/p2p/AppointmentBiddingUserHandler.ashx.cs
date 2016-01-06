using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// AppointmentBiddingUserHandler 的摘要说明
    /// </summary>
    public class AppointmentBiddingUserHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _mobile = "";
        private string _isExtend = "";

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
            _isExtend = ConvertHelper.QueryString(context.Request, "isExtend", "");
            _mobile = ConvertHelper.QueryString(context.Request, "mobile", "");
            var filter = string.Empty;
            if (!string.IsNullOrEmpty(_mobile))
                filter += " AND A.mobile='" + _mobile + "'";

            if (!string.IsNullOrEmpty(_isExtend))
                filter += " AND A.status=" + _isExtend;

            context.Response.Write(GetPageList(_currentPage, _pageSize, filter));
        }

        public object GetPageList(int pageIndex, int pageSize, string filter)
        {
            int total = 0;
            pageIndex += 1;
            var dt = new LoanBll().GetAppointmentBiddingUserList(filter, pageIndex, pageSize, ref total);
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