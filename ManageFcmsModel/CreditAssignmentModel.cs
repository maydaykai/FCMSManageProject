using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class CreditAssignmentModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 出让方会员ID
        /// </summary>
        public int MemberID
        {
            get;
            set;
        }
        /// <summary>
        /// 出让方会员名称
        /// </summary>
        public string MemberName
        {
            get;
            set;
        }
        /// <summary>
        /// 出让方会员真实姓名
        /// </summary>
        public string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// LoanTitle
        /// </summary>
        public string LoanTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 转让标编号
        /// </summary>
        public string LoanNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 原始标编号
        /// </summary>
        public string OldLoanNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 转让金额
        /// </summary>
        public decimal LoanAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 折价率
        /// </summary>
        public decimal DiscountRate
        {
            get;
            set;
        }
        /// <summary>
        /// 实际转让金额
        /// </summary>
        public decimal RealLoanAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 年利率
        /// </summary>
        public decimal LoanRate
        {
            get;
            set;
        }
        /// <summary>
        /// 借款期限
        /// </summary>
        public int LoanTerm
        {
            get;
            set;
        }
        /// <summary>
        /// 借款期限方式：0：按天借款 1：按月借款
        /// </summary>
        public int BorrowMode
        {
            get;
            set;
        }
        public String LoanTermInfo
        {
            get
            {
                switch (BorrowMode)
                {
                    case 0:
                        return LoanTerm + "天";
                    case 1:
                        return LoanTerm + "个月";
                }
                return LoanTerm + "天";
            }
        }
        /// <summary>
        /// 当期使用的天数
        /// </summary>
        public int UseDays
        {
            get;
            set;
        }
        /// <summary>
        /// 当期剩余天数
        /// </summary>
        public string RemainedDays
        {
            get;
            set;
        }
        /// <summary>
        /// 原投标ID
        /// </summary>
        public int OldBidId
        {
            get;
            set;
        }
        /// <summary>
        /// 原始借款标ID
        /// </summary>
        public int OldLoanId
        {
            get;
            set;
        }
        /// <summary>
        /// 竞标开始时间
        /// </summary>
        public DateTime BidStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 竞标结束时间
        /// </summary>
        public DateTime BidEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最小投标金额
        /// </summary>
        public decimal BidAmountMin
        {
            get;
            set;
        }
        /// <summary>
        /// 最大投标金额
        /// </summary>
        public decimal BidAmountMax
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态1：转让中 2：成功转让 3：撤销转让 4：自动撤掉
        /// </summary>
        public int ExamStatus
        {
            get;
            set;
        }
        public string ExamStatusName
        {
            get
            {
                switch (ExamStatus)
                {
                    case 1:
                        return "转让中";
                    case 2:
                        return "成功转让";
                    case 3:
                        return "撤销转让";
                    case 4:
                        return "自动撤掉";
                }
                return "转让中";
            }
        }
        /// <summary>
        /// 投标进度
        /// </summary>
        public decimal BiddingProcess
        {
            get;
            set;
        }
        /// <summary>
        /// 投标笔数
        /// </summary>
        public int BidCount
        {
            get;
            set;
        }
        /// <summary>
        /// 转让手续费率
        /// </summary>
        public decimal TransferRate
        {
            get;
            set;
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同生成时间
        /// </summary>
        public DateTime ContractGenerTime
        {
            get;
            set;
        }
        /// <summary>
        /// 满标放款时间
        /// </summary>
        public DateTime FullScaleTime
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 冻结投标金额
        /// </summary>
        public decimal FreezeAmount
        {
            get;
            set;
        }
    }
}
