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
    public class MemberPointsDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddMemberPoints(DimMemberPointsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DimMemberPoints(");
            strSql.Append("Name,InterestManageFee,MinScore,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@Name,@InterestManageFee,@MinScore,@CreateTime");
            strSql.Append(");select @@IDENTITY ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Name", SqlDbType.NVarChar,50){Value= model.Name},
                        new SqlParameter("@InterestManageFee", SqlDbType.Decimal,9){Value= model.InterestManageFee},
                        new SqlParameter("@MinScore", SqlDbType.Int,4){Value= model.MinScore},
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value= DateTime.Now},
                        };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateMemberPoints(DimMemberPointsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DimMemberPoints set ");

            strSql.Append(" Name = @Name , ");
            strSql.Append(" InterestManageFee = @InterestManageFee , ");
            strSql.Append(" MinScore = @MinScore  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Name", SqlDbType.NVarChar,50){Value= model.Name},
                        new SqlParameter("@InterestManageFee", SqlDbType.Decimal,9){Value= model.InterestManageFee},
                        new SqlParameter("@MinScore", SqlDbType.Int,4){Value= model.MinScore},
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= model.ID}
                        };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimMemberPointsModel GetMemberPointsModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, Name, InterestManageFee, MinScore, CreateTime  ");
            strSql.Append(" from DimMemberPoints ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            DimMemberPointsModel model = new DimMemberPointsModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InterestManageFee"].ToString() != "")
                {
                    model.InterestManageFee = decimal.Parse(ds.Tables[0].Rows[0]["InterestManageFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinScore"].ToString() != "")
                {
                    model.MinScore = int.Parse(ds.Tables[0].Rows[0]["MinScore"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                #endregion
                return model;
            }
            return null;
        }
        /// <summary>
        /// 返回所有会员等级
        /// </summary>
        /// <returns></returns>
        public DataSet GetMemberPointsList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT * FROM dbo.DimMemberPoints ORDER BY MinScore ASC");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }
    }
}
