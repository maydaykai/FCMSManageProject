using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// SysProjectSummaryHandler 的摘要说明
    /// </summary>
    public class SysProjectSummaryHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _loanNumber;
        private int _prepaymentStatus;
        private int _loanStatus;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _loanNumber = ConvertHelper.QueryString(context.Request, "loanNumber", "");
            _prepaymentStatus = ConvertHelper.QueryString(context.Request, "prepaymentStatus", 0);
            _loanStatus = ConvertHelper.QueryString(context.Request, "loanStatus", 0);
            var filter = "";
            _loanNumber = HttpContext.Current.Server.UrlDecode(_loanNumber);
            if (!string.IsNullOrEmpty(_loanNumber))
            {
                filter += " AND charindex('" + _loanNumber.Trim() + "',LoanNumber) >0";
            }

            if(_prepaymentStatus==1)
                filter += " AND EXISTS (SELECT 1 FROM RepaymentPlan R WHERE R.LoanID=Loan.ID AND R.OverStatus=4)";

            if (_loanStatus > 0)
                filter += " AND ExamStatus =" + _loanStatus;

            context.Response.Write(GetPageList(_currentPage, _pageSize, filter));

        }

        //获得分页数据
        public object GetPageList(int currentPage, int pageSize, string filter)
        {
            int total;
            var loanBll = new LoanBll();
            var dt = loanBll.SysProjectSummary(currentPage, pageSize, filter, out total);
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