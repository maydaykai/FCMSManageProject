/*************************************************************
Author:		 卢侠勇
Create date: 2015-08-20
Description: 周报报表ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// WeeklyTotalHandler 的摘要说明
    /// </summary>
    public class WeeklyTotalHandler : IHttpHandler
    {
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
            _dateStart = ConvertHelper.QueryString(context.Request, "dateStart", DateTime.Now.ToString("yyyy-MM-dd"));
            _dateEnd = ConvertHelper.QueryString(context.Request, "dateEnd", DateTime.Now.ToString("yyyy-MM-dd"));
            context.Response.Write(GetWeekly(Convert.ToDateTime(_dateStart), Convert.ToDateTime(_dateEnd)));
        }

        /// <summary>
        /// 获取周报报表数据
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>周报报表数据 json对象</returns>
        /// <remarks>add 卢侠勇,2015-08-20</remarks>
        public string GetWeekly(DateTime dateStart, DateTime endDate)
        {
            var dt = ReportBll.Instance.GetWeekly(dateStart, endDate);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + dt.Rows.Count + ",\"Rows\":" + jsonStr + "}";
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