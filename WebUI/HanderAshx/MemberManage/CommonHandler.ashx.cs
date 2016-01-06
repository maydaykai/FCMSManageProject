using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// CommonHandler 的摘要说明
    /// </summary>
    public class CommonHandler : IHttpHandler
    {
        private int _sign;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _sign = ConvertHelper.QueryString(context.Request, "sign", 0);
            context.Response.Write(GetDate(_sign));
        }

  
        public string GetDate(int caseVal)
        {
            var dataVal = "";
            switch (caseVal)
            {
                case 1://银行卡列表
                    var bankBll = new BankBll();
                    var bankList = bankBll.GetBankTypeList();
                    dataVal = JsonHelper.DataTableToJson(bankList);
                    break;
            }
            return dataVal;

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