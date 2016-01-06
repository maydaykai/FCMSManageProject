using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// BankAccountHandler 的摘要说明
    /// </summary>
    public class BankAccountHandler : IHttpHandler
    {
        private int _memberId;
        private int _type;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _memberId = ConvertHelper.QueryString(context.Request, "memberId", 0);
            _type = ConvertHelper.QueryString(context.Request, "type", 0);
            context.Response.Write(GetBankList(_memberId));
        }

        //获得某个会员所有启用的银行卡列表
        public object GetBankList(int memberId)
        {
            var bankAccountBll = new BankAccountBll();
            int total;
            var dt = _type == 0 ? bankAccountBll.GetPageList(" P.ID,P.MemberID,P.BankID,P.BankCardNo,B.BankName,B.EnglishName", " P.MemberID=" + memberId + " AND P.[Status]=1", " P.ID", 1, 10, out total) : bankAccountBll.GetAuthentPageList(" P.ID,P.MemberID,P.BankID,P.BankCardNo,B.BankName,B.EnglishName", " P.MemberID=" + memberId + " AND P.[Status]=1", " P.ID", 1, 10, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            return jsonStr;
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