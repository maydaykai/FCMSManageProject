using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.Information
{
    /// <summary>
    /// FocusFigureHandler 的摘要说明
    /// </summary>
    public class FocusFigureHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _status = "";
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
            _status = ConvertHelper.QueryString(context.Request, "status", "");

            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and Status=" + _status;
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter, context));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter, HttpContext context)
        {
            pagenum = pagenum + 1;
            int total;
            var focusFigureBll = new FocusFigureBll();
            var dt = focusFigureBll.GetPageList(" *", filter, sortField, pagenum, pagesize, out total);

            //XmlHelper xmlHelper = new XmlHelper(context.Server.MapPath("~/Config/upload.xml"));
            //string remoteDomain = xmlHelper.GetText("upload/remoteDomain");
            //string secLevelDomain = xmlHelper.GetText("upload/secLevelDomain");
            //string imgFullDir = EndUrlDir(remoteDomain);
            //imgFullDir += EndUrlDir(secLevelDomain);
            //imgFullDir += "image/";

            string imgFullDir = ConfigHelper.ImgVirtualPath;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["LargePicture"] != null)
                {
                    string temp = dr["LargePicture"].ToString();
                    dr["LargePicture"] = imgFullDir + temp;
                }
                if (dr["SmallPicture"] != null)
                {
                    string temp = dr["SmallPicture"].ToString();
                    dr["SmallPicture"] = imgFullDir + temp;
                }
            }
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