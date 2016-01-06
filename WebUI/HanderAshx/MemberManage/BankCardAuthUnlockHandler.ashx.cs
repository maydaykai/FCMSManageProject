using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// BankCardAuthUnlockHandler 的摘要说明
    /// </summary>
    public class BankCardAuthUnlockHandler : IHttpHandler
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
            context.Response.Write(Unlock(_ID));
        }
        public string Unlock(int ID)
        {
            bool flag = new BankCardAuthentBLL().unlock(ID);
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