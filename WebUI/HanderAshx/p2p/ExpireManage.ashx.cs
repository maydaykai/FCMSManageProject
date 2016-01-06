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
    /// ExpireManage 的摘要说明
    /// </summary>
    public class ExpireManage : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ExpireDays";
        private string _memberName = "";
        private string _isExtend = "";
        private string _status = "";
        private string _loanNumber = "";
        private int _expireDays;
        private string _filter = "";
        private int MemberId = 0;
        private int _loantypeID = 0;
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
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "ASC");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "ExpireDays");

            _isExtend = ConvertHelper.QueryString(context.Request, "isExtend", "");
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _loanNumber = ConvertHelper.QueryString(context.Request, "loanNumber", "");
            _expireDays = ConvertHelper.QueryString(context.Request, "expireDays", 0);
            _filter = ConvertHelper.QueryString(context.Request, "filter", "");

            
            //筛选贷款类型
            _loantypeID = ConvertHelper.QueryString(context.Request, "loantypeID", 0);

            //角色ID，取角色筛选数据
            MemberId = ConvertHelper.QueryString(context.Request, "MemberId", 0);
            FcmsUserBll userBll = new FcmsUserBll();
            FcmsUserModel userModel = userBll.GetModel(MemberId);
            string Roles = userModel.RoleId;

            switch (_sortField)
            {
                case "MemberName":
                    _sortField = "LoanID"; break;
                case "IsExtendStr": _sortField = "IsExtend"; break;
                case "StatusStr": _sortField = "Status"; break;
                //case "ExpireDays":_sortField = "ExpireDays";break;
                case "ExpireDays":_sortField = "datediff(dd, getdate(), repaytime)";break;
                //case "ExpireDays": _sortField = "ExpireDays"; break;
            }

            var filter = _filter;
            var _where = " 1=1 ";

            _sort = SQLReplace.ReplaceSQLKey(_sort);
            _sortField = SQLReplace.ReplaceSQLKey(_sortField);
            _memberName = SQLReplace.ReplaceSQLKey(_memberName);
            _isExtend = SQLReplace.ReplaceSQLKey(_isExtend);
            _status = SQLReplace.ReplaceSQLKey(_status);
            _loanNumber = SQLReplace.ReplaceSQLKey(_loanNumber);

            //排除掉正在审核和已审核的垫付记录
            filter += " and LoanID not in (SELECT LOANID FROM AdvanceApprove WHERE AdvanceApprove.LOANID = an.LOANID AND AdvanceApprove.PeNumber = an.PeNumber) ";

            //排除作废
            filter += " and an.Status <> 3 and an.OverStatus <> 3 ";

            if (1 == 1)
            {
                filter += " and 1=1 ";
            }
            else
            { 
                RoleRightBll roleBll = new RoleRightBll();

                //p2p模块ID
                int columnID = 91;
                //客服确认垫付权限ID
                int KHRightID = 12;
                //风控确认垫付权限ID
                int FKRightID = 13;
                //是否有权限
                int isAuthority = 0;

                //判断是否有客服确认垫付权限
                if (roleBll.GetIsAuthority(columnID, KHRightID, Roles))
                {
                    filter += " and LoanID in (select ID from Loan where LoanTypeID = 4)";
                    isAuthority++;
                }
                //判断是否有风控确认垫付权限
                if (roleBll.GetIsAuthority(columnID, FKRightID, Roles))
                {
                    filter += " and LoanID in (select ID from Loan where LoanTypeID <> 8)";
                    isAuthority++;
                }

                //如果都没有权限，则不显示数据
                if (isAuthority == 0)
                {
                    filter += " and 1=2 ";
                }
            }
            

            if (!string.IsNullOrEmpty(_memberName))
            {
                _where += " and LoanID in (select ID from Loan where MemberID in (select ID from Member where MemberName like '%" + _memberName + "%'))";
            }
            if (_expireDays > 0)
            {
                _where += " and ExpireDays <" + _expireDays;
            }
            if (!string.IsNullOrEmpty(_loanNumber))
            {
                _where += " and (select LoanNumber from Loan where ID = LoanID) like '%" + _loanNumber + "%'";
            }
            if (!string.IsNullOrEmpty(_isExtend))
            {
                _where += " and IsExtend=" + _isExtend + "";
            }
            if (!string.IsNullOrEmpty(_status))
            {
                _where += " and Status=" + _status;
            }
            if (_loantypeID > 0)
            {
                if (_loantypeID == 1)
                { _where += " and LoanID in (select ID from Loan where LoanTypeID <> 4) "; }
                else
                { _where += " and LoanID in (select ID from Loan where LoanTypeID = 4) "; }
            }

            //_where += " and datediff(dd,getdate(),repaytime)>=0 ";
            filter += " group by convert(varchar(5),loanid)+'|'+convert(varchar(5),penumber),PeNumber,LoanID,IsExtend,Status,RePayTime,datediff(dd,getdate(),repaytime)";
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter, _where));
        }
        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter, string _where)
        {
            pagenum = pagenum + 1;
            int total;
            var overdueBLL = new OverdueBLL();
            string fields = " * FROM ( SELECT " +
                "convert(varchar(5),loanid)+'|'+convert(varchar(5),penumber) as ID,(select LoanTypeID from Loan where ID = an.LoanID) loantypeid,(select 'RJB' + LoanNumber from Loan where ID = LoanID) as LoanNumber,(SELECT Balance FROM dbo.Member WHERE id = (select MemberID from Loan where ID = LoanID)) AS Balance,PeNumber, LoanID, RePayTime,case when (select LoanTypeID from Loan where ID = LoanID) = 4 then datediff(dd,getdate(),repaytime) + 10 else datediff(dd,getdate(),repaytime) end as ExpireDays, SUM(RePrincipal) RePrincipal, SUM(ReInterest) ReInterest, SUM(ReOverInterest) ReOverInterest, SUM(SurPrincipal) SurPrincipal, SUM(SurReInterest) SurReInterest, SUM(SurOverInterest) SurOverInterest," +
                " (select MemberName from Member where ID=(select MemberID from Loan where ID=LoanID)) MemberName," +
                " (select RealName from  MemberInfo where MemberID=(select MemberID  from Loan where ID = LoanID)) RealName, " +
                " Case IsExtend when 0 then '未展期' when 1 then '已展期' end as IsExtendStr," +
                " Case Status when 0 then '未还' when 1 then '部分已还' when 2 then '全额已还' when 3 then '作废' end as StatusStr," +
                " (select Agency from dbo.Loan where ID = LoanID) as Agency " +
                " FROM RepaymentPlan an where " + filter + " ) an ";

            var tables = "";

            var dt = overdueBLL.GetPageList(fields, _where, sortField, pagenum, pagesize, out total, tables);
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