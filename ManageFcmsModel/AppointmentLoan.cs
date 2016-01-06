using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    /// <summary>
    /// 预约借款
    /// </summary>
    public class AppointmentLoan
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 借款ID
        /// </summary>
        public int loanID { get; set; }
        /// <summary>
        /// 是否预约
        /// </summary>
        public int isAppointment { get; set; }
        /// <summary>
        /// 预约截至时间
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 正式开标时间
        /// </summary>
        public DateTime biddingTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
    }
}
