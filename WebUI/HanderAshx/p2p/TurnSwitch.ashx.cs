using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// TurnSwitch 的摘要说明
    /// </summary>
    public class TurnSwitch : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            context.Response.Write(OnOrOffSwitch());
        }

        private string OnOrOffSwitch()
        {
            var loanBll = new LoanBll();
            int type = ConvertHelper.QueryString(HttpContext.Current.Request, "type", 1);
            int id = ConvertHelper.QueryString(HttpContext.Current.Request, "id", 0);
            int status = ConvertHelper.QueryString(HttpContext.Current.Request, "status", 1);

            bool flag = loanBll.OnOrOffSwitch(type, id, status);
            return flag ? "1" : "0";
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