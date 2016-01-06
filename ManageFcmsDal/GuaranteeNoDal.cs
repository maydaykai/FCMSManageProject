using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class GuaranteeNoDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddGuaranteeNo(GuaranteeNoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GuaranteeNo(");
            strSql.Append("GuaranteeId,GuaranteeNo,UpdateTime");
            strSql.Append(") values (");
            strSql.Append("@GuaranteeId,@GuaranteeNo,@UpdateTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] paras = {
			            new SqlParameter("@GuaranteeId", SqlDbType.Int,4){Value= model.GuaranteeId},
                        new SqlParameter("@GuaranteeNo", SqlDbType.VarChar,50){Value= model.GuaranteeNo},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= model.UpdateTime},
                        };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), paras);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 获取保函列表
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public List<GuaranteeNoModel> GetPageGuaranteeNoModel(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            List<GuaranteeNoModel> list = new List<GuaranteeNoModel>();
            string sql1 = "select count(*) as totals from GuaranteeNo";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " where " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                TotalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + OrderBy + ")) AS rownum, G.*,F.AnotherName GuaranteeName from GuaranteeNo G left join dbo.FcmsUser F on G.GuaranteeId=F.ID";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " where " + Where;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + (PageIndex - 1) * PageSize + " and " + PageIndex * PageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                GuaranteeNoModel info = getGuaranteeNoModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private GuaranteeNoModel getGuaranteeNoModelByDr(SqlDataReader dr)
        {
            GuaranteeNoModel model = new GuaranteeNoModel
                {
                    ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                    GuaranteeId = dr["GuaranteeId"] != DBNull.Value ? Convert.ToInt32(dr["GuaranteeId"]) : 0,
                    GuaranteeName = dr["GuaranteeName"] != DBNull.Value ? dr["GuaranteeName"].ToString() : "",
                    GuaranteeNo = dr["GuaranteeNo"] != DBNull.Value ? dr["GuaranteeNo"].ToString() : "",
                    UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : DateTime.MinValue
                };
            return model;
        }
    }
}
