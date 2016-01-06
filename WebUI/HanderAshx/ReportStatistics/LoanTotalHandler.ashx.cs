using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// LoanTotalHandler 的摘要说明
    /// </summary>
    public class LoanTotalHandler : IHttpHandler
    {

        private string _dateStart;
        private string _dateEnd;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _dateStart = ConvertHelper.QueryString(context.Request, "dateStart", DateTime.Now.ToString("yyyy-MM-dd"));
            _dateEnd = ConvertHelper.QueryString(context.Request, "dateEnd", DateTime.Now.ToString("yyyy-MM-dd"));

            context.Response.Write(GetLoanTotalList(Convert.ToDateTime(_dateStart), Convert.ToDateTime(_dateEnd)));
        }

        public string GetLoanTotalList(DateTime dateStart, DateTime endDate)
        {

            var dataset = new LoanTotalBll().GetList(dateStart, endDate); 
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