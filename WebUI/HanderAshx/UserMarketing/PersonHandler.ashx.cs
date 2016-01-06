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
    /// PersonHandler 的摘要说明
    /// </summary>
    public class PersonHandler : IHttpHandler
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
             _uName = ConvertHelper.QueryString(context.Request, "uname", "");
            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_uName) && _uName != "undefined")
            {
                filter += " and Name like '%" + _uName + "%'";
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetFcmsUserList(_currentPage, _pageSize, sortColumn, filter));
        }

        //获取数据
        public Object GetFcmsUserList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;

            int pageCount = 0;
            var fcmsUserbll = new Marketing_Ex_PersonBLL();
            // DataSet dsRoles = new RoleBll().GetRoleList();
            IList<Ex_PersonModel> fcmsUserModelList = fcmsUserbll.GetCompetencePageList(filter, sortField, pagenum, pagesize, ref pageCount);
            var orders = (from fcmsUserModel in fcmsUserModelList
                          select new
                          {
                              ID = fcmsUserModel.Id,
                              Name = fcmsUserModel.Name,
                              Sex = fcmsUserModel.Sex,
                              Mobile = fcmsUserModel.Mobile,
                              Age = fcmsUserModel.Age,
                              CreateTime = fcmsUserModel.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                              ExpandToXML = fcmsUserModel.ExpandToXML,
                              MemberId = fcmsUserModel.MemberId,
                              IsDel=fcmsUserModel.IsDel,
                              RoleName=fcmsUserModel.RoleName
                              
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