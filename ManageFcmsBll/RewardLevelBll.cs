using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class RewardLevelBll
    {
        private readonly RewardLevelDal _dal = new RewardLevelDal();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int addDimRewardLevel(DimRewardLevelModel model)
        {
            return _dal.addDimRewardLevel(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool updateDimRewardLevel(DimRewardLevelModel model)
        {
            return _dal.updateDimRewardLevel(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimRewardLevelModel getModel(string strWhere)
        {
            return _dal.getDimRewardLevelModel(strWhere);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimRewardLevelModel getModel(int id)
        {
            return _dal.getDimRewardLevelModel("ID=" + id);
        }

        /// <summary>
        /// 返回所有推荐奖励比例
        /// </summary>
        /// <returns></returns>
        public DataSet GetRewardLevelList()
        {
            return _dal.GetRewardLevelList();
        }
    }
}
