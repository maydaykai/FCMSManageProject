using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class RepaymentDetailDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetRepaymentDetailList(int loanId, int repaymentType)
        {
            var strSql = new StringBuilder();
            strSql.Append("exec ProcRepaymentDetail " + loanId.ToString() + "," + repaymentType.ToString());

            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }
    }
}
