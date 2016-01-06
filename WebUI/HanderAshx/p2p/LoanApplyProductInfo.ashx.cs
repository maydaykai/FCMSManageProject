using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanApplyProductInfo 的摘要说明
    /// </summary>
    public class LoanApplyProductInfo : IHttpHandler
    {
        private int _loanApplyId;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _loanApplyId = ConvertHelper.QueryString(context.Request, "loanApplyId", 0);

            var filter = " l.loanApplyId = " + _loanApplyId;

            context.Response.Write(GetList(filter));

        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetList(string filter)
        {
            var dataset = new LoanApplyBll().GetLoanApplyProductInfoList(filter);
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