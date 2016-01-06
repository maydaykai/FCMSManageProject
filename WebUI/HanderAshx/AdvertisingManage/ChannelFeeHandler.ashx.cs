/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 渠道费用管理AJAX请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using System.Collections;
using System.Collections.Generic;

namespace WebUI.HanderAshx.AdvertisingManage
{
    /// <summary>
    /// ChannelFeeHandler 的摘要说明
    /// </summary>
    public class ChannelFeeHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private int _id = 0;
        private int _channelId;
        private string _startDate = "";
        private string _endDate = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            switch(context.Request.QueryString["t"])
            {
                case "list":
                    GetChannelFeeList(context);
                    break;
                case "details":
                    GetChannelFeeDetailsList(context);
                    break;
                case "get":
                    GetChannelFee(context);
                    break;
                case "add":
                    AddChannelFee(context);
                    break;
                case "set":
                    SetChannelFee(context);
                    break;
            }
        }

        /// <summary>
        /// 渠道费用列表
        /// </summary>
        /// <param name="context"></param>
        public void GetChannelFeeList(HttpContext context)
        {
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");

            var filter = string.Empty;
            if (!string.IsNullOrEmpty(_startDate))
                filter += " AND CONVERT(CHAR(10),C.createTime,120)>='" + _startDate + "'";
            if (!string.IsNullOrEmpty(_endDate))
                filter += " AND CONVERT(CHAR(10),C.createTime,120)<='" + _endDate + "'";

            int total;
            var dt = AdvertisementBLL.Instance.GetChannelFeeList(filter, _currentPage + 1, _pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        /// <summary>
        /// 渠道费用明细列表
        /// </summary>
        /// <param name="context"></param>
        public void GetChannelFeeDetailsList(HttpContext context)
        {
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _channelId = ConvertHelper.QueryString(context.Request, "channelId", -1);
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");

            var filter = string.Empty;
            if (!string.IsNullOrEmpty(_startDate))
                filter += " AND CONVERT(CHAR(10),CF.createTime,120)>='" + _startDate + "'";
            if (!string.IsNullOrEmpty(_endDate))
                filter += " AND CONVERT(CHAR(10),CF.createTime,120)<='" + _endDate + "'";
            if (_channelId != -1)
                filter += " AND CF.channelID=" + _channelId;
            int total;
            var dt = AdvertisementBLL.Instance.GetChannelFeeDetailsList(filter, _currentPage + 1, _pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        /// <summary>
        /// 获取渠道费用最新详情
        /// </summary>
        /// <param name="context"></param>
        public void GetChannelFee(HttpContext context)
        {
            var isGet = true;
            var filter = string.Empty;
            _startDate = context.Request.QueryString["startDate"];
            _channelId = int.Parse(context.Request.QueryString["channelId"]);
            _id = int.Parse(context.Request.QueryString["id"]);

            if (string.IsNullOrEmpty(_startDate))
                isGet = false;
            else
                filter += " AND CONVERT(CHAR(10),CF.createTime,120)<='" + _startDate + "'";

            if (_channelId == -1)
                isGet = false;
            else
                filter += " AND CF.channelID=" + _channelId;

            if (_id > 0)
                filter = "AND CF.id=" + _id;

            if (isGet)
            {
                string fields = "dayFee,(CASE WHEN DATEPART(WEEK,CF.createTime)=DATEPART(WEEK,'" + DateTime.Parse(_startDate).AddDays(-1).ToString("yyyy-MM-dd") + "') THEN zhouFee ELSE 0 END) zhouFee,(CASE WHEN DATEPART(M,CF.createTime)=DATEPART(M,'" + _startDate + "') THEN CF.monthFee ELSE 0 END) monthFee,feeSum";
                var dt = AdvertisementBLL.Instance.GetChannelFee(fields, filter);
                var jsonStr = JsonHelper.DataTableToJson(dt);
                jsonStr = "{\"TotalRows\":" + dt.Rows.Count + ",\"Rows\":" + jsonStr + "}";
                context.Response.Write(jsonStr);
            }
            else
                context.Response.Write(null);
        }

        /// <summary>
        /// 新增渠道费用
        /// </summary>
        /// <param name="context"></param>
        public void AddChannelFee(HttpContext context)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("channelID", context.Request["channelID"]);
            dic.Add("dayFee", context.Request["dayFee"]);
            dic.Add("createTime", context.Request["createTime"]);
            context.Response.Write("{ \"Result\":" + AdvertisementBLL.Instance.AddChannelFee(dic) + " }");
        }

        /// <summary>
        /// 修改渠道费用
        /// </summary>
        /// <param name="context"></param>
        public void SetChannelFee(HttpContext context)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("id", context.Request["id"]);
            dic.Add("dayFee", context.Request["dayFee"]);
            dic.Add("originalDayFee", context.Request["originalDayFee"]);
            context.Response.Write("{ \"Result\":" + AdvertisementBLL.Instance.SetChannelFee(dic) + " }");
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