using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class RechargeReconciliationBll
    {
        private readonly RechargeReconciliationDal _fcmsUserDal = new RechargeReconciliationDal();

        /// <summary>
        /// 根据结算日期获取充值（通联）对账差异明细
        /// </summary>
        /// <param name="settlementDate"></param>
        /// <returns></returns>
        public List<RechargeRecoDifferenceModel> GetRechargeRecoDifferenceModelList(string settlementDate)
        {
            return _fcmsUserDal.GetRechargeRecoDifferenceModelList(settlementDate);
        }
    }
}
