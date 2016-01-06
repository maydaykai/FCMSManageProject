using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanStudentHandler 的摘要说明
    /// </summary>
    public class LoanStudentHandler : IHttpHandler
    {


        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "C.ID";
        private string _selRecMember = "";
        private string _txtRecMember = "";
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
            _selRecMember = ConvertHelper.QueryString(context.Request, "selRecMember", "");
            _txtRecMember = ConvertHelper.QueryString(context.Request, "txtRecMember", "");
            


            _txtRecMember = HttpContext.Current.Server.UrlDecode(_txtRecMember);
            var filter = "";

            var sortColumn = _sortField + " " + _sort;

            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));

        }



        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            //var bankCardBll = new CreditLineBLL();
            var dt = CreditLineBLL.GetPageList("C.ID, C.CreditLine , C.CardNumber,C.CreditNumber,C.IdentityCard,C.CreateTime,C.UpdateTime,C.OpUid", filter, sortField, pagenum, pagesize, out total);
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