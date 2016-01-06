using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ParameterSetModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 配置参数类型 
        /// <para>1 = 逾期天数:超过指定天数则默认为逾期</para>
        /// <para>2 = 6个月（含）以内的上限年利率</para>
        /// <para>3 = 6个月到1年（含）的年利率上限</para>
        /// <para>4 = 借款最小金额</para>
        /// <para>5 = 借款最大金额</para>
        /// <para>6 = 展期借款利率上浮度</para>
        /// <para>7 = 提前还款违约金投资人所得比例</para>
        /// <para>8 = 最高提现额</para>
        /// </summary>	
        public int ParameterType { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>		
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>		
        public decimal ParameterValue { get; set; }

        /// <summary>
        /// 参数单位
        /// </summary>		
        public string ParameterUnit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remarks { get; set; }

    }
}
