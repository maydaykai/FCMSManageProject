using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class OverdueDAL
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " RepaymentPlan an";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 数据分页，自带表名
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total,string tables)
        {
            int totalPage;
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
    }
}
