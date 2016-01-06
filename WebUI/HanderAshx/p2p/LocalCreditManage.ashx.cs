using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LocalCreditManage 的摘要说明
    /// </summary>
    public class LocalCreditManage : IHttpHandler
    {
        private int _currentPage = 0;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "CreateTime";
        private string _startCreateTime = "";
        private string _endCreateTime = "";
        private string _name = "";
        private int _status;
        private string _loanNumber = "";
        private string _output = "";
        private int _id;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _output = ConvertHelper.QueryString(context.Request, "output", "");
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 20);
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "desc");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "id");
            _id = ConvertHelper.QueryString(context.Request, "Id", 0);

            if (_sortField == "LoanUseName")
            {
                _sortField = "LoanUseID";
            }

            _startCreateTime = ConvertHelper.QueryString(context.Request, "startCreateTime", "");
            _endCreateTime = ConvertHelper.QueryString(context.Request, "endCreateTime", "");
            _name = ConvertHelper.QueryString(context.Request, "name", "");
            _status = ConvertHelper.QueryString(context.Request, "status", 0);
            _loanNumber = ConvertHelper.QueryString(context.Request, "loanNumber", "");
            var filter = " 1=1 AND ExamStatus <= 5";

            if (!string.IsNullOrEmpty(_name))
            {
                filter += " and MemberName like '" + _name + "%'";
            }
            if (_status >= 0)
            {
                filter += " and ExamStatus = " + _status;
            }
            if (!string.IsNullOrEmpty(_startCreateTime))
            {
                filter += " and CreateTime>='" + _startCreateTime + "'";
            }
            if (!string.IsNullOrEmpty(_endCreateTime))
            {
                filter += " and CreateTime<=DATEADD(DAY,1,'" + _endCreateTime + "')";
            }
            if (!string.IsNullOrEmpty(_loanNumber))
            {
                filter += "and LoanNumber = '" + _loanNumber + "'";
            }
            if (_id > 0)
            {
                filter += " and ID = " + _id;
            }
            var sortColumn = _sortField + " " + _sort;

            context.Response.Write(GetPageLoanRapidManage(_currentPage, _pageSize, sortColumn, filter));

        }

        public object GetPageLoanRapidManage(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;

            int pageCount = 0;
            var localCreditBll = new LocalCreditBll();
            IList<LocalCreditModel> list = localCreditBll.GetPagedLoanApplyList(filter, sortField, pagenum, pagesize, ref pageCount);
            var jsonData = new
            {
                TotalRows = pageCount,//记录数
                Rows = list//实体列表
            };
            var s = JsonHelper.ObjectToJson(jsonData);
            return s;
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