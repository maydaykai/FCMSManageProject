/*************************************************************
Author:		 卢侠勇
Create date: 2015-7-14
Description: 报表逻辑类
Update: 
**************************************************************/
using System;
using System.Data;
using ManageFcmsDal;
using System.Text;

namespace ManageFcmsBll
{
    public class ReportBll
    {
        private readonly MemberDal _dal = new MemberDal();
        private static ReportBll _item;
        public static ReportBll Instance
        {
            get { return _item = (_item ?? new ReportBll()); }
        }

        /// <summary>
        /// 广告统计
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="minBalance">最小可用余额</param>
        /// <param name="maxBalance">最大可用余额</param>
        /// <param name="channelRemark">广告来源</param>
        /// <param name="channel">广告渠道</param>
        /// <param name="revStatus">回访状态：0全部 -1未回访 1已回访</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>广告统计结果集</returns>
        /// <remarks>add 卢侠勇,2015-7-14</remarks>
        public DataTable GetAdvertisingList(DateTime dateStart, DateTime endDate, int minBalance, int maxBalance, string channelRemark, int channel, int revStatus, int currentPage, int pageSize, out int total)
        {
            var filter = string.Empty;
            var sql = string.Empty;
            if (!string.IsNullOrEmpty(channelRemark))
                sql += " AND ChannelRemark like '%" + channelRemark + "%'";
            if (channel != -1)
                sql += " AND Channel=" + channel + "";

            sql += " AND Balance>=" + minBalance;
            if(maxBalance>0)
                sql += " AND Balance<=" + maxBalance;

            filter = string.Format("CONVERT(CHAR(10),RegTime,120)>='{0}' AND CONVERT(CHAR(10),RegTime,120)<='{1}'{2}{3}", dateStart.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), revStatus, sql);
            return _dal.SummaryStatistics("Proc_TotalAdvertising", currentPage, pageSize, filter, out total);
        }

