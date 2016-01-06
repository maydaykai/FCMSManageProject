using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// CashbackHandler 的摘要说明
    /// </summary>
    public class CashbackHandler : IHttpHandler
    {

        private string _dateStart;
        private string _dateEnd;
        private string _txtName;
        private int _pageIndex = 0;
        private int _pageSize = 0;
        private int _total = 0;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _pageIndex = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);

            _dateStart = ConvertHelper.QueryString(context.Request, "dateStart", "");
            _dateEnd = ConvertHelper.QueryString(context.Request, "dateEnd", "");
            _txtName = ConvertHelper.QueryString(context.Request, "txtName", "");
            string filter = "where 1=1";
            if (!string.IsNullOrEmpty(_dateStart) && !string.IsNullOrEmpty(_dateEnd))
            {
                filter = filter + " and CONVERT(VARCHAR(10),a.CreateTime,120)>='" + _dateStart + "'  AND CONVERT(VARCHAR(10),a.CreateTime,120)<='" + _dateEnd + "' ";
            
            }
            if (!string.IsNullOrEmpty(_txtName))
            {
                filter = filter + "  AND MemberName LIKE '%" + _txtName + "%'";
            }

             //data.currentpage = data.pagenum || 1;
             //       data.pagesize = data.pagesize || 20;
             //       data.dateStart = $("#txtDateStart").val() || "";
             //       data.dateEnd = $("#txtDateEnd").val() || "";
             //       data.txtName = $("#txtName").val() || "

            context.Response.Write(GetTotalCashList(filter, _pageIndex, _pageSize, out _total));
        }

        public string GetTotalCashList(string filter, int pageIndex, int pageSize, out int total)
        {
            var dataset = new ReportBll().GetCashBackList(filter, pageIndex, pageSize, out total);

            var jsonStr = JsonHelper.DataTableToJson(dataset);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
            //return JsonHelper.DataTableToJson(dataset);
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