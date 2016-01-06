using ManageFcmsDal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class FundRecordBll
    {
        private readonly FundRecordDal _fundRecordDal = new FundRecordDal();

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _fundRecordDal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }

        /// <summary>
        /// 根据条件获取资金记录列表
        /// </summary>
        public DataTable GetFundRecordList(string fields, string filters, string sortStr, out int total)
        {
            return _fundRecordDal.GetFundRecordList(fields, filters, sortStr, out total);
        }

        /// <summary>
        /// 根据条件获取资金记录列表
        /// </summary>
        public DataTable GetFundRecordList(string filters)
        {
            return _fundRecordDal.GetFundRecordList(filters);
        }

        /// <summary>
        /// 获取资金流水
        /// </summary>
        public DataTable GetFundFlow(int memberID, string filter, int currentPage, int pageSize, string strOrderBy, out int recordCount)
        {
            return _fundRecordDal.GetFundFlow(memberID, filter, currentPage, pageSize, strOrderBy, out recordCount);
        }

        public bool ExeProcWithdrawInterestNH()
        {
            return _fundRecordDal.ExeProcWithdrawInterestNH();
        }

        /// <summary>
        /// 根据条件得到汇总金额
        /// </summary>
        public object Aggregate(string filters)
        {
            return _fundRecordDal.Aggregate(filters);
        }
    }
}
