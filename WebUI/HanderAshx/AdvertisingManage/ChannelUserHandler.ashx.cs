/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-17
Description: 渠道商帐户管理AJAX请求处理
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
    /// ChannelUserHandler 的摘要说明
    /// </summary>
    public class ChannelUserHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private int _id = 0;
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
                    GetChannelUserList(context);
                    break;
                case "get":
                    GetChannelUser(context);
                    break;
                case "add":
                    AddChannelUser(context);
                    break;
                case "set":
                    SetChannelUser(context);
                    break;
            }
        }

        /// <summary>
        /// 渠道商用户列表
        /// </summary>
        /// <param name="context"></param>
        public void GetChannelUserList(HttpContext context)
        {
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);

            var filter = string.Empty;
            int total;
            var dt = AdvertisementBLL.Instance.GetChannelUserList(filter, _currentPage + 1, _pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        /// <summary>
        /// 获取渠道商用户详情
        /// </summary>
        /// <param name="context"></param>
        public void GetChannelUser(HttpContext context)
        {
            var fields = string.Empty;
            var filter = string.Empty;
            _id = int.Parse(context.Request.QueryString["id"]);
            fields = "* ";
            filter += " AND CU.Id=" + _id;
            var dt = AdvertisementBLL.Instance.GetChannelUser(fields, filter);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + dt.Rows.Count + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        /// <summary>
        /// 新增渠道商用户
        /// </summary>
        /// <param name="context"></param>
        public void AddChannelUser(HttpContext context)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("channelName", context.Request["channelName"]);
            dic.Add("channelPwd", System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(context.Request["channelPwd"], "md5"));
            dic.Add("channelId", context.Request["channelId"]);
            dic.Add("contactPerson", context.Request["contactPerson"]);
            dic.Add("permissions", context.Request["permissions"]);
            dic.Add("registeredFormula", context.Request["registeredFormula"]);
            dic.Add("investFormula", context.Request["investFormula"]);
            dic.Add("regInvestFormula", context.Request["regInvestFormula"]);
            dic.Add("investQuota", context.Request["investQuota"]);
            context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.AddChannelUser(dic) + "\" }");
        }

        /// <summary>
        /// 修改渠道商用户
        /// </summary>
        /// <param name="context"></param>
        public void SetChannelUser(HttpContext context)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Id", context.Request["Id"]);
            dic.Add("isDelete", context.Request["isDelete"]);
            dic.Add("channelId", context.Request["channelId"]);
            dic.Add("contactPerson", context.Request["contactPerson"]);
            dic.Add("permissions", context.Request["permissions"]);
            dic.Add("registeredFormula", context.Request["registeredFormula"]);
            dic.Add("investFormula", context.Request["investFormula"]);
            dic.Add("regInvestFormula", context.Request["regInvestFormula"]);
            dic.Add("investQuota", context.Request["investQuota"]);
            context.Response.Write("{ \"Result\":\"" + AdvertisementBLL.Instance.SetChannelUser(dic) + "\" }");
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