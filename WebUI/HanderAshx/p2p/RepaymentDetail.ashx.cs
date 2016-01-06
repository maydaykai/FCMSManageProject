using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// RepaymentDetail 的摘要说明
    /// </summary>
    public class RepaymentDetail : IHttpHandler
    {
        private int _loanId;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _loanId = ConvertHelper.QueryString(context.Request, "loanId", 0);

            context.Response.Write(GetRepaymentDetail(_loanId, 0));
        }

        public string GetRepaymentDetail(int loanId, int repaymentType)
        {
            var dataset = new RepaymentDetailBll().GetList(loanId, 0);
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