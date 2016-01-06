using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.FinanceSettlement
{
    /// <summary>
    /// WithdrawInterestNH 的摘要说明
    /// </summary>
    public class WithdrawInterestNH : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            context.Response.Write(GetWithdrawInterestNhList());
        }

        public string GetWithdrawInterestNhList()
        {
            var dataset = new MemberBll().GetWithdrawInterestNhList();
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