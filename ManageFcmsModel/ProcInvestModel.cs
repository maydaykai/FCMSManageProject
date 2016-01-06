using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ProcInvestModel
    {

        /// <summary>
        /// 借款标号
        /// </summary>		
        public int LoanID
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
        /// 投资金额
        /// </summary>		
        public decimal BidAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 返回信息
        /// </summary>		
        public string message
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
        /// 优惠码
        /// </summary>
        public string CouponCode { get; set; }
    }
}
