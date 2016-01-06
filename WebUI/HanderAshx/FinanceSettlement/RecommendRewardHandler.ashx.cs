using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.FinanceSettlement
{
    /// <summary>
    /// RecommendRewardHandler 的摘要说明
    /// </summary>
    public class RecommendRewardHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _year = "";
        private string _status = "";
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

            _year = ConvertHelper.QueryString(context.Request, "year", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _flag = ConvertHelper.QueryString(context.Request, "flag", 0);
            switch (_sortField)
            {
                case "AuditStatusString":
                    _sortField = "AuditStatus"; break;
            }
            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_year))
            {
                if(_flag == 1)
                    filter += " and TotalYear = " + _year;
                else
                    filter += " and Year = " + _year;
            }
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and AuditStatus=" + _status;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(_flag == 1 ? GetPageList1(_currentPage, _pageSize, sortColumn, filter) : GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }

        private string GetPageList1(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int pageCount = 0;
            var dataset = new ReCommendRewardGrantBll().GetList1(filter, sortField, pagenum, pagesize, ref pageCount);
            var dt = dataset.Tables[0];

            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + pageCount + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }

        public string GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int pageCount = 0;
            var dataset = new ReCommendRewardGrantBll().GetList(filter, sortField, pagenum, pagesize, ref pageCount);
            var dt = dataset.Tables[0];

            dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "AuditStatusString" });
            foreach (DataRow dr in dt.Rows)
            {
                int status = dr["AuditStatus"] != null ? Convert.ToInt32(dr["AuditStatus"]) : 0;
                switch (status)
                {
                    case 0: dr["AuditStatusString"] = "初审中"; break;
                    case 1: dr["AuditStatusString"] = "复审中"; break;
                    case 2: dr["AuditStatusString"] = "初审不通过"; break;
                    case 3: dr["AuditStatusString"] = "复审不通过"; break;
                    case 4: dr["AuditStatusString"] = "复审通过"; break;
                    default: dr["AuditStatusString"] = ""; break;
                }
            }

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