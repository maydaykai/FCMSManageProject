using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class ProcFundReportDal
    {
        public DataSet GetList(int memberID, string filter, int currentPage, int pageSize, string strOrderBy, out int recordCount)
        {
            IList<ProcFundReportModel> list = new List<ProcFundReportModel>();
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@memberID", SqlDbType.Int, 4){Value=memberID},
                new SqlParameter("@strWhere", SqlDbType.NVarChar){Value=filter},
                new SqlParameter("@pageSize", SqlDbType.Int, 4){Value=pageSize},
                new SqlParameter("@currentPage", SqlDbType.Int, 4){Value=currentPage},
                new SqlParameter("@strOrderBy", SqlDbType.NVarChar){Value=strOrderBy},
                new SqlParameter("@pageCount", SqlDbType.Int, 4){Direction=ParameterDirection.Output},
                new SqlParameter("@recordCount", SqlDbType.Int, 4){Direction=ParameterDirection.Output}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.Proc_FundReport", parameters);
            recordCount = (int)parameters[6].Value;
            return ds;
        }
    }
}
