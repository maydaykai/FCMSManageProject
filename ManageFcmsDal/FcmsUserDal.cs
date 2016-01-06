using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsModel;
using ManageFcmsCommon;

namespace ManageFcmsDal
{
    public class FcmsUserDal
    {

        /// <summary>
        /// 判断用户名是否已存在
        /// </summary>
        public bool Exists(string userName)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) from FcmsUser");
            strSql.Append(" where ");
            strSql.Append(" UserName = @UserName  ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,20){Value = userName}
			};
            var row = ConvertHelper.ToInt(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters).ToString());
            return row > 0;
        }

        //登陆验证
        public bool LoginValidate(ref FcmsUserModel fcmsUserModel)
        {
            fcmsUserModel = GetUserByNameAndPwd(fcmsUserModel.UserName, fcmsUserModel.PassWord);
            if (fcmsUserModel != null)
            {
                if (fcmsUserModel.ID > 0)
                {
                    return true;
                }
            }
            return false;
        }

        ///// <summary>
        ///// 更新登录信息
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public bool UpdateLoginInfo(FcmsUserModel model)
        //{
        //    var flag = false;
        //    var strSql = new StringBuilder();
        //    strSql.Append("update Member set ");
        //    strSql.Append(" LastIP = @LastIP, ");
        //    strSql.Append(" LastLoginTime = @LastLoginTime, ");
        //    strSql.Append(" Times = Times + 1 ");
        //    strSql.Append(" where ID=@ID");
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@ID", SqlDbType.Int, 4) {Value = model.ID},
        //        new SqlParameter("@LastLoginTime", SqlDbType.DateTime) {Value = model.LastLoginTime},
        //        new SqlParameter("@LastIP", SqlDbType.NVarChar, 50) {Value = HttpConnect. Request.UserHostAddress}
        //    };

        //    var num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

        //    return num > 0;
        //}

        private FcmsUserModel GetUserByNameAndPwd(string userName, string passWord)
        {
            var strSql = new StringBuilder();
            strSql.Append("select [ID], [Email], [QQ], [RegTime], [LastLoginTime], [LastIP], [Times], [IsLock], [UpdateTime], [RoleID], [ParentID], [UserName], [PassWord], [TranPassWord], [RealName],[AnotherName], [Sex], [Phone], [Mobile],[RelationID] ");
            strSql.Append("  from [FcmsUser] ");
            strSql.Append(" where UserName=@UserName ");
            strSql.Append(" AND PassWord=@PassWord ");
            strSql.Append(" AND [IsLock]=0");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@UserName", SqlDbType.NVarChar) {Value = userName},
                    new SqlParameter("@PassWord", SqlDbType.VarChar) {Value = passWord}
                };

            var model = new FcmsUserModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                if (ds.Tables[0].Rows[0]["RegTime"].ToString() != "")
                {
                    model.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["RegTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }
                model.LastIP = ds.Tables[0].Rows[0]["LastIP"].ToString();
                if (ds.Tables[0].Rows[0]["Times"].ToString() != "")
                {
                    model.Times = int.Parse(ds.Tables[0].Rows[0]["Times"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsLock"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsLock"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsLock"].ToString().ToLower() == "true"))
                    {
                        model.IsLock = true;
                    }
                    else
                    {
                        model.IsLock = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                model.RoleId = ds.Tables[0].Rows[0]["RoleID"].ToString();
                if (ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                model.PassWord = ds.Tables[0].Rows[0]["PassWord"].ToString();
                model.TranPassWord = ds.Tables[0].Rows[0]["TranPassWord"].ToString();
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.AnotherName = ds.Tables[0].Rows[0]["AnotherName"].ToString();
                if (ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
                }
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                if (ds.Tables[0].Rows[0]["RelationID"].ToString() != "")
                {
                    model.RelationID = int.Parse(ds.Tables[0].Rows[0]["RelationID"].ToString());
                }
                return model;
            }
            return null;
        }

        /// <summary>
        /// 获取所有角色是担保公司的数据(担保公司角色ID目前是8，如有变动请修改)
        /// </summary>
        public DataSet GetGcList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from dbo.FcmsUser where [parentID]=0 and (charindex(','+ltrim(8)+',',','+RoleID+',')>0 or charindex(','+ltrim(13)+',',','+RoleID+',')>0) AND IsLock=0");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取借款标类型数据
        /// </summary>
        public DataSet GetDimLoanScaleTypeList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from dbo.DimLoanScaleType");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取借款用途数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimLoanUseList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from dbo.DimLoanUse");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 学生贷借款用途
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimLoanUseList_StudenInfo()
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from dbo.DimLoanUseClass");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取还款方式
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimRepaymentMethodList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from dbo.DimRepaymentMethod");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 增加
        /// </summary>
        public bool Add(FcmsUserModel model)
        {
            var falg = false;
            var sqlcon = new SqlConnection(SqlHelper.ConnectionStringLocal);
            sqlcon.Open();
            var sqltran = sqlcon.BeginTransaction();
            var sqlcmd = new SqlCommand { Connection = sqlcon, Transaction = sqltran, CommandType = CommandType.Text };

            try
            {
                var idEntity = 0;
                int resultA = 0;
                var roleStr = model.RoleId;
                roleStr = "," + roleStr + ",";
                if (roleStr.Contains(",5,"))//是否是担保公司
                {
                    var strSql1 = new StringBuilder();
                    strSql1.Append("insert into Member(");
                    strSql1.Append(" CreditPoint,MemberPoint,Times,Type,CompleStatus,MemberName,PassWord,TranPassWord,Mobile,Email,Balance,IsDisable");
                    strSql1.Append(") values (");
                    strSql1.Append(" @CreditPoint,@MemberPoint,@Times,@Type,@CompleStatus,@MemberName,@PassWord,@TranPassWord,@Mobile,@Email,@Balance,@IsDisable");
                    strSql1.Append(") ");
                    strSql1.Append(";select SCOPE_IDENTITY()");
                    SqlParameter[] parameters1 =
                        {
                            new SqlParameter("@CreditPoint", SqlDbType.Int, 4) {Value = 0},
                            new SqlParameter("@MemberPoint", SqlDbType.Int, 4) {Value = 0},
                            new SqlParameter("@Times", SqlDbType.Int, 4) {Value = model.Times},
                            new SqlParameter("@Type", SqlDbType.Int, 4) {Value = 2},
                            new SqlParameter("@CompleStatus", SqlDbType.Int, 4) {Value = 4},
                            new SqlParameter("@MemberName", SqlDbType.NVarChar, 12) {Value = model.UserName},
                            new SqlParameter("@PassWord", SqlDbType.VarChar, 50) {Value = model.PassWord},
                            new SqlParameter("@TranPassWord", SqlDbType.VarChar, 50) {Value = model.TranPassWord},
                            new SqlParameter("@Mobile", SqlDbType.VarChar, 20) {Value = model.Mobile},
                            new SqlParameter("@Email", SqlDbType.NVarChar, 50) {Value = model.Email},
                            new SqlParameter("@Balance", SqlDbType.Decimal, 9) {Value = 0},
                            new SqlParameter("@IsDisable", SqlDbType.Bit, 1) {Value = 0}
                        };
                    sqlcmd.CommandText = strSql1.ToString();
                    sqlcmd.Parameters.Clear();
                    foreach (var sqlParameter in parameters1)
                    {
                        sqlcmd.Parameters.Add(sqlParameter);
                    }
                    var result1 = sqlcmd.ExecuteScalar();
                    if (result1 != null && result1 != DBNull.Value)
                    {
                        idEntity = ConvertHelper.ToInt(result1.ToString());
                        if (idEntity > 0)
                        {
                            resultA = 1;
                        }
                    }
                }
                else
                {
                    resultA = 1;
                }

                var strSql = new StringBuilder();
                strSql.Append("insert into FcmsUser ");
                strSql.Append(" (UserName,Email,QQ ,LastLoginTime,RegTime,IsLock,UpdateTime,RoleID,ParentID,PassWord,TranPassWord,RealName,AnotherName,Sex,Phone,Mobile,Times,RelationID)");
                strSql.Append(" Values(@UserName,@Email,@QQ ,@LastLoginTime,@RegTime,@IsLock,@UpdateTime,@RoleID,@ParentID,@PassWord,@TranPassWord,@RealName,@AnotherName,@Sex,@Phone,@Mobile,@Times,@RelationID)");
                strSql.Append(";select SCOPE_IDENTITY()");
                SqlParameter[] parameters =
                    {
                        new SqlParameter("@UserName", SqlDbType.VarChar, 50) {Value = model.UserName},
                        new SqlParameter("@Email", SqlDbType.NVarChar, 200) {Value = model.Email},
                        new SqlParameter("@QQ", SqlDbType.VarChar, 20) {Value = model.QQ},
                        new SqlParameter("@LastLoginTime", SqlDbType.DateTime) {Value = model.LastLoginTime},
                        new SqlParameter("@RegTime", SqlDbType.DateTime) {Value = model.RegTime},
                        new SqlParameter("@IsLock", SqlDbType.Bit, 1) {Value = model.IsLock},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime) {Value = model.UpdateTime},
                        new SqlParameter("@RoleID", SqlDbType.VarChar, 100) {Value = model.RoleId},
                        new SqlParameter("@ParentID", SqlDbType.Int, 4) {Value = model.ParentID},
                        new SqlParameter("@PassWord", SqlDbType.VarChar, 50) {Value = model.PassWord},
                        new SqlParameter("@TranPassWord", SqlDbType.VarChar, 50) {Value = model.TranPassWord},
                        new SqlParameter("@RealName", SqlDbType.NVarChar, 50) {Value = model.RealName},
                        new SqlParameter("@AnotherName", SqlDbType.NVarChar, 50) {Value = model.AnotherName},
                        new SqlParameter("@Sex", SqlDbType.Int, 4) {Value = model.Sex},
                        new SqlParameter("@Phone", SqlDbType.VarChar, 20) {Value = model.Phone},
                        new SqlParameter("@Mobile", SqlDbType.VarChar, 20) {Value = model.Mobile},
                        new SqlParameter("@Times", SqlDbType.Int, 4) {Value = model.Times},
                        new SqlParameter("@RelationID", SqlDbType.Int, 4) {Value = idEntity}
                    };
                sqlcmd.CommandText = strSql.ToString();
                sqlcmd.Parameters.Clear();
                foreach (var sqlParameter in parameters)
                {
                    sqlcmd.Parameters.Add(sqlParameter);
                }
                var result = sqlcmd.ExecuteNonQuery();

                if (result == 1 && resultA == 1)
                {
                    sqltran.Commit();
                    falg = true;
                }
                else
                {
                    sqltran.Rollback();
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(ex);
                sqltran.Rollback();
            }
            finally
            {
                sqlcmd.Dispose();
                sqlcon.Close();
            }
            return falg;
        }


        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(FcmsUserModel model)
        {
            var falg = false;
            var sqlcon = new SqlConnection(SqlHelper.ConnectionStringLocal);
            sqlcon.Open();
            var sqltran = sqlcon.BeginTransaction();
            var sqlcmd = new SqlCommand { Connection = sqlcon, Transaction = sqltran, CommandType = CommandType.Text };
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("update FcmsUser set ");
                strSql.Append(" Email = @Email , ");
                strSql.Append(" QQ = @QQ , ");
                strSql.Append(" IsLock = @IsLock , ");
                strSql.Append(" UpdateTime = @UpdateTime , ");
                strSql.Append(" RoleID = @RoleID , ");
                strSql.Append(" ParentID = @ParentID , ");
                strSql.Append(" PassWord = @PassWord , ");
                strSql.Append(" TranPassWord = @TranPassWord , ");
                strSql.Append(" RealName = @RealName , ");
                strSql.Append(" AnotherName = @AnotherName , ");
                strSql.Append(" Sex = @Sex , ");
                strSql.Append(" Phone = @Phone , ");
                strSql.Append(" Mobile = @Mobile  ");
                strSql.Append(" where ID=@ID ");

                SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@Email", SqlDbType.NVarChar,200){Value = model.Email} ,            
                        new SqlParameter("@QQ", SqlDbType.VarChar,20){Value = model.QQ} ,                                                  
                        new SqlParameter("@IsLock", SqlDbType.Bit,1){Value = model.IsLock} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@RoleID", SqlDbType.VarChar,100){Value = model.RoleId} ,            
                        new SqlParameter("@ParentID", SqlDbType.Int,4){Value = model.ParentID} ,                     
                        new SqlParameter("@PassWord", SqlDbType.VarChar,50){Value = model.PassWord} ,            
                        new SqlParameter("@TranPassWord", SqlDbType.VarChar,50){Value = model.TranPassWord} ,            
                        new SqlParameter("@RealName", SqlDbType.NVarChar,50){Value = model.RealName} , 
                        new SqlParameter("@AnotherName", SqlDbType.NVarChar,50){Value = model.AnotherName} ,  
                        new SqlParameter("@Sex", SqlDbType.Int,4){Value = model.Sex} ,            
                        new SqlParameter("@Phone", SqlDbType.VarChar,20){Value = model.Phone} ,            
                        new SqlParameter("@Mobile", SqlDbType.VarChar,20){Value = model.Mobile}                           
                 };
                sqlcmd.Parameters.Clear();
                sqlcmd.CommandText = strSql.ToString();
                foreach (var sqlParameter in parameters)
                {
                    sqlcmd.Parameters.Add(sqlParameter);
                }
                var result = sqlcmd.ExecuteNonQuery();
                int result1;
                if (model.RelationID > 0)
                {
                    var strSql1 = new StringBuilder();
                    strSql1.Append("update Member set ");
                    strSql1.Append(" UpdateTime = @UpdateTime , ");
                    strSql1.Append(" MemberName = @MemberName , ");
                    strSql1.Append(" PassWord = @PassWord , ");
                    strSql1.Append(" TranPassWord = @TranPassWord , ");
                    strSql1.Append(" Mobile = @Mobile , ");
                    strSql1.Append(" Email = @Email , ");
                    strSql1.Append(" IsDisable = @IsDisable ");
                    strSql1.Append(" where ID=@ID ");

                    SqlParameter[] parameters1 =
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4) {Value = model.RelationID},
                            new SqlParameter("@UpdateTime", SqlDbType.DateTime) {Value = model.UpdateTime},
                            new SqlParameter("@MemberName", SqlDbType.VarChar, 20) {Value = model.UserName},
                            new SqlParameter("@PassWord", SqlDbType.VarChar, 50) {Value = model.PassWord},
                            new SqlParameter("@TranPassWord", SqlDbType.VarChar, 50) {Value = model.TranPassWord},
                            new SqlParameter("@Mobile", SqlDbType.VarChar, 20) {Value = model.Mobile},
                            new SqlParameter("@Email", SqlDbType.NVarChar, 50) {Value = model.Email},
                            new SqlParameter("@IsDisable", SqlDbType.Bit, 1) {Value = model.IsLock}
                        };
                    sqlcmd.Parameters.Clear();
                    sqlcmd.CommandText = strSql1.ToString();
                    foreach (var sqlParameter1 in parameters1)
                    {
                        sqlcmd.Parameters.Add(sqlParameter1);
                    }
                    result1 = sqlcmd.ExecuteNonQuery();
                }
                else
                {
                    result1 = 1;
                }

                if (result == 1 && result1 == 1)
                {
                    falg = true;
                    sqltran.Commit();
                }
                else
                {
                    sqltran.Rollback();
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(ex);
                sqltran.Rollback();
            }
            finally
            {
                sqlcmd.Dispose();
                sqlcon.Close();
            }
            return falg;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FcmsUserModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select [ID], [Email], [QQ], [RegTime], [LastLoginTime], [LastIP], [Times], [RelationID], [IsLock], [UpdateTime], [RoleID], [ParentID], [UserName], [PassWord], [TranPassWord], [RealName],[AnotherName], [Sex], [Phone], [Mobile]  ");
            strSql.Append("  from [FcmsUser] ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value=id}
			};

            var model = new FcmsUserModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                if (ds.Tables[0].Rows[0]["RegTime"].ToString() != "")
                {
                    model.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["RegTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }
                model.LastIP = ds.Tables[0].Rows[0]["LastIP"].ToString();
                if (ds.Tables[0].Rows[0]["Times"].ToString() != "")
                {
                    model.Times = int.Parse(ds.Tables[0].Rows[0]["Times"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RelationID"].ToString() != "")
                {
                    model.RelationID = int.Parse(ds.Tables[0].Rows[0]["RelationID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsLock"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsLock"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsLock"].ToString().ToLower() == "true"))
                    {
                        model.IsLock = true;
                    }
                    else
                    {
                        model.IsLock = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                model.RoleId = ds.Tables[0].Rows[0]["RoleID"].ToString();
                if (ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                model.PassWord = ds.Tables[0].Rows[0]["PassWord"].ToString();
                model.TranPassWord = ds.Tables[0].Rows[0]["TranPassWord"].ToString();
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.AnotherName = ds.Tables[0].Rows[0]["AnotherName"].ToString();
                if (ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
                }
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();

                return model;
            }
            return null;
        }


        //public DataTable GetUserListForPager(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        //{
        //    const string tables = " [FcmsUser] p";
        //    return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        //}

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<FcmsUserModel> GetFcmsUserList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var fcmsUserList = new List<FcmsUserModel>();
            string sqlCount = "select count(*) as totals from FcmsUser";
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
            var sqlPage = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, [ID],[UserName],[RealName],[AnotherName],[Mobile],[RegTime],[LastLoginTime],[RelationID],[IsLock],[UpdateTime],[RoleID] from FcmsUser";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = "Select [ID],[UserName],[RealName],[AnotherName],[Mobile],[RegTime],[LastLoginTime],[RelationID],[IsLock],[UpdateTime],[RoleID] from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetFcmsUserModel(reader);
                fcmsUserList.Add(info);
            }
            reader.Close();
            return fcmsUserList;
        }

        private static FcmsUserModel GetFcmsUserModel(SqlDataReader dr)
        {
            var fcmsUserModel = new FcmsUserModel
                {
                    ID = ConvertHelper.ToInt(dr["ID"].ToString()),
                    UserName = dr["UserName"].Equals(DBNull.Value) ? "" : dr["UserName"].ToString(),
                    RealName = dr["RealName"].Equals(DBNull.Value) ? "" : dr["RealName"].ToString(),
                    AnotherName = dr["AnotherName"].Equals(DBNull.Value) ? "" : dr["AnotherName"].ToString(),
                    Mobile = dr["Mobile"].Equals(DBNull.Value) ? "" : dr["Mobile"].ToString(),
                    RegTime = dr["RegTime"].Equals(DBNull.Value) ? DateTime.Now : ConvertHelper.ToDateTime(dr["RegTime"].ToString()),
                    LastLoginTime = dr["LastLoginTime"].Equals(DBNull.Value) ? DateTime.Now : ConvertHelper.ToDateTime(dr["LastLoginTime"].ToString()),
                    RelationID = ConvertHelper.ToInt(dr["RelationID"].ToString()),
                    IsLock = ConvertHelper.ToBool(dr["IsLock"].ToString()),
                    UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) ? DateTime.Now : ConvertHelper.ToDateTime(dr["UpdateTime"].ToString()),
                    RoleId = dr["RoleID"].Equals(DBNull.Value) ? "" : dr["RoleID"].ToString()
                };
            return fcmsUserModel;
        }

        /// <summary>
        /// 获取产品类型数据
        /// </summary>
        public DataSet GetDimProductTypeList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select d.ID as ID,d1.Name + '-' + d.Name as Name from dbo.DimProductType d inner join dbo.DimProductType d1 on d.ParentId = d1.ID where d.parentId > 0 and d.IsValidity = 1");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }
    }
}
