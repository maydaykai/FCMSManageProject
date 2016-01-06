using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class BatSmsHistoryDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int pagenum, int pagesize, string sortField, string totalDate, ref int total)
        {
            string sql1 = "select count(*) as totals from BatSmsHistory b inner join dbo.FcmsUser f on b.opuid=f.id";
            if (!string.IsNullOrEmpty(totalDate))
            {
                sql1 = sql1 + " where convert(char(10),b.CreateTime,120) = convert(char(10),'" + totalDate + "',120)";
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + sortField + ")) AS rownum,b.*,f.UserName from BatSmsHistory b inner join dbo.FcmsUser f on b.opuid=f.id";
            if (!string.IsNullOrEmpty(totalDate))
            {
                sql2 = sql2 + "  where convert(char(10),b.CreateTime,120) = convert(char(10),'" + totalDate + "',120)";
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + (pagenum * pagesize + 1) + " and " + (pagenum + 1) * pagesize;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
        }
    }
}
