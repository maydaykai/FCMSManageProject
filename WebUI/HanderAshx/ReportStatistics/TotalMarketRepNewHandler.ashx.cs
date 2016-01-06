using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// TotalMarketRepNewHandler 的摘要说明
    /// </summary>
    public class TotalMarketRepNewHandler : IHttpHandler
    {
        private string _date1;
        private string _date2;
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

            context.Response.Write(GetDealTotalList(Convert.ToDateTime(_date1), Convert.ToDateTime(_date2)));
        }

        public string GetDealTotalList(DateTime date1, DateTime date2)
        {
            var dataset = new TotalMarketRepBll().GetListNew(date1, date2);
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