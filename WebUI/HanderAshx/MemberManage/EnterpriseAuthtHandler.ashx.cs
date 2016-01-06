using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// 企业信息认证
    /// </summary>
    public class EnterpriseAuthtHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _selType = "";
        private string _uname = "";
        private string _AnthStatus = "";
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

            //查询条件
            _selType = ConvertHelper.QueryString(context.Request, "selType", "");
            _uname = ConvertHelper.QueryString(context.Request, "uName", "");
            _AnthStatus = ConvertHelper.QueryString(context.Request, "anthStatus", "");
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
                        filter += " and P.EnterpriseName like '%" + _uname + "%'";
                    }
                }
            }
            if (!string.IsNullOrEmpty(_AnthStatus) && _AnthStatus != "undefined")
            {
                filter += " and P.AuthentResult=" + _AnthStatus;
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));

        }

        //获取分页数据
        public Object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var enterpriseAuthentBll = new EnterpriseAuthentBll();
            const string filed = @"P.ID,P.MemberID,P.EnterpriseName,P.RegistrationNo,P.OrganizationCode,P.LegalName,
                                   P.AuthentNumber,P.AuthentResult,P.ApplyTime,P.UpdateTime,M.MemberName";
            var dt = enterpriseAuthentBll.GetPageList(filed, filter, sortField, pagenum, pagesize, out total);
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