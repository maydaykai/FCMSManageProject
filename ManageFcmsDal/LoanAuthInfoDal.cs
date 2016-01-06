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
    public class LoanAuthInfoDal
    {
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(int id, string authDate, string isAuth)
        {
            var strSql = new StringBuilder();
            strSql.Append("update LoanAuthInfo set ");

            strSql.Append(" AuthDate = @AuthDate, ");
            strSql.Append(" IsAuth = @IsAuth, ");
            strSql.Append(" UpdateTime = getdate()  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = id} ,        
                        new SqlParameter("@IsAuth", SqlDbType.Bit,1){Value = isAuth == "true"?1:0} , 
                        new SqlParameter("@AuthDate", SqlDbType.VarChar,10){Value = authDate}           
              
            };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select l.*,dai.AuthProductName as AuthProductName ");
            strSql.Append(" FROM LoanAuthInfo l inner join dbo.DimAuthInfo dai on l.AuthInfoId = dai.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }
    }
}
