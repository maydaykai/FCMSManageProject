using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class RechargeReconciliationDal
    {
        /// <summary>
        /// 根据结算日期获取充值（通联）对账差异明细
        /// </summary>
        /// <param name="settlementDate"></param>
        /// <returns></returns>
        public List<RechargeRecoDifferenceModel> GetRechargeRecoDifferenceModelList(string settlementDate)
        {
            string strSql = @"SELECT MemberName=ISNULL((SELECT MemberName FROM dbo.Member WHERE ID=RC.MemberID),''),
                            RR.AllinpaySerialNumber,RR.MerchantOrderNumber,RR.PaymentStatus,
                            AllinPayStatus=CASE WHEN RR.[Status]=1 THEN '支付成功' ELSE '支付失败' END,
                            RjbStatus=CASE WHEN RC.[Status]=1 THEN '支付成功' ELSE '支付失败' END
                            FROM dbo.RechargeReconciliation RR,dbo.RechargeRecord RC 
                            WHERE RR.MerchantOrderNumber=RC.MerchantOrderNo AND DATEDIFF(DAY,RR.SettlementDate,'" + ConvertHelper.ToDateTime(settlementDate) + "')=0 AND (RR.[Status]<>1 OR (RR.[Status]=1 AND RC.[Status]<>1))";

            var rrdList = new List<RechargeRecoDifferenceModel>();
            var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
            while (reader.Read())
            {
                var info = GetRechargeRecoDifferenceModel(reader);
                if (info == null) continue;
                rrdList.Add(info);
            }
            reader.Close();
            return rrdList;
        }


        private static RechargeRecoDifferenceModel GetRechargeRecoDifferenceModel(SqlDataReader dr)
        {
            var rechargeRecoDifferenceModel = new RechargeRecoDifferenceModel
                {
                    MemberName = dr["MemberName"].Equals(DBNull.Value) ? "" : dr["MemberName"].ToString(),
                    AllinpaySerialNumber = dr["AllinpaySerialNumber"].Equals(DBNull.Value) ? "" : dr["AllinpaySerialNumber"].ToString(),
                    MerchantOrderNumber = dr["MerchantOrderNumber"].Equals(DBNull.Value) ? "" : dr["MerchantOrderNumber"].ToString(),
                    PaymentStatus = dr["PaymentStatus"].Equals(DBNull.Value) ? "" : dr["PaymentStatus"].ToString(),
                    AllinPayStatus = dr["AllinPayStatus"].Equals(DBNull.Value) ? "" : dr["AllinPayStatus"].ToString(),
                    RjbStatus = dr["RjbStatus"].Equals(DBNull.Value) ? "" : dr["RjbStatus"].ToString()
                };
            return rechargeRecoDifferenceModel;
        }
    }
}
