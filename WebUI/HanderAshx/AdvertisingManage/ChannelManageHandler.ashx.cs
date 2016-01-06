/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 渠道管理AJAX请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace WebUI.HanderAshx.AdvertisingManage
{
    /// <summary>
    /// ChannelManageHandler 的摘要说明
    /// </summary>
    public class ChannelManageHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private int _id = 0;
        private int _channelId;
        private string _secondary;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            switch (context.Request.QueryString["t"])
            {
                case "list":
                    GetChannelList(context);
                    break;
                case "get":
                    GetChannel(context);
                    break;
                case "add":
                    AddChannel(context);
                    break;
                case "set":
                    SetChannel(context);
                    break;
            }
        }

        /// <summary>
        /// 渠道列表
        /// </summary>
        /// <param name="context"></param>
        public void GetChannelList(HttpContext context)
        {
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _channelId = ConvertHelper.QueryString(context.Request, "channelId", -1);
            _secondary = ConvertHelper.QueryString(context.Request, "secondary", "");

            var filter = string.Empty;
            if (_channelId != -1)
                filter += " AND DC.Id=" + _channelId;
            if (!string.IsNullOrEmpty(_secondary))
                filter += " AND (DCA.id LIKE '%" + _secondary + "%' OR A.id LIKE '%" + _secondary + "%')";

            int total;
            var dt = AdvertisementBLL.Instance.GetChannelList(filter, _currentPage + 1, _pageSize, out total).EncryptID("dId");
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        /// <summary>
        /// 获取渠道详情
        /// </summary>
        /// <param name="context"></param>
        public void GetChannel(HttpContext context)
        {
            var fields = string.Empty;
            var filter = string.Empty;
            _id = int.Parse(context.Request.QueryString["id"]);
            switch (context.Request.QueryString["tp"])
            {
                case "1":
                    fields = "DC.Rebate,DC.Channel,DC.Id dId";
                    filter += " AND DC.Id=" + _id;
                    break;
                case "2":
                    fields = "DC.Id dId,DCA.id,DCA.classifyName";
                    filter += " AND DCA.id=" + _id;
                    break;
                case "3":
                    fields = "DC.Id dId,A.id fId,DCA.classifyName,A.classifyName fClassifyName ";
                    filter += " AND A.id=" + _id;
                    break;
            }
            var dt = AdvertisementBLL.Instance.GetChannel(fields, filter);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + dt.Rows.Count + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        /// <summary>
        /// 新增渠道
        /// </summary>
        /// <param name="context"></param>
        public void AddChannel(HttpContext context)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            switch (context.Request["tp"])
            {
                case "1":
                    dic.Add("channel", context.Request["channel"]);
                    dic.Add("rebate", context.Request["rebate"]);
                    context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.AddChannel(dic) + "\" }");
                    break;
                case "2":
                    dic.Add("id", context.Request["id"]);
                    dic.Add("classifyName", context.Request["classifyName"]);
                    context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.AddFoxproChannel(dic) + "\" }");
                    break;
                case "3":
                    dic.Add("id", context.Request["id"]);
                    dic.Add("classifyName", context.Request["classifyName"]);
                    dic.Add("fClassifyName", context.Request["fClassifyName"]);
                    context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.AddFlyersChannel(dic) + "\" }");
                    break;
            }
        }

        /// <summary>
        /// 修改渠道
        /// </summary>
        /// <param name="context"></param>
        public void SetChannel(HttpContext context)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            switch (context.Request["tp"])
            {
                case "1":
                    dic.Add("channel", context.Request["channel"]);
                    dic.Add("rebate", context.Request["rebate"]);
                    dic.Add("id", context.Request["id"]);
                    context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.SetChannel(dic) + "\" }");
                    break;
                case "2":
                    dic.Add("id", context.Request["id"]);
                    dic.Add("classifyName", context.Request["classifyName"]);
                    context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.SetFoxproChannel(dic) + "\" }");
                    break;
                case "3":
                    dic.Add("id", context.Request["id"]);
                    dic.Add("did", context.Request["did"]);
                    dic.Add("classifyName", context.Request["classifyName"]);
                    dic.Add("fClassifyName", context.Request["fClassifyName"]);
                    context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.SetFlyersChannel(dic) + "\" }");
                    break;
            }
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