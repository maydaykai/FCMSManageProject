using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;
using ManageFcmsCommon;

namespace ManageFcmsBll
{
    public class CostSettingBll
    {

        private readonly CostSettingDal _costSettingDal = new CostSettingDal();

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(CostSettingModel model)
        {
            return _costSettingDal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(CostSettingModel model)
        {
            return _costSettingDal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return _costSettingDal.Delete(id);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CostSettingModel GetModel(int id)
        {
            return _costSettingDal.GetModel(id);
        }

        /// <summary>
        /// 获取设置费用分页列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<CostSettingModel> GetCostSetList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return _costSettingDal.GetCostSetList(whereStr, orderBy, currentPage, pageSize, ref  rowsCount);
        }

        /// <summary>
        ///获取费用比例
        /// </summary>
        /// <param name="computeEnumType">计算类型</param>
        /// <param name="feeType">费用类型</param>
        /// <param name="comparValue">比较值</param>
        /// <returns></returns>
        public decimal GetChargeRate(CostSettingModel.ComputeEnumType computeEnumType, FeeType.FeeTypeEnum feeType, decimal comparValue)
        {
            return _costSettingDal.GetChargeRate(computeEnumType, feeType, comparValue);
        }
    }
}
