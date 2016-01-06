using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// DimProjectTemplate 的摘要说明
    /// </summary>
    public class DimProjectTemplate : IHttpHandler
    {
        private int _productTypeId;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _productTypeId = ConvertHelper.QueryString(context.Request, "productTypeId", 0);

            context.Response.Write(GetProjectTemplate(_productTypeId));
        }

        public string GetProjectTemplate(int productTypeId)
        {
            var dataset = new ProjectTemplateBll().GetProjectTemplate(productTypeId);
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