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
    public class ReturnCashFeeBll
    {
        private readonly ReturnCashFeeDal _dal = new ReturnCashFeeDal();
        public DataSet GetList(string filter, string orderBy, int currentPage, int pageSize,  ref int rowsCount)
        {
            return _dal.GetList(filter, orderBy, currentPage, pageSize, ref rowsCount);
        }

        /// <summary>
        /// 新增活动奖励数据
        /// </summary>
        public int Add(ReturnCashFeeModel model)
        {
            return _dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(ReturnCashFeeModel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 线下充值复审通过事务处理
        /// </summary>
        public bool ReturnCashFeeAudit(ReturnCashFeeModel model)
        {
            return _dal.ReturnCashFeeAudit(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReturnCashFeeModel GetModel(int id)
        {
            return _dal.GetModel(id);
        }
    }
}
