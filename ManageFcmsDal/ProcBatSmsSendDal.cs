using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class ProcBatSmsSendDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool BatSmsSend(string message, int chooseAll, int noBid, int canUseBalance, decimal useBalance, decimal useBalance1, int noBidMonth, int canBidAmount, decimal bidAmount, int userid, DateTime sendTime)
        {
            bool flag = false;
            SqlParameter[] parameters = {
                        new SqlParameter("@SmsContent", SqlDbType.NVarChar,200), 
                        new SqlParameter("@chooseAll", SqlDbType.Int,4),
                        new SqlParameter("@noBid", SqlDbType.Int,4),
                        new SqlParameter("@canUseBalance", SqlDbType.Int,4),
                        new SqlParameter("@useBalance", SqlDbType.Decimal,9),
                        new SqlParameter("@useBalance1", SqlDbType.Decimal,9),
                        new SqlParameter("@noBidMonth", SqlDbType.Int,4),
                        new SqlParameter("@canBidAmount", SqlDbType.Int,4),
                        new SqlParameter("@bidAmount", SqlDbType.Decimal,9),
                        new SqlParameter("@OpUID", SqlDbType.Int,4),
                        new SqlParameter("@SendTime", SqlDbType.DateTime)
            };

            parameters[0].Value = message;
            parameters[1].Value = chooseAll;
            parameters[2].Value = noBid;
            parameters[3].Value = canUseBalance;
            parameters[4].Value = useBalance;
            parameters[5].Value = useBalance1;
            parameters[6].Value = noBidMonth;
            parameters[7].Value = canBidAmount;
            parameters[8].Value = bidAmount;
            parameters[9].Value = userid;
            parameters[10].Value = sendTime;
            try
            {
                int num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcBatSmsSend", parameters);
                if (num > 0)
                {
                    flag = true;
                }
            }
            catch
            {

            }
            return flag;
        }
    }
}
