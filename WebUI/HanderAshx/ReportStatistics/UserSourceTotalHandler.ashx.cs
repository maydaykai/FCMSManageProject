/*************************************************************
Author:		 卢侠勇
Create date: 2015-09-14
Description: 访问报表ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// UserSourceTotalHandler 的摘要说明
    /// </summary>
    public class UserSourceTotalHandler : IHttpHandler
    {
        private string _dateStart;
        private string _dateEnd;
        private int _currentPage = 1;
        private int _pageSize;
        private string _memberName;
        private int _channel;
        private string _channelRemark;
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
            _memberName = ConvertHelper.QueryString(context.Request, "txtName", "");
            _channel = ConvertHelper.QueryString(context.Request, "channel",0);
            _channelRemark = ConvertHelper.QueryString(context.Request, "channelRemark","");
            context.Response.Write(GetUserSourceTotalList(Convert.ToDateTime(_dateStart), Convert.ToDateTime(_dateEnd),_memberName, _channel, _channelRemark , _currentPage, _pageSize));
        }

        /// <summary>
        /// 获取访问报表数据
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>访问报表数据 json对象</returns>
        public string GetUserSourceTotalList(DateTime dateStart, DateTime endDate,string memberName,int channel,string channelRemark, int currentPage, int pageSize)
        {
            int total = 0;
            var dt = ReportBll.Instance.GetUserSourceTotalList(dateStart, endDate,memberName,channel,channelRemark, currentPage, pageSize, out total);
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