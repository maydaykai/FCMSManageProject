using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class GreenChannelRecordModel
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
        /// 对应分公司所属账号ID(Member表)
        /// </summary>
        public int BranchCompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// LoanID
        /// </summary>
        public int LoanID
        {
            get;
            set;
        }
        /// <summary>
        /// Scale
        /// </summary>
        public decimal Scale
        {
            get;
            set;
        }
    }
}
