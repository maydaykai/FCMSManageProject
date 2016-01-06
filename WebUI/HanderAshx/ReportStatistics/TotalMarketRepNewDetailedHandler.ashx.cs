using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// TotalMarketRepNewDetailedHandler 的摘要说明
    /// </summary>
    public class TotalMarketRepNewDetailedHandler : IHttpHandler
    {
        private string _date1;
        private string _date2;
        
        private string _memberName;
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
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            context.Response.Write(GetDealTotalList(Convert.ToDateTime(_date1), Convert.ToDateTime(_date2), _memberName));
        }

        public string GetDealTotalList(DateTime date1, DateTime date2, string memberName)
        {
            string nexttime = date1.AddMonths(-1).ToString("yyyy-MM-dd");
            var dataset = new TotalMarketRepBll().GetListNewByMemberId(date1, date2, nexttime, memberName);
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