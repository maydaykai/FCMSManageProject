using ManageFcmsCommon;
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
    public class Marketing_RoleDAL
    {

        /// <summary>
        /// 给用户分配角色
        /// </summary>
        public DataSet GetRoleList()
        {
            string strSql = "SELECT Id,RoleName FROM Marketing_RoleName where [Status]=1";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
            return ds;
        }

        public DataSet GetSystemRoleList()
        {
            string strSql = "SELECT ID,UserName FROM dbo.FcmsUser   WHERE IsLock=0";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
            return ds;
        }

        //保存用户角色
        public int UserJoinRoleSave(string RoleXml, int UserInfoId,int type)
        {

            SqlParameter[] parameters = {
					new SqlParameter("@xml", SqlDbType.Xml){Value = RoleXml},
                    new SqlParameter("@userId",SqlDbType.Int,4){Value = UserInfoId},
                    new SqlParameter("@type",SqlDbType.Int,4){Value = type}
			};
            var relust = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Marketing_UserJoinRole_Save", parameters);
            return Convert.ToInt32(relust.ToString());
        }
        /// <summary>
        /// 查询当前角色查询所属角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetRoleListByUserInfoId(int userId,int type)
        {
            string strSql = "SELECT * FROM Marketing_UserJoinRole where RoleNameId=@RoleNameId and [Type]=@Type";
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleNameId",SqlDbType.Int,4){Value = userId},
                    new SqlParameter("@Type",SqlDbType.Int,4){Value = type}
			};
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameters);
            return ds;
        }

        /// <summary>
        /// 查询当前登录用户的营销角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetMarkingRoleListByUserInfoId(int userId, int type)
        {
            string strSql = "SELECT * FROM Marketing_UserJoinRole where UserInfoId=@UserInfoId and [Type]=@Type ORDER BY RoleNameId ASC ";
            SqlParameter[] parameters = {
                    new SqlParameter("@UserInfoId",SqlDbType.Int,4){Value = userId},
                    new SqlParameter("@Type",SqlDbType.Int,4){Value = type}
			};
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameters);
            return ds;
        }
        #region 角色模块
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<Marketing_RoleNameModel> GetRoleListToPage(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var fcmsUserList = new List<Marketing_RoleNameModel>();
           
            string sqlCount = "select count(*) as totals from Marketing_RoleName";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlCount = sqlCount + " where " + whereStr;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlCount);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            var sqlPage = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, [ID],[Status],[RoleName],[RoleCode] from Marketing_RoleName";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = "Select [Id],[Status],[RoleName],[RoleCode] from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetFcmsUserModel(reader);
                fcmsUserList.Add(info);
            }
            reader.Close();
            return fcmsUserList;
        }

        private static Marketing_RoleNameModel GetFcmsUserModel(SqlDataReader dr)
        {
            var RloleModel = new Marketing_RoleNameModel
                {
                    ID = ConvertHelper.ToInt(dr["ID"].ToString()),
                    Status = ConvertHelper.ToInt(dr["Status"].ToString()),
                    RoleName = dr["RoleName"].Equals(DBNull.Value) ? "" : dr["RoleName"].ToString(),
                    RoleCode = dr["RoleCode"].Equals(DBNull.Value) ? "" : dr["RoleCode"].ToString(),
                    PartentID = ConvertHelper.ToInt(dr["PartentID"].ToString()),
                    Leave = ConvertHelper.ToInt(dr["Leave"].ToString()),
                    PName = dr["PName"].Equals(DBNull.Value) ? "" : dr["PName"].ToString()

                };
            return RloleModel;
        }

        public Marketing_RoleNameModel GetRoleNameModelById(int Id)
        {
            Marketing_RoleNameModel model = null;
            string sql = " SELECT  *,RoleName as PName FROM dbo.Marketing_RoleName WHERE Id=@id ";
            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) };
            parameters[0].Value = Id;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text,sql, parameters);
            if (reader.Read())
            {
                model = GetFcmsUserModel(reader);
               
            }
            reader.Close();
            return model;
        }

        //增加角色
        public int AddRole(Marketing_RoleNameModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Marketing_RoleName (RoleName,Status,RoleCode,PartentID,Leave) ");
            strSql.Append(" Values(@RoleName,@Status,@RoleCode,@PartentID,@Leave)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@RoleName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4), 
                        new SqlParameter("@RoleCode", SqlDbType.VarChar,100),
                        new SqlParameter("@PartentID", SqlDbType.Int,4),
                        new SqlParameter("@Leave", SqlDbType.Int,4)
 
              
            };

            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.Status;
            parameters[2].Value = model.RoleCode;
            parameters[3].Value = model.PartentID;
            parameters[4].Value = model.Leave;


            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }

        //修改角色
        public int UpdateRole(Marketing_RoleNameModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE Marketing_RoleName SET RoleName=@RoleName   where Id=@Id");
            SqlParameter[] parameters = {
			            new SqlParameter("@RoleName", SqlDbType.VarChar,100) ,            
                           new SqlParameter("@Id",SqlDbType.Int,4)
 
              
            };

            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.ID;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }


        #endregion

        #region 权限模块
        //基本模块列表
        public List<Marketing_FunctionOperationModel> GetFunctionOperationPageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var fcmsUserList = new List<Marketing_FunctionOperationModel>();
            string sqlCount = "select count(*) as totals from Marketing_FunctionOperation";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlCount = sqlCount + " where " + whereStr;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlCount);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            var sqlPage = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, [Id],[OperationName],[OperationCode],[Remake] from Marketing_FunctionOperation";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = "Select [Id],[OperationName],[OperationCode],[Remake] from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetFunctionOperationModel(reader);
                fcmsUserList.Add(info);
            }
            reader.Close();
            return fcmsUserList;
        }

        private static Marketing_FunctionOperationModel GetFunctionOperationModel(SqlDataReader dr)
        {
            var FunctionOperationModel = new Marketing_FunctionOperationModel
            {
                Id = ConvertHelper.ToInt(dr["ID"].ToString()),
                OperationCode = dr["OperationCode"].Equals(DBNull.Value) ? "" : dr["OperationCode"].ToString(),
                OperationName = dr["OperationName"].Equals(DBNull.Value) ? "" : dr["OperationName"].ToString(),
                Remake = dr["Remake"].Equals(DBNull.Value) ? "" : dr["Remake"].ToString()
            };
            return FunctionOperationModel;
        }

        //权限列表
        public List<Marketing_CompetenceModel> GetCompetencePageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var fcmsUserList = new List<Marketing_CompetenceModel>();
            string sqlCount = "select count(*) as totals from Marketing_Competence";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlCount = sqlCount + " where " + whereStr;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlCount);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            var sqlPage = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, [Id],[CompetenceType] from Marketing_Competence";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = "Select [Id],[CompetenceType] from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetCompetenceModel(reader);
                fcmsUserList.Add(info);
            }
            reader.Close();
            return fcmsUserList;
        }

        private static Marketing_CompetenceModel GetCompetenceModel(SqlDataReader dr)
        {
            var FunctionOperationModel = new Marketing_CompetenceModel
            {
                Id = ConvertHelper.ToInt(dr["ID"].ToString()),
                CompetenceType = dr["CompetenceType"].Equals(DBNull.Value) ? "" : dr["CompetenceType"].ToString()

            };
            return FunctionOperationModel;
        }

        //


        #endregion

        #region 自定义功能模块
        //新增功能
        public int AddFunctionOperation(Marketing_FunctionOperationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Marketing_FunctionOperation (OperationName,OperationCode,Remake,OperType) ");
            strSql.Append(" Values(@OperationName,@OperationCode,@Remake,@OperType)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@OperationName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@OperationCode", SqlDbType.VarChar,100), 
                        new SqlParameter("@Remake", SqlDbType.VarChar,100),
                        new SqlParameter("@OperType", SqlDbType.VarChar,200)
 
              
            };

            parameters[0].Value = model.OperationName;
            parameters[1].Value = model.OperationCode;
            parameters[2].Value = model.Remake;
            parameters[3].Value = model.OperType;


            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }
        //修改功能
        public int UpdateFunctionOperation(Marketing_FunctionOperationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE Marketing_FunctionOperation SET OperationName=@OperationName,OperationCode=@OperationCode,Remake=@Remake,OperType=@OperType   where Id=@Id");
            SqlParameter[] parameters = {
			            new SqlParameter("@OperationName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@OperationCode", SqlDbType.VarChar,100), 
                        new SqlParameter("@Remake", SqlDbType.VarChar,100),
                        new SqlParameter("@OperType", SqlDbType.VarChar,200),
                        new SqlParameter("@Id",SqlDbType.Int,4)
 
              
            };

            parameters[0].Value = model.OperationName;
            parameters[1].Value = model.OperationCode;
            parameters[2].Value = model.Remake;
            parameters[3].Value = model.OperType;
            parameters[4].Value = model.Id;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }
        //根据Id查询功能
        public Marketing_FunctionOperationModel GetFunctionOperationById(int Id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select [Id], [OperationName], [OperationCode], [Remake],[OperType]  ");
            strSql.Append("  from [Marketing_FunctionOperation] ");
            strSql.Append(" where [ID]=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = Id}
			};

            var model = new Marketing_FunctionOperationModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.OperationName = ds.Tables[0].Rows[0]["OperationName"].ToString();
                model.OperationCode = ds.Tables[0].Rows[0]["OperationCode"].ToString();
                model.Remake = ds.Tables[0].Rows[0]["Remake"].ToString();
                return model;
            }
            return null;
        }

        /// <summary>
        /// 同步营销人员
        /// </summary>
        /// <param name="mouth"></param>
        public void SynchronizeEx_Person(string mouth)
        { 
            //同步营销人员信息
            SqlParameter[] parameters = {
					new SqlParameter("@mouth", SqlDbType.VarChar,20){Value = mouth}
			};
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_AddMarketing_Ex_Person", null);
        }
        #endregion

        #region 用户关联角色
        /// <summary>
        /// 根据当前用户查询所属的角色
        /// </summary>
        /// <returns></returns>
        public List<Marketing_UserJoinRoleModel> GetUserJoinRoleByUserId(int UserInfoId)
        {
            List<Marketing_UserJoinRoleModel> list = new List<Marketing_UserJoinRoleModel>();
            var strSql = new StringBuilder();
            strSql.Append(" SELECT [Id],[UserInfoId],[RoleNameId] FROM Marketing_UserJoinRole WHERE UserInfoId=@UserInfoId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserInfoId", SqlDbType.Int,4){Value = UserInfoId}
                  
			};

            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (reader.Read())
            {
                var info = GetUserJoinRoleModel(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private static Marketing_UserJoinRoleModel GetUserJoinRoleModel(SqlDataReader dr)
        {
            var FunctionOperationModel = new Marketing_UserJoinRoleModel
            {
                Id = ConvertHelper.ToInt(dr["ID"].ToString()),
                UserInfoId = ConvertHelper.ToInt(dr["UserInfoId"].ToString()),
                RoleNameId = ConvertHelper.ToInt(dr["RoleNameId"].ToString()),

            };
            return FunctionOperationModel;
        }


        //查询角色树行

        /// <summary>
        /// 获取栏目列表 相关列数据
        /// </summary>
        /// <param name="fields"> ID, UpdateTime, Description, Name, LinkUrl, ICon, ParentID, Sort, Visible, CreateTime</param>
        /// <param name="visible">0:获取所有栏目 1：获取启用的栏目</param>
        /// <returns></returns>
        public DataSet GetColumnlList(int visible)
        {
            var strSql = new StringBuilder();
            strSql.Append("select [Id],[RoleName],[Status],[RoleCode],[PartentID],[Leave]  from  Marketing_RoleName  ");
           // strSql.Append("  from [Column] ");
            if (visible == 1)
            {
                strSql.Append("  where Status=" + visible);
            }

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 分配权限列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<Marketing_RoleNameModel> GetRoleNamePageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            StringBuilder tables = new StringBuilder();
            tables.Append("(SELECT a.*,b.RoleName AS PName FROM dbo.Marketing_RoleName a ");
            tables.Append(" LEFT JOIN (SELECT RoleName,ID FROM dbo.Marketing_RoleName ) AS b ON b.ID= a.partentID WHERE a.leave=3) AS temp ");
            var marketing_roleList = new List<Marketing_RoleNameModel>();
            string sqlCount = "select count(1) as totals from " + tables;
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlCount = sqlCount + " where " + whereStr;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlCount);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            var sqlPage = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, [Id],[RoleName],[Status],[RoleCode],[PartentID],[Leave],[PName] from " + tables.ToString();
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = "Select [Id],[RoleName],[Status],[RoleCode],[PartentID],[Leave],[PName] from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetFcmsUserModel(reader);
                marketing_roleList.Add(info);
            }
            reader.Close();
            return marketing_roleList;
        }


        //分配人员 可以分配的列表
        public DataSet GetDistributionTable(int Id)
        {
            var strSql = new StringBuilder();
            //可以分配的人员=没有分配，且不存在角色中,且包含自己所属组
            strSql.Append("SELECT Id,Name FROM dbo.Marketing_Ex_Person  WHERE  id NOT IN(SELECT userInfoID FROM Marketing_UserJoinRole where [type]=0 AND RoleNameId>0 GROUP BY  userInfoID) and IsAllocation=0  UNION ");
            strSql.Append(" SELECT a.Id,Name FROM dbo.Marketing_Ex_Person   a  LEFT JOIN  dbo.Marketing_Ex_PersonToRole b ON a.Id=b.PersonId  WHERE b.RoleId=@Id");
            strSql.Append(" SELECT c.Id,c.Name FROM Marketing_UserJoinRole a INNER JOIN dbo.Marketing_RoleName b ON a.RoleNameId=b.Id AND b.Leave=3 ");
            strSql.Append(" AND a.[Type]=0 INNER JOIN dbo.Marketing_Ex_Person  c ON c.Id=a.UserInfoId  WHERE a.RoleNameId=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id",SqlDbType.Int,4){Value = Id},
			};

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(),parameters);
            return ds;
        }

        public DataSet SetDistributionTable(int roleId)
        {
            var strSql = new StringBuilder();
            //可以分配的人员=没有分配，且不存在角色中
            strSql.Append(" SELECT * FROM Marketing_Ex_PersonToRole WHERE RoleId=@RoleId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleId",SqlDbType.Int,4){Value = roleId},
			};


            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(),parameters);
            return ds;
        }

        //保存 给组员分配关系
        //保存用户角色
        public int Ex_PersonToRoleSave(string RoleXml, int UserInfoId)
        {

            SqlParameter[] parameters = {
					new SqlParameter("@xml", SqlDbType.Xml){Value = RoleXml},
                    new SqlParameter("@roleId",SqlDbType.Int,4){Value = UserInfoId},
			};
            var relust = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Marketing_PersonToRole_Save", parameters);
            return Convert.ToInt32(relust.ToString());
        } 
       

        #endregion

        
    }
}
