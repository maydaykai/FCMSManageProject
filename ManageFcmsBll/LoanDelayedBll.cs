using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class LoanDelayedBll
    {
        private readonly ManageFcmsDal.LoanDelayedDal dal = new ManageFcmsDal.LoanDelayedDal();

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManageFcmsModel.LoanDelayedModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManageFcmsModel.LoanDelayedModel GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }
}
