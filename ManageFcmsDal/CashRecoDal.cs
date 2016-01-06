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
    public class CashRecoDal
    {
        /// <summary>
        /// 根据结算日期获取提现（通联）对账差异明细
        /// </summary>
        /// <param name="settlementDate"></param>
        /// <returns></returns>
        public List<CashRecoModel> GetCashRecoDifferenceModelList(string settlementDate)
        {
            const string strSql = @"SELECT MemberName=ISNULL((SELECT MemberName FROM dbo.Member WHERE ID=C.MemberID),''),
                              CR.TransactionAmount,CR.AllinpaySerialNumber,CR.TransactionStatusCode,
                              AllinPayStatus=CASE WHEN CR.PaymentStatus=1 THEN '支付成功' WHEN CR.PaymentStatus=0 THEN '中间状态' ELSE '支付失败' END,
                              RjbStatus=CASE WHEN C.CashStatus=1 THEN '支付成功' ELSE '支付失败' END
                              FROM dbo.CashReconciliation CR LEFT JOIN dbo.CashRecord C 
                              ON C.REQ_SN= CR.AllinpaySerialNumber
                              WHERE SettlementDate=@SettlementDate AND (CR.PaymentStatus<>1 OR (CR.PaymentStatus=1 AND C.CashStatus<>1))";

            SqlParameter[] parameter =
                {
                    new SqlParameter("@SettlementDate",SqlDbType.Char,10){Value = settlementDate}
                };

            var rrdList = new List<CashRecoModel>();
            var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameter);
            while (reader.Read())
            {
                var info = GetCashRecoModel(reader);
                if (info == null) continue;
                rrdList.Add(info);
            }
            reader.Close();
            return rrdList;
        }


        private static CashRecoModel GetCashRecoModel(SqlDataReader dr)
        {
            var cashRecoModel = new CashRecoModel
            {
                MemberName = dr["MemberName"].Equals(DBNull.Value) ? "" : dr["MemberName"].ToString(),
                AllinpaySerialNumber = dr["AllinpaySerialNumber"].Equals(DBNull.Value) ? "" : dr["AllinpaySerialNumber"].ToString(),
                TransactionAmount = dr["TransactionAmount"].Equals(DBNull.Value) ? "" : dr["TransactionAmount"].ToString(),
                TransactionStatusCode = dr["TransactionStatusCode"].Equals(DBNull.Value) ? "" : dr["TransactionStatusCode"].ToString(),
                AllinPayStatus = dr["AllinPayStatus"].Equals(DBNull.Value) ? "" : dr["AllinPayStatus"].ToString(),
                RjbStatus = dr["RjbStatus"].Equals(DBNull.Value) ? "" : dr["RjbStatus"].ToString()
            };
            return cashRecoModel;
        }
    }
}
