using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// MemberFondRecordHandler 的摘要说明
    /// </summary>
    public class MemberFondRecordHandler : IHttpHandler
    {
        private int _currentPage;
        private int _pageSize;
        private string _uName;
        private string _startDate = "";
        private string _endDate = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _currentPage = ConvertHelper.QueryString(context.Request, "currentpage", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _uName = ConvertHelper.QueryString(context.Request, "uName", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");
            var filter = " 1=1 ";
            if (!string.IsNullOrEmpty(_startDate))
            {
                if (_uName.ToUpper() == "RJB777")
                {
                    filter += " and P.UpdateTime>='" + _startDate + "'";
                }
                else
                {
                    filter += " and P.CreateTime>='" + _startDate + "'";
                }
                
            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                if (_uName.ToUpper() == "RJB777")
                {
                    filter += " and P.UpdateTime<=DATEADD(DAY,1,'" + _endDate + "')";
                }
                else
                {
                    filter += " and P.CreateTime<=DATEADD(DAY,1,'" + _endDate + "')";
                }
                
            }
            context.Response.Write(GetPageList(_uName,_currentPage, _pageSize, filter));
        }

        //获得分页数据
        public object GetPageList(string uName, int currentPage, int pageSize, string filter)
        {
            currentPage = currentPage + 1;
            int memberId = new MemberBll().GetMemberId(uName);
            int total;
            DataSet dataset;
            if (_uName.ToUpper() == "RJB777")
            {
                dataset = new ProcFundReportBll().GetList(memberId, filter, currentPage, pageSize, "P.UpdateTime asc, ID asc", out total);
            }
            else
            {
                dataset = new ProcFundReportBll().GetList(memberId, filter, currentPage, pageSize, "P.CreateTime asc, ID asc", out total);
            }
            
            var dt = dataset.Tables[0];

            dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "FeeTypeString" });
            foreach (DataRow dr in dt.Rows)
            {
                int feeType = dr["FeeType"] != null ? Convert.ToInt32(dr["FeeType"]) : -1;
                if (feeType == -1)
                {
                    dr["FeeTypeString"] = "";
                }
                else
                {
                    dr["FeeTypeString"] = FeeType.GetNameByType((FeeType.FeeTypeEnum)feeType);
                }
            }
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