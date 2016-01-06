using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// TotalOperationHandlerNew 的摘要说明
    /// </summary>
    public class TotalOperationHandlerNew : IHttpHandler
    {
        private string _date1;
        private string _date2;
        private string _nexttime;
        private int _pageIndex = 0;
        private int _pageSize = 0;
        private int _TotalRows = 0;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _date1 = ConvertHelper.QueryString(context.Request, "date1", DateTime.Now.ToString("yyyy-MM-dd"));
            _date2 = ConvertHelper.QueryString(context.Request, "date2", DateTime.Now.ToString("yyyy-MM-dd"));
            _nexttime = Convert.ToDateTime(_date1).AddMonths(-1).ToString("yyyy-MM-dd");
            _pageIndex = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            if (_pageIndex == 0)
            {
                _pageIndex = 1;
            }
            else
            {
                _pageIndex = _pageIndex + 1;
            }

            //_pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 100);
            _pageSize = 100;
            context.Response.Write(GetDealTotalList(Convert.ToDateTime(_date1), Convert.ToDateTime(_date2)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public string GetDealTotalList(DateTime date1, DateTime date2)
        {

            var dataset = new TotalMarketRepBll().GetListNew(date1, date2);
            //var jsonStr = "{\"TotalRows\":0,\"Rows\":" + JsonHelper.DataTableToJson(dataset.Tables[0]) + "}";
            //return jsonStr;
            return JsonHelper.DataTableToJson(dataset.Tables[0]);
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