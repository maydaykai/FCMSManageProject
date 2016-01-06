/*************************************************************
Author:		 卢侠勇
Create date: 2015-8-4
Description: 报表数据处理类
Update: 
**************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using ManageFcmsConn;
using System.Collections.Generic;

namespace ManageFcmsDal
{
    public class ReportDal
    {

        private static ReportDal _item;
        public static ReportDal Instance
        {
            get { return _item = (_item ?? new ReportDal()); }
        }

        /// <summary>
        /// 添加客服回访记录
        /// </summary>
        /// <param name="memberId">用户ID</param>
        /// <param name="operatorId">操作员ID</param>
        /// <param name="record">操作结果</param>
        /// <param name="notes">备注</param>
        /// <returns>true false</returns>
        /// <remarks>add 卢侠勇,2015-8-04</remarks>
        public bool AddReturnVisit(int memberId, int operatorId, string record, string notes)
        {
            var strSql = new StringBuilder();
            strSql.Append("INSERT INTO ReturnVisit");
            strSql.Append(" VALUES(");
            strSql.Append("@memberId,@operatorId,@record,@notes,@createTime");
            strSql.Append(")");
            SqlParameter[] parameters = {
			            new SqlParameter("@memberId", SqlDbType.Int){Value=memberId} ,        
			            new SqlParameter("@operatorId", SqlDbType.Int){Value=operatorId} ,          
                        new SqlParameter("@record", SqlDbType.VarChar,20){Value=record} ,            
                        new SqlParameter("@notes", SqlDbType.VarChar,200){Value=notes},            
                        new SqlParameter("@createTime", SqlDbType.DateTime){Value=DateTime.Now}
            };

            var rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows == 1;
        }

        /// <summary>
        /// 周报报表
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>周报报表结果集</returns>
        /// <remarks>add 卢侠勇,2015-08-20</remarks>
        public DataTable GetWeekly(DateTime dateStart, DateTime endDate)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@startTime", SqlDbType.Date){Value=dateStart} ,        
			            new SqlParameter("@endTime", SqlDbType.Date){Value=endDate}
            };
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_TotalWeekly", parameters).Tables[0];
        }

        /// <summary>
        /// 访问报表
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>访问报表结果集</returns>
        public DataTable GetUserSourceTotalList(DateTime dateStart, DateTime endDate, string memberName, int channel, string channelRemark, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(US.ID) AS totals FROM UserSource US INNER JOIN Member AS M ON M.ID=US.memberId WHERE CONVERT(CHAR(10),VisitTime,120)>=@dateStart AND CONVERT(CHAR(10),VisitTime,120)<=@endDate ");

            if (!string.IsNullOrEmpty(memberName))
            {
                sql.Append(" AND M.MemberName like '%"+ memberName+"%'");
            }

            if (channel != -1)
            {
                sql.Append(" AND M.channel="+channel+"");
            }

            //二级渠道
            if (!string.IsNullOrEmpty(channelRemark))
            {
                sql.Append(" AND M.channelRemark='"+channelRemark+"'");
            }
          SqlParameter[] parameter={ 
             new SqlParameter("@dateStart", SqlDbType.Date){Value=dateStart},
			            new SqlParameter("@endDate", SqlDbType.Date){Value=endDate}
            };
           
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameter);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            sql.Clear();

            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY us.ID) AS rowNum,US.ID,M.MemberName,DC.Channel,M.ChannelRemark,HostIP,VisitPreUrl,VisitUrl,CONVERT(CHAR(10),VisitTime,120) AS VisitTime FROM UserSource US " +
            "INNER JOIN Member AS M ON M.ID=US.memberId  INNER JOIN DimChannel as DC ON DC.Id=M.Channel WHERE CONVERT(CHAR(10),VisitTime,120)>=@dateStart AND CONVERT(CHAR(10),VisitTime,120)<=@endDate ");

            if (!string.IsNullOrEmpty(memberName))
            {
                sql.Append(" AND M.MemberName like @memberName ");
            } 
            //渠道
            if (channel != -1)
            {
                sql.Append(" AND M.channel=@channel");
            }
            //二级渠道
            if (!string.IsNullOrEmpty(channelRemark))
            {
                sql.Append(" AND M.channelRemark=@channelRemark");
            }

            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            List<SqlParameter> sps = new List<SqlParameter>() { 
             new SqlParameter("@dateStart", SqlDbType.Date){Value=dateStart},
			            new SqlParameter("@endDate", SqlDbType.Date){Value=endDate}
            };
            if (!string.IsNullOrEmpty(memberName))
            {
                sps.Add(new SqlParameter("@memberName", "%" + memberName + "%"));
            }
            if (channel != -1)
            {
                sps.Add(new SqlParameter("@channel", channel));
            }
            if (!string.IsNullOrEmpty(channelRemark))
            {
                sps.Add(new SqlParameter("@channelRemark", channelRemark));
            }
            SqlParameter[] parms = sps.ToArray();
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parms).Tables[0];
        }

        /// <summary>
        /// 还款报表
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <param name="sort">分类</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>还款报表结果集</returns>
        public DataTable GetRepaymentTotalList(string fields, string filter, string sort, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT R.LoanID) AS totals FROM RepaymentPlan R,Loan L,MemberInfo M WHERE ").Append(filter);

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY CONVERT(CHAR(10),R.RePayTime,120)) AS rowNum,R.LoanID,").Append(fields);
            sql.Append(" FROM RepaymentPlan R,Loan L,MemberInfo M WHERE ").Append(filter).Append(sort);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");

            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null).Tables[0];
        }

        /// <summary>
        /// 还款报表
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>还款报表结果集</returns>
        /// <remarks>add 卢侠勇,2015-09-25</remarks>
        public DataTable GetRepaymentTotalList(string startDate, string endDate, int pageIndex, int pageSize, out int total)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@startTime", SqlDbType.Date){Value=startDate} ,        
			            new SqlParameter("@endTime", SqlDbType.Date){Value=endDate} ,        
			            new SqlParameter("@CurrentPage", SqlDbType.Int){Value=pageIndex} ,        
			            new SqlParameter("@PageSize", SqlDbType.Int){Value=pageSize} ,        
			            new SqlParameter("@RecordCount", SqlDbType.Int){Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_TotalRepayment", parameters).Tables[0];
            total = int.Parse(parameters[4].Value.ToString());
            return data;
        }

        /// <summary>
        /// 还款报表
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>还款报表结果集</returns>
        /// <remarks>add 卢侠勇,2015-09-25</remarks>
        public DataTable GetRepaymentTotalList(string startDate, string endDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT l.Agency,SUM(f.Amount) Amount FROM FundRecord F,Loan L");
            sql.Append(" WHERE F.LoanID=L.ID AND L.Agency <> '' AND L.Agency IS NOT NULL AND F.FeeType IN(4,7,8,9,10) AND F.Status=1");
            sql.Append(" AND CONVERT(CHAR(10),F.CreateTime,120)>=@startTime AND CONVERT(CHAR(10),F.CreateTime,120)<=@endTime GROUP BY L.Agency");
            SqlParameter[] parameters = {
			            new SqlParameter("@startTime", SqlDbType.Date){Value=startDate} ,        
			            new SqlParameter("@endTime", SqlDbType.Date){Value=endDate}
            };
            
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).Tables[0];
        }

        #region 红包报表
        /// <summary>
        /// 红包报表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>红包报表结果集</returns>
        /// <remarks>add 卢侠勇,2015-11-24</remarks>
        public DataTable GetRedenvelopeDetailsList(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(R.ID) AS totals FROM RedenvelopeDetails R,DimRedenvelope DR,Member M WHERE R.RedenvelopeID=DR.ID AND R.MemberID=M.ID ").Append(filter);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY R.ID) AS rowNum,DR.RedenvelopeName,DR.AmountType,R.*,(CASE WHEN R.Status=0 THEN '未使用' WHEN R.Status=1 THEN '已使用' WHEN R.Status=2 THEN '已过期' ELSE '作废' END) StatusStr,M.MemberName ");
            sql.Append(" FROM RedenvelopeDetails R,DimRedenvelope DR,Member M WHERE R.RedenvelopeID=DR.ID AND R.MemberID=M.ID ").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 红包类型
        /// <summary>
        /// 红包类型
        /// </summary>
        /// <returns>类型数据集</returns>
        /// <remarks>add 卢侠勇,2015-11-24</remarks>
        public DataTable GetRedenvelopeType()
        {
            string sql = "SELECT ID,RedenvelopeName FROM DimRedenvelope";
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 积分报表
        /// <summary>
        /// 积分报表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>红包报表结果集</returns>
        /// <remarks>add 卢侠勇,2015-11-27</remarks>
        public DataTable GetMemberPointsList(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(MPD.ID) AS totals FROM MemberPointsDetail MPD,DimMemberPointsSource DM,Member M,MemberPoints MP WHERE MPD.SourceID=DM.ID AND MPD.MemberID=M.ID AND MPD.MemberID=MP.MemberID ").Append(filter);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY MPD.ID) AS rowNum,M.MemberName,DM.Name,MPD.ID,(CASE WHEN MPD.ChangeType=0 THEN MPD.Points ELSE 0-MPD.Points END) PointsVal,MPD.Points,MPD.Notes,MPD.CreateTime,MP.Points AS PointsSum,MP.CurrentLevel ");
            sql.Append(" FROM MemberPointsDetail MPD,DimMemberPointsSource DM,Member M,MemberPoints MP WHERE MPD.SourceID=DM.ID AND MPD.MemberID=M.ID AND MPD.MemberID=MP.MemberID ").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 返现列表
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable GetCashBackList(string filter, int pageIndex, int pageSize, out int total)
        {

            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(1) as totals from (SELECT Amount,CreateTime,Description,CASE WHEN  PartyMemberID>0 THEN PartyMemberID ELSE PayeeMemberID END AS MemberId  FROM dbo.FundRecord  WHERE FeeType=31) a INNER JOIN dbo.Member b ON a.MemberId=b.ID ").Append(filter);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY ID) AS rowNum, *");
            sql.Append(" FROM (SELECT Amount,CreateTime,Description,CASE WHEN  PartyMemberID>0 THEN PartyMemberID ELSE PayeeMemberID END AS MemberId  FROM dbo.FundRecord  WHERE FeeType=31) a INNER JOIN dbo.Member b ON a.MemberId=b.ID ").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            return data == null ? new DataTable() : data.Tables[0];
        
        }


        #endregion
    }
}