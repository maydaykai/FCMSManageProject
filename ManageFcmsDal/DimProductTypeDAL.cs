using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using System.Data.SqlClient;

namespace ManageFcmsDal
{
    public class DimProductTypeDAL
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetProductList()
        {
            const string strSql = "SELECT ID,ParentId,Name,Designate=ISNULL((SELECT COUNT(ID) FROM dbo.DimProductScore WHERE ProductTypeId=PT.ID),0) FROM dbo.DimProductType PT";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
            return ds;
        }

        /// <summary>
        /// 获取某个产品下面的所有项目
        /// </summary>
        /// <returns></returns>
        public DataSet GePtProjectList(int productId)
        {
            const string strSql = "SELECT [ID],[ProductTypeId],[ScoreItems],[FullMarks],[Remark] FROM dbo.DimProductScore WHERE ProductTypeId=@ProductTypeId";
            var parameter = new[]
                {
                    new SqlParameter("@ProductTypeId",SqlDbType.Int,4){Value = productId}
                };
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameter);
            return ds;
        }
    }
}
