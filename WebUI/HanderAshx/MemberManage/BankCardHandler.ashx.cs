using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// BankCardHandler 的摘要说明
    /// </summary>
    public class BankCardHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _memberName = "";
        private string _status = "";
        private int _type;

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
            _type = ConvertHelper.QueryString(context.Request, "type", 0);

            if (_sortField == "StatusString")
            {
                _sortField = "P.Status";
            }
            if (_sortField == "UpdateTime")
            {
                _sortField = "P.UpdateTime";
            }
            if (_sortField == "BankCardTypeString")
            {
                _sortField = "P.BankCardType";
            }
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and M.MemberName like '%" + _memberName + "%'";
            }

            if (!string.IsNullOrEmpty(_status))
            {
                filter += _type == 1
                              ? (("0").Equals(_status) ? " and P.Status=3" : " and P.Status<3")
                              : " and P.Status=" + _status;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var bankAccountBll = new BankAccountBll();
            var dt = _type == 0 ? bankAccountBll.GetPageList(" P.ID, B.BankName, P.CreateTime, P.BankCardNo, BankCardTypeString=(case when P.BankCardType=1 then '借记卡' when P.BankCardType=2 then '信用卡' end), P.MemberID, StatusString=(case when P.Status=-1 then '<span style=\"color:#FF0000;\">禁用</span>' when P.Status=1 then '<span style=\"color:#436EEE\">启用</span>' end),P.UpdateTime, M.MemberName, MI.RealName", filter, sortField, pagenum, pagesize, out total) :
                bankAccountBll.GetAuthentPageList(" P.ID, B.BankName, P.CreateTime, P.BankCardNo, P.MemberID, StatusString=(case when P.Status=3 then '<span style=\"color:#FF0000;\">禁用</span>' else '<span style=\"color:#436EEE\">启用</span>' end),P.UpdateTime, M.MemberName, MI.RealName", filter, sortField, pagenum, pagesize, out total); ;
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