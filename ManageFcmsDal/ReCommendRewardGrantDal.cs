using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class ReCommendRewardGrantDal
    {
        public DataSet GetList(string filter, string strOrderBy, int currentPage, int pageSize, ref int recordCount)
        {
            string sql1 =
                "select count(*) as totals from (select r.ID,r.Year,r.Month,r.Amount,r.AuditStatus,r.ApplyTime from dbo.ReCommendRewardGrant r) a";
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
                          ")) AS rownum,* from (select r.ID,r.Year,r.Month,r.Amount,r.AuditStatus,r.ApplyTime from dbo.ReCommendRewardGrant r) a";
            if (!string.IsNullOrEmpty(filter))
            {
                sql2 = sql2 + " WHERE " + filter;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((currentPage - 1) * pageSize + 1) + " and " + currentPage * pageSize;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
        }
        public DataSet GetList1(string filter, string strOrderBy, int currentPage, int pageSize, ref int recordCount)
        {
            string sql1 =
                "select count(*) as totals from (SELECT TotalYear, TotalMonth, SUM(Reward) Reward FROM dbo.ReCommendRewardHistory where IsPay = 0 GROUP BY TotalYear, TotalMonth union all SELECT CONVERT(CHAR(4),createTime,120) TotalYear,SUBSTRING(CONVERT(CHAR(10),createTime,120),6,2) TotalMonth,SUM(directReward+IndirectReward) Reward FROM BrokerReward WHERE investCount>=3 AND memberId>0 group by CONVERT(CHAR(4),createTime,120),SUBSTRING(CONVERT(CHAR(10),createTime,120),6,2)) a";
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
                          ")) AS rownum,* from (SELECT TotalYear, TotalMonth, SUM(Reward) Reward FROM dbo.ReCommendRewardHistory where IsPay = 0 GROUP BY TotalYear, TotalMonth union all SELECT CONVERT(CHAR(4),createTime,120) TotalYear,SUBSTRING(CONVERT(CHAR(10),createTime,120),6,2) TotalMonth,SUM(directReward+IndirectReward) Reward FROM BrokerReward WHERE investCount>=3 AND memberId>0 group by CONVERT(CHAR(4),createTime,120),SUBSTRING(CONVERT(CHAR(10),createTime,120),6,2)) a";
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
        public int Add(ReCommendRewardGrantModel model)
        {
            const string strSql = "INSERT INTO dbo.ReCommendRewardGrant([Year], [Month], Amount, ApplyUserID, ApplyTime, UpdateTime, AuditStatus, AuditRecords) VALUES (@Year, @Month, @Amount, @ApplyUserID, GETDATE(), GETDATE(), 0, N'');SELECT SCOPE_IDENTITY()";
            SqlParameter[] parameters = {                                            
                        new SqlParameter("@Year", SqlDbType.Int,4){Value=model.Year} ,            
                        new SqlParameter("@Month", SqlDbType.Int,4){Value=model.Month} ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9){Value=model.Amount} ,             
                        new SqlParameter("@ApplyUserID", SqlDbType.Int,4){Value=model.ApplyUserID}
            };
            var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameters);
            return ConvertHelper.ToInt(obj.ToString());
        }
        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(ReCommendRewardGrantModel model)
        {
            const string strSql = "UPDATE dbo.ReCommendRewardGrant SET UpdateTime=@UpdateTime,AuditStatus=@AuditStatus,AuditRecords=@AuditRecords WHERE ID=@ID";
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,                       
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,200){Value = model.AuditRecords} ,                                      
                        new SqlParameter("@AuditStatus", SqlDbType.Int,4){Value = model.AuditStatus}
            };
            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameters);
            return rows > 0;
        }
        /// <summary>
        /// 复审成功事务处理
        /// </summary>
        public bool ReCommendRewardGrantSuccessHandler(ReCommendRewardGrantModel model)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@TotalYear", SqlDbType.Int,4){Value=model.Year} ,            
                        new SqlParameter("@TotalMonth", SqlDbType.Int,4){Value=model.Month} ,   
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,200){Value = model.AuditRecords}
            };

            //update by 2015-10-09 lxy 2015-9后执行新推荐结算
            object result = null;
            if (model.Year >= 2015 && model.Month >= 9)
                result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "ProcBuildCommendReward_New", parameters);
            else
                result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "ProcBuildCommendReward", parameters);

            return result != null && ConvertHelper.ToInt(result.ToString()) > 0;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReCommendRewardGrantModel GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, Year, Month, Amount, ApplyUserID, ApplyTime, UpdateTime, AuditStatus, AuditRecords  ");
            strSql.Append(" from ReCommendRewardGrant ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            ReCommendRewardGrantModel model = new ReCommendRewardGrantModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Year"].ToString() != "")
                {
                    model.Year = int.Parse(ds.Tables[0].Rows[0]["Year"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Month"].ToString() != "")
                {
                    model.Month = int.Parse(ds.Tables[0].Rows[0]["Month"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApplyUserID"].ToString() != "")
                {
                    model.ApplyUserID = int.Parse(ds.Tables[0].Rows[0]["ApplyUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApplyTime"].ToString() != "")
                {
                    model.ApplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditStatus"].ToString() != "")
                {
                    model.AuditStatus = int.Parse(ds.Tables[0].Rows[0]["AuditStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditRecords"].ToString() != "")
                {
                    model.AuditRecords = ds.Tables[0].Rows[0]["AuditRecords"].ToString();
                }
                #endregion
                return model;
            }
            return null;
        }
    }
}
