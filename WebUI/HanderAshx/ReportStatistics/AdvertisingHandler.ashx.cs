/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-14
Description: 广告统计ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// AdvertisingHandler 的摘要说明
    /// </summary>
    public class AdvertisingHandler : IHttpHandler
    {
        private string _dateStart;
        private string _dateEnd;
        private string _channelRemark;
        private int _channel;
        private int _currentPage = 1;
        private int _pageSize;
        private int _revStatus = 0;
        private int _minBalance = 0;
        private int _maxBalance = 0;

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
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _revStatus = ConvertHelper.QueryString(context.Request, "revStatus", 0);
            _channelRemark = ConvertHelper.QueryString(context.Request, "channelRemark", "");
            _channel = ConvertHelper.QueryString(context.Request, "channel", 0);
            _minBalance = ConvertHelper.QueryString(context.Request, "minBalance", 0);
            _maxBalance = ConvertHelper.QueryString(context.Request, "maxBalance", 0);

            context.Response.Write(GetAdvertisingList(Convert.ToDateTime(_dateStart), Convert.ToDateTime(_dateEnd), _minBalance, _maxBalance, _channelRemark, _channel, _revStatus, _currentPage, _pageSize));
        }

        /// <summary>
        /// 获取广告统计数据
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="minBalance">最小可用余额</param>
        /// <param name="maxBalance">最大可用余额</param>
        /// <param name="channelRemark">广告来源</param>
        /// <param name="channel">广告渠道</param>
        /// <param name="revStatus">回访状态：0全部 -1未回访 1已回访</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>广告统计数据 json对象</returns>
        /// <remarks>add 卢侠勇,2015-7-14</remarks>
        public string GetAdvertisingList(DateTime dateStart, DateTime endDate, int minBalance, int maxBalance, string channelRemark, int channel, int revStatus, int currentPage, int pageSize)
        {
            int total = 0;
            var dt = ReportBll.Instance.GetAdvertisingList(dateStart, endDate, minBalance, maxBalance, channelRemark, channel, revStatus, currentPage, pageSize, out total);
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