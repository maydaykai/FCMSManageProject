using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class StudentRlust
    {
        //查询信息 用户信息 借款信息

        public MemberModel MemberModel { get; set; }

        public MemberInfoModel MemberInfoModel { get; set; }

        public LoanModel loanModel { get; set; }

        public StudentLoanApply studentLoanApply { get; set; }

        public StudentInfo studentInfo { get; set; }

    }



}
