/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 广告管理处理类
Update: 
**************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ManageFcmsConn;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ManageFcmsDal
{
    public class AdvertisementDAL
    {
        private static AdvertisementDAL _item;
        public static AdvertisementDAL Instance
        {
            get { return _item = (_item ?? new AdvertisementDAL()); }
        }

        #region 用户来源明细
        /// <summary>
        /// 用户来源明细
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-07</remarks>
        public DataTable GetUserSourceDetails(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT M.ID) AS totals FROM Member M LEFT JOIN DimChannel DC ON(M.Channel=DC.Id) ");
            sql.Append(" LEFT JOIN RechargeRecord RR ON(M.ID=RR.MemberID) ");
            sql.Append(" LEFT JOIN CashRecord CR ON(M.ID=CR.MemberID) ");
            sql.Append(" LEFT JOIN UserSource US ON(M.ID=US.memberId) WHERE M.ID<>-1 ").Append(filter);

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY M.ID) AS rowNum");
            sql.Append(",M.RegTime,M.RegTime RegTime2,M.MemberName,DC.Channel");
            sql.Append(",ISNULL(SUM(CASE WHEN RR.Status=1 THEN RR.Amount ELSE 0 END),0) RechargeAmount,ISNULL(SUM(CR.CashAmount),0) CashAmount");
            sql.Append(",(SELECT CASE WHEN COUNT(BA.ID)>0 THEN '是' ELSE '否' END FROM BankAccount BA WHERE BA.Status=1 AND BA.MemberID=M.ID) BankStatus");
            sql.Append(",(CASE WHEN M.CompleStatus>=2 THEN '是' ELSE '否' END) CompleStatus");
            sql.Append(",(CASE M.RegSource WHEN 0 THEN 'PC' WHEN 1 THEN 'WAP' WHEN 2 THEN 'IOS' ELSE 'Android' END) RegSource");
            sql.Append(",US.HostIP,US.VisitPreUrl,US.VisitUrl");
            sql.Append(" FROM Member M LEFT JOIN DimChannel DC ON(M.Channel=DC.Id)");
            sql.Append(" LEFT JOIN RechargeRecord RR ON(M.ID=RR.MemberID)");
            sql.Append(" LEFT JOIN CashRecord CR ON(M.ID=CR.MemberID)");
            sql.Append(" LEFT JOIN UserSource US ON(M.ID=US.memberId)");
            sql.Append(" WHERE M.ID<>-1 ").Append(filter);
            sql.Append(" GROUP BY M.ID,M.MemberName,M.RegTime,M.CompleStatus,M.RegSource,DC.Channel,US.HostIP,US.VisitPreUrl,US.VisitUrl");
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 渠道费用列表
        /// <summary>
        /// 渠道费用列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelFeeList(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) AS totals FROM DimChannel");
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY DC.Id) AS rowNum");
            sql.Append(",DC.Id,DC.Channel,DC.Rebate,ISNULL(CF.dayFee,0) dayFee,ISNULL(CF.zhouFee,0) zhouFee");
            sql.Append(",ISNULL(CF.monthFee,0) monthFee,ISNULL(CF.feeSum,0) feeSum,CF.createTime");
            sql.Append(",ISNULL((SELECT SUM(C.dayFee) FROM ChannelFee C WHERE C.channelID=CF.channelID").Append(filter).Append("),0) periodFee");
            sql.Append(" FROM DimChannel DC OUTER APPLY (SELECT TOP 1 * FROM ChannelFee CF WHERE CF.channelID=DC.ID ORDER BY CF.createTime DESC) CF");
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 渠道费用明细列表
        /// <summary>
        /// 渠道费用明细列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelFeeDetailsList(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) AS totals FROM ChannelFee CF");
            sql.Append(" WHERE 1=1 ").Append(filter);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY CF.createTime) AS rowNum");
            sql.Append(",DC.Channel,CF.id,CF.channelID,CF.dayFee,CF.zhouFee,CF.monthFee,CF.feeSum,CONVERT(CHAR(10),CF.createTime,120) createTime,CONVERT(CHAR(10),CF.updateTime,120) updateTime FROM ChannelFee CF,DimChannel DC");
            sql.Append(" WHERE CF.channelID=DC.Id ").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 获取渠道列表
        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelList() 
        {
            string sql = "SELECT * FROM DimChannel";
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 获取渠道费用详情
        /// <summary>
        /// 获取渠道费用详情
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelFee(string fields, string filter)
        {
            string sql = "SELECT TOP 1 " + fields + " FROM ChannelFee CF WHERE 1=1" + filter + " ORDER BY CF.createTime DESC";
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 添加渠道费用
        /// <summary>
        /// 添加渠道费用
        /// </summary>
        /// <param name="dic">渠道费用数据</param>
        /// <returns>-1 数据重复，0 失败， >0 成功</returns>
        /// <remarks>add 卢侠勇,2015-12-10</remarks>
        public int AddChannelFee(IDictionary<string, object> dic)
        {
            var dayCount = 0;
            var count = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ISNULL(SUM(CASE WHEN CONVERT(CHAR(10),CF.createTime,120)='").Append(dic["createTime"]).Append("' THEN 1 ELSE 0 END),0) dayCount,COUNT(CF.id) count FROM ChannelFee CF WHERE CF.channelID=").Append(dic["channelID"]);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                dayCount = reader["dayCount"] != DBNull.Value ? Convert.ToInt32(reader["dayCount"]) : 0;
                count = reader["count"] != DBNull.Value ? Convert.ToInt32(reader["count"]) : 0;
            }
            reader.Close();
            if (dayCount > 0)
                return -1;

            SqlParameter[] parameters = {
			            new SqlParameter("@channelID", SqlDbType.Int){Value=dic["channelID"]} ,   
			            new SqlParameter("@dayFee", SqlDbType.Decimal){Value=dic["dayFee"]} ,                            
                        new SqlParameter("@createTime", SqlDbType.Date){Value=dic["createTime"]}
            };
            sql.Clear();
            sql.Append("IF ").Append(count).Append("!=0");
            sql.Append(" BEGIN");
            sql.Append(" INSERT INTO ChannelFee VALUES(@channelID,@dayFee");
            sql.Append(",@dayFee+ISNULL((SELECT MAX(CASE WHEN datepart(week,createTime)=datepart(week,@createTime) THEN C.zhouFee ELSE 0 END) FROM ChannelFee C WHERE C.channelID=@channelID AND createTime<@createTime),0)");
            sql.Append(",@dayFee+ISNULL((SELECT MAX(CASE WHEN datepart(MM,createTime)=datepart(MM,@createTime) THEN C.monthFee ELSE 0 END) FROM ChannelFee C WHERE C.channelID=@channelID AND createTime<@createTime),0)");
            sql.Append(",@dayFee+ISNULL((SELECT MAX(C.feeSum) FROM ChannelFee C WHERE C.channelID=@channelID AND createTime<@createTime),0),GETDATE(),@createTime");
            sql.Append(")");
            sql.Append(" END");
            sql.Append(" ELSE");
            sql.Append(" BEGIN");
            sql.Append(" INSERT INTO ChannelFee VALUES(@channelID,@dayFee,@dayFee,@dayFee,@dayFee,GETDATE(),@createTime)");
            sql.Append(" END");
            sql.Append(" UPDATE ChannelFee SET zhouFee+= CASE WHEN datepart(week,createTime)=datepart(week,@createTime) THEN @dayFee ELSE 0 END");
            sql.Append(",monthFee+= CASE WHEN datepart(MM,createTime)=datepart(MM,@createTime) THEN @dayFee ELSE 0 END,feeSum+=@dayFee");
            sql.Append(",updateTime=GETDATE() WHERE channelID=@channelID AND createTime>@createTime");
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters);
        }
        #endregion

        #region 修改渠道费用
        /// <summary>
        /// 修改渠道费用
        /// </summary>
        /// <param name="dic">渠道费用数据</param>
        /// <returns></returns>
        /// <returns>0 失败， >0 成功</returns>
        /// <remarks>add 卢侠勇,2015-12-10</remarks>
        public int SetChannelFee(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DECLARE @channelID int,@createTime datetime");
            sql.Append(" UPDATE ChannelFee SET dayFee=@dayFee,zhouFee+=@dayFee-@originalDayFee,monthFee+=@dayFee-@originalDayFee");
            sql.Append(",feeSum+=@dayFee-@originalDayFee,updateTime=GETDATE(),@channelID=channelID,@createTime=createTime WHERE id=@id");
            sql.Append(" UPDATE ChannelFee SET zhouFee+= CASE WHEN datepart(week,createTime)=datepart(week,@createTime) THEN @dayFee-@originalDayFee ELSE 0 END");
            sql.Append(",monthFee+= CASE WHEN datepart(MM,createTime)=datepart(MM,@createTime) THEN @dayFee-@originalDayFee ELSE 0 END,feeSum+=@dayFee-@originalDayFee");
            sql.Append(",updateTime=GETDATE() WHERE channelID=@channelID AND createTime>@createTime");
            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int){Value=dic["id"]} ,   
			            new SqlParameter("@dayFee", SqlDbType.Decimal){Value=dic["dayFee"]},   
			            new SqlParameter("@originalDayFee", SqlDbType.Decimal){Value=dic["originalDayFee"]}
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters);
        }
        #endregion

        #region 投资用户明细
        /// <summary>
        /// 投资用户明细
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-11</remarks>
        public DataTable GetBiddingUserDetailsList(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(M.ID) AS totals");
            sql.Append(" FROM Member M LEFT JOIN DimChannel DC ON(M.Channel=DC.Id)");
            sql.Append(" CROSS APPLY (SELECT TOP 1 B.BidAmount,B.CreateTime,B.BidType FROM Bidding B,Loan L WHERE B.LoanID=L.ID AND L.LoanTypeID<>6 AND M.ID=B.MemberID ORDER BY B.CreateTime) B");
            sql.Append(" OUTER APPLY (SELECT TOP 1 R.ApplicationTime,R.RechargeChannel FROM RechargeRecord R WHERE R.MemberID=M.ID AND R.Status=1 ORDER BY R.ApplicationTime) R");
            sql.Append(" LEFT JOIN UserSource U ON(M.ID=U.memberId) WHERE 1=1").Append(filter);
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY B.CreateTime) AS rowNum");
            sql.Append(",M.MemberName,DC.Channel,B.BidAmount,B.CreateTime,B.CreateTime CreateTime2");
            sql.Append(",(CASE M.RegSource WHEN 0 THEN 'PC' WHEN 1 THEN 'WAP' WHEN 2 THEN 'IOS' WHEN 3 THEN 'Android' ELSE '' END) RegSource");
            sql.Append(",(CASE B.BidType WHEN 0 THEN 'WEB' WHEN 1 THEN '自动竞标' WHEN 2 THEN 'IOS' WHEN 3 THEN 'Android' WHEN 4 THEN 'WAP' ELSE '' END) BidType");
            sql.Append(",(DATEDIFF(day,M.RegTime,R.ApplicationTime)) payGap");
            sql.Append(",(CASE R.RechargeChannel WHEN 0 THEN '线下充值' WHEN 1 THEN '通联支付' WHEN 2 THEN '通联移动支付（IOS）' WHEN 3 THEN '通联移动支付（Android）' WHEN 4 THEN '通联WAP支付' WHEN 5 THEN '连连支付' WHEN 6 THEN '连连移动支付（IOS）' WHEN 7 THEN '连连移动支付（Android）' WHEN 8 THEN '连连WAP支付' ELSE '' END) RechargeChannel");
            sql.Append(",DATEDIFF(day,M.RegTime,B.CreateTime) investGap,U.VisitPreUrl,U.VisitUrl");
            sql.Append(" FROM Member M LEFT JOIN DimChannel DC ON(M.Channel=DC.Id)");
            sql.Append(" CROSS APPLY (SELECT TOP 1 B.BidAmount,B.CreateTime,B.BidType FROM Bidding B,Loan L WHERE B.LoanID=L.ID AND L.LoanTypeID<>6 AND M.ID=B.MemberID ORDER BY B.CreateTime) B");
            sql.Append(" OUTER APPLY (SELECT TOP 1 R.ApplicationTime,R.RechargeChannel FROM RechargeRecord R WHERE R.MemberID=M.ID AND R.Status=1 ORDER BY R.ApplicationTime) R");
            sql.Append(" LEFT JOIN UserSource U ON(M.ID=U.memberId) WHERE 1=1").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 渠道号列表
        /// <summary>
        /// 渠道号列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public DataTable GetChannelList(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DC.Id) AS totals FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) ");
            sql.Append(" LEFT JOIN (SELECT * FROM DimChannelAttach WHERE rootID>=10000) A ON(DCA.id=A.rootID) WHERE 1=1 ").Append(filter);

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY DC.Id) AS rowNum");
            sql.Append(",DC.Id dId,DCA.id,A.id fId,DC.Channel,DCA.classifyName,A.classifyName fClassifyName");
            sql.Append(" FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID)");
            sql.Append(" LEFT JOIN (SELECT * FROM DimChannelAttach WHERE rootID>=10000) A ON(DCA.id=A.rootID)");
            sql.Append(" WHERE 1=1 ").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 获取渠道号详情
        /// <summary>
        /// 获取渠道号详情
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannel(string fields, string filter)
        {
            string sql = "SELECT TOP 1 " + fields + " FROM DimChannel DC LEFT JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) LEFT JOIN (SELECT * FROM DimChannelAttach WHERE rootID>=10000) A ON(DCA.id=A.rootID) WHERE 1=1" + filter;
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 添加渠道
        /// <summary>
        /// 添加渠道
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public string AddChannel(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("IF (SELECT COUNT(*) FROM DimChannel WHERE Channel = @channel)=0");
            sql.Append(" BEGIN");
            sql.Append(" INSERT INTO DimChannel() VALUES((SELECT TOP 1 Id+1 FROM DimChannel ORDER BY Id DESC),@channel,@rebate)");
            sql.Append(" IF @@rowcount > 0");
            sql.Append(" SELECT '1|添加成功'");
            sql.Append(" ELSE");
            sql.Append("  SELECT '2|添加失败'");
            sql.Append(" END");
            sql.Append(" ELSE");
            sql.Append(" BEGIN");
            sql.Append(" SELECT '3|渠道名已存在'");
            sql.Append(" END");

            SqlParameter[] parameters = {
			            new SqlParameter("@channel", SqlDbType.VarChar,20){Value=dic["channel"]} ,                       
                        new SqlParameter("@rebate", SqlDbType.Decimal){Value=dic["rebate"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 添加渠道二级分类
        /// <summary>
        /// 添加渠道二级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public string AddFoxproChannel(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("IF (SELECT COUNT(DC.Id) FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) WHERE DC.Id=@id AND DCA.classifyName=@classifyName)=0");
            sql.Append(" BEGIN");
            sql.Append(" INSERT INTO DimChannelAttach VALUES(@id,@classifyName)");
            sql.Append(" IF @@rowcount > 0");
            sql.Append(" SELECT '1|添加成功'");
            sql.Append(" ELSE");
            sql.Append("  SELECT '2|添加失败'");
            sql.Append(" END");
            sql.Append(" ELSE");
            sql.Append(" BEGIN");
            sql.Append(" SELECT '3|渠道分类已存在'");
            sql.Append(" END");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int){Value=dic["id"]} ,                       
                        new SqlParameter("@classifyName", SqlDbType.VarChar,20){Value=dic["classifyName"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 添加渠道三级分类
        /// <summary>
        /// 添加渠道三级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public string AddFlyersChannel(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("IF (SELECT COUNT(DC.Id) FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) WHERE DC.Id=@id AND DCA.classifyName=@classifyName)>0");
            sql.Append(" BEGIN");
            sql.Append("  IF (SELECT COUNT(DCA.id) FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) INNER JOIN (SELECT * FROM DimChannelAttach WHERE rootID>=10000) A ON(DCA.id=A.rootID) WHERE DC.Id=@id AND DCA.classifyName=@classifyName AND A.classifyName=@fClassifyName)=0");
            sql.Append("  BEGIN");
            sql.Append("   INSERT INTO DimChannelAttach VALUES((SELECT id FROM DimChannelAttach WHERE classifyName=@classifyName AND rootID=@id),@fClassifyName)");
            sql.Append("  IF @@rowcount > 0");
            sql.Append("   SELECT '1|添加成功'");
            sql.Append("  ELSE");
            sql.Append("   SELECT '2|添加失败'");
            sql.Append("  END");
            sql.Append("  ELSE");
            sql.Append("  BEGIN");
            sql.Append("   SELECT '3|渠道名已存在'");
            sql.Append("  END");
            sql.Append(" END");
            sql.Append(" ELSE");
            sql.Append(" BEGIN");
            sql.Append("  SELECT '3|二级渠道不存在'");
            sql.Append(" END");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int){Value=dic["id"]} ,                       
                        new SqlParameter("@classifyName", SqlDbType.VarChar,20){Value=dic["classifyName"]} ,                       
                        new SqlParameter("@fClassifyName", SqlDbType.VarChar,20){Value=dic["fClassifyName"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 修改渠道
        /// <summary>
        /// 修改渠道
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetChannel(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE DimChannel SET Channel=@channel,Rebate=@rebate WHERE Id=@id");
            sql.Append(" IF @@rowcount > 0");
            sql.Append("  SELECT '1|修改成功'");
            sql.Append(" ELSE");
            sql.Append("  SELECT '2|修改失败'");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int){Value=dic["id"]} ,  
			            new SqlParameter("@channel", SqlDbType.VarChar,20){Value=dic["channel"]} ,                       
                        new SqlParameter("@rebate", SqlDbType.Decimal){Value=dic["rebate"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 修改渠道二级分类
        /// <summary>
        /// 修改渠道二级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetFoxproChannel(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("IF (SELECT COUNT(DC.Id) FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) WHERE DC.Id=(SELECT rootID FROM DimChannelAttach WHERE id=@id) AND DCA.classifyName=@classifyName)=0");
            sql.Append(" BEGIN");
            sql.Append(" UPDATE DimChannelAttach SET classifyName=@classifyName WHERE id=@id");
            sql.Append(" IF @@rowcount > 0");
            sql.Append("  SELECT '1|修改成功'");
            sql.Append(" ELSE");
            sql.Append("  SELECT '2|修改失败'");
            sql.Append(" END");
            sql.Append(" ELSE");
            sql.Append(" BEGIN");
            sql.Append("  SELECT '3|渠道分类已存在'");
            sql.Append(" END");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int){Value=dic["id"]} ,                       
                        new SqlParameter("@classifyName", SqlDbType.VarChar,20){Value=dic["classifyName"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 修改渠道三级分类
        /// <summary>
        /// 修改渠道三级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetFlyersChannel(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("IF (SELECT COUNT(DC.Id) FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) WHERE DC.Id=@did AND DCA.classifyName=@classifyName)>0");
            sql.Append(" BEGIN");
            sql.Append("  IF (SELECT COUNT(DCA.id) FROM DimChannel DC INNER JOIN DimChannelAttach DCA ON(DC.Id=DCA.rootID) INNER JOIN (SELECT * FROM DimChannelAttach WHERE rootID>=10000) A ON(DCA.id=A.rootID) WHERE DC.Id=@did AND DCA.classifyName=@classifyName AND A.classifyName=@fClassifyName)=0");
            sql.Append("  BEGIN");
            sql.Append("   UPDATE DimChannelAttach SET classifyName=@fClassifyName,rootID=(SELECT D.id FROM DimChannelAttach D WHERE D.classifyName=@classifyName AND D.rootID=@did) WHERE id=@id");
            sql.Append("  IF @@rowcount > 0");
            sql.Append("   SELECT '1|修改成功'");
            sql.Append("  ELSE");
            sql.Append("   SELECT '2|修改失败'");
            sql.Append("  END");
            sql.Append("  ELSE");
            sql.Append("  BEGIN");
            sql.Append("   SELECT '3|渠道名已存在'");
            sql.Append("  END");
            sql.Append(" END");
            sql.Append(" ELSE");
            sql.Append(" BEGIN");
            sql.Append("  SELECT '3|二级渠道不存在'");
            sql.Append(" END");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int){Value=dic["id"]} ,                 
			            new SqlParameter("@did", SqlDbType.Int){Value=dic["did"]} ,                 
                        new SqlParameter("@classifyName", SqlDbType.VarChar,20){Value=dic["classifyName"]} ,                       
                        new SqlParameter("@fClassifyName", SqlDbType.VarChar,20){Value=dic["fClassifyName"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 渠道商帐户列表
        /// <summary>
        /// 渠道商帐户列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public DataTable GetChannelUserList(string filter, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(CU.Id) AS totals FROM ChannelUser CU JOIN DimChannel DC ON(CU.DimChannelId=DC.Id) ").Append(filter);

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY CU.Id) AS rowNum");
            sql.Append(",CU.*,DC.Channel");
            sql.Append(" FROM ChannelUser CU JOIN DimChannel DC ON(CU.DimChannelId=DC.Id)");
            sql.Append(" WHERE 1=1 ").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 添加渠道商帐户
        /// <summary>
        /// 添加渠道商帐户
        /// </summary>
        /// <param name="dic">渠道商数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string AddChannelUser(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("IF (SELECT COUNT(Id) FROM ChannelUser WHERE ChannelName=@channelName)=0");
            sql.Append(" BEGIN");
            sql.Append(" INSERT INTO ChannelUser VALUES(@channelName,@channelPwd,@channelId,@contactPerson,@permissions,@registeredFormula,@investFormula,@regInvestFormula,@investQuota,1,GETDATE())");
            sql.Append(" IF @@rowcount > 0");
            sql.Append("  SELECT '1|添加成功'");
            sql.Append(" ELSE");
            sql.Append("  SELECT '2|添加失败'");
            sql.Append(" END");
            sql.Append(" ELSE");
            sql.Append(" BEGIN");
            sql.Append("  SELECT '3|用户名已存在'");
            sql.Append(" END");

            SqlParameter[] parameters = {
			            new SqlParameter("@channelName", SqlDbType.VarChar,50){Value=dic["channelName"]} ,                       
                        new SqlParameter("@channelPwd", SqlDbType.VarChar,50){Value=dic["channelPwd"]} ,                       
                        new SqlParameter("@channelId", SqlDbType.Int){Value=dic["channelId"]} ,                       
                        new SqlParameter("@contactPerson", SqlDbType.VarChar,50){Value=dic["contactPerson"]} ,                       
                        new SqlParameter("@permissions", SqlDbType.VarChar,50){Value=dic["permissions"]} ,                       
                        new SqlParameter("@registeredFormula", SqlDbType.VarChar,50){Value=dic["registeredFormula"]} ,                       
                        new SqlParameter("@investFormula", SqlDbType.VarChar,50){Value=dic["investFormula"]} ,                       
                        new SqlParameter("@regInvestFormula", SqlDbType.VarChar,50){Value=dic["regInvestFormula"]} ,                       
                        new SqlParameter("@investQuota", SqlDbType.VarChar,50){Value=dic["investQuota"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 查询渠道商帐户信息
        /// <summary>
        /// 查询渠道商帐户信息
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public DataTable GetChannelUser(string fields, string filter)
        {
            string sql = "SELECT " + fields + " FROM ChannelUser CU WHERE 1=1" + filter + "";
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 修改渠道商帐户
        /// <summary>
        /// 修改渠道商帐户
        /// </summary>
        /// <param name="dic">渠道商数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetChannelUser(IDictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE ChannelUser SET DimChannelId=@channelId,ContactPerson=@contactPerson,[Permissions]=@permissions,RegisteredFormula=@registeredFormula,InvestFormula=@investFormula,RegInvestFormula=@regInvestFormula,InvestQuota=@investQuota,IsDelete=@isDelete WHERE Id=@Id");
            sql.Append(" IF @@rowcount > 0");
            sql.Append("  SELECT '1|修改成功'");
            sql.Append(" ELSE");
            sql.Append("  SELECT '2|修改失败'");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int){Value=dic["Id"]} ,                       
                        new SqlParameter("@isDelete", SqlDbType.Int){Value=dic["isDelete"]} ,                       
                        new SqlParameter("@channelId", SqlDbType.Int){Value=dic["channelId"]} ,                       
                        new SqlParameter("@contactPerson", SqlDbType.VarChar,50){Value=dic["contactPerson"]} ,                       
                        new SqlParameter("@permissions", SqlDbType.VarChar,50){Value=dic["permissions"]} ,                       
                        new SqlParameter("@registeredFormula", SqlDbType.VarChar,50){Value=dic["registeredFormula"]} ,                       
                        new SqlParameter("@investFormula", SqlDbType.VarChar,50){Value=dic["investFormula"]} ,                       
                        new SqlParameter("@regInvestFormula", SqlDbType.VarChar,50){Value=dic["regInvestFormula"]} ,                       
                        new SqlParameter("@investQuota", SqlDbType.VarChar,50){Value=dic["investQuota"]}
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parameters).ToString();
        }
        #endregion

        #region 获取渠道效果分析列表
        /// <summary>
        /// 获取渠道效果分析列表
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-18</remarks>
        public DataTable GetChannelAnalyzeList(string fields, string filter, string order, int pageIndex, int pageSize, out int total)
        {
            total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) AS totals FROM (SELECT DC.Channel FROM DimChannel DC CROSS APPLY (SELECT * FROM Member M WHERE M.Channel=DC.Id) A ");
            sql.Append(" OUTER APPLY (SELECT B.* FROM Bidding B,Loan L WHERE B.LoanID=L.ID AND L.LoanTypeID<>6 AND B.MemberID=A.ID AND B.BidStatus=1) B ");
            sql.Append(" OUTER APPLY (SELECT * FROM RechargeRecord R WHERE R.MemberID=A.ID AND R.Status=1) R ");
            sql.Append(" OUTER APPLY (SELECT * FROM RepaymentPlan RE WHERE R.MemberID=A.ID AND (RE.OverStatus=0 OR RE.OverStatus=1)) RE WHERE 1=1 ").Append(filter);
            sql.Append(") S");
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();

            sql.Clear();
            sql.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY ").Append(order).Append(") AS rowNum,");
            sql.Append(fields);
            sql.Append(" FROM DimChannel DC CROSS APPLY (SELECT * FROM Member M WHERE M.Channel=DC.Id) A");
            sql.Append(" OUTER APPLY (SELECT B.* FROM Bidding B,Loan L WHERE B.LoanID=L.ID AND L.LoanTypeID<>6 AND B.MemberID=A.ID AND B.BidStatus=1) B");
            sql.Append(" OUTER APPLY (SELECT * FROM RechargeRecord R WHERE R.MemberID=A.ID AND R.Status=1) R");
            sql.Append(" OUTER APPLY (SELECT * FROM RepaymentPlan RE WHERE R.MemberID=A.ID AND (RE.OverStatus=0 OR RE.OverStatus=1)) RE WHERE 1=1 ").Append(filter);
            sql.Append(") AS TEMP WHERE rowNum>" + (pageSize * (pageIndex - 1)) + " AND rowNum<=" + (pageSize * pageIndex) + "");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion

        #region 渠道号导入
        /// <summary>
        /// 渠道号导入
        /// </summary>
        /// <param name="dt">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-22</remarks>
        public string ImportChannel(DataTable dt)
        {
            try
            {
                //渠道表
                DataTable channels = new DataTable();
                channels.Columns.Add("Id", typeof(int));
                channels.Columns.Add("Channel", typeof(string));
                channels.Columns.Add("Rebate", typeof(decimal));

                //二级分类
                DataTable channels2 = new DataTable();
                channels2.Columns.Add("rootID", typeof(int));
                channels2.Columns.Add("classifyName", typeof(string));

                //三级分类
                DataTable channels3 = new DataTable();
                channels3.Columns.Add("rootID", typeof(int));
                channels3.Columns.Add("classifyName", typeof(string));

                StringBuilder sql = new StringBuilder();

                #region 更新渠道
                sql.Append("SELECT * FROM DimChannel ORDER BY Id DESC");
                var chaneel = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
                if (chaneel != null)
                {
                    var cid = Convert.ToInt32(chaneel.Tables[0].Rows[0]["Id"]);
                    var cNames = chaneel.Tables[0].AsEnumerable().Select(p => p.Field<string>("Channel"));
                    var channelNews = dt.AsEnumerable().Where(p => !cNames.Contains(p.Field<string>("一级分类"))).Select(p => p.Field<string>("一级分类")).Distinct();
                    foreach (var name in channelNews)
                    {
                        cid++;
                        var dr = channels.NewRow();
                        dr["Id"] = cid;
                        dr["Channel"] = name;
                        dr["Rebate"] = 0;
                        channels.Rows.Add(dr);
                    }

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlHelper.ConnectionStringLocal);
                    sqlBulkCopy.DestinationTableName = "DimChannel";
                    if (channels != null && channels.Rows.Count != 0)
                    {
                        sqlBulkCopy.WriteToServer(channels);
                    }
                    sqlBulkCopy.Close();

                    channels.Merge(chaneel.Tables[0]);
                }
                sql.Clear();
                #endregion

                #region 更新二级渠道
                sql.Append("SELECT * FROM DimChannelAttach WHERE rootID<10000");
                var chaneelAttach = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
                if (chaneelAttach != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["一级分类"].ToString() != "")
                        {
                            var dr = channels2.NewRow();
                            var name = row["一级分类"].ToString();
                            var classifyName = row["二级分类"].ToString();
                            var rootID = channels.AsEnumerable().Where(p => p.Field<string>("Channel") == name).FirstOrDefault().Field<int>("Id");
                            dr["rootID"] = rootID;
                            dr["classifyName"] = classifyName;

                            //不存在数据中则添加
                            if (chaneelAttach.Tables[0].AsEnumerable().Where(p => p.Field<string>("classifyName") == classifyName && p.Field<int>("rootID") == rootID).Count() == 0)
                                channels2.Rows.Add(dr);
                        }
                    }

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlHelper.ConnectionStringLocal);
                    sqlBulkCopy.DestinationTableName = "DimChannelAttach";
                    sqlBulkCopy.ColumnMappings.Add("rootID", "rootID");
                    sqlBulkCopy.ColumnMappings.Add("classifyName", "classifyName");
                    if (channels2 != null && channels2.Rows.Count != 0)
                    {
                        DataView dv = new DataView(channels2);
                        sqlBulkCopy.WriteToServer(dv.ToTable(true));//去重复添加
                        channels2.Clear();
                        channels2 = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null).Tables[0];
                    }
                    else
                        channels2 = chaneelAttach.Tables[0];
                    sqlBulkCopy.Close();
                }
                sql.Clear();
                #endregion

                #region 更新三级渠道
                sql.Append("SELECT * FROM DimChannelAttach WHERE rootID>=10000");
                var chaneelAttach2 = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), null);
                if (chaneelAttach2 != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["一级分类"].ToString() != "")
                        {
                            var dr = channels3.NewRow();
                            var cname = row["一级分类"].ToString();
                            var name = row["二级分类"].ToString();
                            var classifyName = row["三级分类"].ToString();
                            //获取一级渠道ID
                            var cid = channels.AsEnumerable().Where(p => p.Field<string>("Channel") == cname).FirstOrDefault().Field<int>("Id");
                            //获取二级渠道ID
                            var rootID = channels2.AsEnumerable().Where(p => p.Field<string>("classifyName") == name && p.Field<int>("rootID") == cid).FirstOrDefault().Field<int>("id");
                            dr["rootID"] = rootID;
                            dr["classifyName"] = classifyName;

                            //不存在数据中则添加
                            if (chaneelAttach2.Tables[0].AsEnumerable().Where(p => p.Field<string>("classifyName") == classifyName && p.Field<int>("rootID") == rootID).Count() == 0)
                                channels3.Rows.Add(dr);
                        }
                    }

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlHelper.ConnectionStringLocal);
                    sqlBulkCopy.DestinationTableName = "DimChannelAttach";
                    sqlBulkCopy.ColumnMappings.Add("rootID", "rootID");
                    sqlBulkCopy.ColumnMappings.Add("classifyName", "classifyName");
                    if (channels3 != null && channels3.Rows.Count != 0)
                    {
                        DataView dv = new DataView(channels3);
                        sqlBulkCopy.WriteToServer(dv.ToTable(true));//去重复添加
                    }
                    sqlBulkCopy.Close();
                }
                #endregion

                return "1|数据导入成功!";
            }
            catch (Exception ex) { return "2|数据导入失败!"; }            
        }
        #endregion
    }
}
