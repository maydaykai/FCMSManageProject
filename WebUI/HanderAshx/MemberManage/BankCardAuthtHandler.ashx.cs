using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// BankCardAuthtHandler 的摘要说明
    /// </summary>
    public class BankCardAuthtHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _sign = "";
        private string _payType = "";
        private string _payStatus = "";
        private string _memberName = "";
        private int _flag = 0;

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
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "P.ID");

            switch (_sortField)
            {
                case "SignString": _sortField = "P.sign"; break;
                case "PayTypeString": _sortField = "P.PayType"; break;
                case "PayStatusString": _sortField = "P.PayStatus"; break;
                case "UpdateTime": _sortField = "P.UpdateTime"; break;
            }

            _sign = ConvertHelper.QueryString(context.Request, "sign", "");
            _payType = ConvertHelper.QueryString(context.Request, "payType", "");
            _payStatus = ConvertHelper.QueryString(context.Request, "payStatus", "");
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _flag = ConvertHelper.QueryString(context.Request, "flag", 0);

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and M.MemberName like '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_sign))
            {
                filter += " and P.sign=" + _sign;
            }
            if (!string.IsNullOrEmpty(_payType))
            {
                filter += " and P.PayType="+_payType; 
            }
            if (!string.IsNullOrEmpty(_payStatus))
            {
                filter += " and P.PayStatus=" + _payStatus;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(_flag == 1 ? GetPageList1(_currentPage, _pageSize, sortColumn, filter) : GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var bankCardAuthentBLL = new BankCardAuthentBLL();
            var dt = bankCardAuthentBLL.GetPageList(" P.ID, B.BankName, MI.RealName as Account, P.BankCardNo , P.Amount, P.AuthentResult,AuthentStr = (case when P.AuthentResult = -2 then '<span style=\"color:#FF0000\">重新认证</span>' when P.AuthentResult = -1 then  '<span style=\"color:#FF0000\">认证失败</span>' when P.AuthentResult = 0 then  '认证中' else '<span style=\"color:#007EFF\">认证成功</span>' end), SignString=(case when P.sign=1 then '<span style=\"color:#436EEE\">已汇款</span>' else '未汇款' end), PayTypeString=(case when P.PayType=0 then '线下' when P.PayType=1 then '线上' end), PayStatusString=(case when P.PayStatus=-1 then '<span style=\"color:#FF0000;\">汇款失败</span>' when P.PayStatus=0 then '未汇款' when P.PayStatus=1 then '<span style=\"color:#436EEE\">汇款成功</span>' end), P.CreateTime, P.UpdateTime, P.VerTimes, P.REQ_SN, M.MemberName, P.Remark", filter, sortField, pagenum, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }

        //获得分页数据1
        public object GetPageList1(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var bankCardAuthentBLL = new BankCardAuthentBLL();
            var dt = bankCardAuthentBLL.GetPageList(" P.ID, B.BankName, MI.RealName as Account,BankCardNo=LEFT(P.BankCardNo,4)+'XXXX'+RIGHT(P.BankCardNo,4) , P.Amount, P.AuthentResult,AuthentStr = (case when P.AuthentResult = -2 then '<span style=\"color:#FF0000\">重新认证</span>' when P.AuthentResult = -1 then  '<span style=\"color:#FF0000\">认证失败</span>' when P.AuthentResult = 0 then  '认证中' else '<span style=\"color:#007EFF\">认证成功</span>' end), SignString=(case when P.sign=1 then '<span style=\"color:#436EEE\">已汇款</span>' else '未汇款' end), PayTypeString=(case when P.PayType=0 then '线下' when P.PayType=1 then '线上' end), PayStatusString=(case when P.PayStatus=-1 then '<span style=\"color:#FF0000;\">汇款失败</span>' when P.PayStatus=0 then '未汇款' when P.PayStatus=1 then '<span style=\"color:#436EEE\">汇款成功</span>' end), P.CreateTime, P.UpdateTime, P.VerTimes, P.REQ_SN, M.MemberName, P.Remark", filter, sortField, pagenum, pagesize, out total);
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