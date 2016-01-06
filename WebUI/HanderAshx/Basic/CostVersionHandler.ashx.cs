using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.Basic
{
    /// <summary>
    /// CostVersionHandler 的摘要说明
    /// </summary>
    public class CostVersionHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            context.Response.Write(GetJson());
        }


        private Object GetJson()
        {
            var costVersionBll = new CostVersionBll();
            var ds = costVersionBll.GetCostVersionList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var s = JsonHelper.DataTableToJson(ds.Tables[0]);
                return s;
            }
            return "";
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