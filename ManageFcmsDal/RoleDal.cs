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
    public class RoleDal
    {

        /// <summary>
        /// 获取所有已启用的角色ID，角色名称
        /// </summary>
        public DataSet GetRoleList()
        {
            string strSql = "select ID,Name from [Role] where [Status]=1";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
            return ds;
        }

        /// <summary>
        /// 添加角色 角色权限
        /// </summary>
        public bool Add(RoleModel model, string roleRight)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@Name", SqlDbType.NVarChar,50){Value=model.Name} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value=model.Status} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} ,            
                        new SqlParameter("@Description", SqlDbType.NVarChar,500){Value=model.Description} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime},
                        new SqlParameter("@RoleRight", SqlDbType.VarChar){Value = roleRight}      
            };

            int rows = SqlHelper.ExecuteNonQueryVal(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "[Proc_RoleAdd]", parameters);
            return rows == 1;
        }


        /// <summary>
        /// 更新角色信息，同时更新角色权限
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleRight">角色权限字符串</param>
        /// <returns></returns>
        public bool Update(RoleModel model, string roleRight)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,50){Value = model.Name},            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value = model.Status} ,                       
                        new SqlParameter("@Description", SqlDbType.NVarChar,500){Value = model.Description} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime},
                        new SqlParameter("@RoleRight", SqlDbType.VarChar){Value = roleRight}     
            };

            int rows = SqlHelper.ExecuteNonQueryVal(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "[Proc_RoleUpdate]", parameters);
            return rows == 1;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoleModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select [ID], [Name], [Status], [CreateTime], [Description], [UpdateTime]  ");
            strSql.Append("  from [Role] ");
            strSql.Append(" where [ID]=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};

            var model = new RoleModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }

                return model;
            }
            return null;
        }

        /// <summary>
        /// 获角色分页列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<RoleModel> GetRoleList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var fcmsUserList = new List<RoleModel>();
            string sqlCount = "select count(*) as totals from [Role]";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlCount = sqlCount + " where " + whereStr;
            }
            var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlCount);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            var sqlPage = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, [ID],[Name],[Status],[CreateTime],[UpdateTime] from [Role]";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = "Select [ID],[Name],[Status],[CreateTime],[UpdateTime] from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetRoleModel(reader);
                fcmsUserList.Add(info);
            }
            reader.Close();
            return fcmsUserList;
        }

        private static RoleModel GetRoleModel(IDataRecord dr)
        {
            var roleModel = new RoleModel
            {
                ID = ConvertHelper.ToInt(dr["ID"].ToString()),
                Name = dr["Name"].Equals(DBNull.Value) ? "" : dr["Name"].ToString(),
                Status = dr["Status"].Equals(DBNull.Value) ? 0 : ConvertHelper.ToInt(dr["Status"].ToString()),
                CreateTime = dr["CreateTime"].Equals(DBNull.Value) ? DateTime.Now : ConvertHelper.ToDateTime(dr["CreateTime"].ToString()),
                UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) ? DateTime.Now : ConvertHelper.ToDateTime(dr["UpdateTime"].ToString())
            };
            return roleModel;
        }

    }
}
