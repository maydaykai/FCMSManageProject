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
    public partial class LoanScoreDal
    {
        public bool Exists(int loanId)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) from LoanScore");
            strSql.Append(" where ");
            strSql.Append(" LoanId = @LoanId ");
            SqlParameter[] parameters = {
					new SqlParameter("@LoanId", SqlDbType.Int,4)
			};
            parameters[0].Value = loanId;

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
        /// 增加一条数据
        /// </summary>
        public bool Add(int loanId, int productTypeId)
		{
            bool flag = false;
            SqlParameter[] parameters = {
			            new SqlParameter("@loanId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ProductTypeId", SqlDbType.Int,4)            
              
            };

            parameters[0].Value = loanId;
            parameters[1].Value = productTypeId;
            try
            {
                int num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcLoanScoreAdd", parameters);
                if (num > 0)
                {
                    flag = true;
                }
            }
            catch
            {

            }
            return flag;
           			
		}


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(int id ,int score)
        {
            var strSql = new StringBuilder();
            strSql.Append("update LoanScore set ");

            strSql.Append(" Score = @Score , ");
            strSql.Append(" UpdateTime = getdate()  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = id} ,            
                        new SqlParameter("@Score", SqlDbType.Int,4){Value = score}           
              
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int loanId)
        {

            var strSql = new StringBuilder();
            strSql.Append("delete from LoanScore ");
            strSql.Append(" LoanId = @LoanId");
            SqlParameter[] parameters = {
					new SqlParameter("@LoanId", SqlDbType.Int,4)
			};
            parameters[0].Value = loanId;


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
            strSql.Append("select l.*,dps.ScoreItems as ScoreItemsName ");
            strSql.Append(" FROM LoanScore l inner join dbo.DimProductScore dps on l.ScoreItemsId = dps.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSumScoreList(int loanId)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@loanId", SqlDbType.Int,4)          
            };
            parameters[0].Value = loanId;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcLoanScoreSum", parameters);
        }
    }
}
