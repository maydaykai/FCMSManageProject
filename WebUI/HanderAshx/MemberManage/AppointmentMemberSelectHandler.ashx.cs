using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// AppointmentMemberSelectHandler 的摘要说明
    /// </summary>
    public class AppointmentMemberSelectHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _dateStart;
        private string _dateEnd;

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
            _dateStart = ConvertHelper.QueryString(context.Request, "startCreateTime", "");
            _dateEnd = ConvertHelper.QueryString(context.Request, "endCreateTime", "");

            var filter = string.Empty;
            if (!string.IsNullOrEmpty(_dateStart))
                filter += " AND A.createTime>='" + _dateStart + "'";

            if (!string.IsNullOrEmpty(_dateEnd))
                filter += " AND A.createTime<" + DateTime.Parse(_dateEnd).AddDays(1).ToString("yyyy-MM-dd");

            context.Response.Write(GetPageList(_currentPage, _pageSize, filter));
        }

        public object GetPageList(int pageIndex, int pageSize, string filter)
        {
            int total = 0;
            pageIndex += 1;
            var dt = new LoanBll().GetAppointmentBiddingUserList(pageIndex, pageSize, ref total, filter);
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