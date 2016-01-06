using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ReCommendRewardGrantModel
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
        /// 申请年
        /// </summary>
        public int Year
        {
            get;
            set;
        }
        /// <summary>
        /// 申请月
        /// </summary>
        public int Month
        {
            get;
            set;
        }
        /// <summary>
        /// 申请金额
        /// </summary>
        public decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public int ApplyUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get;
            set;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态：0-初审中 1-复审中 2-初审不通过 3-复审不通过 4-复审通过
        /// </summary>
        public int AuditStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 审核记录：例如：初审通过—Norton—2014-06-17 09:36:46—（审核意见：同意）|复审通过—Norton—2014-06-17 09:36:55—（审核意见：同意）
        /// </summary>
        public string AuditRecords
        {
            get;
            set;
        }
    }
}
