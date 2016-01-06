using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.HanderAshx.Basic
{
    /// <summary>
    /// CostSettingHander 的摘要说明
    /// </summary>
    public class CostSettingHander : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _feeType = "";
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
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "asc");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "FeeType");
            if (_sortField == "FeeTypeTitle")
            {
                _sortField = @"FeeType";
            }
            _feeType = ConvertHelper.QueryString(context.Request, "feeType", "");
            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_feeType) && _feeType != "undefined")
            {
                filter += " and FeeType like '%" + _feeType + "%'";
            }
            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetCostSetList(_currentPage, _pageSize, sortColumn, filter));

        }

        //获取数据
        public Object GetCostSetList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;

            int pageCount = 0;
            var costSettingBll = new CostSettingBll();
            IList<CostSettingModel> costSetModelList = costSettingBll.GetCostSetList(filter, sortField, pagenum, pagesize, ref pageCount);            
            var orders = (from costSetModel in costSetModelList
                          select new
                          {
                              costSetModel.Id,
                              costSetModel.CostVersionId,
                              costSetModel.FeeType,
                              costSetModel.CalculationMode,
                              costSetModel.CalInitialValue,
                              costSetModel.CalInitialProportion,
                              costSetModel.IncreasingMode,
                              costSetModel.IncreasUnit,
                              costSetModel.IncreasProportion,
                              costSetModel.EnableStatus,
                              costSetModel.CreateTime,
                              costSetModel.UpdateTime,
                              costSetModel.FeeTypeTitle
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