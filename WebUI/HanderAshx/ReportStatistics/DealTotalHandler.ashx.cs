using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// DealTotalHandler 的摘要说明
    /// </summary>
    public class DealTotalHandler : IHttpHandler
    {

        private string _totalDate;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _totalDate = ConvertHelper.QueryString(context.Request, "totalDate", DateTime.Now.ToString("yyyy-MM-dd"));

            context.Response.Write(GetDealTotalList(Convert.ToDateTime(_totalDate)));
        }

        public string GetDealTotalList(DateTime totalDate)
        {

            var dataset = new DealTotalBll().GetList(totalDate);
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