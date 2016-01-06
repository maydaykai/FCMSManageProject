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
    public class LoanTotalDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime dateStart, DateTime endDate)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@dateStart", SqlDbType.DateTime){Value=dateStart} ,  
                new SqlParameter("@dateEnd", SqlDbType.DateTime){Value=endDate}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcTotalLoan", parameters);
            return ds;
        }
    }
}
