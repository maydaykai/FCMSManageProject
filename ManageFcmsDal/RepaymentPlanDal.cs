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
    public class RepaymentPlanDal
    {

        /// <summary>
        /// 获取我的收款中的投标详细
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="peNumber"></param>
        /// <returns></returns>
        public RepaymentPlanModel GetRepaymentPlanList(int loanId, int peNumber)
        {
            var model = new RepaymentPlanModel();
            string sql = "select * from RepaymentPlan where LoanID=" + loanId + " and PeNumber=" + peNumber;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql);
                while (dr.Read())
                {
                    model.PeNumber = dr["PeNumber"] != DBNull.Value ? Convert.ToInt32(dr["PeNumber"]) : 0;
                    model.Status = dr["Status"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : 0;
                    model.RePrincipal = dr["RePrincipal"] != DBNull.Value ? Convert.ToDecimal(dr["RePrincipal"]) : 0;
                    model.ReInterest = dr["ReInterest"] != DBNull.Value ? Convert.ToDecimal(dr["ReInterest"]) : 0;
                    model.ReOverInterest = dr["ReOverInterest"] != DBNull.Value ? Convert.ToDecimal(dr["ReOverInterest"]) : 0;
                    model.SurPrincipal = dr["SurPrincipal"] != DBNull.Value ? Convert.ToDecimal(dr["SurPrincipal"]) : 0;
                    model.SurReInterest = dr["SurReInterest"] != DBNull.Value ? Convert.ToDecimal(dr["SurReInterest"]) : 0;
                    model.SurOverInterest = dr["SurOverInterest"] != DBNull.Value ? Convert.ToDecimal(dr["SurOverInterest"]) : 0;
                    model.RePayTime = dr["RePayTime"] != DBNull.Value ? Convert.ToDateTime(dr["RePayTime"]) : PublicConst.MinDate;
                    model.OverStatus = dr["OverStatus"] != DBNull.Value ? Convert.ToInt32(dr["OverStatus"]) : 0;
                }
                dr.Close();
            }
            catch (Exception)
            {

            }
            return model;
        }

        /// <summary>
        /// 根据借款ID获取还款计划
        /// </summary>
        public DataTable GetRepaymentPlanByLoanID(int loanId)
        {
            var model = new RepaymentPlanModel();
            var sqlStr = new StringBuilder();
            sqlStr.Append(" SELECT PeNumber,RePayDate=CONVERT(varchar(10), RePayTime, 23),RePrincipal=SUM(RePrincipal),ReInterest=SUM(ReInterest),RemainAmount=(SELECT ISNULL(SUM(RePrincipal+ReInterest),0) from dbo.RepaymentPlan B where LoanID=@LoanID AND [Status]<2 AND B.PeNumber>A.PeNumber)");
            sqlStr.Append(" FROM dbo.RepaymentPlan A");
            sqlStr.Append(" WHERE LoanID=@LoanID AND [Status]<2");
            sqlStr.Append(" GROUP BY PeNumber,CONVERT(varchar(10), RePayTime, 23)");
            sqlStr.Append(" ORDER BY PeNumber");

            SqlParameter[] parameters =
                {
                    new SqlParameter("@LoanID",SqlDbType.Int,4){Value = loanId}
                };

            var dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlStr.ToString(), parameters).Tables[0];

            return dt;
        }

        /// <summary>
        /// 根据会员ID获取待收总额
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public decimal GetDueInMoneyByMemberId(int memberId)
        {
            const string sql = "SELECT [dbo].[GetMemberAccountPayable](@MemberID);";
            SqlParameter[] paras = 
                {
                    new SqlParameter("@MemberID", SqlDbType.Int,4){Value=memberId},
                };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras);
            return obj == null ? 0 : Convert.ToDecimal(obj);
        }

        /// <summary>
        /// [还款明细数据]查看提前还款信息
        /// </summary>
        /// <param name="loanId">借款ID</param>
        /// <param name="type">还款类型：0:逾期还款 1:正常还款 2:提前还款</param>
        /// <returns>还款信息数据</returns>
        /// <remarks>add 卢侠勇,2015-08-26</remarks>
        public DataTable GetRepaymentAmount(int loanId, int type)
        {
            SqlParameter[] parameters =
                {
                    new SqlParameter("@LoanID",SqlDbType.Int,4){Value = loanId},
                    new SqlParameter("@RepaymentType",SqlDbType.Int,4){Value = type}
                };

            var dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "ProcRepaymentDetail", parameters).Tables[0];

            return dt;
        }

        /// <summary>
        /// 提取最近一期未还的期号
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public int GetLatelyNotPeNumber(int loanId)
        { 
            string sql = "select top 1 penumber from dbo.RepaymentPlan where loanid = @LoanID and status < 2 order by penumber";
             SqlParameter[] parameters =
                {
                    new SqlParameter("@LoanID",SqlDbType.Int,4){Value = loanId}
                };

             var peNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters));
             return peNumber;
        }

        /// <summary>
        /// 写入审批申请垫付记录
        /// </summary>
        /// <param name="loanId">贷款ID</param>
        /// <param name="peNumber">期限</param>
        /// <param name="_operator">操作人</param>
        /// <param name="currStep">操作步骤</param>
        /// <param name="currStep">返回消息</param>
        /// <returns></returns>
        /// <remarks>add wangzhe,2015-12-03</remarks>
        public bool InsertApproveAppayAdvanceInfo(int loanId, int peNumber, int _operator, ref string message)
        {
            bool flag = false;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@loanId",SqlDbType.Int,4),
                    new SqlParameter("@peNumber",SqlDbType.Int,4),
                    new SqlParameter("@operator",SqlDbType.Int,4),
                    new SqlParameter("@message",SqlDbType.VarChar,500)

                };
            parameters[0].Value = loanId;
            parameters[1].Value = peNumber;
            parameters[2].Value = _operator;
            parameters[3].Direction = ParameterDirection.Output;

            try
            {

                int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "ProcAdvanceApproveAdd", parameters));
                
                flag = true;
                message = parameters[3].Value.ToString();
               
            }
            catch
            {

            }
            return flag;

        }
    }
}
