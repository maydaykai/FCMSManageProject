using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// BankCardAuthResetHandler 的摘要说明
    /// </summary>
    public class BankCardAuthResetHandler : IHttpHandler
    {

        private int _ID = 0;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _ID = ConvertHelper.QueryString(context.Request, "ID", 0);
            context.Response.Write(Reset(_ID));
        }
        public string Reset(int ID)
        {
            bool flag = new BankCardAuthentBLL().Reset(ID);
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