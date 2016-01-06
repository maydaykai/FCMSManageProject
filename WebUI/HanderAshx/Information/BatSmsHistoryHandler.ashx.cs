using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.Information
{
    /// <summary>
    /// BatSmsHandler 的摘要说明
    /// </summary>
    public class BatSmsHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "CreateTime";

        private string _totalDate;
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
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "desc");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "id");
            _totalDate = ConvertHelper.QueryString(context.Request, "totalDate", "");

            if (_sortField == "ID")
            {
                _sortField = "b.ID";
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetBatSmsHistoryList(_currentPage, _pageSize, sortColumn, _totalDate));
        }


        public string GetBatSmsHistoryList(int pagenum, int pagesize, string sortField, string totalDate)
        {
            int total = 0;
            var dataset = new BatSmsHistoryBll().GetList(pagenum, pagesize, sortField, totalDate, ref total);
            var jsonStr = JsonHelper.DataTableToJson(dataset.Tables[0]);
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