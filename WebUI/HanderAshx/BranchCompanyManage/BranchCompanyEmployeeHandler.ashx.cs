using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.BranchCompanyManage
{
    /// <summary>
    /// BranchCompanyEmployeeHandler 的摘要说明
    /// </summary>
    public class BranchCompanyEmployeeHandler : IHttpHandler
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
            context.Response.Write(GetJson());
        }
        private Object GetJson()
        {
            var bll = new BranchCompanyBll();
            var lockAmount = bll.GetBranchCompanyLockAmount(_memberId);//绿色通道锁定金额
            var balance = new MemberBll().GetModel(_memberId).Balance;//账户余额
            var duein = new RepaymentPlanBll().GetDueInMoneyByMemberId(_memberId);//待收总额
            return "{\"lockAmount\":" + lockAmount + ",\"balance\":" + (balance + duein) + "}";
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