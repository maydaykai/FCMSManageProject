using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.Information
{
    /// <summary>
    /// InformationHandler 的摘要说明
    /// </summary>
    public class InformationHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private int _columnId = 0;
        private string _status = "";
        private string _recommend = "";
        private string _startDate = "";
        private string _endDate = "";

        private string _Image_value = "0";

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
            _columnId = ConvertHelper.QueryString(context.Request, "columnId", 0);

            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _recommend = ConvertHelper.QueryString(context.Request, "recommend", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");

            _Image_value = ConvertHelper.QueryString(context.Request, "Image_value", "0");


            var filter = " 1=1";
            filter += " and SectionID=" + _columnId;

            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and Status=" + _status;
            }
            if (!string.IsNullOrEmpty(_recommend))
            {
                filter += " and Recommend=" + _recommend;
            }
            if (!string.IsNullOrEmpty(_startDate))
            {
                filter += " and PubTime>='" + _startDate + "'";
            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                filter += " and PubTime<='" + _endDate + "'";
            }
            if (Convert.ToInt32(_Image_value)>0)
            {
                filter += " and MediaTypeID=" + _Image_value;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var informationBll = new InformationBll();
            var dt = informationBll.GetPageList(" P.ID, P.SectionID, P.Title, P.Status, P.Recommend, P.PubTime,P.ShowDesc,P.Image_value,b.Title as BName", filter, sortField, pagenum, pagesize, out total);
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