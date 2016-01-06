using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// WithdrawCashHandler 的摘要说明
    /// </summary>
    public class WithdrawCashHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _memberName = "";
        private string _sn = "";
        private string _checkStatus = "";
        private string _status = "";
        private string _cashModeStatus = "";
        private string _startDate;
        private string _endDate;
        private decimal _withdrawAmountMin;
        private decimal _withdrawAmountMax;
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
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "C.ID");

            _checkStatus = ConvertHelper.QueryString(context.Request, "checkStatus", "");
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _sn = ConvertHelper.QueryString(context.Request, "SN", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", ""); 
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");
            _withdrawAmountMin = ConvertHelper.QueryString(context.Request, "withdrawAmountMin", 0);
            _withdrawAmountMax = ConvertHelper.QueryString(context.Request, "withdrawAmountMax", 0);
            _cashModeStatus = ConvertHelper.QueryString(context.Request, "cashModeStatus", "");

            switch (_sortField)
            {
                case "MemberName":_sortField = "C.MemberID"; break;
                case "BankName": _sortField = "C.BankAccountID"; break;
                case "StatusStr": _sortField = "C.Status"; break;
                case "CashStatusStr": _sortField = "C.CashStatus"; break;
                case "UpdateTime": _sortField = "C.UpdateTime"; break;
                case "RealName": _sortField = "MI.RealName"; break;
            }

            var filter = " C.Type=0 and (C.WarningStatus=0 or C.WarningStatus=3 or C.WarningStatus=4)";

            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and M.MemberName like '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_sn))
            {
                filter += " and C.REQ_SN like '%" + _sn + "%'";
            }
            if (!string.IsNullOrEmpty(_checkStatus))
            {
                filter += " and C.Status=" + _checkStatus;
            }
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and C.CashStatus=" + _status;
            }
            if (!string.IsNullOrEmpty(_cashModeStatus))
            {
                filter += " and C.CashMode=" + _cashModeStatus;
            }
            if (!string.IsNullOrEmpty(_startDate))
            {
                filter += " and DATEDIFF(s,ApplicationTime,'" + _startDate + "')<=0";
            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                filter += " and DATEDIFF(s,ApplicationTime,'" + _endDate + "')>=0";
            }
            if (_withdrawAmountMin>0)
            {
                filter += " and C.CashAmount>=" + _withdrawAmountMin;
            }
            if (_withdrawAmountMax > 0)
            {
                filter += " and C.CashAmount<=" + _withdrawAmountMax;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }
        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var cashRecordBll = new CashRecordBLL();
            string fields = "C.ID, C.MemberID, C.Type, C.CashMode, C.BankAccountType, C.BankAccountID, C.CashAmount, C.CashFee,C.FundsTrusteeshipFee, C.Status, C.CashStatus, C.REQ_SN, C.ApplicationTime, C.UpdateTime," +
                " M.MemberName, B.BankName,BLBankName=(select B.BankName from BankAccount_BL BL INNER JOIN dbo.Bank B ON BL.BankID=B.ID WHERE BL.Id=C.BankAccountID),AuthentBankName=(select B.BankName from BankAccount_Authent BA INNER JOIN dbo.Bank B ON BA.BankCode=B.BankCode WHERE BA.ID=C.BankAccountID),Case C.Status when 0 then '初审中' when 1 then '复审中' when 2 then '初审不通过' when 3 then '复审不通过' when 4 then '复审通过' end as StatusStr," +
                " Case C.CashStatus when 0 then '未汇款' when 1 then '汇款成功' when 2 then '汇款失败' end as CashStatusStr, MI.RealName,dbo.GetIsWithdrawAlertNew(C.MemberID,C.ApplicationTime) as IsWithdrawAlert,WarningStatus,WarningNote ";
            var dt = cashRecordBll.GetPageList(fields, filter, sortField, pagenum, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            //jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            var aggregate = cashRecordBll.Aggregate(filter);
            if (aggregate == null)
            {
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":0,\"Rows\":" + jsonStr + "}";
            }
            else
            {
                aggregate = ConvertHelper.ToDecimal(aggregate.ToString());
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":" + aggregate + ",\"Rows\":" + jsonStr + "}";
            }
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