using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BidModel
    {
        /// <summary>
        /// 投资ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 竞标方式：0.手动竞标 1.自动竞标
        /// </summary>
        public int BidType { get; set; }

        /// <summary>
        /// 投资人ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 借款ID
        /// </summary>
        public int LoanID { get; set; }

        /// <summary>
        /// 竞标金额
        /// </summary>
        public decimal BidAmount { get; set; }

        /// <summary>
        /// 竞标状态 1:正常,2:作废
        /// </summary>
        public int BidStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>		
        public string Mobile { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>		
        public string MemberName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string RealName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>		
        public string IdentityCard { get; set; }
        /// <summary>
        /// 推荐人姓名
        /// </summary>
        public string RecRealName { get; set; }
        /// <summary>
        /// 年利率
        /// </summary>
        public decimal LoanRate { get; set; }
    }
}
