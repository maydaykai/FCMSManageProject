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
    /// LoanMemberInfo 的摘要说明
    /// </summary>
    public class LoanMemberInfo : IHttpHandler
    {
        private int _ID ;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            
            _ID = ConvertHelper.QueryString(context.Request, "ID", 0);

            context.Response.Write(SaveUserCreditLine(context));
        }

        private string SaveUserCreditLine(HttpContext context)
        {
            CreditLineModel model = new CreditLineModel();
            model.ID = ConvertHelper.QueryString(context.Request, "ID", 0);
            model.CreditLine = ConvertHelper.QueryString(context.Request, "CreditLine", 0);
            model.CardNumber = ConvertHelper.QueryString(context.Request, "CardNumber", "");
            model.CreditNumber = ConvertHelper.QueryString(context.Request, "CreditNumber", "");
            model.IdentityCard = ConvertHelper.QueryString(context.Request, "IdentityCard", "");
            model.OpUid = ConvertHelper.QueryString(context.Request, "ID", 0);
            bool flag = CreditLineBLL.LoanCreditLineser(model);
            return flag ? "success" : "error";
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