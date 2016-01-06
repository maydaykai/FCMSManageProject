using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanUserInfo 的摘要说明
    /// </summary>
    public class LoanUserInfo : IHttpHandler
    {

        private int _memberId = 1;
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
            if (_memberId <= 0) return;
            context.Response.Write(GetUserInfo(_memberId, _type));
        }

        public object GetUserInfo(int memberId, int type)
        {
            string sql = type == 0 ? "SELECT LI.*,A.ParentID FROM dbo.LoanMemberInfo LI inner join Area A ON LI.DomicilePlace=A.ID WHERE MemberId=@MemberID" : "SELECT LI.*,A.ParentID FROM dbo.LoanEnterpriseMemberInfo LI inner join Area A ON LI.CityId=A.ID WHERE MemberId = @MemberID";
            var dt = new LoanBll().GetUserInfo(memberId, sql);
            return (dt != null && dt.Rows.Count > 0) ? JsonHelper.DataTableToJson(dt) : null;
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