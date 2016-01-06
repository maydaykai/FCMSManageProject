/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-20
Description: 客户流失情况ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// UserDrainDetailsHandler 的摘要说明
    /// </summary>
    public class UserDrainDetailsHandler : IHttpHandler
    {
        private string _dateStart;
        private string _dateEnd;
        private DateTime? dateStart = null;
        private DateTime? dateEnd = null;
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
            _dateStart = ConvertHelper.QueryString(context.Request, "dateStart", "");
            _dateEnd = ConvertHelper.QueryString(context.Request, "dateEnd", "");
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);

            if (_dateStart != "")
                dateStart = Convert.ToDateTime(_dateStart);
            if (_dateEnd != "")
                dateEnd = Convert.ToDateTime(_dateEnd);

            context.Response.Write(GetUserDrainDetailsList(dateStart, dateEnd, _currentPage, _pageSize));
        }

        /// <summary>
        /// 客户流失情况数据
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>客户流失情况数据 json对象</returns>
        /// <remarks>add 卢侠勇,2015-7-20</remarks>
        public string GetUserDrainDetailsList(DateTime? dateStart, DateTime? endDate, int currentPage, int pageSize)
        {
            int total = 0;
            var dt = ReportBll.Instance.GetUserDrainDetailsList(dateStart, endDate, currentPage, pageSize, out total);
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