using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class ParameterSetBll
    {
        private readonly ParameterSetDal _parameterSetDal = new ParameterSetDal();

        /// <summary>
        /// 更新所有参数设置数据
        /// </summary>
        public bool Update(List<ParameterSetModel> list)
        {
            return _parameterSetDal.Update(list);
        }

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
        public ParameterSetModel GetParameterSetByParameType(int parameterType)
        {
            return _parameterSetDal.GetParameterSetByParameType(parameterType);
        }

        /// <summary>
        /// 获取所有参数设置数据
        /// </summary>
        public List<ParameterSetModel> GetParameterSetList()
        {
            return _parameterSetDal.GetParameterSetList();
        }
    }
}
