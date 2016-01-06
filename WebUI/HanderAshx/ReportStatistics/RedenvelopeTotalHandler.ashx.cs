/*************************************************************
Author:		 卢侠勇
Create date: 2015-11-24
Description: 红包报表ajax请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// RedenvelopeTotalHandler 的摘要说明
    /// </summary>
    public class RedenvelopeTotalHandler : IHttpHandler
    {
        int _currentPage = 1;
        int _pageSize;
        string _checkStatus = "";
        string _amountType = "";
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
            _amountType = ConvertHelper.QueryString(context.Request, "amountType", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");
            _name = ConvertHelper.QueryString(context.Request, "name", "");

            context.Response.Write(GetRedenvelopeDetailsList(_startDate, _endDate, _name, _checkStatus, _amountType, _currentPage, _pageSize));
        }

        public string GetRedenvelopeDetailsList(string dateStart, string endDate, string name, string checkStatus, string amountType, int currentPage, int pageSize)
        {
            currentPage = currentPage + 1;
            string filter = string.Empty;
            if (!string.IsNullOrEmpty(dateStart))
                filter += " AND R.CreateTime>='" + DateTime.Parse(dateStart).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(endDate))
                filter += " AND R.CreateTime<'" + DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(name))
                filter += " AND M.MemberName='" + name + "'";

            if (!string.IsNullOrEmpty(checkStatus))
                filter += " AND R.Status=" + checkStatus;

            if (amountType != "-1" && !string.IsNullOrEmpty(amountType))
                filter += " AND R.RedenvelopeID=" + amountType;

            int total = 0;
            var dt = ReportBll.Instance.GetRedenvelopeDetailsList(filter, currentPage, pageSize, out total);
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