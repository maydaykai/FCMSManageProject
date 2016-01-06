using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// MemberReferrHandler 的摘要说明
    /// </summary>
    public class MemberReferrHandler : IHttpHandler
    {

        private int _memberId;

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "P.ID";
        private string _selRecMember = "";
        private string _txtRecMember = "";
   //   private string _txtRecoMember = "";
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
            _selRecMember = ConvertHelper.QueryString(context.Request, "selRecMember", "");
            _txtRecMember = ConvertHelper.QueryString(context.Request, "txtRecMember", "");

            _txtRecMember = HttpContext.Current.Server.UrlDecode(_txtRecMember);
            var filter = " P.ID not in (select RecedMemberID from dbo.MemberRecommended) ";

            if (!string.IsNullOrEmpty(_selRecMember) && !string.IsNullOrEmpty(_txtRecMember))
            {
                if (_selRecMember == "0")
                {
                    filter += " AND M.RealName LIKE '%" + _txtRecMember + "%'";
                }
                else if (_selRecMember == "1")
                {
                    filter += " AND P.MemberName LIKE '%" + _txtRecMember + "%'";
                }
                else if (_selRecMember == "2")
                {
                    filter += "AND P.Mobile LIKE '" + _txtRecMember + "%'";
                }
            }
            var sortColumn = _sortField + " " + _sort;

            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }

        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var memberBll = new MemberBll();
            var dt = MemberReferrBll.GetPageList("P.ID,P.MemberName,P.Mobile,M.RealName ", filter, sortField, pagenum, pagesize, out total);
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