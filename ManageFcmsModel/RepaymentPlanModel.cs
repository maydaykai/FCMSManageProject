using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class RepaymentPlanModel
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
        /// 期数：无分期业务默认期数是1
        /// </summary>
        public int PeNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 借款ID
        /// </summary>
        public int LoanID
        {
            get;
            set;
        }
        /// <summary>
        /// 投资ID
        /// </summary>
        public int BidID
        {
            get;
            set;
        }
        /// <summary>
        /// 借款人会员ID
        /// </summary>
        public int LoanMemberID
        {
            get;
            set;
        }
        /// <summary>
        /// 投资人会员ID
        /// </summary>
        public int BidMemberID
        {
            get;
            set;
        }
        /// <summary>
        /// 应还本金
        /// </summary>
        public decimal RePrincipal
        {
            get;
            set;
        }
        /// <summary>
        /// 应还利息
        /// </summary>
        public decimal ReInterest
        {
            get;
            set;
        }
        /// <summary>
        /// 应还逾期利息
        /// </summary>
        public decimal ReOverInterest
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余（未还）本金
        /// </summary>
        public decimal SurPrincipal
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余（未还）利息
        /// </summary>
        public decimal SurReInterest
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余（未还）逾期利息
        /// </summary>
        public decimal SurOverInterest
        {
            get;
            set;
        }
        /// <summary>
        /// 是否展期
        /// </summary>
        public bool IsExtend
        {
            get;
            set;
        }
        public string IsExtendStr
        {
            get
            {
                switch (IsExtend)
                {
                    case true:
                        return "已展期";
                    case false:
                        return "未展期";
                }
                return "";
            }
        }
        /// <summary>
        /// 还款状态：0-未还，1-部分已还，2-全额已还，3-作废
        /// </summary>
        public int Status
        {
            get;
            set;
        }
        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "未还";
                    case 1:
                        return "部分已还";
                    case 2:
                        return "全额已还";
                    case 3:
                        return "作废";
                }
                return "";
            }
        }
        /// <summary>
        /// 逾期状态：0-正常(未逾期)，1-逾期，2-代偿，3-作废，4-提前还款
        /// </summary>
        public int OverStatus
        {
            get;
            set;
        }
        public string OverStatusStr
        {
            get
            {
                switch (OverStatus)
                {
                    case 0:
                        return "正常";
                    case 1:
                        return "逾期";
                    case 2:
                        return "代偿";
                    case 3:
                        return "作废";
                    case 4:
                        return "提前还款";
                }
                return "";
            }
        }
        /// <summary>
        /// 应还款时间
        /// </summary>
        public DateTime RePayTime
        {
            get;
            set;
        }
        /// <summary>
        /// LastPayTime
        /// </summary>
        public DateTime LastPayTime
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
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
        /// 其他费用合计
        /// </summary>
        public decimal SumOtherFee
        {
            get;
            set;
        }
    }
}
