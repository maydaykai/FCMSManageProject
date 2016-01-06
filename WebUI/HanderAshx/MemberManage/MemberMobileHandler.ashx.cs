using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// MemberMobileHandler 的摘要说明
    /// </summary>
    public class MemberMobileHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _memberName = "";
        private string _sn = "";
        private string _checkStatus = "";
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
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "P.ID");

            _checkStatus = ConvertHelper.QueryString(context.Request, "checkStatus", "");
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");

            switch (_sortField)
            {
                case "AuditStatusStr": _sortField = "P.AuditStatus"; break;
            }

            var filter = " P.Type=1";

            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and M.MemberName like '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_checkStatus))
            {
                filter += " and P.AuditStatus=" + _checkStatus;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }
        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var mobileBll = new MobileUpdateRecordBll();
            string fields = "P.ID,P.RealName,P.OldMobile,P.NewMobile,P.Remark,P.AuditStatus,case P.AuditStatus when 1 then '未审核' when 2 then '审核通过' else '审核不通过' end as AuditStatusStr, P.CreateTime,M.MemberName";
            var dt = mobileBll.GetPageList(fields, filter, sortField, pagenum, pagesize, out total);
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