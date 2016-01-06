using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanDelayedModel
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
        /// 借款标ID
        /// </summary>		
        public int LoanID
        {
            get;
            set;
        }
        /// <summary>
        /// 借款标编号
        /// </summary>		
        public string LoanNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 借款人ID
        /// </summary>		
        public int LoanMemberID
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
        /// 竞标延时结束时间
        /// </summary>		
        public DateTime NewBidEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人员用户ID
        /// </summary>		
        public int AuditUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态：0：未审核 1：审核通过 2：审核不通过
        /// </summary>		
        public int AuditStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 申请时间（创建时间）
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// AuditTime
        /// </summary>		
        public DateTime AuditTime
        {
            get;
            set;
        }

        /// <summary>
        /// 借款人名
        /// </summary>		
        public string MemberName
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人名
        /// </summary>		
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 审核状态：0：未审核 1：审核通过 2：审核不通过
        /// </summary>		
        public string AuditStatusName
        {
            get;
            set;
        }
    }
}
