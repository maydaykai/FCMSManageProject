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
    /// LoanInfo 的摘要说明
    /// </summary>
    public class LoanInfo : IHttpHandler
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

            var filter = " 1=1";

            if (_loanId > 0)
            {
                filter += " ID = " + _loanId;
            }

            context.Response.Write(GetLoanInfo(filter));
        }

        public string GetLoanInfo(string filter)
        {
            LoanModel model = new LoanBll().GetLoanInfoModel(filter);
            return JsonHelper.ObjectToJson(model);
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