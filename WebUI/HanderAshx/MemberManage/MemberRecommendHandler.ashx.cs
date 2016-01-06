using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// MemberRecommendHandler 的摘要说明
    /// </summary>
    public class MemberRecommendHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "DESC";
        private string _sortField = "P.ID";

        private string _selRecMember = "";
        private string _txtRecMember = "";
        private string _selRecoMember = "";
        private string _txtRecoMember = "";

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
            _selRecoMember = ConvertHelper.QueryString(context.Request, "selRecoMember", "");
            _txtRecoMember = ConvertHelper.QueryString(context.Request, "txtRecoMember", "");

            _txtRecMember = HttpContext.Current.Server.UrlDecode(_txtRecMember);
            _txtRecoMember = HttpContext.Current.Server.UrlDecode(_txtRecoMember);
            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_selRecMember) && !string.IsNullOrEmpty(_txtRecMember))
            {
                if (_selRecMember == "0")
                {
                    filter += " AND M1.MemberName='" + _txtRecMember + "'";
                }
                else if (_selRecMember == "1")
                {
                    filter += " AND MI1.RealName='" + _txtRecMember + "'";
                }
            }
            if (!string.IsNullOrEmpty(_selRecoMember) && !string.IsNullOrEmpty(_txtRecoMember))
            {
                if (_selRecoMember == "0")
                {
                    filter += " AND M2.MemberName='" + _txtRecoMember + "'";
                }
                else if (_selRecoMember == "1")
                {
                    filter += " AND MI2.RealName='" + _txtRecoMember + "'";
                }
            }

            var sortColumn = _sortField + " " + _sort;

            context.Response.Write(GetPageList(_pageSize, _currentPage, filter, sortColumn));


        }

        public object GetPageList(int pageSize, int currentPage, string filter, string sortField)
        {
            currentPage = currentPage + 1;
            int total;
            var dt = new MemberBll().GetMemberRecommend(pageSize, currentPage, filter, sortField, out total);
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