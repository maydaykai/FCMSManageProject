using ManageFcmsConn;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsDal
{
    public class PayLimitationDal
    {

        /// <summary>
        /// 查询限额列表
        /// </summary>
        /// <param name="bankName">银行名称(可传)</param>
        /// <returns></returns>
        public DataSet GetData(string bankName)
        {
            StringBuilder sb = new StringBuilder("select * from Paylimitation");
            if (!string.IsNullOrEmpty(bankName))
            {
                sb.Append(string.Format(" where bankname like %{0}%", bankName));
            }
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sb.ToString());
        }

        public int Update(PayLimitation limitatioin)
        {
            string str = "update PayLimitation set BankName=@bankname , SinglePay=@singlepay ,SingleDay=@singleday ,SingleMonth=@singlemonth ,Remark=@remark where Id=@id";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@bankname",limitatioin.BankName),
                new SqlParameter("@singlepay",limitatioin.SinglePay),
                new SqlParameter("@singleday",limitatioin.SingleDay),
                new SqlParameter("@singlemonth",limitatioin.SingleMonth),
                new SqlParameter("@remark",limitatioin.Remark),
                new SqlParameter("@id",limitatioin.Id)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, str, para);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limitatioin"></param>
        /// <returns></returns>
        public int Add(PayLimitation limitatioin)
        {
            string str = "insert into PayLimitation(BankName,SinglePay,SingleDay,SingleMonth,Remark) values(@bankname,@singlepay,@singleday,@singlemonth,@remark)";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@bankname",limitatioin.BankName),
                new SqlParameter("@singlepay",limitatioin.SinglePay),
                new SqlParameter("@singleday",limitatioin.SingleDay),
                new SqlParameter("@singlemonth",limitatioin.SingleMonth),
                new SqlParameter("@remark",limitatioin.Remark),
                new SqlParameter("@id",limitatioin.Id)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, str, para);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            string str = string.Format("delete Paylimitation where id={0}", id);
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, str);
        }

    }
}
