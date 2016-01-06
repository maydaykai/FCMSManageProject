using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class ProcBatSmsSend
    {
        ProcBatSmsSendDal _dal = new ProcBatSmsSendDal();
        /// <summary>
        /// 批量发送短信
        /// </summary>
        public bool BatSmsSend(string message, int chooseAll, int noBid, int canUseBalance, decimal useBalance, decimal useBalance1, int noBidMonth, int canBidAmount, decimal bidAmount, int userid, DateTime sendTime)
        {
            return _dal.BatSmsSend(message, chooseAll, noBid, canUseBalance, useBalance, useBalance1, noBidMonth, canBidAmount, bidAmount, userid, sendTime);
        }
    }
}
