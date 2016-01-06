using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// MemberAccountManageHandler 的摘要说明
    /// </summary>
    public class MemberAccountManageHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "LoanNumber";

        private string _sUserType = "0";
        private string _uName = "";
        private int _balance = 0;
        private int _accountPayable = 0;
        private int _accountPayableMax = 0;
        private string _dateEnd = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "desc");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "id");

            _sUserType = ConvertHelper.QueryString(context.Request, "sUserType", "0");
            _uName = ConvertHelper.QueryString(context.Request, "uName", "");
            _balance = ConvertHelper.QueryString(context.Request, "balance", 0);
            _accountPayable = ConvertHelper.QueryString(context.Request, "accountPayable", 0);
            _accountPayableMax = ConvertHelper.QueryString(context.Request, "accountPayableMax", _accountPayable);
            _dateEnd = ConvertHelper.QueryString(context.Request, "dateEnd", DateTime.Now.ToString("yyyy-MM-dd"));
            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_uName))
            {
                if (_sUserType == "0")
                {
                    filter += " and m.MemberName like '%" + _uName + "%'";
                }
                else
                {
                    filter += " and mi.RealName like '%" + _uName + "%'";
                }
            }
            filter += " and m.Balance >= " + _balance;
            filter += " and dbo.GetMemberAccountPayable(m.id) >= " + _accountPayable;

            if (_accountPayableMax > 0)
            {
                filter += " and dbo.GetMemberAccountPayable(m.id) < " + _accountPayableMax;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetMemberAccountManageList(_currentPage, _pageSize, sortColumn, filter, Convert.ToDateTime(_dateEnd)));
        }

        public string GetMemberAccountManageList(int pagenum, int pagesize, string sortField, string filter, DateTime dateEnd)
        {
            pagenum = pagenum + 1;
            int pageCount = 0;
            var dataset = new MemberBll().GetList(filter, sortField, pagenum, pagesize,dateEnd, ref pageCount);
            var jsonStr = JsonHelper.DataTableToJson(dataset.Tables[0]);
            jsonStr = "{\"TotalRows\":" + pageCount + ",\"Rows\":" + jsonStr + "}";
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