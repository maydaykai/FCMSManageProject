using ManageFcmsDal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class RechargeRecordBll
    {
        private readonly RechargeRecordDal _rechargeRecordDal = new RechargeRecordDal();

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _rechargeRecordDal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }

        /// <summary>
        /// 总计
        /// </summary>
        public object Aggregate(string filters)
        {
            return _rechargeRecordDal.Aggregate(filters);
        }

        /// <summary>
        /// 线下充值申请
        /// </summary>
        public int Add(RechargeRecordModel model)
        {
            return _rechargeRecordDal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(RechargeRecordModel model)
        {
            return _rechargeRecordDal.Update(model);
        }

        /// <summary>
        /// 线下充值复审通过事务处理
        /// </summary>
        public int RechargeBelowLine(RechargeRecordModel model)
        {
            return _rechargeRecordDal.RechargeBelowLine(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RechargeRecordModel GetModel(int id)
        {
            return _rechargeRecordDal.GetModel(id);
        }
    }
}
