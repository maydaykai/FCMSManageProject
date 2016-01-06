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
    public class RightDal
    {
        /// <summary>
        /// 返回所有权限组
        /// </summary>
        public DataSet GetRightList(int sign)
        {
            string strSql = sign == 0 ? @"select [ID],[RightName] from [dbo].[Right]" : @"select [ID],[RightName] from [dbo].[Right] where [Visible]=1";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
            return ds;
        }


        /// <summary>
        /// 增加
        /// </summary>
        public int Add(RightModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into [Right](");
            strSql.Append("RightName,Visible,CreateTime,UpdateTime");
            strSql.Append(") values (");
            strSql.Append("@RightName,@Visible,@CreateTime,@UpdateTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@RightName", SqlDbType.NVarChar,50){Value=model.RightName} ,            
                        new SqlParameter("@Visible", SqlDbType.Bit,1){Value=model.Visible} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime}                        
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : ConvertHelper.ToInt(obj.ToString());
        }


        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(RightModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update [Right] set ");
            strSql.Append(" RightName = @RightName , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" UpdateTime = @UpdateTime  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@RightName", SqlDbType.NVarChar,50){Value = model.RightName} ,            
                        new SqlParameter("@Visible", SqlDbType.Bit,1){Value = model.Visible} ,                    
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime}             
              
            };

            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RightModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, RightName, Visible, CreateTime, UpdateTime  ");
            strSql.Append("  from [Right] ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};
            var model = new RightModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.RightName = ds.Tables[0].Rows[0]["RightName"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Visible"].ToString() == "1") || (ds.Tables[0].Rows[0]["Visible"].ToString().ToLower() == "true"))
                    {
                        model.Visible = true;
                    }
                    else
                    {
                        model.Visible = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }

                return model;
            }
            return null;
        }
    }
}
