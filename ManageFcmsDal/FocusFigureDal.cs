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
    public class FocusFigureDal
    {
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(FocusFigureModel model)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FocusFigure(");
            strSql.Append("Url,Title,LargePicture,SmallPicture,Status,CreateTime,UpdateTime,ShowDesc");
			strSql.Append(") values (");
            strSql.Append("@Url,@Title,@LargePicture,@SmallPicture,@Status,@CreateTime,@UpdateTime,@ShowDesc");            
            strSql.Append(") ");            
            strSql.Append(";select SCOPE_IDENTITY()");		
			SqlParameter[] parameters = {
			            new SqlParameter("@Url", SqlDbType.NVarChar,800){Value=model.Url} ,            
                        new SqlParameter("@Title", SqlDbType.NVarChar,200){Value=model.Title} ,            
                        new SqlParameter("@LargePicture", SqlDbType.NVarChar,100){Value=model.LargePicture} ,            
                        new SqlParameter("@SmallPicture", SqlDbType.NVarChar,100){Value=model.SmallPicture} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value=model.Status} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} , 
                        new SqlParameter("@ShowDesc", SqlDbType.Int,4){Value=model.ShowDesc} , 
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
        public bool Update(FocusFigureModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FocusFigure set ");

            strSql.Append(" Url = @Url , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" LargePicture = @LargePicture , ");
            strSql.Append(" SmallPicture = @SmallPicture , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = @UpdateTime,  ");
            strSql.Append(" ShowDesc = @ShowDesc  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@Url", SqlDbType.NVarChar,800){Value = model.Url} ,            
                        new SqlParameter("@Title", SqlDbType.NVarChar,200){Value = model.Title} ,            
                        new SqlParameter("@LargePicture", SqlDbType.NVarChar,100){Value = model.LargePicture} ,            
                        new SqlParameter("@SmallPicture", SqlDbType.NVarChar,100){Value = model.SmallPicture} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value = model.Status} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,    
                        new SqlParameter("@ShowDesc", SqlDbType.Int,4){Value = model.ShowDesc} ,   
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
        public FocusFigureModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, Url, Title, LargePicture, SmallPicture, Status, CreateTime, UpdateTime ,ShowDesc ");
            strSql.Append("  from FocusFigure ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            FocusFigureModel model = new FocusFigureModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.LargePicture = ds.Tables[0].Rows[0]["LargePicture"].ToString();
                model.SmallPicture = ds.Tables[0].Rows[0]["SmallPicture"].ToString();
                model.ShowDesc = Convert.ToInt32(ds.Tables[0].Rows[0]["ShowDesc"].ToString());
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
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
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " FocusFigure P";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
    }
}
