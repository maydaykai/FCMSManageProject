using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    public class AdvanceManage : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _memberName = "";
        private string _isExtend = "";
        private string _status = "";
        private string _filter = "";
        
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
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "LoanID");

            _isExtend = ConvertHelper.QueryString(context.Request, "isExtend", "");
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _filter = ConvertHelper.QueryString(context.Request, "filter", "");
            

            switch (_sortField)
            {
                case "MemberName":
                    _sortField = "LoanID"; break;
                case "IsExtendStr": _sortField = "IsExtend"; break;
                case "StatusStr": _sortField = "Status"; break;
            }

            var filter = _filter;

            filter += " EXISTS (select t2.loanid,t2.Penumber from  AdvanceApprove t2 where an.LoanID = t2.LoanID and an.PeNumber = t2.PeNumber and t2.CurrStep = 6)";

            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and LoanID in (select ID from Loan where MemberID in (select ID from Member where MemberName like '%" + _memberName + "%'))";
            }
            
            if (!string.IsNullOrEmpty(_isExtend))
            {
                filter += " and IsExtend=" + _isExtend + "";
            }
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and Status=" + _status;
            }
            filter += " group by PeNumber,LoanID,IsExtend,Status,RePayTime";
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }
        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var overdueBLL = new OverdueBLL();
            string fields = "(select 'RJB' + LoanNumber from Loan where ID = LoanID) as LoanNumber,PeNumber, LoanID, RePayTime, SUM(RePrincipal) RePrincipal, SUM(ReInterest) ReInterest, SUM(ReOverInterest) ReOverInterest, SUM(SurPrincipal) SurPrincipal, SUM(SurReInterest) SurReInterest, SUM(SurOverInterest) SurOverInterest," +
                " (select MemberName from Member where ID=(select MemberID from Loan where ID=LoanID)) MemberName," +
                " Case IsExtend when 0 then '未展期' when 1 then '已展期' end as IsExtendStr," +
                " Case Status when 0 then '未还' when 1 then '部分已还' when 2 then '全额已还' when 3 then '作废' end as StatusStr";
            var dt = overdueBLL.GetPageList(fields, filter, sortField, pagenum, pagesize, out total);
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