using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class RepaymentPlanBll
    {
        RepaymentPlanDal dal = new RepaymentPlanDal();

        /// <summary>
        /// 获取我的收款中的投标详细
        /// </summary>
        /// <param name="loanID"></param>
        /// <param name="bidID"></param>
        /// <returns></returns>
        public RepaymentPlanModel GetRepaymentPlanList(int loanID, int peNumber)
        {
            return dal.GetRepaymentPlanList(loanID, peNumber);
        }

        /// <summary>
        /// 根据借款ID获取还款计划
        /// </summary>
        public DataTable GetRepaymentPlanByLoanID(int loanId)
        {
            return dal.GetRepaymentPlanByLoanID(loanId);
        }

        /// <summary>
        /// 根据会员ID获取待收总额
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public decimal GetDueInMoneyByMemberId(int memberId)
        {
            return dal.GetDueInMoneyByMemberId(memberId);
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
            return dal.GetRepaymentAmount(loanId, type);
        }

        /// <summary>
        /// 提取最近一期未还的期号
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public int GetLatelyNotPeNumber(int loanId)
        {
            return dal.GetLatelyNotPeNumber(loanId);
        }

        /// <summary>
        /// 写入审批申请垫付记录
        /// </summary>
        /// <param name="loanId">贷款ID</param>
        /// <param name="peNumber">期限</param>
        /// <param name="_operator">操作人</param>
        /// <param name="currStep">操作步骤</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        /// <remarks>add wangzhe,2015-12-03</remarks>
        public bool InsertApproveAppayAdvanceInfo(int loanId, int peNumber, int _operator, ref string message)
        {
            return dal.InsertApproveAppayAdvanceInfo(loanId, peNumber, _operator, ref message);
        }
    }
}
