using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCMSModel;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class CreditAssignmentBll
    {
        private CreditAssignmentDal _dal = new CreditAssignmentDal();
        /// <summary>
        /// 获取债权转让分页
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public List<CreditAssignmentModel> GetPageCreditAssignmentModel(string Where, string OrderBy, int PageIndex,
                                                                        int PageSize, ref int TotalRows)
        {
            return _dal.GetPageCreditAssignmentModel(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }

        /// <summary>
        /// 获得转让投标记录详情
        /// </summary>
        public CreditAssignmentDetailModel GetCreditAssignmentDetail(int creditAssignmentId)
        {
            return _dal.GetCreditAssignmentDetail(creditAssignmentId);
        }

    }
}
