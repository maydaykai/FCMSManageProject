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
    public class ReCommendRewardDal
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " dbo.ReCommendRewardHistory P left join Member M on P.MemberId=M.ID left join MemberInfo MI on P.MemberId=MI.MemberID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        public bool GetData()
        {
            int num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, "EXEC ProcReCommendReward");
            return num > 0;
        }

        public string GetLowerLevel(int memberId, int year, int month)
        {
            string sql = "select LowerLevel from dbo.ReCommendRewardHistory where MemberId=@MemberId and TotalYear = @Year and TotalMonth = @Month";
            
            SqlParameter[] parameters =
                {
                    new SqlParameter("@MemberId", SqlDbType.Int, 4){Value =memberId},
                    new SqlParameter("@Year", SqlDbType.Int, 4){Value =year},
                    new SqlParameter("@Month", SqlDbType.Int, 4){Value =month}
                };
            SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            string lowerLevel = "";
            if (sdr.Read())
            {
                lowerLevel = sdr["LowerLevel"].ToString();
            }
            sdr.Close();
            return lowerLevel;
        }

        /// <summary>
        /// 推荐统计列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-10-08</remarks>
        public DataTable GetPageList1(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(B.id) AS totals  FROM BrokerReward B,Member M,MemberInfo MI WHERE B.memberId=M.ID AND M.ID=MI.MemberID AND B.memberId>0 AND (B.IndirectReward >0 OR B.directReward>0)").Append(filter);

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY B.createTime) AS rowNum,CONVERT(CHAR(7),B.createTime,120) x_createTime,M.ID x_MemberId,M.MemberName x_MemberName,MI.RealName x_RealName,B.level x_level,(B.IndirectReward+B.directReward) x_amount,B.IndirectInterest x_IndirectInterest,B.IndirectProportion x_IndirectProportion,B.IndirectReward x_IndirectReward,B.directInterest x_directInterest,B.directProportion x_directProportion,B.directReward x_directReward,B.investCount x_investCount");
            sql.Append(" FROM BrokerReward B,Member M,MemberInfo MI WHERE B.memberId=M.ID AND M.ID=MI.MemberID AND B.memberId>0 AND (B.IndirectReward >0 OR B.directReward>0)").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");

            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null).Tables[0];
        }

        /// <summary>
        /// 获取下级推荐数据
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="filter2">查询条件2</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-10-10</remarks>
        public DataTable GetRecommendDetalis(string filter, string filter2, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            //StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT COUNT(*) AS totals FROM (SELECT CONVERT(CHAR(7),B.createTime,120) dateDay,B.createTime,'直接' dType,(SELECT M1.MemberName FROM Member M1 WHERE M1.ID=B.recMemberID) recMemberName,(SELECT MI1.RealName FROM MemberInfo MI1 WHERE MI1.MemberID=B.recMemberID) recRealName,M.MemberName,MI.RealName");
            //sql.Append(",B.monthInterst,(SELECT B1.directProportion FROM BrokerReward B1 WHERE B1.memberId=B.recMemberID AND CONVERT(CHAR(7),B1.createTime,120)=CONVERT(CHAR(7),B.createTime,120)) Proportion");
            //sql.Append(" FROM BrokerReward B,Member M,MemberInfo MI");
            //sql.Append(" WHERE B.memberId=M.ID AND M.ID=MI.MemberID AND B.memberId>0 AND B.recMemberID>0 AND B.monthInterst >0");
            //sql.Append(" union all ");
            //sql.Append("SELECT CONVERT(CHAR(7),B.createTime,120) dateDay,B.createTime,'间接' dType,(SELECT M1.MemberName FROM Member M1 WHERE M1.ID=B.recMemberID) recMemberName,(SELECT MI1.RealName FROM MemberInfo MI1 WHERE MI1.MemberID=B.recMemberID) recRealName,M.MemberName,MI.RealName");
            //sql.Append(",B.monthInterst,(SELECT B3.IndirectProportion FROM BrokerReward B3 WHERE CONVERT(CHAR(7),B3.createTime,120)=CONVERT(CHAR(7),B.createTime,120) AND B3.memberId=(SELECT B1.recMemberID FROM BrokerReward B1 WHERE B1.memberId=B.recMemberID AND CONVERT(CHAR(7),B1.createTime,120)=CONVERT(CHAR(7),B.createTime,120))) Proportion");
            //sql.Append(" FROM BrokerReward B,Member M,MemberInfo MI");
            //sql.Append(" WHERE B.memberId=M.ID AND M.ID=MI.MemberID AND B.memberId>0 AND B.recMemberID>0 AND B.monthInterst >0");
            //sql.Append(" AND B.recMemberID IN (SELECT distinct B2.memberId FROM BrokerReward B2 WHERE B2.memberId>0 AND B2.recMemberID>0 AND B2.monthInterst >0)) MB");
            //sql.Append(filter);
            //SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            //if (reader.Read())
            //{
            //    total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            //}
            //reader.Close();

            //sql.Clear();
            //sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY dateDay) AS rowNum,*,CAST((monthInterst*Proportion/100) AS decimal(18, 2)) Reward FROM (SELECT CONVERT(CHAR(7),B.createTime,120) dateDay,B.createTime,'直接' dType,(SELECT M1.MemberName FROM Member M1 WHERE M1.ID=B.recMemberID) recMemberName,(SELECT MI1.RealName FROM MemberInfo MI1 WHERE MI1.MemberID=B.recMemberID) recRealName,M.MemberName,MI.RealName");
            //sql.Append(",B.monthInterst,(SELECT B1.directProportion FROM BrokerReward B1 WHERE B1.memberId=B.recMemberID AND CONVERT(CHAR(7),B1.createTime,120)=CONVERT(CHAR(7),B.createTime,120)) Proportion");
            //sql.Append(" FROM BrokerReward B,Member M,MemberInfo MI");
            //sql.Append(" WHERE B.memberId=M.ID AND M.ID=MI.MemberID AND B.memberId>0 AND B.recMemberID>0 AND B.monthInterst >0");
            //sql.Append(" union all ");
            //sql.Append("SELECT CONVERT(CHAR(7),B.createTime,120) dateDay,B.createTime,'间接' dType,(SELECT M1.MemberName FROM Member M1 WHERE M1.ID=B.recMemberID) recMemberName,(SELECT MI1.RealName FROM MemberInfo MI1 WHERE MI1.MemberID=B.recMemberID) recRealName,M.MemberName,MI.RealName");
            //sql.Append(",B.monthInterst,(SELECT B3.IndirectProportion FROM BrokerReward B3 WHERE CONVERT(CHAR(7),B3.createTime,120)=CONVERT(CHAR(7),B.createTime,120) AND B3.memberId=(SELECT B1.recMemberID FROM BrokerReward B1 WHERE B1.memberId=B.recMemberID AND CONVERT(CHAR(7),B1.createTime,120)=CONVERT(CHAR(7),B.createTime,120))) Proportion");
            //sql.Append(" FROM BrokerReward B,Member M,MemberInfo MI");
            //sql.Append(" WHERE B.memberId=M.ID AND M.ID=MI.MemberID AND B.memberId>0 AND B.recMemberID>0 AND B.monthInterst >0");
            //sql.Append(" AND B.recMemberID >0) MB");
            //sql.Append(filter);
            //sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");

            //return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null).Tables[0];

            SqlParameter[] parameters = {
			            new SqlParameter("@filter", SqlDbType.NVarChar,200){Value=filter} ,        
			            new SqlParameter("@filter2", SqlDbType.NVarChar,200){Value=filter2} ,        
			            new SqlParameter("@CurrentPage", SqlDbType.Int){Value=pageIndex} ,        
			            new SqlParameter("@PageSize", SqlDbType.Int){Value=pageSize} ,        
			            new SqlParameter("@RecordCount", SqlDbType.Int){Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_TotalBrokerRewardList", parameters).Tables[0];
            total = int.Parse(parameters[4].Value.ToString());
            return data;
        }
    }
}
