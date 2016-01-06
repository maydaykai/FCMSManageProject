using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BidConfigModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 借款金额
        /// </summary>		
        public decimal LoanAmount { get; set; }

        /// <summary>
        /// 最小投标金额
        /// </summary>		
        public decimal MinInvestment { get; set; }

        /// <summary>
        /// 最大投标金额
        /// </summary>		
        public decimal MaxInvestment { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>		
        public bool EnableStatus { get; set; }
    }
}
