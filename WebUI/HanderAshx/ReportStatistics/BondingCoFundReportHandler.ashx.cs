using ManageFcmsBll;
using ManageFcmsCommon;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// BondingCoFundReportHandler 的摘要说明
    /// </summary>
    public class BondingCoFundReportHandler : IHttpHandler, IRequiresSessionState
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "P.ID";

        private string _status = "";
        private string _feeType = "";
        private string _startDate = "";
        private string _endDate = "";
        private string _output = "";
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
            _output = ConvertHelper.QueryString(context.Request, "output", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _feeType = ConvertHelper.QueryString(context.Request, "feeType", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");

            var loginUserId = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
            var loginMemberId = new FcmsUserBll().GetModel(loginUserId).RelationID;
            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and P.Status=" + _status;
            }
            if (!string.IsNullOrEmpty(_feeType))
            {
                filter += " and P.FeeType in (" + _feeType + ")";
            }
            if (!string.IsNullOrEmpty(_startDate))
            {
                filter += " and P.UpdateTime>='" + _startDate + "'";
            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                filter += " and P.UpdateTime<=DATEADD(DAY,1,'" + _endDate + "')";
            }

            var sortColumn = _sortField + " " + _sort;
            if (!string.IsNullOrEmpty(_output))
            {
                context.Response.ContentType = "application/x-xls";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=BondingCoFundReport.xls");
                HSSFWorkbook hssfWorkbook = OutputSearchResult(sortColumn, filter);
                hssfWorkbook.Write(context.Response.OutputStream);
            }
            else
            {
                context.Response.Write(GetPageList(loginMemberId, _currentPage, _pageSize, sortColumn, filter));
            }          
        }

        public object GetPageList(int memberId, int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var dt = new FundRecordBll().GetFundFlow(memberId, filter, pagenum, pagesize, sortField, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }
        public HSSFWorkbook OutputSearchResult(string sortField, string filter)
        {
            int total;
            var fundRecordBll = new FundRecordBll();
            var dt = fundRecordBll.GetFundFlow(-1, filter, 1, int.MaxValue - 1, sortField, out total);
            dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "FeeTypeString" });
            dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "StatusString" });
            foreach (DataRow dr in dt.Rows)
            {
                int feeType = dr["FeeType"] != null ? Convert.ToInt32(dr["FeeType"]) : -1;
                int status = dr["Status"] != null ? Convert.ToInt32(dr["Status"]) : 0;
                if (feeType == -1)
                {
                    dr["FeeTypeString"] = "";
                }
                else
                {
                    dr["FeeTypeString"] = FeeType.GetNameByType((FeeType.FeeTypeEnum)feeType);
                }
                switch (status)
                {
                    case 1: dr["StatusString"] = "正常"; break;
                    case 2: dr["StatusString"] = "冻结"; break;
                    case 3: dr["StatusString"] = "作废"; break;
                    default: dr["StatusString"] = ""; break;
                }
            }
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("担保公司资金流水  ");
            IRow rowHeader = sheet.CreateRow(0);
            rowHeader.CreateCell(0, CellType.STRING).SetCellValue("费用类型");
            rowHeader.CreateCell(1, CellType.STRING).SetCellValue("金额(元)");
            rowHeader.CreateCell(2, CellType.STRING).SetCellValue("收款方账户可用余额(元)");
            rowHeader.CreateCell(3, CellType.STRING).SetCellValue("状态");
            rowHeader.CreateCell(4, CellType.STRING).SetCellValue("资金变动描述说明");
            rowHeader.CreateCell(5, CellType.STRING).SetCellValue("记录时间");
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                IRow dataRow = sheet.CreateRow(rowIndex + 1);
                dataRow.CreateCell(0, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["FeeTypeString"]));
                dataRow.CreateCell(1, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Amount1"]));
                dataRow.CreateCell(2, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["MemberBalance"]));
                dataRow.CreateCell(3, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["StatusString"]));
                dataRow.CreateCell(4, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Description"]));
                dataRow.CreateCell(5, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["UpdateTime"]));
            }
            return hssfWorkbook;
        }
        private string ConvertToString(object obj)
        {
            return obj == null ? "" : obj.ToString();
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