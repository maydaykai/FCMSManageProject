using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsCommon
{
    public class FeeType
    {
        /// <summary>
        /// 费用类型 枚举
        /// <para>0=充值=FRecharge</para>
        /// <para>1=借款=FLoan</para>
        /// <para>2=竞标=FBidding</para>
        /// <para>3=提现=FWithdrawals</para>
        /// <para>4=本金=FRepayment</para>
        /// <para>5=返回投标金额=FReBidAmount</para>
        /// <para>6=担保费=FGuaranteeFee</para>
        /// <para>7=借款利息=FLoanInterest</para>
        /// <para>8=逾期利息=FOverInterest</para>
        /// <para>9=逾期滞纳金=FOverManageFees</para>
        /// <para>10=提前还款违约金=FPrepayPenalty</para>
        /// <para>11=利息管理费=FBidServiceCharges</para>
        /// <para>12=借款人居间服务费=FLoanServiceCharges</para>
        /// <para>13=会员充值手续费=FmRechargeFee</para>
        /// <para>14=用户提现手续费=FmCashFee</para>
        /// <para>15=用户充值手续费=FuRechargeFee</para>
        /// <para>16=会员提现手续费=FuCashFee</para>
        /// <para>17=银行卡认证汇款=FBankRemittance</para>
        /// <para>18=银行结算利息=FBankInterest</para>
        /// <para>19=发标保证金=FBidBond</para>
        /// <para>20=悔标违约金=FutureDamages</para>
        /// <para>21=退还担保费=FReGuaranteeFee</para>
        /// <para>22=退还借款人居间服务费=FReLoanServiceCharges</para>
        /// <para>23=返回发标保证金=FReBidBond</para>
        /// <para>24=推荐投资奖励=FRecommendInterestFee</para>
        /// <para>25=VIP年费=FvipFee</para>
        /// <para>26=红包=FRedEnvelope</para>
        /// <para>27=居间提前违约金=FLoanServicePenalty</para>
        /// <para>28=返回提现冻结金额=FReturnCashAmount</para>
        /// <para>29=垫付=FPaymentAmount</para>
        /// <para>30=返回提现冻结手续费=FReturnCashFee</para>
        /// <para>31=活动奖励=FReturnCashFee</para>
        /// <para>32=债权转让=FCreditAssignment</para>
        /// <para>33=体验币=FExperienceCoin</para>
        /// <para>34=体验币收回=FWithdrawExperienceCoin</para>
        /// <para>35=体验利息收回=FWithdrawInterest</para>
        /// <para>36=债权申购=FCreditPurchase</para>
        /// <para>37=退还申购金额=FReCreditPurchase</para>
        /// <para>38=活动奖励收回=FWithdrawReturnCashFee</para>
        /// <para>39=债权转让管理费=CreditAssignmentManageFee</para>
        /// <para>40=资金托管费=FFundsTrusteeshipFee</para>
        /// <para>41=返还资金托管费=FReturnFundsTrusteeshipFee</para>
        /// <para>42=慈善捐赠=CharitableContribution</para>
        /// <para>43=利息管理费抵扣费=Withheld</para>
        /// <para>44=预约保证金=AppointmentMargin</para>
        /// <para>45=退回预约保证金=ReturnAppointmentMargin</para>
        /// <para>46=返还垫付=RPaymentAmount</para>
        /// <para>47=会员积分利息管理费抵扣=MemberPointsWithheld</para>
        /// <para>48=平台调账[支]=PlatformOutlay</para>
        /// <para>49=平台调账[收]=PlatformRevenue</para>
        /// </summary>
        public enum FeeTypeEnum
        {
            #region 费用类型属性

            /// <summary>
            /// 0=充值
            /// </summary>
            [FeeAttruate(Title = "充值")]
            FRecharge = 0,

            /// <summary>
            /// 1=借款
            /// </summary>
            [FeeAttruate(Title = "借款")]
            FLoan = 1,

            /// <summary>
            /// 2=竞标
            /// </summary>
            [FeeAttruate(Title = "竞标")]
            FBidding = 2,

            /// <summary>
            /// 提现
            /// </summary>
            [FeeAttruate(Title = "提现")]
            FWithdrawals = 3,

            /// <summary>
            /// 本金
            /// </summary>
            [FeeAttruate(Title = "本金")]
            FRepayment = 4,

            /// <summary>
            /// 返回投标金额
            /// </summary>
            [FeeAttruate(Title = "返回投标金额")]
            FReBidAmount = 5,

            /// <summary>
            /// 担保费
            /// </summary>
            [FeeAttruate(Title = "担保费")]
            FGuaranteeFee = 6,

            /// <summary>
            /// 借款利息
            /// </summary>
            [FeeAttruate(Title = "借款利息")]
            FLoanInterest = 7,

            /// <summary>
            /// 逾期利息
            /// </summary>
            [FeeAttruate(Title = "逾期利息")]
            FOverInterest = 8,

            /// <summary>
            /// 逾期滞纳金
            /// </summary>
            [FeeAttruate(Title = "逾期滞纳金")]
            FOverManageFees = 9,

            /// <summary>
            /// 提前还款违约金
            /// </summary>
            [FeeAttruate(Title = "提前还款违约金")]
            FPrepayPenalty = 10,

            /// <summary>
            /// 利息管理费
            /// </summary>
            [FeeAttruate(Title = "利息管理费")]
            FBidServiceCharges = 11,

            /// <summary>
            /// 借款人居间服务费
            /// </summary>
            [FeeAttruate(Title = "借款人居间服务费")]
            FLoanServiceCharges = 12,

            /// <summary>
            /// 会员充值手续费
            /// </summary>
            [FeeAttruate(Title = "会员充值手续费")]
            FmRechargeFee = 13,

            /// <summary>
            /// 用户提现手续费
            /// </summary>
            [FeeAttruate(Title = "用户提现手续费")]
            FmCashFee = 14,

            /// <summary>
            /// 用户充值手续费
            /// </summary>
            [FeeAttruate(Title = "用户充值手续费")]
            FuRechargeFee = 15,

            /// <summary>
            /// 会员提现手续费
            /// </summary>
            [FeeAttruate(Title = "会员提现手续费")]
            FuCashFee = 16,

            /// <summary>
            /// 银行卡认证汇款
            /// </summary>
            [FeeAttruate(Title = "银行卡认证汇款")]
            FBankRemittance = 17,

            /// <summary>
            /// 银行结算利息
            /// </summary>
            [FeeAttruate(Title = "银行结算利息")]
            FBankInterest = 18,

            /// <summary>
            /// 发标保证金
            /// </summary>
            [FeeAttruate(Title = "发标保证金")]
            FBidBond = 19,

            /// <summary>
            /// 悔标违约金
            /// </summary>
            [FeeAttruate(Title = "悔标违约金")]
            FutureDamages = 20,

            /// <summary>
            /// 退还担保费
            /// </summary>
            [FeeAttruate(Title = "退还担保费")]
            FReGuaranteeFee = 21,
            /// <summary>
            /// 退还借款人居间服务费
            /// </summary>
            [FeeAttruate(Title = "退还借款人居间服务费")]
            FReLoanServiceCharges = 22,

            /// <summary>
            /// 返回发标保证金
            /// </summary>
            [FeeAttruate(Title = "返回发标保证金")]
            FReBidBond = 23,

            /// <summary>
            /// 推荐投资奖励
            /// </summary>
            [FeeAttruate(Title = "推荐投资奖励")]
            FRecommendInterestFee = 24,

            /// <summary>
            /// VIP年费
            /// </summary>
            [FeeAttruate(Title = "VIP年费")]
            FvipFee = 25,

            /// <summary>
            /// 红包
            /// </summary>
            [FeeAttruate(Title = "红包")]
            FRedEnvelope = 26,

            /// <summary>
            /// 借款人居间提前违约金
            /// </summary>
            [FeeAttruate(Title = "借款人居间提前违约金")]
            FLoanServicePenalty = 27,

            /// <summary>
            /// 返回提现冻结金额
            /// </summary>
            [FeeAttruate(Title = "返回提现冻结金额")]
            FReturnCashAmount = 28,

            /// <summary>
            /// 返回垫付金额
            /// </summary>
            [FeeAttruate(Title = "垫付")]
            FPaymentAmount = 29,

            /// <summary>
            /// 返回提现冻结手续费
            /// </summary>
            [FeeAttruate(Title = "返回提现冻结手续费")]
            FReturnCashFee = 30,

            /// <summary>
            /// 活动奖励
            /// </summary>
            [FeeAttruate(Title = "活动奖励")]
            FActivitiesFee = 31,

            /// <summary>
            /// 债权转让
            /// </summary>
            [FeeAttruate(Title = "债权转让")]
            FCreditAssignment = 32,

            /// <summary>
            /// 体验币
            /// </summary>
            [FeeAttruate(Title = "体验币")]
            FExperienceCoin = 33,

            /// <summary>
            /// 体验币收回
            /// </summary>
            [FeeAttruate(Title = "体验币收回")]
            FWithdrawExperienceCoin = 34,

            /// <summary>
            /// 体验利息收回
            /// </summary>
            [FeeAttruate(Title = "体验利息收回")]
            FWithdrawInterest = 35,

            /// <summary>
            /// 债权申购
            /// </summary>
            [FeeAttruate(Title = "债权申购")]
            FCreditPurchase = 36,

            /// <summary>
            /// 返回申购金额
            /// </summary>
            [FeeAttruate(Title = "退还申购金额")]
            FReCreditPurchase = 37,

            /// <summary>
            /// 返回申购金额
            /// </summary>
            [FeeAttruate(Title = "活动奖励收回")]
            FWithdrawReturnCashFee = 38,

            /// <summary>
            /// 债权转让管理费
            /// </summary>
            [FeeAttruate(Title = "债权转让管理费")]
            CreditAssignmentManageFee = 39,

            /// <summary>
            /// 资金托管费
            /// </summary>
            [FeeAttruate(Title = "资金托管费")]
            FFundsTrusteeshipFee = 40,

            /// <summary>
            /// 返还资金托管费
            /// </summary>
            [FeeAttruate(Title = "返还资金托管费")]
            FReturnFundsTrusteeshipFee = 41,

            /// <summary>
            /// 慈善捐赠
            /// </summary>
            [FeeAttruate(Title = "慈善捐赠")]
            CharitableContribution = 42,

            /// <summary>
            /// 利息管理费抵扣费
            /// </summary>
            [FeeAttruate(Title = "利息管理费抵扣劵")]
            Withheld = 43,

            /// <summary>
            /// 预约保证金
            /// </summary>
            [FeeAttruate(Title = "预约保证金")]
            AppointmentMargin = 44,

            /// <summary>
            /// 退回预约保证金
            /// </summary>
            [FeeAttruate(Title = "退回预约保证金")]
            ReturnAppointmentMargin = 45,

            /// <summary>
            /// 返还垫付
            /// </summary>
            [FeeAttruate(Title = "返还垫付")]
            RPaymentAmount = 46,

            /// <summary>
            /// 会员积分利息管理费抵扣
            /// </summary>
            [FeeAttruate(Title = "会员积分利息管理费抵扣")]
            MemberPointsWithheld = 47,

            /// <summary>
            /// 平台调账[支]
            /// </summary>
            [FeeAttruate(Title = "平台调账[支]")]
            PlatformOutlay = 48,

            /// <summary>
            /// 平台调账[收]
            /// </summary>
            [FeeAttruate(Title = "平台调账[收]")]
            PlatformRevenue = 49

            #endregion
        }

        /// <summary>
        /// 获取所有费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetFeetypeList()
        {
            var feeTypeDic = new Dictionary<int, string>();

            AddItem(feeTypeDic, FeeTypeEnum.FRecharge); //充值
            AddItem(feeTypeDic, FeeTypeEnum.FLoan); //借款金额
            AddItem(feeTypeDic, FeeTypeEnum.FBidding); //竞标
            AddItem(feeTypeDic, FeeTypeEnum.FWithdrawals); //提现
            AddItem(feeTypeDic, FeeTypeEnum.FRepayment); //本金
            AddItem(feeTypeDic, FeeTypeEnum.FReBidAmount); //返回投标金额
            AddItem(feeTypeDic, FeeTypeEnum.FGuaranteeFee); //担保费
            AddItem(feeTypeDic, FeeTypeEnum.FLoanInterest); //借款利息
            AddItem(feeTypeDic, FeeTypeEnum.FOverInterest); //逾期利息
            AddItem(feeTypeDic, FeeTypeEnum.FOverManageFees); //逾期滞纳金
            AddItem(feeTypeDic, FeeTypeEnum.FPrepayPenalty); //提前还款违约金
            AddItem(feeTypeDic, FeeTypeEnum.FBidServiceCharges); //利息管理费
            AddItem(feeTypeDic, FeeTypeEnum.FLoanServiceCharges); //借款人居间服务费
            AddItem(feeTypeDic, FeeTypeEnum.FmRechargeFee); //会员充值手续费
            AddItem(feeTypeDic, FeeTypeEnum.FmCashFee); //会员提现手续费
            AddItem(feeTypeDic, FeeTypeEnum.FuRechargeFee); //用户充值手续费
            AddItem(feeTypeDic, FeeTypeEnum.FuCashFee); //用户提现手续费
            AddItem(feeTypeDic, FeeTypeEnum.FBankRemittance); //银行卡认证汇款
            AddItem(feeTypeDic, FeeTypeEnum.FBankInterest); //银行结算利息
            AddItem(feeTypeDic, FeeTypeEnum.FBidBond); //发标保证金
            AddItem(feeTypeDic, FeeTypeEnum.FutureDamages); //悔标违约金
            AddItem(feeTypeDic, FeeTypeEnum.FReGuaranteeFee); //退还担保费
            AddItem(feeTypeDic, FeeTypeEnum.FReLoanServiceCharges); //退还借款人居间服务费
            AddItem(feeTypeDic, FeeTypeEnum.FReBidBond); //返回发标保证金
            AddItem(feeTypeDic, FeeTypeEnum.FRecommendInterestFee); //推荐投资奖励
            AddItem(feeTypeDic, FeeTypeEnum.FvipFee); //VIP年费
            AddItem(feeTypeDic, FeeTypeEnum.FRedEnvelope); //红包
            AddItem(feeTypeDic, FeeTypeEnum.FLoanServicePenalty); //居间提前违约金
            AddItem(feeTypeDic, FeeTypeEnum.FReturnCashAmount); //返回提现冻结金额
            AddItem(feeTypeDic, FeeTypeEnum.FPaymentAmount); //垫付
            AddItem(feeTypeDic, FeeTypeEnum.FReturnCashFee); //返回提现冻结手续费
            AddItem(feeTypeDic, FeeTypeEnum.FActivitiesFee); //活动奖励
            AddItem(feeTypeDic, FeeTypeEnum.FCreditAssignment); //债权转让
            AddItem(feeTypeDic, FeeTypeEnum.FExperienceCoin); //体验币
            AddItem(feeTypeDic, FeeTypeEnum.FWithdrawExperienceCoin); //体验币收回
            AddItem(feeTypeDic, FeeTypeEnum.FWithdrawInterest); //体验利息收回
            AddItem(feeTypeDic, FeeTypeEnum.FCreditPurchase); //债权申购
            AddItem(feeTypeDic, FeeTypeEnum.FReCreditPurchase); //退还申购金额
            AddItem(feeTypeDic, FeeTypeEnum.FWithdrawReturnCashFee); //活动奖励收回
            AddItem(feeTypeDic, FeeTypeEnum.CreditAssignmentManageFee); //债权转让管理费
            AddItem(feeTypeDic, FeeTypeEnum.FFundsTrusteeshipFee); //资金托管费
            AddItem(feeTypeDic, FeeTypeEnum.FReturnFundsTrusteeshipFee); //返还资金托管费
            AddItem(feeTypeDic, FeeTypeEnum.CharitableContribution); //慈善捐赠
            AddItem(feeTypeDic, FeeTypeEnum.Withheld); //利息管理费抵扣费
            AddItem(feeTypeDic, FeeTypeEnum.AppointmentMargin); //预约保证金
            AddItem(feeTypeDic, FeeTypeEnum.ReturnAppointmentMargin); //退回预约保证金
            AddItem(feeTypeDic, FeeTypeEnum.RPaymentAmount); //返还垫付
            AddItem(feeTypeDic, FeeTypeEnum.MemberPointsWithheld); //会员积分利息管理费抵扣
            AddItem(feeTypeDic, FeeTypeEnum.PlatformRevenue); //平台调账[收]
            AddItem(feeTypeDic, FeeTypeEnum.PlatformOutlay); //平台调账[支]

            return feeTypeDic;
        }


        /// <summary>
        /// 获取所有属于（会员）收入的费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetRevenueFeeList()
        {
            var feeDic = new Dictionary<int, string>();
            AddItem(feeDic, FeeTypeEnum.FRecharge); //充值
            AddItem(feeDic, FeeTypeEnum.FLoan); //借款金额
            AddItem(feeDic, FeeTypeEnum.FRepayment); //本金
            AddItem(feeDic, FeeTypeEnum.FReBidAmount); //返回投标金额
            AddItem(feeDic, FeeTypeEnum.FLoanInterest); //借款利息
            AddItem(feeDic, FeeTypeEnum.FOverInterest); //逾期利息
            AddItem(feeDic, FeeTypeEnum.FPrepayPenalty); //提前还款违约金
            AddItem(feeDic, FeeTypeEnum.FBankRemittance); //银行卡认证汇款
            AddItem(feeDic, FeeTypeEnum.FBankInterest); //银行结算利息
            AddItem(feeDic, FeeTypeEnum.FReGuaranteeFee); //退还担保费
            AddItem(feeDic, FeeTypeEnum.FReLoanServiceCharges); //退还借款人居间服务费
            AddItem(feeDic, FeeTypeEnum.FReBidBond); //返回发标保证金
            AddItem(feeDic, FeeTypeEnum.FutureDamages); //悔标违约金
            AddItem(feeDic, FeeTypeEnum.FRecommendInterestFee); //推荐投资奖励
            AddItem(feeDic, FeeTypeEnum.FRedEnvelope); //红包
            AddItem(feeDic, FeeTypeEnum.FLoanServicePenalty); //居间提前违约金
            AddItem(feeDic, FeeTypeEnum.FReturnCashAmount); //返回提现冻结金额
            AddItem(feeDic, FeeTypeEnum.FReturnCashFee); //返回提现冻结手续费
            AddItem(feeDic, FeeTypeEnum.FActivitiesFee); //活动奖励
            AddItem(feeDic, FeeTypeEnum.FCreditAssignment); //债权转让
            AddItem(feeDic, FeeTypeEnum.FExperienceCoin); //体验币
            AddItem(feeDic, FeeTypeEnum.FReCreditPurchase); //退还申购金额
            AddItem(feeDic, FeeTypeEnum.FReturnFundsTrusteeshipFee); //返还资金托管费
            AddItem(feeDic, FeeTypeEnum.Withheld); //利息管理费抵扣费
            AddItem(feeDic, FeeTypeEnum.ReturnAppointmentMargin); //退回预约保证金
            AddItem(feeDic, FeeTypeEnum.MemberPointsWithheld); //会员积分利息管理费抵扣
            AddItem(feeDic, FeeTypeEnum.PlatformOutlay); //平台调账[支]

            return feeDic;
        }

        /// <summary>
        /// 获取所有属于（会员）支出的费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetDefrayFeeList()
        {
            var feeDic = new Dictionary<int, string>();

            AddItem(feeDic, FeeTypeEnum.FBidding); //竞标
            AddItem(feeDic, FeeTypeEnum.FWithdrawals); //提现
            AddItem(feeDic, FeeTypeEnum.FRepayment); //本金
            AddItem(feeDic, FeeTypeEnum.FLoanInterest); //借款利息
            AddItem(feeDic, FeeTypeEnum.FOverInterest); //逾期利息
            AddItem(feeDic, FeeTypeEnum.FOverManageFees); //逾期滞纳金
            AddItem(feeDic, FeeTypeEnum.FPrepayPenalty); //提前还款违约金
            AddItem(feeDic, FeeTypeEnum.FBidServiceCharges); //利息管理费
            AddItem(feeDic, FeeTypeEnum.FLoanServiceCharges); //借款人居间服务费
            AddItem(feeDic, FeeTypeEnum.FmRechargeFee); //会员充值手续费
            AddItem(feeDic, FeeTypeEnum.FuCashFee); //会员提现手续费
            AddItem(feeDic, FeeTypeEnum.FBidBond); //发标保证金
            AddItem(feeDic, FeeTypeEnum.FutureDamages); //悔标违约金
            AddItem(feeDic, FeeTypeEnum.FGuaranteeFee); //担保费
            AddItem(feeDic, FeeTypeEnum.FvipFee); //VIP年费
            AddItem(feeDic, FeeTypeEnum.FWithdrawExperienceCoin); //体验币收回
            AddItem(feeDic, FeeTypeEnum.FWithdrawInterest); //体验利息收回
            AddItem(feeDic, FeeTypeEnum.FCreditPurchase); //债权申购
            AddItem(feeDic, FeeTypeEnum.FWithdrawReturnCashFee); //活动奖励收回
            AddItem(feeDic, FeeTypeEnum.CreditAssignmentManageFee); //债权转让管理费
            AddItem(feeDic, FeeTypeEnum.FFundsTrusteeshipFee); //资金托管费
            AddItem(feeDic, FeeTypeEnum.CharitableContribution); //慈善捐赠
            AddItem(feeDic, FeeTypeEnum.AppointmentMargin); //预约保证金
            AddItem(feeDic, FeeTypeEnum.PlatformRevenue); //平台调账[收]

            return feeDic;
        }

        /// <summary>
        /// 获取所有属于（会员）冻结的费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetFreezeFeeList()
        {
            var feeDic = new Dictionary<int, string>();

            AddItem(feeDic, FeeTypeEnum.FBidding); //竞标
            AddItem(feeDic, FeeTypeEnum.FWithdrawals); //提现
            AddItem(feeDic, FeeTypeEnum.FmRechargeFee); //会员充值手续费
            AddItem(feeDic, FeeTypeEnum.FuCashFee); //会员提现手续费
            AddItem(feeDic, FeeTypeEnum.FBidBond); //发标保证金
            AddItem(feeDic, FeeTypeEnum.FCreditPurchase); //债权申购
            AddItem(feeDic, FeeTypeEnum.FFundsTrusteeshipFee); //资金托管费
            AddItem(feeDic, FeeTypeEnum.AppointmentMargin); //预约保证金

            return feeDic;
        }

        /// <summary>
        /// 获取所有平台相关的费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetP2PFeeList()
        {
            var feeDic = new Dictionary<int, string>();

            AddItem(feeDic, FeeTypeEnum.FPrepayPenalty); //提前还款违约金
            AddItem(feeDic, FeeTypeEnum.FBidServiceCharges); //利息管理费
            AddItem(feeDic, FeeTypeEnum.FLoanServiceCharges); //借款人居间服务费
            AddItem(feeDic, FeeTypeEnum.FmRechargeFee); //会员充值手续费
            AddItem(feeDic, FeeTypeEnum.FmCashFee); //会员提现手续费
            AddItem(feeDic, FeeTypeEnum.FuRechargeFee); //用户充值手续费
            AddItem(feeDic, FeeTypeEnum.FuCashFee); //用户提现手续费
            AddItem(feeDic, FeeTypeEnum.FBankRemittance); //银行卡认证汇款
            AddItem(feeDic, FeeTypeEnum.FBankInterest); //银行结算利息
            AddItem(feeDic, FeeTypeEnum.FutureDamages); //悔标违约金
            AddItem(feeDic, FeeTypeEnum.FRecommendInterestFee); //推荐投资奖励
            AddItem(feeDic, FeeTypeEnum.FvipFee); //VIP年费
            AddItem(feeDic, FeeTypeEnum.FLoanServicePenalty); //居间提前违约金
            AddItem(feeDic, FeeTypeEnum.FActivitiesFee); //活动奖励
            AddItem(feeDic, FeeTypeEnum.FRedEnvelope); //红包
            AddItem(feeDic, FeeTypeEnum.FExperienceCoin); //体验币
            AddItem(feeDic, FeeTypeEnum.FWithdrawExperienceCoin); //体验币收回
            AddItem(feeDic, FeeTypeEnum.FWithdrawReturnCashFee); //活动奖励收回
            AddItem(feeDic, FeeTypeEnum.CreditAssignmentManageFee); //债权转让管理费
            AddItem(feeDic, FeeTypeEnum.FFundsTrusteeshipFee); //资金托管费
            AddItem(feeDic, FeeTypeEnum.CharitableContribution); //慈善捐赠
            AddItem(feeDic, FeeTypeEnum.Withheld); //利息管理费抵扣费
            AddItem(feeDic, FeeTypeEnum.AppointmentMargin); //预约保证金
            AddItem(feeDic, FeeTypeEnum.ReturnAppointmentMargin); //退回预约保证金
            AddItem(feeDic, FeeTypeEnum.FPaymentAmount); //返还垫付
            AddItem(feeDic, FeeTypeEnum.MemberPointsWithheld); //会员积分利息管理费抵扣
            AddItem(feeDic, FeeTypeEnum.PlatformRevenue); //平台调账[收]
            AddItem(feeDic, FeeTypeEnum.PlatformOutlay); //平台调账[支]


            return feeDic;
        }

        /// <summary>
        /// 获取所有担保公司相关的费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetBondingCompanyFeeList()
        {
            var feeDic = new Dictionary<int, string>();

            AddItem(feeDic, FeeTypeEnum.FGuaranteeFee); //担保费
            AddItem(feeDic, FeeTypeEnum.FOverManageFees); //逾期滞纳金
            AddItem(feeDic, FeeTypeEnum.FuRechargeFee); //用户充值手续费
            AddItem(feeDic, FeeTypeEnum.FuCashFee); //用户提现手续费
            AddItem(feeDic, FeeTypeEnum.FReGuaranteeFee); //退还担保费
            AddItem(feeDic, FeeTypeEnum.FReLoanServiceCharges); //退还借款人居间服务费
            AddItem(feeDic, FeeTypeEnum.FPaymentAmount); //垫付

            return feeDic;
        }


        /// <summary>
        /// 获取用于费用设置的费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetSetFeeList()
        {
            var feeSetDic = new Dictionary<int, string>();

            AddItem(feeSetDic, FeeTypeEnum.FGuaranteeFee); //担保费
            AddItem(feeSetDic, FeeTypeEnum.FOverManageFees); //逾期滞纳金
            AddItem(feeSetDic, FeeTypeEnum.FPrepayPenalty); //提前还款违约金
            AddItem(feeSetDic, FeeTypeEnum.FBidServiceCharges); //利息管理费
            AddItem(feeSetDic, FeeTypeEnum.FLoanServiceCharges); //借款人居间服务费
            AddItem(feeSetDic, FeeTypeEnum.FmRechargeFee); //会员充值手续费
            AddItem(feeSetDic, FeeTypeEnum.FmCashFee); //会员提现手续费
            AddItem(feeSetDic, FeeTypeEnum.FuRechargeFee); //用户充值手续费
            AddItem(feeSetDic, FeeTypeEnum.FuCashFee); //用户提现手续费
            AddItem(feeSetDic, FeeTypeEnum.FBidBond); //发标保证金
            AddItem(feeSetDic, FeeTypeEnum.FutureDamages); //悔标违约金
            AddItem(feeSetDic, FeeTypeEnum.CreditAssignmentManageFee); //债权转让管理费
            AddItem(feeSetDic, FeeTypeEnum.FFundsTrusteeshipFee); //资金托管费

            return feeSetDic;
        }


        #region 根据费用类型，取出费用类型名称

        /// <summary>
        /// 根据费用类型，取出费用类型名称
        /// </summary>
        /// <param name="type">类型枚举值</param>
        /// <returns></returns>
        public static string GetNameByType(FeeTypeEnum type)
        {
            return GetAttrTitle(type);
        }

        #endregion

        #region  获取指定费用列表的ID字符串

        /// <summary>
        /// 获取指定费用列表的ID字符串
        /// </summary>
        /// <returns></returns>
        public static string GetFeeIds(Dictionary<int, string> feetype)
        {
            var ids = new StringBuilder();
            foreach (KeyValuePair<int, string> model in feetype)
            {
                ids.AppendFormat("{0},", model.Key);
            }
            string idstr = ids.Replace(",", "", ids.Length - 1, 1).ToString();
            return idstr;
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// 添加Dictionary的项
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="type"></param>
        private static void AddItem(Dictionary<int, string> dic, FeeTypeEnum type)
        {
            dic.Add((int)type, GetAttrTitle(type));
        }

        /// <summary>
        /// 根据费用类型，取出费用类型名称
        /// </summary>
        /// <param name="type">类型枚举值</param>
        /// <returns></returns>
        private static string GetAttrTitle(FeeTypeEnum type)
        {
            Type enumType = type.GetType();

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("不是枚举类型");
            }

            string enumItem = Enum.GetName(enumType, (int)type);
            if (enumItem == null)
            {
                return string.Empty;
            }

            object[] objs = enumType.GetField(enumItem).GetCustomAttributes(typeof(FeeAttruate), false);
            if (objs.Length > 0)
            {
                FeeAttruate attr = objs[0] as FeeAttruate;
                return attr.Title;
            }
            return "未知";
        }

        /// <summary>
        /// 计费类型属性
        /// </summary>
        public class FeeAttruate : Attribute
        {
            public string Title;
        }

        #endregion
    }
}
