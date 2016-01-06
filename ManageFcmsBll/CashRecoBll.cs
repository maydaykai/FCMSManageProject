using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class CashRecoBll
    {
        private readonly CashRecoDal _fcmsUserDal = new CashRecoDal();

        /// <summary>
        /// 根据结算日期获取提现（通联）对账差异明细
        /// </summary>
        /// <param name="settlementDate"></param>
        /// <returns></returns>
        public List<CashRecoModel> GetCashRecoDifferenceModelList(string settlementDate)
        {
            return _fcmsUserDal.GetCashRecoDifferenceModelList(settlementDate);
        }
    }
}
