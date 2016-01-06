using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.BranchCompanyManage
{
    /// <summary>
    /// BranchCompanyHandler 的摘要说明
    /// </summary>
    public class BranchCompanyHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            context.Response.Write(GetJson());
        }
        private Object GetJson()
        {
            var bll = new BranchCompanyBll();
            var ds = bll.GetBranchCompanyList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var s = JsonHelper.DataTableToJson(ds.Tables[0]);
                return s;
            }
            return "{\"TotalRows\":0,\"Rows\":''}";
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