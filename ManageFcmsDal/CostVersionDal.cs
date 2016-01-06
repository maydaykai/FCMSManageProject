using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class CostVersionDal
    {
        /// <summary>
        /// 返回所有费用版本
        /// </summary>
        /// <returns></returns>
        public DataSet GetCostVersionList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT [ID],[VersionName],[CreateTime],[UpdateTime],[EnableStatus] FROM dbo.[CostVersion] ORDER BY ID DESC");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }
    }
}
