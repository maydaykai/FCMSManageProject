using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.UserMarketing
{
    /// <summary>
    /// FunctionOperationHandler 的摘要说明（查询）
    /// </summary>
    public class FunctionOperationHandler : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _uName = "";
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
            // _uName = ConvertHelper.QueryString(context.Request, "uname", "");
            var filter = " 1=1";
            //if (!string.IsNullOrEmpty(_uName) && _uName != "undefined")
            //{
            //    filter += " and UserName like '%" + _uName + "%'";
            //}
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetFcmsUserList(_currentPage, _pageSize, sortColumn, filter));
        }

        //获取数据
        public Object GetFcmsUserList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;

            int pageCount = 0;
            var fcmsUserbll = new Marketing_RoleBLL();
            // DataSet dsRoles = new RoleBll().GetRoleList();
            IList<Marketing_FunctionOperationModel> fcmsUserModelList = fcmsUserbll.GetFunctionOperationPageList(filter, sortField, pagenum, pagesize, ref pageCount);
            var orders = (from fcmsUserModel in fcmsUserModelList
                          select new
                          {
                              ID = fcmsUserModel.Id,
                              OperationName = fcmsUserModel.OperationName,
                              OperationCode = fcmsUserModel.OperationCode,
                              Remake = fcmsUserModel.Remake
                            });
            var jsonData = new
            {
                TotalRows = pageCount,//记录数
                Rows = orders//实体列表
            };
            var s = JsonHelper.ObjectToJson(jsonData);
            return s;
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