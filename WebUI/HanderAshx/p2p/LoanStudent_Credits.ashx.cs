using ManageFcmsBll;
using ManageFcmsCommon;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanStudent_Credits 的摘要说明
    /// </summary>
    public class LoanStudent_Credits : IHttpHandler
    {
        private int _currentPage = 1;//当前页面
        private int _pageSize;//当前页大小
        private string _sort = "asc";//排序条件

        //查询条件 会员名称 审核状态 省份 城市  申请时间 结束时间 
        private string _memberName;
        private int _checkstatus;
        private int _province;
        private int _city;

        private string _begintime;
        private string _endtime;

        private string _output = "";//是否导出数据
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            //获取参数
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "DESC");

            _output = ConvertHelper.QueryString(context.Request, "output", "");
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _checkstatus = ConvertHelper.QueryString(context.Request, "checkstatus", -1);
            _province = ConvertHelper.QueryString(context.Request, "province", 0);
            _city = ConvertHelper.QueryString(context.Request, "city", -1);
            _begintime = ConvertHelper.QueryString(context.Request, "begintime", "");
            _endtime = ConvertHelper.QueryString(context.Request, "endtime", "");
            //组合where 条件
            var filter = " 1=1";
            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and MemberName like '%" + _memberName + "%'";
            }
            if (_checkstatus > -1)
            {
                filter += " and m.ExamStatus=" + _checkstatus + " or m.ExamStatus=2";
            }
            if (_province > 0)
            {
                filter += " and PlaceResidenceProvince=" + _province;
            }
            if (_city > -1)
            {
                filter += " and PlaceResidenceCity=" + _city;
            }
            if (!string.IsNullOrEmpty(_begintime) && !string.IsNullOrEmpty(_endtime))
            {
                filter += " and  CONVERT(VARCHAR(10),m.CreateTime,120)  BETWEEN '" + _begintime + "'  AND '" + _endtime + "'";
            }
            // var sortColumn = _sortField + " " + _sort;
            //是否为导出数据
            if (!string.IsNullOrEmpty(_output))
            {
                context.Response.ContentType = "application/x-xls";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=学生贷申请列表.xls");
                HSSFWorkbook hssfWorkbook = OutputSearchResult(filter);
                hssfWorkbook.Write(context.Response.OutputStream);
            }
            else
            {
                context.Response.Write(GetPageList(_currentPage, _pageSize, _sort, filter));
            }

        }

        /// <summary>
        /// 查询数据解析为json对象
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="sortField"></param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public string GetPageList(int page, int pagesize, string sortField, string filter)
        {
            page = page + 1;
            int total;
            StringBuilder filedStr = new StringBuilder();
            filedStr.Append("CASE m.ExamStatus WHEN 0 THEN '审核中'  WHEN 1 THEN '审核通过'  ELSE '审核不通过'  END  AS ExamStatus,CASE  WHEN LEN(m.AuditRecords)>0 THEN '已审核' ELSE '未审核' END  AS AuditRecords,CASE  ISNULL(d.ExamStatus,0)  WHEN 0 THEN (CASE m.ExamStatus WHEN 2 THEN '平台初审不通过'   ELSE '平台初审中'  END)  ELSE dd.ExamStatusName end  AS ExamStatusName1");
            filedStr.Append(",a.ID,a.MemberName,b.RealName,a.Mobile,c.UniversityName,c.PlaceResidenceCity,");
            filedStr.Append("PlaceResidenceProvince,m.LoanAmount,m.CreateTime,m.LoanId,p.Name AS PName,city.NAME AS PCity ,m.ID AS StudentLoanApplyId ,");
            filedStr.Append(" (SELECT RepaymentMethodName FROM dbo.DimRepaymentMethod WHERE id=m.RepaymentMethod) AS RepaymentMethodName,(SELECT Name FROM dbo.DimProductType WHERE ID=23) AS LoanTypeName");
            StringBuilder tablename = new StringBuilder();
            tablename.Append("dbo.Member a INNER JOIN dbo.MemberInfo b ON a.ID=b.MemberID INNER JOIN dbo.StudentInfo c ON c.MemberID=b.MemberID ");
            tablename.Append(" INNER JOIN dbo.StudentLoanApply m ON m.MemberId=a.ID LEFT JOIN dbo.Loan d ON d.ID=m.LoanId AND d.LoanTypeID=23 ");
            tablename.Append(" LEFT JOIN  (SELECT * FROM dbo.Area WHERE ParentID=1) AS p ON p.ID=c.PlaceResidenceProvince LEFT JOIN (SELECT * FROM dbo.Area WHERE ParentID>1) AS city ON city.ID=c.PlaceResidenceCity  LEFT JOIN dbo.DimExamStatus dd ON dd.ID=d.ExamStatus");


            var dt = CreditLineBLL.GetPage_StudentList(tablename.ToString(), filedStr.ToString(), filter, sortField, page, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;

        }

        //查询导出数据
        public DataTable GetStudentDtable(string filter)
        {
            StringBuilder filedStr = new StringBuilder();
            filedStr.Append("CASE m.ExamStatus WHEN 0 THEN '审核中'  WHEN 1 THEN '审核通过'  ELSE '审核不通过'  END  AS ExamStatus,CASE  WHEN LEN(m.AuditRecords)>0 THEN '已审核' ELSE '未审核' END  AS AuditRecords");
            filedStr.Append(",a.ID,a.MemberName,b.RealName,a.Mobile,c.UniversityName,c.PlaceResidenceCity,");
            filedStr.Append("PlaceResidenceProvince,m.LoanAmount,m.CreateTime,m.LoanId,p.Name AS PName,city.NAME AS PCity, ");
            filedStr.Append(" (SELECT RepaymentMethodName FROM dbo.DimRepaymentMethod WHERE id=m.RepaymentMethod) AS RepaymentMethodName,(SELECT Name FROM dbo.DimProductType WHERE ID=23) AS LoanTypeName");
            StringBuilder tablename = new StringBuilder();
            tablename.Append("dbo.Member a INNER JOIN dbo.MemberInfo b ON a.ID=b.MemberID INNER JOIN dbo.StudentInfo c ON c.MemberID=b.MemberID ");
            tablename.Append(" INNER JOIN dbo.StudentLoanApply m ON m.MemberId=a.ID LEFT JOIN dbo.Loan d ON d.ID=m.LoanId AND d.LoanTypeID=23 ");
            tablename.Append(" LEFT JOIN  (SELECT * FROM dbo.Area WHERE ParentID=1) AS p ON p.ID=c.PlaceResidenceProvince LEFT JOIN (SELECT * FROM dbo.Area WHERE ParentID>1) AS city ON city.ID=c.PlaceResidenceCity");
            var dt = CreditLineBLL.Getpage_StudentTable(tablename.ToString(), filedStr.ToString(), filter, "ID DESC");
            return dt;
        }

        //数据导出
        public HSSFWorkbook OutputSearchResult(string filter)
        {

            var memberBll = new MemberBll();
            var dt = GetStudentDtable(filter);
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("学生贷款列表");
            IRow rowHeader = sheet.CreateRow(0);
            rowHeader.CreateCell(0, CellType.STRING).SetCellValue("会员名称");
            rowHeader.CreateCell(1, CellType.STRING).SetCellValue("真实姓名");
            rowHeader.CreateCell(2, CellType.STRING).SetCellValue("手机号码");
            rowHeader.CreateCell(3, CellType.STRING).SetCellValue("院校名称");
            rowHeader.CreateCell(4, CellType.STRING).SetCellValue("借款金额");
            rowHeader.CreateCell(5, CellType.STRING).SetCellValue("审核状态");
            rowHeader.CreateCell(6, CellType.STRING).SetCellValue("所在省份");
            rowHeader.CreateCell(7, CellType.STRING).SetCellValue("所在城市");
            rowHeader.CreateCell(8, CellType.STRING).SetCellValue("申请时间");
            rowHeader.CreateCell(9, CellType.STRING).SetCellValue("还款方式");
            rowHeader.CreateCell(10, CellType.STRING).SetCellValue("借款标类型");
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                IRow dataRow = sheet.CreateRow(rowIndex + 1);
                dataRow.CreateCell(0, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["MemberName"]));
                dataRow.CreateCell(1, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["RealName"]));
                dataRow.CreateCell(2, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Mobile"]));
                dataRow.CreateCell(3, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["UniversityName"]));
                dataRow.CreateCell(4, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["LoanAmount"]));
                dataRow.CreateCell(5, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["AuditRecords"]));
                dataRow.CreateCell(6, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PName"]));
                dataRow.CreateCell(7, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["PCity"]));
                dataRow.CreateCell(8, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["CreateTime"]));
                dataRow.CreateCell(9, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["RepaymentMethodName"]));
                dataRow.CreateCell(10, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["LoanTypeName"]));
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