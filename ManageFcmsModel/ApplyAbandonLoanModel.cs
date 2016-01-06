using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ApplyAbandonLoanModel
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
        /// 申请人ID
        /// </summary>
        public int MemberID
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
        /// 申请原因
        /// </summary>
        public string ApplyReason
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
        /// 审核状态 ：0-未审核 1-初审不通过 2-复审中 3-复审不通过 4-复审通过
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
                        return "未审核";
                    case 1:
                        return "初审不通过";
                    case 2:
                        return "初审通过";
                    case 3:
                        return "复审不通过";
                    case 4:
                        return "复审通过";
                }
                return "未审核";
            }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核备注
        /// </summary>
        public string AuditRemark
        {
            get;
            set;
        }
        /// <summary>
        /// 审核记录
        /// </summary>
        public string AuditRecords
        {
            get;
            set;
        }
    }
}
