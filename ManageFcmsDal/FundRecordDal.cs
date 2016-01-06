using ManageFcmsConn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class FundRecordDal
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = @" FundRecord P LEFT OUTER JOIN
                      Member M1 ON P.PayeeMemberID = M1.ID left outer join 
                      MemberInfo MI1 on P.PayeeMemberID=MI1.MemberID LEFT OUTER JOIN
                      Member M2 ON P.PartyMemberID = M2.ID left outer join
                      MemberInfo MI2 on P.PartyMemberID=MI2.MemberID LEFT OUTER JOIN
                      Loan AS L ON P.LoanID = L.ID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 获取资金流水
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="filter"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="strOrderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable GetFundFlow(int memberID, string filter, int currentPage, int pageSize, string strOrderBy, out int recordCount)
        {
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
            if (ds == null)
            {
                recordCount = 0;
                return new DataTable();
            }
            recordCount = (int)parameters[6].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据条件获取资金记录列表
        /// </summary>
        public DataTable GetFundRecordList(string fields, string filters, string sortStr, out int total)
        {
            const string tables = @" FundRecord P LEFT OUTER JOIN
                      Member M1 ON P.PayeeMemberID = M1.ID left outer join 
                      MemberInfo MI1 on P.PayeeMemberID=MI1.MemberID LEFT OUTER JOIN
                      Member M2 ON P.PartyMemberID = M2.ID left outer join
                      MemberInfo MI2 on P.PartyMemberID=MI2.MemberID LEFT OUTER JOIN
                      Loan AS L ON P.LoanID = L.ID";
            string sql1 = "select count(*) as totals from " + tables;
            if (!string.IsNullOrEmpty(filters))
            {
                sql1 = sql1 + " where " + filters;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            else
            {
                total = 0;
            }
            reader.Close();

            StringBuilder sbSql2 = new StringBuilder();
            sbSql2.Append("select ").Append(fields).Append(" from ").Append(tables).Append(" where ").Append(filters).Append(" order by ").Append(sortStr);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sbSql2.ToString(), null);
            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 根据条件获取资金记录列表
        /// </summary>
        public DataTable GetFundRecordList(string filters)
        {
            string sqlStr = " SELECT ISNULL(M1.MemberName,'—'),ISNULL(MI1.RealName,'—'),ISNULL(M2.MemberName,'—'),ISNULL(MI2.RealName,'—')," +
                            " ISNULL(L.LoanNumber,'—'),[dbo].[Fun_GetFeeTypeVal](P.FeeType), P.Amount, P.PayeeBalance, P.PartyBalance,CASE P.[Status] WHEN 1 THEN '正常' WHEN 2 THEN '冻结' WHEN 3 THEN '作废' ELSE '—' END, P.[Description], P.CreateTime,P.UpdateTime " +
                            " FROM FundRecord P LEFT OUTER JOIN" +
                            " Member M1 ON P.PayeeMemberID = M1.ID left outer join " +
                            " MemberInfo MI1 on P.PayeeMemberID=MI1.MemberID LEFT OUTER JOIN" +
                            " Member M2 ON P.PartyMemberID = M2.ID left outer join" +
                            " MemberInfo MI2 on P.PartyMemberID=MI2.MemberID LEFT OUTER JOIN" +
                            " Loan AS L ON P.LoanID = L.ID WHERE " + filters + " ORDER BY P.CreateTime DESC";

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlStr);
            return ds.Tables[0];

        }

        /// <summary>
        /// 根据条件获取金额
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public decimal getAmountByWhere(string where)
        {
            var strSql = new StringBuilder();
            strSql.Append("select top 1 Amount from dbo.FundRecord");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" where " + where);
            }

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return obj == null ? 0 : Convert.ToInt32(obj.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int addFundRecord(SqlCommand cmd, FundRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FundRecord(");
            strSql.Append("RelationID,Description,CreateTime,UpdateTime,PayeeMemberID,PartyMemberID,FeeType,Amount,PayeeBalance,PartyBalance,Status,LoanID");
            strSql.Append(") values (");
            strSql.Append("@RelationID,@Description,@CreateTime,@UpdateTime,@PayeeMemberID,@PartyMemberID,@FeeType,@Amount,@PayeeBalance,@PartyBalance,@Status,@LoanID");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] paras = {
			            new SqlParameter("@RelationID", SqlDbType.Int,4){Value= model.RelationID},
                        new SqlParameter("@Description", SqlDbType.NVarChar,500){Value= model.Description},
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value= model.CreateTime},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= model.UpdateTime},
                        new SqlParameter("@PayeeMemberID", SqlDbType.Int,4){Value= model.PayeeMemberID},
                        new SqlParameter("@PartyMemberID", SqlDbType.Int,4){Value= model.PartyMemberID},
                        new SqlParameter("@FeeType", SqlDbType.Int,4){Value= model.FeeType},
                        new SqlParameter("@Amount", SqlDbType.Decimal,9){Value= model.Amount},
                        new SqlParameter("@PayeeBalance", SqlDbType.Decimal,9){Value= model.PayeeBalance},
                        new SqlParameter("@PartyBalance", SqlDbType.Decimal,9){Value= model.PartyBalance},
                        new SqlParameter("@Status", SqlDbType.Int,4){Value= model.Status},
                        new SqlParameter("@LoanID", SqlDbType.Int,4){Value= model.LoanID}
            };

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql.ToString();
            cmd.Parameters.Clear();
            foreach (var para in paras)
            {
                cmd.Parameters.Add(para);
            }
            object obj = cmd.ExecuteScalar();
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 执行年会收益收回存储过程
        /// </summary>
        /// <returns></returns>
        public bool ExeProcWithdrawInterestNH()
        {
            int rtnvalue = Convert.ToInt16(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcWithdrawInterestNH"));
            return rtnvalue == 1;
        }

        /// <summary>
        /// 根据条件得到汇总金额
        /// </summary>
        public object Aggregate(string filters)
        {
            string strSql = "SELECT SUM(P.Amount)  FROM " +
                            "FundRecord P LEFT OUTER JOIN " +
                            "Member M1 ON P.PayeeMemberID = M1.ID left outer join " +
                            "MemberInfo MI1 on P.PayeeMemberID=MI1.MemberID LEFT OUTER JOIN " +
                            "Member M2 ON P.PartyMemberID = M2.ID left outer join " +
                            "MemberInfo MI2 on P.PartyMemberID=MI2.MemberID LEFT OUTER JOIN " +
                            "Loan AS L ON P.LoanID = L.ID " +
                            "where " + filters;

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
        }
    }
}
