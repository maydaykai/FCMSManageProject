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
    /// LoanDelayed 的摘要说明
    /// </summary>
    public class LoanDelayed : IHttpHandler
    {
        private string _memberName = "";
        private int _auditStatus;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _auditStatus = ConvertHelper.QueryString(context.Request, "auditStatus", -1);

            var filter = "1 = 1 ";
            if (!string.IsNullOrEmpty(_memberName) && _memberName != "undefined")
            {
                filter += " and MemberName='" + _memberName + "' ";
            }
            if (_auditStatus!= -1)
            {
                filter += " and AuditStatus=" + _auditStatus + " ";
            }

            context.Response.Write(GetLoanDelayedList(filter));
        }

        public string GetLoanDelayedList(string filter)
        {
            var dataset = new LoanDelayedBll().GetList(filter);
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