using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ProcFundReportModel
    {
        public int ID { get; set; }
        public int FeeType { get; set; }
        public int PayeeMemberID { get; set; }
        public int PartyMemberID { get; set; }
        public decimal Amount1 { get; set; }
        public decimal MemberBalance { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Description { get; set; }
    }
}
