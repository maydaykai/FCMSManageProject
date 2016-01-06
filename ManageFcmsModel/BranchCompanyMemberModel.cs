using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BranchCompanyMemberModel
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
        /// MemberID
        /// </summary>
        public int MemberID
        {
            get;
            set;
        }
        /// <summary>
        /// BranchCompanyID
        /// </summary>
        public int BranchCompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
    }
}
