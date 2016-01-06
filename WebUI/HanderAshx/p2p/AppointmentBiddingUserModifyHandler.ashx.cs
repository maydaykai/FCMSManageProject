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
    /// AppointmentBiddingUserModifyHandler 的摘要说明
    /// </summary>
    public class AppointmentBiddingUserModifyHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            var name = ConvertHelper.QueryString(context.Request, "name", "");
            var data = new MemberBll().Getmember(name);
            var jsonStr = JsonHelper.DataTableToJson(data);
            jsonStr = "{\"TotalRows\":" + data.Rows.Count + ",\"Rows\":" + jsonStr + "}";
            context.Response.Write(jsonStr);
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