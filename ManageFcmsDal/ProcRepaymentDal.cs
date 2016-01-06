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
    public class ProcRepaymentDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(int loanId, int repaymentType, ref string message)
        {
            bool flag = false;
            SqlParameter[] parameters = {
			            new SqlParameter("@LoanID", SqlDbType.Int,4) ,            
                        new SqlParameter("@RepaymentType", SqlDbType.Int,4) ,   
                        new SqlParameter("@Auto", SqlDbType.Int,4) ,   
                        new SqlParameter("@message", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = loanId;
            parameters[1].Value = repaymentType;
            parameters[2].Value = 0;
            parameters[3].Direction = ParameterDirection.Output;
            try
            {
                int num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcRepayment", parameters);
                if (num > 0)
                {
                    flag = true;
                }
                message = parameters[3].Value.ToString();
            }
            catch
            {

            }
            return flag;
        }
    }
}
