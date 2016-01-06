using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class DimRewardLevelModel
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
        /// 月利息收益指标
        /// </summary>
        public decimal Interest
        {
            get;
            set;
        }
        /// <summary>
        /// 奖励比例（考核利息收益指标）
        /// </summary>
        public decimal RewardScale
        {
            get;
            set;
        }
        /// <summary>
        /// 等级
        /// </summary>
        public int RankLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 等级说明
        /// </summary>
        public string LevelDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsDisable
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
        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }
    }
}
