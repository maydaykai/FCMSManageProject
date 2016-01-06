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
    public class DealTotalDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime totalDate)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@TotalDate", SqlDbType.DateTime){Value=totalDate}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcTotalDeal", parameters);
            return ds;
        }
    }
}
