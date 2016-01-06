using ManageFcmsBll;
using ManageFcmsCommon;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// FundRecordHandler 的摘要说明
    /// </summary>
    public class FundRecordHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "P.ID";

        private string _payeeType = "0";
        private string _payeeMemberName = "";
        private string _partyType = "0";
        private string _partyMemberName = "";
        private string _bondingCo = "";
        private string _status = "";
        private string _feeType = "";
        private string _startDate = "";
        private string _endDate = "";
        private string _output = "";
        private string _andor = "0";

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

            switch (_sortField)
            {
                case "FeeTypeString": _sortField = "P.FeeType"; break;
                case "PayeeMemberName": _sortField = "M1.MemberName"; break;
                case "PayeeRealName": _sortField = "MI1.RealName"; break;
                case "PartyMemberName": _sortField = "M2.MemberName"; break;
                case "PartyRealName": _sortField = "MI2.RealName"; break;
                case "CreateTime": _sortField = "P.CreateTime"; break;
                case "UpdateTime": _sortField = "P.UpdateTime"; break;
            }

            _payeeType = ConvertHelper.QueryString(context.Request, "payeeType", "0");
            _payeeMemberName = ConvertHelper.QueryString(context.Request, "payeeMemberName", "");
            _partyType = ConvertHelper.QueryString(context.Request, "partyType", "0");
            _partyMemberName = ConvertHelper.QueryString(context.Request, "partyMemberName", "");
            _bondingCo = ConvertHelper.QueryString(context.Request, "bondingCo", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _feeType = ConvertHelper.QueryString(context.Request, "feeType", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");
            _andor = ConvertHelper.QueryString(context.Request, "andor", "0");

            _output = ConvertHelper.QueryString(context.Request, "output", "");

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_payeeMemberName))
            {
                filter += " and ";
                if (_andor == "1" && !string.IsNullOrEmpty(_partyMemberName))
                {
                    filter += " ( ";
                }
                if (_payeeType == "0")
                {
                    filter += " M1.MemberName like '%" + _payeeMemberName + "%'";
                }
                else
                {
                    filter += " MI1.RealName like '%" + _payeeMemberName + "%'";
                }
            }
            if (!string.IsNullOrEmpty(_partyMemberName))
            {
                if (_andor == "0")
                {
                    filter += " and ";
                }
                else
                {
                    filter += " or ";
                }

                if (_partyType == "0")
                {
                    filter += " M2.MemberName like '%" + _partyMemberName + "%'";
                }
                else
                {
                    filter += " MI2.RealName like '%" + _partyMemberName + "%'";
                }
                if (_andor == "1" && !string.IsNullOrEmpty(_payeeMemberName))
                {
                    filter += " ) ";
                }
            }

            if (!string.IsNullOrEmpty(_bondingCo))
            {
                filter += " and L.GuaranteeID=" + _bondingCo;
            }
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and P.Status=" + _status;
            }
            if (!string.IsNullOrEmpty(_feeType) && !_feeType.Trim().Equals("-1") && !_feeType.Trim().Equals("-1,"))
            {
                filter += " and P.FeeType in (" + _feeType.Trim().Remove(_feeType.Length - 1, 1) + ")";
            }
            if (!string.IsNullOrEmpty(_startDate))
            {
                filter += " and P.UpdateTime>='" + _startDate + "'";
            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                filter += " and P.UpdateTime<='" + _endDate + "'";
            }

            var sortColumn = _sortField + " " + _sort;

            //if (!string.IsNullOrEmpty(_output))
            //{
            //    //context.Response.ContentType = "application/x-xls";
            //    //context.Response.AddHeader("Content-Disposition", "attachment;filename=FundRecord.xls");
            //    //HSSFWorkbook hssfWorkbook = OutputSearchResult(sortColumn, filter);
            //    //hssfWorkbook.Write(context.Response.OutputStream);
            //}
            //else
            //{
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
            //}          
        }

        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var fundRecordBll = new FundRecordBll();
            var dt = fundRecordBll.GetPageList(" P.FeeType, P.Amount, P.PayeeBalance, P.PartyBalance, P.Status, P.Description, P.CreateTime, P.UpdateTime, P.ID, M1.MemberName AS PayeeMemberName, M2.MemberName AS PartyMemberName, MI1.RealName as PayeeRealName, MI2.RealName as PartyRealName, L.LoanNumber", filter, sortField, pagenum, pagesize, out total);
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
            var aggregate = fundRecordBll.Aggregate(filter);
            if (aggregate == null)
            {
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":0,\"Rows\":" + jsonStr + "}";
            }
            else
            {
                aggregate = ConvertHelper.ToDecimal(aggregate.ToString());
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":" + aggregate + ",\"Rows\":" + jsonStr + "}";
            }
            return jsonStr;
        }

        //public HSSFWorkbook OutputSearchResult(string sortField, string filter)
        //{
        //    int total;
        //    var fundRecordBll = new FundRecordBll();
        //    var dt = fundRecordBll.GetFundRecordList(" P.FeeType, P.Amount, P.PayeeBalance, P.PartyBalance, P.Status, P.Description, P.CreateTime, P.UpdateTime, P.ID, M1.MemberName AS PayeeMemberName, M2.MemberName AS PartyMemberName, MI1.RealName as PayeeRealName, MI2.RealName as PartyRealName, L.LoanNumber", filter, sortField, out total);
        //    dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "FeeTypeString" });
        //    dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "StatusString" });
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        int feeType = dr["FeeType"] != null ? Convert.ToInt32(dr["FeeType"]) : -1;
        //        int status = dr["Status"] != null ? Convert.ToInt32(dr["Status"]) : 0;
        //        if (feeType == -1)
        //        {
        //            dr["FeeTypeString"] = "";
        //        }
        //        else
        //        {
        //            dr["FeeTypeString"] = FeeType.GetNameByType((FeeType.FeeTypeEnum)feeType);
        //        }
        //        switch (status)
        //        {
        //            case 1: dr["StatusString"] = "正常"; break;
        //            case 2: dr["StatusString"] = "冻结"; break;
        //            case 3: dr["StatusString"] = "作废"; break;
        //            default: dr["StatusString"] = ""; break;
        //        }
        //    }

        //    HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        //    ISheet sheet = hssfWorkbook.CreateSheet("资金流水明细");
        //    IRow rowHeader = sheet.CreateRow(0);
        //    rowHeader.CreateCell(0, CellType.STRING).SetCellValue("收款方会员名");
        //    rowHeader.CreateCell(1, CellType.STRING).SetCellValue("收款方姓名");
        //    rowHeader.CreateCell(2, CellType.STRING).SetCellValue("出款方会员名");
        //    rowHeader.CreateCell(3, CellType.STRING).SetCellValue("出款方姓名");
        //    rowHeader.CreateCell(4, CellType.STRING).SetCellValue("借款标编号");
        //    rowHeader.CreateCell(5, CellType.STRING).SetCellValue("费用类型");
        //    rowHeader.CreateCell(6, CellType.STRING).SetCellValue("金额(元)");
        //    rowHeader.CreateCell(7, CellType.STRING).SetCellValue("收款方账户可用余额(元)");
        //    rowHeader.CreateCell(8, CellType.STRING).SetCellValue("出款方账户可用余额(元)");
        //    rowHeader.CreateCell(9, CellType.STRING).SetCellValue("状态");
        //    rowHeader.CreateCell(10, CellType.STRING).SetCellValue("资金变动描述说明");
        //    rowHeader.CreateCell(11, CellType.STRING).SetCellValue("记录时间");
        //    for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
        //    {
        //        IRow dataRow = sheet.CreateRow(rowIndex + 1);
        //        dataRow.CreateCell(0, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PayeeMemberName"]));
        //        dataRow.CreateCell(1, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PayeeRealName"]));
        //        dataRow.CreateCell(2, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PartyMemberName"]));
        //        dataRow.CreateCell(3, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PartyRealName"]));
        //        dataRow.CreateCell(4, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["LoanNumber"]));
        //        dataRow.CreateCell(5, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["FeeTypeString"]));
        //        dataRow.CreateCell(6, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Amount"]));
        //        dataRow.CreateCell(7, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PayeeBalance"]));
        //        dataRow.CreateCell(8, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PartyBalance"]));
        //        dataRow.CreateCell(9, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["StatusString"]));
        //        dataRow.CreateCell(10, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Description"]));
        //        dataRow.CreateCell(11, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["UpdateTime"]));
        //    }
        //    return hssfWorkbook;
        //}

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