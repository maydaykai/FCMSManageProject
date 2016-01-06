using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// AuditHistory 的摘要说明
    /// </summary>
    public class AuditHistory : IHttpHandler
    {
        private int _loanId;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";


            _loanId = ConvertHelper.QueryString(context.Request, "loanid", 0);

            var filter = " LoanID = " + _loanId;

            context.Response.Write(GetList(filter));
        }

        public string GetList(string filter)
        {
            var dataset = new AuditHistoryBll().GetList(filter);
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