using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// IdentityAuthentHandler 的摘要说明
    /// </summary>
    public class IdentityAuthentHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _selType = "";
        private string _uname = "";
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
            _flag = ConvertHelper.QueryString(context.Request, "flag", 0);

            //查询条件
            _selType = ConvertHelper.QueryString(context.Request, "selType", "");
            _uname = ConvertHelper.QueryString(context.Request, "uName", "");
            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_uname) && _uname != "undefined")
            {
                if (!string.IsNullOrEmpty(_selType) && _selType != "undefined")
                {
                    if (_selType == "0")
                    {
                        filter += " and M.MemberName like '%" + _uname + "%'";
                    }
                    else
                    {
                        filter += " and P.RealName like '%" + _uname + "%'";
                    }
                }
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(_flag == 1 ? GetPageList1(_currentPage, _pageSize, sortColumn, filter) : GetPageList(_currentPage, _pageSize, sortColumn, filter));

        }

        //获取分页数据
        public Object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int totalPage;
            int pageCount;
            var identityAuthentBll = new IdentityAuthentBll();
            var dt = identityAuthentBll.GetPageList(" P.ID,P.MemberID,P.RealName, P.IdentityCard ,AnthString=(CASE WHEN AuthentResult=1 THEN '<span style=\"color:#436EEE\">认证通过</span>' WHEN AuthentResult=-1 THEN '<span style=\"color:#FF0000;\">认证不通过</span>' ELSE '未认证' END),P.ApplyTime,P.UpdateTime,M.MemberName", " IdentityAuthent P LEFT JOIN dbo.Member M ON M.ID=P.MemberID", filter, sortField, pagenum, pagesize, out pageCount, out totalPage);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + pageCount + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }

        //获取分页数据1
        public Object GetPageList1(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int totalPage;
            int pageCount;
            var identityAuthentBll = new IdentityAuthentBll();
            var dt = identityAuthentBll.GetPageList(" P.ID,P.MemberID,P.RealName,IdentityCard=LEFT(P.IdentityCard,6)+'XXXX'+RIGHT(P.IdentityCard,4) ,AnthString=(CASE WHEN AuthentResult=1 THEN '<span style=\"color:#436EEE\">认证通过</span>' WHEN AuthentResult=-1 THEN '<span style=\"color:#FF0000;\">认证不通过</span>' ELSE '未认证' END),P.ApplyTime,P.UpdateTime,M.MemberName", " IdentityAuthent P LEFT JOIN dbo.Member M ON M.ID=P.MemberID", filter, sortField, pagenum, pagesize, out pageCount, out totalPage);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + pageCount + ",\"Rows\":" + jsonStr + "}";
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