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
    /// Guarantee 的摘要说明
    /// </summary>
    public class Guarantee : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            context.Response.Write(GetGuaranteeList());
        }

        public string GetGuaranteeList()
        {
            List<GuaranteeModel> list = new GuaranteeBll().GetGuaranteeModelList();
            return JsonHelper.ObjectToJson(list);
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