using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanAuthInfoModel
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
        public int LoanId
        {
            get;
            set;
        }
        /// <summary>
        /// 审核信息ID
        /// </summary>		
        public int AuthInfoId
        {
            get;
            set;
        }
        /// <summary>
        /// 审核项目
        /// </summary>		
        public int AuthProductName
        {
            get;
            set;
        }
        /// <summary>
        /// 审核日期
        /// </summary>		
        public string AuthDate
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime UpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsAuth
        {
            get;
            set;
        }
    }
}
