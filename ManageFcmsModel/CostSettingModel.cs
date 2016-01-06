using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;

namespace ManageFcmsModel
{
    public class CostSettingModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 费用版本ID
        /// </summary>		
        public int CostVersionId { get; set; }

        /// <summary>
        /// 费用类型
        /// </summary>		
        public int FeeType { get; set; }

        /// <summary>
        /// 计算方式:0-按天、1-按月、2-按金额
        /// </summary>		
        public int CalculationMode { get; set; }

        /// <summary>
        /// 计算方式初始值
        /// </summary>		
        public decimal CalInitialValue { get; set; }

        /// <summary>
        /// 计算方式初始比例
        /// </summary>		
        public decimal CalInitialProportion { get; set; }

        /// <summary>
        /// 增加方式：0-固定比例、1-以后按固定比例、2-以后按递增比例
        /// </summary>		
        public int IncreasingMode { get; set; }

        /// <summary>
        /// 增加初始值单位
        /// </summary>		
        public decimal IncreasUnit { get; set; }

        /// <summary>
        /// 增加比例值(%)
        /// </summary>		
        public decimal IncreasProportion { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>		
        public bool EnableStatus { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 费用名称
        /// </summary>		
        public string FeeTypeTitle
        {
            get
            {
                return ManageFcmsCommon.FeeType.GetNameByType((FeeType.FeeTypeEnum)FeeType);
            }
        }

        /// <summary>
        /// 费用计算类型
        /// </summary>
        public enum ComputeEnumType
        {
            /// <summary>
            /// 0=按天计算
            /// </summary>
            Day = 0,
            /// <summary>
            /// 1=按月计算
            /// </summary>
            Month = 1,
            /// <summary>
            /// 2=按金额计算
            /// </summary>
            Money = 2
        }
    }

}
