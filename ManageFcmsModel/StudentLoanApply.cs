using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class StudentLoanApply
    {
        //申请借款记录表

        public int ID { get; set; }

        public int MemberId { get; set; }

        public int LoanUseId { get; set; }

        public decimal LoanAmount { get; set; }

        /// <summary>
        /// 利率
        /// </summary>
        public decimal LoanRate { get; set; }

        /// <summary>
        /// 借款期限
        /// </summary>
        public int LoanTerm { get; set; }

        public int RepaymentMethod { get; set; }

        public int BorrowMode { get; set; }

        public int ExamStatus { get; set; }

        public string UseDescription { get; set; }

        public string AuditRecords { get; set; }

        public int LoanId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }


    }
}
