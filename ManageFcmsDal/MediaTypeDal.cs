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
    public class MediaTypeDal
    {
        public int AddMediaType(MediaTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO MediaType (Title,LogUrl,CreateTime,LogUrlName,LogUrlValue) ");
            strSql.Append(" Values(@Title,@LogUrl,@CreateTime,@LogUrlName,@LogUrlValue)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@Title", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@LogUrl", SqlDbType.VarChar,100), 
                        new SqlParameter("@CreateTime", SqlDbType.VarChar,100), 
                        new SqlParameter("@LogUrlName", SqlDbType.VarChar,100), 
                        new SqlParameter("@LogUrlValue", SqlDbType.Int,4)
            };

            parameters[0].Value = model.Title;
            parameters[1].Value = model.LogUrl;
            parameters[2].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[3].Value = model.LogUrlName;
            parameters[4].Value = model.LogUrlValue;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }

        public bool UpdateMediaType(MediaTypeModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update  MediaType set Title=@Title,LogUrl=@LogUrl,  ");
            strSql.Append(" LogUrlName=@LogUrlName,LogUrlValue=@LogUrlValue");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Title", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@LogUrl", SqlDbType.VarChar,100),
                        new SqlParameter("@LogUrlName", SqlDbType.VarChar,100),
                        new SqlParameter("@LogUrlValue", SqlDbType.Int,4),
                        new SqlParameter("@ID", SqlDbType.Int,4)
              
            };

            parameters[0].Value = model.Title;
            parameters[1].Value = model.LogUrl;
            parameters[2].Value = model.LogUrlName;
            parameters[3].Value = model.LogUrlValue;
            parameters[4].Value = model.ID;


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MediaTypeModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,LogUrl,LogUrlName,CreateTime,LogUrlValue ");
            strSql.Append("  from MediaType ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            MediaTypeModel model = new MediaTypeModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                model.LogUrl = ds.Tables[0].Rows[0]["LogUrl"].ToString();
                model.LogUrlName = ds.Tables[0].Rows[0]["LogUrlName"].ToString();
                model.LogUrlValue =Convert.ToInt32(ds.Tables[0].Rows[0]["LogUrlValue"].ToString());
                return model;
            }
            return null;
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " MediaType P";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
    }
}
