using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.Information
{
    /// <summary>
    /// FocusFigureHandler 的摘要说明
    /// </summary>
    public class AppFileHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _os = "";
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
            _os = ConvertHelper.QueryString(context.Request, "os", "");

            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_os))
            {
                filter += " and os=" + _os;
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter, context));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter, HttpContext context)
        {
            pagenum = pagenum + 1;
            int total;
            var  appVersionBll = new AppVersionBll();
            var dt = appVersionBll.GetPageList(" *", filter, sortField, pagenum, pagesize, out total);

            //string imgFullDir = ConfigHelper.ImgVirtualPath;

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr["LargePicture"] != null)
            //    {
            //        string temp = dr["LargePicture"].ToString();
            //        dr["LargePicture"] = imgFullDir + temp;
            //    }
            //    if (dr["SmallPicture"] != null)
            //    {
            //        string temp = dr["SmallPicture"].ToString();
            //        dr["SmallPicture"] = imgFullDir + temp;
            //    }
            //}
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }

        private string EndUrlDir(string url)
        {
            if (!url.EndsWith("/"))
            {
                url += "/";
            }
            return url;
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