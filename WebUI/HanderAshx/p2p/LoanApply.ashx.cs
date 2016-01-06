using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanApply 的摘要说明
    /// </summary>
    public class LoanApply : IHttpHandler
    {
        private int _currentPage = 0;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "CreateTime";
        private string _startCreateTime = "";
        private string _endCreateTime = "";
        private string _name = "";
        private int _status;
        private string _loanuse = "";
        private string _province = "";
        private string _city = "";
        private string _output = "";
        private int _id;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _output = ConvertHelper.QueryString(context.Request, "output", "");
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 0);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 20);
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "desc");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "id");
            _id = ConvertHelper.QueryString(context.Request, "Id", 0);

            if (_sortField == "LoanUseName")
            {
                _sortField = "LoanUseID";
            }

            _startCreateTime = ConvertHelper.QueryString(context.Request, "startCreateTime", "");
            _endCreateTime = ConvertHelper.QueryString(context.Request, "endCreateTime", "");
            _name = ConvertHelper.QueryString(context.Request, "name", "");
            _status = ConvertHelper.QueryString(context.Request, "status", 0);
            _loanuse = ConvertHelper.QueryString(context.Request, "loanuse", "");
            _city = ConvertHelper.QueryString(context.Request, "city", "");
            _province = ConvertHelper.QueryString(context.Request, "province", "");
            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_name))
            {
                filter += " and dbo.Member.MemberName like '%" + _name + "%'";
            }
            if (_status >= 0)
            {
                filter += " and dbo.LoanApply.ExamStatus = " + _status ;
            }
            if (!string.IsNullOrEmpty(_startCreateTime))
            {
                filter += " and dbo.LoanApply.CreateTime>='" + _startCreateTime + "'";
            }
            if (!string.IsNullOrEmpty(_endCreateTime))
            {
                filter += " and dbo.LoanApply.CreateTime<=DATEADD(DAY,1,'" + _endCreateTime + "')";
            }
            if (!string.IsNullOrEmpty(_loanuse))
            {
                filter += "and dbo.LoanApply.LoanUseID = " + _loanuse;
            }
            if (!string.IsNullOrEmpty(_province))
            {
                filter += " and AreaProvince.Name = " + _province;
            }
            if (!string.IsNullOrEmpty(_city))
            {
                filter += " and dbo.LoanApply.CityId = " + _city;
            }
            if (_id > 0)
            {
                filter += " and dbo.LoanApply.ID = " + _id;
            }
            var sortColumn = _sortField + " " + _sort;
            if (!string.IsNullOrEmpty(_output))
            {
                context.Response.ContentType = "application/x-xls";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=LoanRapid.xls");
                HSSFWorkbook hssfWorkbook = OutputSearchResult(sortColumn, filter);
                hssfWorkbook.Write(context.Response.OutputStream);

            }
            else
            {
                context.Response.Write(GetPageLoanRapidManage(_currentPage, _pageSize, sortColumn, filter));
            }
        }

        public object GetPageLoanRapidManage(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;

            int pageCount = 0;
            var loanApplyBll = new LoanApplyBll();
            IList<LoanApplyModel> list = loanApplyBll.GetPagedLoanApplyList(filter, sortField, pagenum, pagesize, ref pageCount);
            var jsonData = new
            {
                TotalRows = pageCount,//记录数
                Rows = list//实体列表
            };
            var s = JsonHelper.ObjectToJson(jsonData);
            return s;
        }

        public HSSFWorkbook OutputSearchResult(string sortField, string filter)
        {
            int pageCount = 0;
            var loanReqidBLL = new LoanRapidBLL();

            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("快速融资");
            IRow rowHeader = sheet.CreateRow(0);
            rowHeader.CreateCell(0, CellType.STRING).SetCellValue("姓名");
            rowHeader.CreateCell(1, CellType.STRING).SetCellValue("手机号码");
            rowHeader.CreateCell(2, CellType.STRING).SetCellValue("贷款额度");
            rowHeader.CreateCell(3, CellType.STRING).SetCellValue("借款用途");
            rowHeader.CreateCell(4, CellType.STRING).SetCellValue("借款期限");
            rowHeader.CreateCell(5, CellType.STRING).SetCellValue("贷款方式");
            rowHeader.CreateCell(6, CellType.STRING).SetCellValue("贷款状态");
            rowHeader.CreateCell(7, CellType.STRING).SetCellValue("所在省份");
            rowHeader.CreateCell(8, CellType.STRING).SetCellValue("所在城市");
            rowHeader.CreateCell(9, CellType.STRING).SetCellValue("申请时间");
            rowHeader.CreateCell(10, CellType.STRING).SetCellValue("描述");
            rowHeader.CreateCell(11, CellType.STRING).SetCellValue("会员名字");

            IList<LoanRapidModel> list = loanReqidBLL.GetLoanRapidList(filter, sortField, ref pageCount);
            for (int rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                LoanRapidModel model = list[rowIndex];
                IRow dataRow = sheet.CreateRow(rowIndex + 1);
                dataRow.CreateCell(0, CellType.STRING).SetCellValue(model.Name);
                dataRow.CreateCell(1, CellType.STRING).SetCellValue(model.Phone);
                dataRow.CreateCell(2, CellType.STRING).SetCellValue(model.LoanAmount.ToString("0.00"));
                string strLoanMode = null;
                switch (model.LoanMode)
                {
                    case 0: strLoanMode = "按天"; break;
                    case 1: strLoanMode = "按月"; break;
                }
                dataRow.CreateCell(3, CellType.STRING).SetCellValue(model.LoanUseName);
                dataRow.CreateCell(4, CellType.STRING).SetCellValue(model.LoanTerm);
                dataRow.CreateCell(5, CellType.STRING).SetCellValue(strLoanMode);
                string strStatus = null;
                switch (model.Status)
                {
                    case 0: strStatus = "未审核"; break;
                    case 1: strStatus = "已审核"; break;
                }
                dataRow.CreateCell(6, CellType.STRING).SetCellValue(strStatus);
                dataRow.CreateCell(7, CellType.STRING).SetCellValue(model.ProvinceName);
                dataRow.CreateCell(8, CellType.STRING).SetCellValue(model.CityName);
                dataRow.CreateCell(9, CellType.STRING).SetCellValue(model.CreateTime.ToString());
                dataRow.CreateCell(10, CellType.STRING).SetCellValue(model.Describe);
                dataRow.CreateCell(11, CellType.STRING).SetCellValue(model.RealName);
            }
            return hssfWorkbook;
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