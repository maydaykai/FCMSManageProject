using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ApplyDeferLoanModel
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
		/// 申请人会员ID
        /// </summary>
        public int MemberID
        {
            get;
            set;
        }        
		/// <summary>
		/// 申请展期的借款标ID
        /// </summary>
        public int LoanID
        {
            get;
            set;
        }        
		/// <summary>
		/// 申请展期的期限
        /// </summary>
        public int ExtensionTerm
        {
            get;
            set;
        }        
		/// <summary>
		/// 展期利率
        /// </summary>
        public decimal ExtensionRate
        {
            get;
            set;
        }
        /// <summary>
        /// 展期期间担保费费率
        /// </summary>
        public decimal GuaranteeFee
        {
            get;
            set;
        }
        /// <summary>
        /// 展期期间借款人居间服务费费率
        /// </summary>
        public decimal LoanServiceCharges
        {
            get;
            set;
        }
		/// <summary>
		/// 申请展期原因
        /// </summary>
        public string ExtensionReason
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
        /// 审核状态 ：0-未审核 1-初审不通过 2-初审通过 3-复审不通过 4-复审通过
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
		/// 审核备注说明
        /// </summary>
        public string Remark
        {
            get;
            set;
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
		/// 审核记录：例如：
        /// 初审通过 (2012-10-12 17:52)|
        /// 复审通过 (2012-10-15 10:13)
        /// </summary>
        public string AuditRecords
        {
            get;
            set;
        }
    }
}
