using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// RepaymentTotalHandler 的摘要说明
    /// </summary>
    public class RepaymentTotalHandler : IHttpHandler
    {
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
                case "q":
                    GetRepaymentList(context);
                    break;
                case "t":
                    GetRepaymentList2(context);
                    break;
                case "z":
                    GetRepaymentList3(context);
                    break;
            }
        }

        //获取还款列表
        public void GetRepaymentList(HttpContext context)
        {
            int _currentPage = 1;
            int _pageSize;
            string _checkStatus = "";
            string _startDate;
            string _endDate;
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);

            _checkStatus = ConvertHelper.QueryString(context.Request, "checkStatus", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", DateTime.Now.ToString("yyyy-MM-dd"));
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", DateTime.Now.ToString("yyyy-MM-dd"));

            _currentPage = _currentPage + 1;
            int total;
            var dt = ReportBll.Instance.GetRepaymentTotalList(_startDate, _endDate, _checkStatus, _currentPage, _pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        //获取每期已还数据
        public void GetRepaymentList2(HttpContext context)
        {
            int _currentPage = 1;
            int _pageSize;
            string _startDate;
            string _endDate;
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);

            _startDate = ConvertHelper.QueryString(context.Request, "startDate", DateTime.Now.ToString("yyyy-MM-dd"));
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", DateTime.Now.ToString("yyyy-MM-dd"));

            _currentPage = _currentPage + 1;
            int total;
            var dt = ReportBll.Instance.GetRepaymentTotalList(_startDate, _endDate, _currentPage, _pageSize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
        }

        //获取分支已还数据
        public void GetRepaymentList3(HttpContext context)
        {
            string _startDate;
            string _endDate;

            _startDate = ConvertHelper.QueryString(context.Request, "startDate", DateTime.Now.ToString("yyyy-MM-dd"));
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", DateTime.Now.ToString("yyyy-MM-dd"));

            var dt = ReportBll.Instance.GetRepaymentTotalList(_startDate, _endDate);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + dt.Rows.Count + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
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