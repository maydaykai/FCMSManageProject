using LuckyDraw.Business;
using LuckyDraw.BusinessModel;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.LuckyDraw
{
    /// <summary>
    /// ReportHandler 的摘要说明
    /// </summary>
    public class ReportHandler : IHttpHandler
    {
        private string sd;
        private string ed;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            int id = Convert.ToInt32(context.Request["id"]);
            sd = ConvertHelper.QueryString(context.Request, "sd", DateTime.Now.ToString("yyyy-MM-dd"));
            ed = ConvertHelper.QueryString(context.Request, "ed", DateTime.Now.ToString("yyyy-MM-dd"));
            List<Report> data = BizSweepstakeRecord.GetReport(id, Convert.ToDateTime(sd), Convert.ToDateTime(ed));
            //decimal resCount = 0;
            //foreach (var item in data)
            //{
            //    resCount += item.TotaL;
            //}
            //var result = new { TotalRows = data.Count, AmountAggregate = resCount, Rows = data };
            context.Response.Write(JsonHelper.ObjectToJson(data));
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