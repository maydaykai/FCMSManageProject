/*************************************************************
Author:		 卢侠勇
Create date: 2015-11-27
Description: 积分报表ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// MemberPointsTotalHandler 的摘要说明
    /// </summary>
    public class MemberPointsTotalHandler : IHttpHandler
    {
        int _currentPage = 1;
        int _pageSize;
        string _checkStatus = "";
        string _startDate;
        string _endDate;
        string _name;

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

            _checkStatus = ConvertHelper.QueryString(context.Request, "checkStatus", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");
            _name = ConvertHelper.QueryString(context.Request, "name", "");

            context.Response.Write(GetMemberPointsList(_startDate, _endDate, _name, _checkStatus, _currentPage, _pageSize));
        }

        public string GetMemberPointsList(string dateStart, string endDate, string name, string checkStatus, int currentPage, int pageSize)
        {
            currentPage = currentPage + 1;
            string filter = string.Empty;
            if (!string.IsNullOrEmpty(dateStart))
                filter += " AND MPD.CreateTime>='" + DateTime.Parse(dateStart).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(endDate))
                filter += " AND MPD.CreateTime<'" + DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(name))
                filter += " AND M.MemberName='" + name + "'";

            if (!string.IsNullOrEmpty(checkStatus))
                filter += " AND DM.ID=" + checkStatus;

            int total = 0;
            var dt = ReportBll.Instance.GetMemberPointsList(filter, currentPage, pageSize, out total);
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