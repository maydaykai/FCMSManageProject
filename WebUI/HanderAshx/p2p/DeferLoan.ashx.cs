using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// DeferLoan 的摘要说明
    /// </summary>
    public class DeferLoan : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "A.ID";
        private string _memberName = "";
        private string _status = "";
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
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "DESC");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "A.ID");

            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");

            switch (_sortField)
            {
                case "MemberName":
                    _sortField = "MemberID"; break;
                case "StatusStr": _sortField = "Status"; break;
            }

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and MemberName like '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and Status=" + _status;
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }
        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var deferLoanBLL = new ApplyDeferLoanBLL();
            string fields = "A.*,M.MemberName,L.LoanNumber,case A.Status when 0 then '未审核' when 1 then '初审不通过' when 2 then '复审中' when 3 then '复审不通过' when 4 then '复审通过' end as StatusStr," +
                "case L.BorrowMode when 0 then '天' when 1 then '月' end as ExtensionModeStr";
            var dt = deferLoanBLL.GetPageList(fields, filter, sortField, pagenum, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
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