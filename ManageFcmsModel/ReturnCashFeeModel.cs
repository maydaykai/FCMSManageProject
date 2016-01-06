using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ReturnCashFeeModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>		
        public int MemberId { get; set; }

        /// <summary>
        /// 费用类型
        /// </summary>		
        public int FeeType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 审核状态：1-初审中 2-初审不通过 3-复审中 4-复审不通过 5-复审通过
        /// </summary>		
        public int AuditStatus { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 审核记录
        /// </summary>
        public string AuditRecords { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        
        
    }
}
