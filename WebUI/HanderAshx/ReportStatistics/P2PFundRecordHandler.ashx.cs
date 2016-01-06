/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-16
Description: 平台资金情况ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// P2PFundRecordHandler 的摘要说明
    /// </summary>
    public class P2PFundRecordHandler : IHttpHandler
    {
        private string _dateStart;
        private string _dateEnd;
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
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _dateStart = ConvertHelper.QueryString(context.Request, "dateStart", DateTime.Now.ToString("yyyy-MM-dd"));
            _dateEnd = ConvertHelper.QueryString(context.Request, "dateEnd", DateTime.Now.ToString("yyyy-MM-dd"));
            context.Response.Write(GetFundRecordList(Convert.ToDateTime(_dateStart), Convert.ToDateTime(_dateEnd), _currentPage, _pageSize));
        }

        /// <summary>
        /// 获取平台资金情况数据
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>平台资金情况数据 json对象</returns>
        /// <remarks>add 卢侠勇,2015-7-16</remarks>
        public string GetFundRecordList(DateTime dateStart, DateTime endDate, int currentPage, int pageSize)
        {
            int total = 0;
            var dt = ReportBll.Instance.GetFundRecordList(dateStart, endDate, currentPage, pageSize, out total);
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