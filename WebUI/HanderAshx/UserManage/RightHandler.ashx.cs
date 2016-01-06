using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.UserManage
{
    /// <summary>
    /// RightHandler 的摘要说明
    /// </summary>
    public class RightHandler : IHttpHandler
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


        private static Object GetJson()
        {
            var rightBll = new RightBll();
            var s = JsonHelper.DataTableToJson(rightBll.GetRightList(0).Tables[0]);
            return s;
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