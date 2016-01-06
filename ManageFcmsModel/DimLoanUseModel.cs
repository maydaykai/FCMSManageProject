using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class DimLoanUseModel
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
        /// 借款用途
        /// </summary>
        public string LoanUseName
        {
            get;
            set;
        }
        /// <summary>
        /// 标识：0：正常借款  1：快速借款 
        /// </summary>
        public string Sign
        {
            get;
            set;
        }
    }
}