        /// <summary>
        /// 发标统计
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>发标统计结果集</returns>
        /// <remarks>add 卢侠勇,2015-7-15</remarks>
        public DataTable GetLoanDetailsList(DateTime dateStart, DateTime endDate, int currentPage, int pageSize, out int total)
        {
            var filter = string.Empty;
            filter = string.Format("CONVERT(CHAR(10),CreateTime,120)>='{0}' AND CONVERT(CHAR(10),CreateTime,120)<='{1}' AND ExamStatus>4", dateStart.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            return _dal.SummaryStatistics("Proc_TotalLoanDetails", currentPage, pageSize, filter, out total);            
        }

        /// <summary>
        /// 平台资金情况
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>发标统计结果集</returns>
        /// <remarks>add 卢侠勇,2015-7-16</remarks>
        public DataTable GetFundRecordList(DateTime dateStart, DateTime endDate, int currentPage, int pageSize, out int total)
        {
            var filter = string.Empty;
            filter = string.Format("CreateTime>='{0}' AND CreateTime<='{1}'", dateStart.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            return _dal.SummaryStatistics("Proc_TotalP2PFundRecord", currentPage, pageSize, filter, out total);
        }

        /// <summary>
        /// 客户流失情况
        /// </summary>
        /// <param name="dateStart">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>客户流失情况结果集</returns>
        /// <remarks>add 卢侠勇,2015-7-20</remarks>
        public DataTable GetUserDrainDetailsList(DateTime? dateStart, DateTime? endDate, int currentPage, int pageSize, out int total)
        {
            var filter = string.Empty;
            filter = (dateStart != null && endDate != null) ? "CONVERT(CHAR(10),RegTime,120)>='" + dateStart.Value.ToString("yyyy-MM-dd") + "' AND CONVERT(CHAR(10),RegTime,120)<='" + endDate.Value.ToString("yyyy-MM-dd") + "'" : "1=1";
            return _dal.SummaryStatistics("Proc_TotalUserDrainDetails", currentPage, pageSize, filter, out total);
        }

        /// <summary>
        /// 日期总表
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>日期总表结果集</returns>
        /// <remarks>add 卢侠勇,2015-7-27</remarks>
        public DataTable GetDateDataTotalList(DateTime dateStart, DateTime endDate, int currentPage, int pageSize, out int total)
        {
            var filter = string.Empty;
            filter = string.Format("RegTime>='{0}' AND RegTime<='{1}'", dateStart.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            return _dal.SummaryStatistics("Proc_TotalData", currentPage, pageSize, filter, out total);
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
            return ReportDal.Instance.AddReturnVisit(memberId, operatorId, record, notes);
        }

        /// <summary>
        /// 客服回访记录
        /// </summary>
        /// <param name="memberId">会员ID</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>客服回访记录</returns>
        /// <remarks>add 卢侠勇,2015-08-04</remarks>
        public DataTable GetReturnVisitList(int memberId,int currentPage, int pageSize, out int total)
        {
            var filter = string.Empty;
            filter = string.Format("memberId={0}", memberId);
            return _dal.SummaryStatistics("Proc_ReturnVisitDetail", currentPage, pageSize, filter, out total);
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
            return ReportDal.Instance.GetWeekly(dateStart, endDate);
        }

        /// <summary>
        /// 访问报表
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns>访问报表结果集</returns>
        public DataTable GetUserSourceTotalList(DateTime dateStart, DateTime endDate,string memberName,int channel,string channelRemark, int currentPage, int pageSize, out int total)
        {
            return ReportDal.Instance.GetUserSourceTotalList(dateStart, endDate, memberName, channel, channelRemark,currentPage, pageSize, out total);
        }

        #region 还款报表
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
        public DataTable GetRepaymentTotalList(string startDate, string endDate, string checkStatus, int pageIndex, int pageSize, out int total)
        {
            var filter = " L.ID=R.LoanID AND L.MemberID=M.MemberID AND R.Status=2";

            if (!string.IsNullOrEmpty(checkStatus))
                filter += " and L.Agency='" + checkStatus + "'";

            if (!string.IsNullOrEmpty(startDate))
                filter += " and R.UpdateTime>='" + startDate + "'";
            if (!string.IsNullOrEmpty(endDate))
                filter += " and R.UpdateTime<'" + DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";

            string fields = "L.Agency,(L.LoanAmount/10000) LoanAmount,(CASE WHEN L.BorrowMode=0 THEN CAST(L.LoanTerm AS VARCHAR(6))+'天' ELSE CAST(L.LoanTerm AS VARCHAR(6))+'月' END) LoanTerm,L.LoanNumber,M.RealName,CONVERT(CHAR(10),R.RePayTime,120) RePayTime,SUM(R.RePrincipal+R.ReInterest) ReAmount,SUM(R.RePrincipal) RePrincipal,SUM(R.ReInterest+R.ReOverInterest+R.PenaltyAmount) ReInterest,SUM(R.ReOverInterest) ReOverInterest,SUM(R.PenaltyAmount) PenaltyAmount,CONVERT(CHAR(10),R.UpdateTime,120) UpdateTime,(SELECT ISNULL(SUM(F.Amount),0) FROM FundRecord F WHERE F.LoanID=R.LoanID AND F.FeeType IN(7,8) AND CONVERT(CHAR(10),F.CreateTime,120)=CONVERT(CHAR(10),R.UpdateTime,120)) FactReInterest";
            var sort = " GROUP BY R.LoanID,CONVERT(CHAR(10),R.RePayTime,120),L.Agency,L.LoanAmount,L.LoanTerm,L.BorrowMode,L.LoanNumber,M.RealName,CONVERT(CHAR(10),R.UpdateTime,120)";

            return ReportDal.Instance.GetRepaymentTotalList(fields, filter, sort, pageIndex, pageSize, out total);
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
            return ReportDal.Instance.GetRepaymentTotalList(startDate, endDate, pageIndex, pageSize, out total);
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
            return ReportDal.Instance.GetRepaymentTotalList(startDate, endDate);
        }
        #endregion

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
            return ReportDal.Instance.GetRedenvelopeDetailsList(filter, pageIndex, pageSize, out total);
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
            return ReportDal.Instance.GetRedenvelopeType();
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
            return ReportDal.Instance.GetMemberPointsList(filter, pageIndex, pageSize, out total);
        }
        #endregion

        #region 返现数据
        /// <summary>
        /// 返现数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable GetCashBackList(string filter, int pageIndex, int pageSize, out int total)
        {
            return ReportDal.Instance.GetCashBackList(filter, pageIndex, pageSize,out total);
        }
        #endregion


    }
}
