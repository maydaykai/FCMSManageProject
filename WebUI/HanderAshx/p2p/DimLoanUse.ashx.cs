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
    /// DimLoanUse 的摘要说明
    /// </summary>
    public class DimLoanUse : IHttpHandler
    {
        private string _strWhere = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _strWhere = ConvertHelper.QueryString(context.Request, "strWhere", "");

            var filter = " 1=1";

            if (_strWhere.Contains("0"))
            {
                filter += "CHARINDEX('0|',[Sign]) > 0";
            }
            else if (_strWhere.Contains("1"))
            {
                filter += "CHARINDEX('1|',[Sign]) > 0";
            }


            context.Response.Write(GetDimLoanUseModelList(filter));
        }

        public string GetDimLoanUseModelList(string filter)
        {
            var list = new DimLoanUseBll().GetDimLoanUseModelList(filter);
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