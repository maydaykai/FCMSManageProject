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
    public class DimLoanUseDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddDimLoanUse(DimLoanUseModel model)
        {
            int returnval = 0;
            var strSql = new StringBuilder();
            strSql.Append("insert into DimLoanUse(");
            strSql.Append("LoanUseName,Sign");
            strSql.Append(") values (");
            strSql.Append("@LoanUseName,@Sign");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@LoanUseName", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Sign", SqlDbType.VarChar,6)             
              
            };

            parameters[0].Value = model.LoanUseName;
            parameters[1].Value = model.Sign;

            try
            {
                object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
                if (obj != null)
                {
                    returnval = Convert.ToInt32(obj);
                }
            }
            catch
            {
            }
            return returnval;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DimLoanUseModel> GetDimLoanUseModelList(string strWhere)
        {
            var list = new List<DimLoanUseModel>();
            var strSql = new StringBuilder();
            strSql.Append("select *  from DimLoanUse ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
                while (dr.Read())
                {
                    DimLoanUseModel info = getDimLoanUseModelByDr(dr);
                    list.Add(info);
                }
                dr.Close();
            }
            catch (Exception)
            {

            }
            return list;
        }
        private DimLoanUseModel getDimLoanUseModelByDr(SqlDataReader dr)
        {
            var model = new DimLoanUseModel();
            model.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            model.LoanUseName = dr["LoanUseName"] != DBNull.Value ? dr["LoanUseName"].ToString() : "";
            model.Sign = dr["Sign"] != DBNull.Value ? dr["Sign"].ToString() : "";
            return model;
        }
    }
}
