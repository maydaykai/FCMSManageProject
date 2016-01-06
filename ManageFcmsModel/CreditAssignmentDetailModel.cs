using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCMSModel
{
    public class CreditAssignmentDetailModel
    {

        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 出让方会员ID
        /// </summary>		
        public int MemberID { get; set; }

        /// <summary>
        /// LoanTitle
        /// </summary>		
        public string LoanTitle { get; set; }

        /// <summary>
        /// 转让标编号
        /// </summary>		
        public string LoanNumber { get; set; }

        /// <summary>
        /// 转让金额
        /// </summary>		
        public decimal LoanAmount { get; set; }

        /// <summary>
        /// 折价率
        /// </summary>		
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// 实际转让金额
        /// </summary>		
        public decimal RealLoanAmount { get; set; }

        /// <summary>
        /// 年利率
        /// </summary>		
        public decimal LoanRate { get; set; }

        /// <summary>
        /// 当期使用的天数
        /// </summary>		
        public int UseDays { get; set; }

        /// <summary>
        /// 剩余期限（动态）
        /// </summary>	
        public string RemainedDays { get; set; }

        /// <summary>
        /// 原投标ID
        /// </summary>		
        public int OldBidId { get; set; }

        /// <summary>
        /// 原始借款标ID
        /// </summary>		
        public int OldLoanId { get; set; }

        /// <summary>
        /// 原始借款标ID(加密字符串)
        /// </summary>		
        public string LoanIdSec { get; set; }

        /// <summary>
        /// 竞标开始时间
        /// </summary>		
        public DateTime BidStartTime { get; set; }

        /// <summary>
        /// 竞标结束时间
        /// </summary>		
        public DateTime BidEndTime { get; set; }

        /// <summary>
        /// 最小投标金额
        /// </summary>		
        public decimal BidAmountMin { get; set; }

        /// <summary>
        /// 最大投标金额
        /// </summary>		
        public decimal BidAmountMax { get; set; }

        /// <summary>
        /// 审核状态1：转让中 2：成功转让 3：撤销转让 4：自动撤掉
        /// </summary>		
        public int ExamStatus { get; set; }

        /// <summary>
        /// 投标进度
        /// </summary>		
        public decimal BiddingProcess { get; set; }

        /// <summary>
        /// 投标笔数
        /// </summary>		
        public int BidCount { get; set; }

        /// <summary>
        /// 转让手续费率
        /// </summary>		
        public decimal TransferRate { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>		
        public string ContractNo { get; set; }

        /// <summary>
        /// 合同生成时间
        /// </summary>		
        public DateTime ContractGenerTime { get; set; }

        /// <summary>
        /// 满标时间
        /// </summary>		
        public DateTime FullScaleTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 借款期限
        /// </summary>	
        public string LoanTerm { get; set; }

        /// <summary>
        /// 还款方式
        /// </summary>		
        public string RepaymentMethodName { get; set; }

        /// <summary>
        /// 转让应收利息
        /// </summary>		
        public decimal AssignmentReInterest { get; set; }

       /// <summary>
        /// 转让人会员名
        /// </summary>		
        public string MemberName { get; set; }

        
       /// <summary>
        /// 竞标剩余时间
        /// </summary>		
        public string BiddingTimeRemain { get; set; }

        /// <summary>
        /// 当前转让状态
        /// </summary>		
        public string ExamStatusName { get; set; }

        /// <summary>
        /// 当前剩余申购金额
        /// </summary>		
        public decimal SurplusTransferAmount { get; set; }

        /// <summary>
        /// 本期还款日
        /// </summary>		
        public string CurrentPaymentDate { get; set; }
        
    }
}
