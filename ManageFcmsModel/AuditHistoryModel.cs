using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class AuditHistoryModel
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
        /// Process审核流程
        /// </summary>		
        public int Process
        {
            get;
            set;
        }
        /// <summary>
        /// Result审核结果
        /// </summary>		
        public bool Result
        {
            get;
            set;
        }
        /// <summary>
        /// UserID审核用户ID
        /// </summary>		
        public int UserID
        {
            get;
            set;
        }
        /// <summary>
        /// AuditTime//审核时间
        /// </summary>		
        public DateTime AuditTime
        {
            get;
            set;
        }
        /// <summary>
        /// Reason原因
        /// </summary>		
        public string Reason
        {
            get;
            set;
        }
        /// <summary>
        /// LoanID借款标ID
        /// </summary>		
        public int LoanID
        {
            get;
            set;
        }
        /// <summary>
        /// ReviewComments评审意见
        /// </summary>		
        public string ReviewComments
        {
            get;
            set;
        }

        /// <summary>
        /// ProcessName审核流程名称
        /// </summary>		
        public string ProcessName
        {
            get;
            set;
        }
        /// <summary>
        /// UserName用户名
        /// </summary>		
        public string UserName
        {
            get;
            set;
        }

    }
}
