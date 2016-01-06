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
    /// ApproveAdvance 的摘要说明
    /// </summary>
    public class ApproveAdvance : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _memberName = "";
        private string _isExtend = "";
        private string _status = "";
        private int _filter = 0;
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
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "DESC");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "LoanID");

            _isExtend = ConvertHelper.QueryString(context.Request, "isExtend", "");
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _filter = ConvertHelper.QueryString(context.Request, "filter", 0);
            //筛选贷款类型
            _loantypeID = ConvertHelper.QueryString(context.Request, "loantypeID", 0);

            //获取当前登录用户ID
            //通过用户ID获取用户的角色
            //通过角色获取审批的权限
            int userID = _filter;
            FcmsUserBll userBll = new FcmsUserBll();
            FcmsUserModel userModel = userBll.GetModel(userID);
            string Roles = userModel.RoleId;


            var _where = " an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and ve.CurrStep in (1,2,3,4,5)) ";

            var filter = "";

            //通过角色找到对应下属
            if (userID == 1)
            {
                filter += " 1=1 ";
            }
            else
            {
                RoleRightBll roleBll = new RoleRightBll();

                //p2p模块ID
                int columnID = 176;
                //客服主管审核权限ID
                int KHChargeRightID = 14;
                //风控主管审核权限ID
                int FKChargeRightID = 15;
                //风控总监审核权限ID
                int FKDirectorRightID = 16;
                //财务审核权限ID
                int CWRightID = 17;
                //是否有权限
                int isAuthority = 0;

                //判断是否有客服主管审核权限
                if (roleBll.GetIsAuthority(columnID, KHChargeRightID, Roles))
                {
                    filter = " an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and CurrStep = 2) ";
                    isAuthority++;
                }
                //判断是否有风控主管审核权限
                if (roleBll.GetIsAuthority(columnID, FKChargeRightID, Roles))
                {
                    if (isAuthority > 0)
                        filter += " or an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and CurrStep = 1) ";
                    else
                        filter = " an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and CurrStep = 1) ";
                    isAuthority++;
                }
                //判断是否有风控总监审核权限
                if (roleBll.GetIsAuthority(columnID, FKDirectorRightID, Roles))
                {
                    if (isAuthority > 0)
                        filter += " or an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and CurrStep = 3) ";
                    else
                        filter = " an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and CurrStep = 3) ";
                    isAuthority++;
                }
                //判断是否有财务审核权限
                if (roleBll.GetIsAuthority(columnID, CWRightID, Roles))
                {
                    if (isAuthority > 0)
                        filter += " or an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and CurrStep in (4,5)) ";
                    else
                        filter = " an.LoanID = (select LoanID from AdvanceApprove ve where ve.LoanID = an.LoanID and ve.PeNumber = an.PeNumber and CurrStep in (4,5)) ";
                    isAuthority++;
                }

                //如果都没有权限，则不显示数据
                if (isAuthority == 0)
                {
                    filter = " 1=2 ";
                }
            }

            //排除作废
            filter += " and an.Status <> 3 and an.OverStatus <> 3 ";


            switch (_sortField)
            {
                case "MemberName":
                    _sortField = "LoanID"; break;
                case "IsExtendStr": _sortField = "IsExtend"; break;
                case "StatusStr": _sortField = "Status"; break;
            }

            

            if (!string.IsNullOrEmpty(_memberName))
            {
                _where += " and LoanID in (select ID from Loan where MemberID in (select ID from Member where MemberName like '%" + _memberName + "%'))";
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
            filter += " group by convert(varchar(5),loanid)+'|'+convert(varchar(5),penumber),PeNumber,LoanID,IsExtend,Status,RePayTime";
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
                "convert(varchar(5),loanid)+'|'+convert(varchar(5),penumber) as ID,(select LoanTypeID from Loan where ID = an.LoanID) loantypeid,(select 'RJB' + LoanNumber from Loan where ID = LoanID) as LoanNumber,PeNumber, LoanID, RePayTime, SUM(RePrincipal) RePrincipal, SUM(ReInterest) ReInterest, SUM(ReOverInterest) ReOverInterest, SUM(SurPrincipal) SurPrincipal, SUM(SurReInterest) SurReInterest, SUM(SurOverInterest) SurOverInterest," +
                " (select MemberName from Member where ID=(select MemberID from Loan where ID=LoanID)) MemberName," +
                " Case IsExtend when 0 then '未展期' when 1 then '已展期' end as IsExtendStr," +
                " Case Status when 0 then '未还' when 1 then '部分已还' when 2 then '全额已还' when 3 then '作废' end as StatusStr" +
                " FROM RepaymentPlan an where " + filter + " ) an ";
            var tables = "";


            var dt = overdueBLL.GetPageList(fields, _where, sortField, pagenum, pagesize, out total,tables);
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