using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class CreditLineBLL
    {
        public static bool LoanCreditLineser(CreditLineModel model)
        {
            return CreditLineDAL.LoanCreditLineser(model);
        }

        public static DataTable GetUserInfo(int ID, string sql)
        {
            return CreditLineDAL.GetUserInfo(ID, sql);
        }

        public static DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return CreditLineDAL.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }

        /// <summary>
        /// 查询学生贷款信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="filters"></param>
        /// <param name="sortStr"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable GetPage_StudentList(string tableName,string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return CreditLineDAL.Getpage_StudentList(tableName, fields, filters, sortStr, currentPage, pageSize, out total);
        }

        /// <summary>
        /// 查询导出数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="filters"></param>
        /// <param name="sortStr"></param>
        /// <returns></returns>
        public static DataTable Getpage_StudentTable(string tableName, string fields, string filters, string sortStr)
        {
            return CreditLineDAL.Getpage_StudentTable(tableName, fields, filters, sortStr);
        }
    }
}
