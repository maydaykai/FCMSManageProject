using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanAuthInfo 的摘要说明
    /// </summary>
    public class LoanAuthInfo : IHttpHandler
    {
        private int _loanId;
        private int _sign;
        private int _id;
        private string _authDate;
        private string _isAuth;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _loanId = ConvertHelper.QueryString(context.Request, "loanId", 0);
            _sign = ConvertHelper.QueryString(context.Request, "sign", 0);
            _authDate = ConvertHelper.QueryString(context.Request, "AuthDate", "");
            _isAuth = ConvertHelper.QueryString(context.Request, "IsAuth", "false");
            _id = ConvertHelper.QueryString(context.Request, "Id", 0);

            var filter = " l.LoanID = " + _loanId;

            if (_sign == 0) //获取列表
            {
                context.Response.Write(GetList(filter));
            }
            if (_sign == 1) //修改
            {
                context.Response.Write(Update(_id, _authDate, _isAuth, _loanId));
            }
            if (_sign == 2) //审核通过列表
            {
                filter = filter + " and l.IsAuth = 1 ";
                context.Response.Write(GetList(filter));
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetList(string filter)
        {
            var dataset = new LoanAuthInfoBll().GetList(filter);
            return JsonHelper.DataTableToJson(dataset.Tables[0]);
        }

        public string Update(int id, string authDate, string isAuth, int loanId)
        {
            var returnval = new LoanAuthInfoBll().Update(id, authDate, isAuth);
            return returnval ? PublicConst.Success : PublicConst.Error;
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