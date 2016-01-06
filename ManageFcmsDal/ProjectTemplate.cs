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
    public class ProjectTemplateDal
    {
        /// <summary>
        /// 获取用户对应未发标借款标号数据列表
        /// </summary>
        public DataSet GetProductTemplate(int productTypeId)
        {
            var strSql = new StringBuilder();
            strSql.Append("select Template from DimProjectTemplate where ProductTypeId = @productTypeId");
            SqlParameter[] parameters = {
			            new SqlParameter("@ProductTypeId", SqlDbType.Int,4){Value = productTypeId}         
            };
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }
    }
}
