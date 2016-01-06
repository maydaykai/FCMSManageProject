using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// WithdrawAlertHandler 的摘要说明
    /// </summary>
    public class WithdrawAlertHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _checkStatus = "";
        private string _startDate;
        private string _endDate;
        private string _sUserType = "0";
        private string _uName = "";
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
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");

            _sUserType = ConvertHelper.QueryString(context.Request, "sUserType", "0");
            _uName = ConvertHelper.QueryString(context.Request, "uName", "");

            switch (_sortField)
            {
                case "MemberName": _sortField = "C.MemberID"; break;
                case "BankName": _sortField = "C.BankAccountID"; break;
                case "StatusStr": _sortField = "C.Status"; break;
                case "CashStatusStr": _sortField = "C.CashStatus"; break;
                case "UpdateTime": _sortField = "C.UpdateTime"; break;
                case "RealName": _sortField = "MI.RealName"; break;
            }

            var filter = " C.Type=0";

            if (!string.IsNullOrEmpty(_uName))
            {
                if (_sUserType == "0")
                {
                    filter += " and M.MemberName = '" + _uName + "'";
                }
                else if (_sUserType == "1")
                {
                    filter += " and MI.RealName = '" + _uName + "'";
                }
                else
                    filter += " and M.Mobile = '" + _uName + "'";
            }

            if (!string.IsNullOrEmpty(_checkStatus))
                filter += " and C.WarningStatus=" + _checkStatus;
            else
                filter += " and C.WarningStatus>0";

            if (!string.IsNullOrEmpty(_startDate))
                filter += " and DATEDIFF(s,ApplicationTime,'" + _startDate + "')<=0";
            if (!string.IsNullOrEmpty(_endDate))
                filter += " and DATEDIFF(s,ApplicationTime,'" + _endDate + "')>=0";

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }
        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var cashRecordBll = new CashRecordBLL();
            string fields = "C.ID, C.MemberID, C.Type, C.CashMode, C.BankAccountType, C.BankAccountID, C.CashAmount, C.CashFee,C.FundsTrusteeshipFee, C.Status, C.CashStatus, C.REQ_SN, C.ApplicationTime, C.UpdateTime,C.WarningStatus,C.WarningNote," +
                " M.MemberName,M.Mobile, B.BankName,BLBankName=(select B.BankName from BankAccount_BL BL INNER JOIN dbo.Bank B ON BL.BankID=B.ID WHERE BL.Id=C.BankAccountID),AuthentBankName=(select B.BankName from BankAccount_Authent BA INNER JOIN dbo.Bank B ON BA.BankCode=B.BankCode WHERE BA.ID=C.BankAccountID),Case C.Status when 0 then '初审中' when 1 then '复审中' when 2 then '初审不通过' when 3 then '复审不通过' when 4 then '复审通过' end as StatusStr," +
                " Case C.CashStatus when 0 then '未汇款' when 1 then '汇款成功' when 2 then '汇款失败' end as CashStatusStr, MI.RealName,dbo.GetIsWithdrawAlert(C.MemberID) as IsWithdrawAlert ";
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