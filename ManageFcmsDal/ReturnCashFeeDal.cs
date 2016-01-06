using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class ReturnCashFeeDal
    {
        public DataSet GetList(string filter, string strOrderBy, int currentPage, int pageSize, ref int recordCount)
        {
            var list = new List<LoanModel>();
            string sql1 = "select count(*) as totals from (select r.ID,r.FeeType,r.Amount,R.AuditStatus,r.Description,r.CreateTime,m.MemberName,mi.RealName from dbo.ReturnCashFee r " +
                          "  inner join dbo.Member m on r.MemberId=m.ID " +
                          "  inner join dbo.MemberInfo mi on r.MemberId=mi.MemberID ) a ";
            if (!string.IsNullOrEmpty(filter))
            {
                sql1 = sql1 + " WHERE " + filter;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                recordCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + strOrderBy +
                          ")) AS rownum,* from (select r.ID,r.FeeType,r.Amount,R.AuditStatus,r.Description,r.CreateTime,m.MemberName,mi.RealName from dbo.ReturnCashFee r" +
                          "  inner join dbo.Member m on r.MemberId=m.ID " +
                          "  inner join dbo.MemberInfo mi on r.MemberId=mi.MemberID ) a ";
            if (!string.IsNullOrEmpty(filter))
            {
                sql2 = sql2 + " WHERE " + filter;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((currentPage - 1) * pageSize + 1) + " and " + currentPage * pageSize;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
        }

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(ReturnCashFeeModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into ReturnCashFee(");
            strSql.Append(" MemberId,FeeType,Amount,AuditStatus,Description");
            strSql.Append(") values (");
            strSql.Append(" @MemberId,@FeeType,@Amount,@AuditStatus,@Description");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {                                            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberId} ,            
                        new SqlParameter("@FeeType", SqlDbType.Int,4){Value=model.FeeType} ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9){Value=model.Amount} ,            
                        new SqlParameter("@AuditStatus", SqlDbType.Int,4){Value=model.AuditStatus},
                        new SqlParameter("@Description", SqlDbType.NVarChar,20){Value=model.Description}
            };
            var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return ConvertHelper.ToInt(obj.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(ReturnCashFeeModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update ReturnCashFee set ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" AuditRecords = @AuditRecords , ");
            strSql.Append(" MemberID = @MemberID , ");
            strSql.Append(" AuditStatus = @AuditStatus,  ");
            strSql.Append(" Description = @Description  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.Id} ,                       
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,200){Value = model.AuditRecords} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value = model.MemberId} ,                                            
                        new SqlParameter("@AuditStatus", SqlDbType.Int,4){Value = model.AuditStatus} ,
                        new SqlParameter("@Description", SqlDbType.NVarChar,20){Value=model.Description}
            };

            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;

        }

        /// <summary>
        /// 发放红包复审事物处理
        /// </summary>
        public bool ReturnCashFeeAudit(ReturnCashFeeModel model)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.Id} ,                                              
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,200){Value = model.AuditRecords} ,
                        new SqlParameter("@MemberId", SqlDbType.Int,4){Value = model.MemberId} ,    
                        new SqlParameter("@ReturnCashFee", SqlDbType.Decimal,9){Value= model.Amount} ,  
                        new SqlParameter("@Message", SqlDbType.NVarChar,20){Value=model.Description}
                         
            };

            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "ProcSendReturnCashFee", parameters);

            return result != null && ConvertHelper.ToInt(result.ToString()) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReturnCashFeeModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID,MemberId,FeeType,Amount,AuditStatus,Description,CreateTime,AuditRecords,UpdateTime ");
            strSql.Append("  from ReturnCashFee ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};

            var model = new ReturnCashFeeModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["ID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                model.MemberId = ds.Tables[0].Rows[0]["MemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["MemberID"]) : 0;
                model.FeeType = ds.Tables[0].Rows[0]["FeeType"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["FeeType"]) : 0;
                model.Amount = ds.Tables[0].Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"]) : 0.00M;
                model.AuditStatus = ds.Tables[0].Rows[0]["AuditStatus"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["AuditStatus"]) : 0;
                model.Description = ds.Tables[0].Rows[0]["Description"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["Description"]) : "";
                model.CreateTime = ds.Tables[0].Rows[0]["CreateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateTime"]) : PublicConst.MinDate;
                model.AuditRecords = ds.Tables[0].Rows[0]["AuditRecords"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["AuditRecords"]) : "";
                model.UpdateTime = ds.Tables[0].Rows[0]["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdateTime"]) : PublicConst.MinDate;
                return model;
            }
            return null;
        }
    }
}
