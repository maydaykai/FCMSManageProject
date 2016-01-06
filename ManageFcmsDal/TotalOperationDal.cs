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
    public class TotalOperationDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime dateStart, DateTime endDate)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@dateStart", SqlDbType.DateTime){Value=dateStart} ,  
                new SqlParameter("@dateEnd", SqlDbType.DateTime){Value=endDate}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcTotalOperation", parameters);
            return ds;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetMonthList(string yearstr)
        {
            var strSql = new StringBuilder();
            strSql.Append("select cast(TotalMonth as varchar(2)) + '月' as TotalMonth,AddRegUserNum,AddVipUserNum,AddBidUserNum,RegBidRate,BackflowUserNum,KeepUserNum,WithdrawUserNum,UserTotal,DealTotal,AdvanceTotal,LoanAmountTotal,LoanNumTotal from TotalOperationMonth where TotalYear = " + yearstr);
            strSql.Append(" order by TotalMonth ");
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }
    }
}
