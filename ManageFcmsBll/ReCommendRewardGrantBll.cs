using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class ReCommendRewardGrantBll
    {
        private readonly ReCommendRewardGrantDal _dal = new ReCommendRewardGrantDal();

        public DataSet GetList(string filter, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return _dal.GetList(filter, orderBy, currentPage, pageSize, ref rowsCount);
        }
        public DataSet GetList1(string filter, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return _dal.GetList1(filter, orderBy, currentPage, pageSize, ref rowsCount);
        }
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(ReCommendRewardGrantModel model)
        {
            return _dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(ReCommendRewardGrantModel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 复审成功事务处理
        /// </summary>
        public bool ReCommendRewardGrantSuccessHandler(ReCommendRewardGrantModel model)
        {
            return _dal.ReCommendRewardGrantSuccessHandler(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReCommendRewardGrantModel GetModel(string strWhere)
        {
            return _dal.GetModel(strWhere);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReCommendRewardGrantModel GetModel(int id)
        {
            return _dal.GetModel("ID=" + id);
        }
    }
}
