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
    public class BidBLL
    {
        readonly BidDAL _dal = new BidDAL();

        /// <summary>
        /// 根据借款ID获取所有投资人信息
        /// </summary>
        /// <param name="loanId">借款ID</param>
        /// <returns></returns>
        public List<BidModel> GetBidListByLoanId(int loanId)
        {
            return _dal.GetBidListByLoanId(loanId);
        }

        /// <summary>
        /// 根据条件获取所有投资人信息
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns>
        public List<BidModel> GetBidRecordList(string Where)
        {
            return _dal.GetBidRecordList(Where);
        }

        /// <summary>
        /// 获取投资记录
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public DataTable GetBidRecordDt(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            return _dal.GetBidRecordDt(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }

        /// <summary>
        /// 返回受让人列表（债权转让合同）
        /// </summary>
        /// <param name="caId"></param>
        /// <returns></returns>
        public DataSet GetCaBidListByCaID(int caId)
        {
            return _dal.GetCaBidListByCaID(caId);
        }
    }
}
