using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class OverdueBLL
    {
        OverdueDAL dal = new OverdueDAL();
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }

        /// <summary>
        /// 数据分页，自带表名
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total,string tables)
        {
            return dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total, tables);
        }
    }
}
