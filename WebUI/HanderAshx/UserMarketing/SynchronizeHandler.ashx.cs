using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.UserMarketing
{
    /// <summary>
    /// SynchronizeHandler 的摘要说明 同步营销人员
    /// </summary>
    public class SynchronizeHandler : IHttpHandler
    {
        private string _mouth;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _mouth = ConvertHelper.QueryString(context.Request, "mouth", "");
            var productBll = new Marketing_RoleBLL();
            productBll.SynchronizeEx_Person(_mouth);
           // var relust=new {}
            context.Response.Write("1");
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