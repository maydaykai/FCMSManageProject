using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanApplyNumber 的摘要说明
    /// </summary>
    public class LoanApplyNumber : IHttpHandler
    {
        private int _memberId;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _memberId = ConvertHelper.QueryString(context.Request, "memberId", 0);

            context.Response.Write(GetLoanNumberList(_memberId));
        }

        public string GetLoanNumberList(int memberId)
        {
            var dataset = new LoanApplyBll().GetLoanNumberList(memberId);
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