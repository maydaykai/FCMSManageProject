/*************************************************************
Author:		 卢侠勇
Create date: 2015-08-04
Description: 客服回访ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// ReturnVisitHandler 的摘要说明
    /// </summary>
    public class ReturnVisitHandler : IHttpHandler
    {
        private int _mid;
        private int _currentPage = 1;
        private int _pageSize;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _mid = ConvertHelper.QueryString(context.Request, "mid", 0);
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            context.Response.Write(GetReturnVisitList(_mid, _currentPage, _pageSize));
        }

        /// <summary>
        /// 获取客服回访数据
        /// </summary>
        /// <param name="mid">会员ID</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>客服回访 json对象</returns>
        /// <remarks>add 卢侠勇,2015-08-04</remarks>
        public string GetReturnVisitList(int mid, int currentPage, int pageSize)
        {
            int total = 0;
            var dt = ReportBll.Instance.GetReturnVisitList(mid, currentPage, pageSize, out total);
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