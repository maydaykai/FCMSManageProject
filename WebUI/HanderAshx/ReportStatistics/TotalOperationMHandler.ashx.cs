using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// TotalOperationMHandler 的摘要说明
    /// </summary>
    public class TotalOperationMHandler : IHttpHandler
    {
        private string _year;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _year = ConvertHelper.QueryString(context.Request, "yearnum", DateTime.Now.ToString("yyyy"));

            context.Response.Write(GetTotalOperationMList(_year));
        }

        public string GetTotalOperationMList(string yearstr)
        {
            var dataset = new TotalOperationBll().GetMonthList(yearstr);
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