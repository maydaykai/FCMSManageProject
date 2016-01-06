/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-11
Description: 投资用户明细AJAX请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.AdvertisingManage
{
    /// <summary>
    /// BiddingUserDetailsHandler 的摘要说明
    /// </summary>
    public class BiddingUserDetailsHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _secondary = "";
        private string _startDate = "";
        private string _endDate = "";
        private string _memberName = "";
        private string _payTerminal = "";
        private string _investTerminal = "";
        private string _regSource = "";
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
            _secondary = ConvertHelper.QueryString(context.Request, "Secondary", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", DateTime.Now.ToString("yyyy-MM-dd"));
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            _memberName = ConvertHelper.QueryString(context.Request, "name", "");
            _payTerminal = ConvertHelper.QueryString(context.Request, "payTerminal", "");
            _investTerminal = ConvertHelper.QueryString(context.Request, "investTerminal", "");
            _regSource = ConvertHelper.QueryString(context.Request, "regSource", "");

            var filter = "AND B.CreateTime>='" + _startDate + "' AND B.CreateTime<='" + DateTime.Parse(_endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(_secondary))
            {
                filter += " AND M.ChannelRemark LIKE '%" + _secondary + "%'";
            }
            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " AND M.MemberName LIKE '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_payTerminal))
            {
                filter += " AND R.RechargeChannel=" + _payTerminal;
            }
            if (!string.IsNullOrEmpty(_investTerminal))
            {
                filter += " AND B.BidType=" + _investTerminal;
            }
            if (!string.IsNullOrEmpty(_regSource))
            {
                filter += " AND M.RegSource=" + _regSource;
            }

            context.Response.Write(GetPageList(_currentPage, _pageSize, filter));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var dt = AdvertisementBLL.Instance.GetBiddingUserDetailsList(filter, pagenum, pagesize, out total);
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