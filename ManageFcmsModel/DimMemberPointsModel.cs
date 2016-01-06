using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class DimMemberPointsModel
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
        /// 会员等级名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 该等级应收利息管理费
        /// </summary>
        public decimal InterestManageFee
        {
            get;
            set;
        }
        /// <summary>
        /// 等级积分下限
        /// </summary>
        public int MinScore
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
    }
}
