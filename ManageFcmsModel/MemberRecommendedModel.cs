using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
   public class MemberRecommendedModel
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
        /// 推荐人ID
        /// </summary>
        public int RecMemberID
        {
            get;
            set;
        }

        /// <summary>
        /// 被推荐人ID
        /// </summary>
        public int RecedMemberID
        {
            get;
            set;
        }

        /// <summary>
        /// 奖金
        /// </summary>
        public Decimal Bonus
        {
            get;
            set;
        }

        /// <summary>
        /// 奖金发放状态：0-未发放、1-已发放
        /// </summary>
        public int BonusPutStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }
    }
}
