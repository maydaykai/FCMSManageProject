using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class DimProductScoreDAL
    {
        public bool UpdateByIdToField(string tableName, string field, int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE dbo.DimProductScore SET @Fields  WHERE ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4){Value=id},
					new SqlParameter("@Fields", SqlDbType.NVarChar,2000){Value=field},
                    new SqlParameter("@TableName", SqlDbType.VarChar,20){Value=tableName}
			};

            var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_UpdateByIdToField", parameters);
            return ConvertHelper.ToInt(obj.ToString()) > 0;
        }
    }
}
