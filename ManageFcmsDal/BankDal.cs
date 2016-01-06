using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class BankDal
    {
        //获取银行列表
        public DataTable GetBankTypeList()
        {
            const string sqlStr = "SELECT ID,BankName,BankCode,EnglishName,(CAST(ID as varchar(20))+','+CAST(BankCode as varchar(20))) CodeId FROM dbo.Bank";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlStr);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
    }
}
