using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //TotalOperation
    public class TotalOperationModel
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
        /// 统计日期
        /// </summary>		
        public DateTime TotalDate
        {
            get;
            set;
        }
        /// <summary>
        /// 新增注册用户数
        /// </summary>		
        public int AddRegUserNum
        {
            get;
            set;
        }
        /// <summary>
        /// 新增实名认证用户数
        /// </summary>		
        public int AddRealAuthUserNum
        {
            get;
            set;
        }
        /// <summary>
        /// 新增VIP用户数
        /// </summary>		
        public int AddVipUserNum
        {
            get;
            set;
        }
        /// <summary>
        /// 新增充值用户数
        /// </summary>		
        public int AddRechargeUserNum
        {
            get;
            set;
        }
        /// <summary>
        /// 新增投资用户数
        /// </summary>		
        public int AddBidUserNum
        {
            get;
            set;
        }
        /// <summary>
        /// 注册用户总数
        /// </summary>		
        public int RegUserTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 实名认证用户总数
        /// </summary>		
        public int RealUserTotal
        {
            get;
            set;
        }
        /// <summary>
        /// VIP用户总数
        /// </summary>		
        public int VipUserTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 投资用户总数
        /// </summary>		
        public int BidUserTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 未投资VIP用户总数
        /// </summary>		
        public int NoBidVipUserTotal
        {
            get;
            set;
        }
        /// <summary>
        /// 注册投资比
        /// </summary>		
        public decimal RegBidRate
        {
            get;
            set;
        }

    }
}
