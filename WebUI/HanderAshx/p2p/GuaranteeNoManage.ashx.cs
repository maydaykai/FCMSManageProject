using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// GuaranteeNoManage 的摘要说明
    /// </summary>
    public class GuaranteeNoManage : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort;
        private string _sortField;

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
            var filter = " 1=1";
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageGuaranteeNoManage(_currentPage, _pageSize, sortColumn, filter));
        }

        //获取数据
        public Object GetPageGuaranteeNoManage(int pagenum, int pagesize, string sortColumn, string filter)
        {
            var total = 0;
            pagenum = pagenum + 1;
            IList<GuaranteeNoModel> list = new GuaranteeNoBll().GetPageGuaranteeNoModel(filter, sortColumn, pagenum, pagesize, ref total);
            var orders = (from model in list
                          select new
                          {
                              model.ID,
                              model.GuaranteeId,
                              model.GuaranteeName,
                              model.GuaranteeNo,
                              model.UpdateTime
                          });
            var jsonData = new
            {
                TotalRows = total,//记录数
                Rows = orders//实体列表
            };
            return JsonHelper.ObjectToJson(jsonData);
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