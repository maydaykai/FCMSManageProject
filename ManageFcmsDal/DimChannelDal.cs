using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;



namespace ManageFcmsDal
{
    /// <summary>
    /// 
    /// </summary>
    public class DimChannelDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimChannel()
        {
            string sql = "select * from DimChannel";
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql);
        }
    }
}
