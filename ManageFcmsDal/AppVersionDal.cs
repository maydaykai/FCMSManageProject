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
    public class AppVersionDal
    {

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(AppVersionModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AppVersion set ");

            strSql.Append(" OS = @OS , ");
            strSql.Append(" AppName = @AppName , ");
            strSql.Append(" Version = @Version , ");
            strSql.Append(" VersionNum = @VersionNum , ");
            strSql.Append(" DownURL = @DownURL , ");
            strSql.Append(" UpdateTime = getdate()  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@OS", SqlDbType.NChar,10){Value = model.OS} ,            
                        new SqlParameter("@AppName", SqlDbType.NChar,20){Value = model.AppName} ,            
                        new SqlParameter("@Version", SqlDbType.NChar,10){Value = model.Version} ,            
                        new SqlParameter("@VersionNum", SqlDbType.Int,4){Value = model.VersionNum} ,            
                        new SqlParameter("@DownURL", SqlDbType.NChar,100){Value = model.DownURL}          
              
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
        public AppVersionModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, OS, AppName, Version, VersionNum, DownURL, UpdateTime  ");
            strSql.Append("  from AppVersion ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            AppVersionModel model = new AppVersionModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ID = ds.Tables[0].Rows[0]["ID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                model.OS = ds.Tables[0].Rows[0]["OS"] != DBNull.Value ? ds.Tables[0].Rows[0]["OS"].ToString() : "";
                model.AppName = ds.Tables[0].Rows[0]["AppName"] != DBNull.Value ? ds.Tables[0].Rows[0]["AppName"].ToString() : "";
                model.Version = ds.Tables[0].Rows[0]["Version"] != DBNull.Value ? ds.Tables[0].Rows[0]["Version"].ToString() : "";
                model.VersionNum = ds.Tables[0].Rows[0]["VersionNum"] != DBNull.Value ? ds.Tables[0].Rows[0]["VersionNum"].ToString() : "";
                model.DownURL = ds.Tables[0].Rows[0]["DownURL"] != DBNull.Value ? ds.Tables[0].Rows[0]["DownURL"].ToString() : "";
                model.UpdateTime = ds.Tables[0].Rows[0]["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdateTime"]) : PublicConst.MinDate;

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
            const string tables = " AppVersion P";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
    }
}
