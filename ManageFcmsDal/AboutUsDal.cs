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
    public class AboutUsDal
    {
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(AboutUsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AboutUs(");
            strSql.Append("ColumnID,Title,Content,CreateTime,UpdateTime");
            strSql.Append(") values (");
            strSql.Append("@ColumnID,@Title,@Content,@CreateTime,@UpdateTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@ColumnID", SqlDbType.Int,4){Value=model.ColumnID} ,            
                        new SqlParameter("@Title", SqlDbType.NVarChar,400){Value=model.Title} ,            
                        new SqlParameter("@Content", SqlDbType.NVarChar,-1){Value=model.Content} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime}             
              
            };

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(AboutUsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AboutUs set ");

            strSql.Append(" ColumnID = @ColumnID , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = @UpdateTime  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@ColumnID", SqlDbType.Int,4){Value = model.ColumnID} ,            
                        new SqlParameter("@Title", SqlDbType.NVarChar,400){Value = model.Title} ,            
                        new SqlParameter("@Content", SqlDbType.NVarChar,-1){Value = model.Content} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime}             
              
            };


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AboutUsModel GetModel(int columnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, ColumnID, Title, Content, CreateTime, UpdateTime  ");
            strSql.Append("  from AboutUs ");
            strSql.Append(" where ColumnID=@ColumnID");
            SqlParameter[] parameters = {
					new SqlParameter("@ColumnID", SqlDbType.Int,4)
			};
            parameters[0].Value = columnID;


            AboutUsModel model = new AboutUsModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ColumnID"].ToString() != "")
                {
                    model.ColumnID = int.Parse(ds.Tables[0].Rows[0]["ColumnID"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
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
            else
            {
                return null;
            }
        }
    }
}
