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
    /// <summary>
    /// 营销报表扩展 属性
    /// </summary>
    public class Marketing_Ex_PersonDAL
    {

        //营销报表 人员管理

        public List<Ex_PersonModel> GetCompetencePageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var fcmsUserList = new List<Ex_PersonModel>();
            string sqlCount = "select count(*) as totals from Marketing_Ex_Person";
            StringBuilder table = new StringBuilder();
            table.Append(" SELECT  a.*,b1.RoleName FROM dbo.Marketing_Ex_Person a LEFT JOIN ");
            table.Append("(SELECT a.PersonId,(CASE Leave WHEN 2 THEN RoleName+'部长' WHEN 3 THEN RoleName+'业务员' ELSE RoleName END) AS RoleName FROM dbo.Marketing_Ex_PersonToRole a INNER JOIN dbo.Marketing_RoleName  b ");
            table.Append(" ON a.RoleId=b.Id UNION ");
            table.Append(" SELECT a1.UserInfoId,(CASE Leave WHEN 2 THEN RoleName+'部长' WHEN 3 THEN RoleName+'组长' ELSE RoleName END) AS RoleName  FROM dbo.Marketing_UserJoinRole a1 INNER JOIN dbo.Marketing_RoleName  b1 ");
            table.Append(" ON a1.RoleNameId = b1.Id AND a1.Type=0) b1 ON b1.PersonId=a.Id ");
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
            var sqlPage = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, [Id],[Name],[Sex],[Mobile],[Age],[CreateTime],[ExpandToXML],[MemberId],[IsDel],[RoleName] from (" + table.ToString() + ") as temp";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = "Select [Id],[Name],[Sex],[Mobile],[Age],[CreateTime],[ExpandToXML],[MemberId],[IsDel],[RoleName] from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetCompetenceModel(reader);
                fcmsUserList.Add(info);
            }
            reader.Close();
            return fcmsUserList;
        }

        /// <summary>
        /// 查询组长下面的营销人员
        /// </summary>
        /// <returns></returns>
        public List<Ex_PersonModel> GetPersonList(int roleId )
        {
            var fcmsUserList = new List<Ex_PersonModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  a.*,b1.RoleName ,b1.RoleId FROM dbo.Marketing_Ex_Person a LEFT JOIN ");
            strSql.Append(" (SELECT a.PersonId,a.RoleId,(CASE Leave WHEN 2 THEN RoleName+'部长' WHEN 3 THEN RoleName+'业务员' ELSE RoleName END) AS RoleName ");
            strSql.Append("  FROM dbo.Marketing_Ex_PersonToRole a INNER JOIN dbo.Marketing_RoleName  b  ON a.RoleId=b.Id ) b1 ON b1.PersonId=a.Id   WHERE b1.RoleId=@roleId");
            SqlParameter[] parameters = {
                     new SqlParameter("@roleId", SqlDbType.Int,4) 
                              };
            parameters[0].Value = roleId;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text,strSql.ToString(),parameters);
            while (reader.Read())
            {
                var info = GetCompetenceModel(reader);
                fcmsUserList.Add(info);
            }
            reader.Close();
            return fcmsUserList;


        
        }

        private static Ex_PersonModel GetCompetenceModel(SqlDataReader dr)
        {
            var FunctionOperationModel = new Ex_PersonModel
            {
                Id = ConvertHelper.ToInt(dr["ID"].ToString()),
                Name = dr["Name"].Equals(DBNull.Value) ? "" : dr["Name"].ToString(),
                Sex = Convert.ToBoolean(dr["Sex"].ToString()),
                Mobile = dr["Mobile"].Equals(DBNull.Value) ? "" : dr["Mobile"].ToString(),
                Age = ConvertHelper.ToInt(dr["Age"].ToString()),
                CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString()),
                ExpandToXML = dr["ExpandToXML"].Equals(DBNull.Value) ? "" : dr["ExpandToXML"].ToString(),
                MemberId = ConvertHelper.ToInt(dr["MemberId"].ToString()),
                IsDel = ConvertHelper.ToInt(dr["IsDel"].ToString()),
                RoleName = dr["RoleName"].Equals(DBNull.Value) ? "未分配" : dr["RoleName"].ToString()

            };
            return FunctionOperationModel;
        }

        //查询 新增 编辑
        public int AddEx_Person(Ex_PersonModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Marketing_Ex_Person (Name,Sex,Mobile,Age,CreateTime,ExpandToXML,MemberId) ");
            strSql.Append(" Values(@Name,@Sex,@Mobile,@Age,@CreateTime,@ExpandToXML,@MemberId)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@Name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Sex", SqlDbType.Bit), 
                        new SqlParameter("@Mobile", SqlDbType.VarChar,100),
                        new SqlParameter("@Age", SqlDbType.Int,4),
                        new SqlParameter("@CreateTime", SqlDbType.DateTime),
                        new SqlParameter("@ExpandToXML", SqlDbType.Xml),
                        new SqlParameter("@MemberId", SqlDbType.Int,4)
 
              
            };

            parameters[0].Value = model.Name;
            parameters[1].Value = model.Sex;
            parameters[2].Value = model.Mobile;
            parameters[3].Value = model.Age;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.MemberId;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }
        //修改功能
        public int UpdateEx_Person(Ex_PersonModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE Marketing_Ex_Person SET Name=@Name,Sex=@Sex,Mobile=@Mobile  ");
            strSql.Append(" ,Age=@Age,ExpandToXML=@ExpandToXML,MemberId=@MemberId");
            strSql.Append("  where Id=@Id ");
            SqlParameter[] parameters = {
			             new SqlParameter("@Name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Sex", SqlDbType.Bit), 
                        new SqlParameter("@Mobile", SqlDbType.VarChar,100),
                        new SqlParameter("@Age", SqlDbType.Int,4),
                        new SqlParameter("@ExpandToXML", SqlDbType.Xml),
                        new SqlParameter("@MemberId", SqlDbType.Int,4),
                        new SqlParameter("@Id", SqlDbType.Int,4), 
 
              
            };

            parameters[0].Value = model.Name;
            parameters[1].Value = model.Sex;
            parameters[2].Value = model.Mobile;
            parameters[3].Value = model.Age;
            parameters[4].Value = model.MemberId;
            parameters[5].Value = model.Id;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }
        //根据Id查询功能
        public Ex_PersonModel GetEx_PersonById(int Id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select [Id], [Name], [Sex], [Mobile],[Age],[CreateTime],[ExpandToXML],[MemberId]  ");
            strSql.Append("  from [Marketing_Ex_Person] ");
            strSql.Append(" where [ID]=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = Id}
			};

            var model = new Ex_PersonModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Sex = Convert.ToBoolean(ds.Tables[0].Rows[0]["Sex"]);
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.Age = Convert.ToInt32(ds.Tables[0].Rows[0]["Age"]);
                model.CreateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateTime"]);
                model.ExpandToXML = ds.Tables[0].Rows[0]["ExpandToXML"].ToString();
                model.MemberId = int.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                return model;
            }
            return null;
        }

        //组合关系






        #region 营销报表模块
        //根据当前角色获得memberId 集合
        public DataSet GetPartentTable(int PartentID, int leave)
        {
            string sql = "SELECT * FROM dbo.Marketing_RoleName WHERE Id=@PartentID AND Leave<>1  UNION SELECT * FROM dbo.Marketing_RoleName WHERE PartentID=@PartentID and  Leave=@Leave";
            SqlParameter[] parameters = {
					new SqlParameter("@PartentID", SqlDbType.Int,4){Value = PartentID},
                    new SqlParameter("@Leave", SqlDbType.Int,4){Value = leave}
			};

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            return ds;

        }

        public DataSet GetPartentTable_Second(int PartentID, int leave)
        {
            string sql = "SELECT * FROM dbo.Marketing_RoleName WHERE PartentID=@PartentID and  Leave=@Leave";
            SqlParameter[] parameters = {
					new SqlParameter("@PartentID", SqlDbType.Int,4){Value = PartentID},
                    new SqlParameter("@Leave", SqlDbType.Int,4){Value = leave}
			};

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            return ds;
        }

        /// <summary>
        /// 查询报表数据
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="roleId"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageCount"></param>
        /// <param name="SumRegcount"></param>
        /// <param name="SumRegmoney"></param>
        /// <param name="SumBidNumContinued"></param>
        /// <param name="SumSuccessTransferMoney"></param>
        /// <param name="SumRealMoney"></param>
        /// <param name="SumInterest"></param>
        /// <returns></returns>
        public List<Marketing_EX_Data> GetEx_DataPageList(string starttime, string endtime, int userId, int roleId, int PageSize, int CurrentPage, ref int PageCount, ref int RecordCount, ref int SumRegcount, ref decimal SumRegmoney,
           ref int SumBidNumContinued, ref decimal SumBidAmount, ref decimal SumSuccessTransferMoney, ref decimal SumRealMoney, ref decimal SumInterest, ref decimal SumCurr_MouthMoney)
        {
            List<Marketing_EX_Data> list = new List<Marketing_EX_Data>();

            SqlParameter[] parameters = {
					new SqlParameter("@starttime", SqlDbType.VarChar,20),
                    new SqlParameter("@endtime", SqlDbType.VarChar,20),
                     new SqlParameter("@userId",SqlDbType.Int,4),
                    new SqlParameter("@roleId",SqlDbType.Int,4),
                    new SqlParameter("@PageSize",SqlDbType.Int,4),
                    new SqlParameter("@CurrentPage",SqlDbType.Int,4),
                    new SqlParameter("@PageCount",SqlDbType.Int,4),
                    new SqlParameter("@RecordCount",SqlDbType.Int,4),
                    new SqlParameter("@SumRegcount",SqlDbType.Int,4),
                    new SqlParameter("@SumRegmoney",SqlDbType.Decimal),
                    new SqlParameter("@SumBidNumContinued",SqlDbType.Int,4),
                    new SqlParameter("@SumBidAmount_new",SqlDbType.Decimal),
                    new SqlParameter("@SumSuccessTransferMoney",SqlDbType.Decimal),
                    new SqlParameter("@SumRealMoney",SqlDbType.Decimal),
                    new SqlParameter("@SumInterest",SqlDbType.Decimal),
                    new SqlParameter("@SumCurr_MouthMoney",SqlDbType.Decimal)
                    
			};
            parameters[0].Value = starttime;
            parameters[1].Value = endtime;
            parameters[2].Value = userId;
            parameters[3].Value = roleId;
            parameters[4].Value = PageSize;

            parameters[5].Value = CurrentPage;

            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;
            parameters[9].Direction = ParameterDirection.Output;
            parameters[10].Direction = ParameterDirection.Output;
            parameters[11].Direction = ParameterDirection.Output;
            parameters[12].Direction = ParameterDirection.Output;
            parameters[13].Direction = ParameterDirection.Output;
            parameters[14].Direction = ParameterDirection.Output;
            parameters[15].Direction = ParameterDirection.Output;
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_MarketingDataList", parameters);
            //获得输出参数

            while (reader.Read())
            {
                Marketing_EX_Data model = GetCEX_DataModel(reader);
                list.Add(model);
            }
            reader.Close();
            PageCount = Convert.ToInt32(parameters[7].Value);//总页数
            SumRegcount = Convert.ToInt32(parameters[8].Value);//注册人数
            SumRegmoney = Convert.ToDecimal(parameters[9].Value);//注册人投资金额
            SumBidNumContinued = Convert.ToInt32(parameters[10].Value);//续投人数
            SumBidAmount = Convert.ToDecimal(parameters[11].Value);//续投人数投资金额
            SumSuccessTransferMoney = Convert.ToDecimal(parameters[12].Value);//成功转让金额
            SumRealMoney = Convert.ToDecimal(parameters[13].Value);//实际投资金额
            SumInterest = Convert.ToDecimal(parameters[14].Value);//已收利息
            SumCurr_MouthMoney = Convert.ToDecimal(parameters[15].Value);//当月投资金额
            return list;
        }

        private static Marketing_EX_Data GetCEX_DataModel(SqlDataReader dr)
        {
            var FunctionOperationModel = new Marketing_EX_Data
            {
                Id = ConvertHelper.ToInt(dr["Id"].ToString()),
                Department = dr["Department"].Equals(DBNull.Value) ? "" : dr["Department"].ToString(),
                RoleName = dr["Name"].Equals(DBNull.Value) ? "" : dr["Name"].ToString(),
                BidNumContinued = ConvertHelper.ToInt(dr["BidNumContinued"].ToString()),
                Group = dr["GroupName"].Equals(DBNull.Value) ? "" : dr["GroupName"].ToString(),
                // Name = dr["Name"].Equals(DBNull.Value) ? "" : dr["Name"].ToString(),
                Regcount = ConvertHelper.ToInt(dr["Regcount"].ToString()),
                Interest = ConvertHelper.ToDecimal(dr["Interest"].ToString()),
                RealMoney = ConvertHelper.ToDecimal(dr["RealMoney"].ToString()),
                Regmoney = ConvertHelper.ToDecimal(dr["Regmoney"].ToString()),
                SuccessTransferMoney = ConvertHelper.ToDecimal(dr["SuccessTransferMoney"].ToString()),
                SumBidAmount = ConvertHelper.ToDecimal(dr["SumBidAmount"].ToString()),
                Curr_MouthMoney = ConvertHelper.ToDecimal(dr["Curr_MouthMoney"].ToString())


            };
            return FunctionOperationModel;
        }

        /// <summary>
        /// 删除营销人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelMarketing_Ex_Person(int id)
        {
            string sqlstr = "UPDATE dbo.Marketing_Ex_Person SET IsDel=1 WHERE id=@id";
            SqlParameter[] parameters =  { 
            new SqlParameter("@id",SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlstr.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);

        }

        ///// <summary>
        ///// 添加异动信息
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public int AddMoveModel(Marketing_MoveModel model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("INSERT INTO Marketing_Move ([Type],StartTime,FirstMemberId,SecondMemberId,ParentId,Leave) ");
        //    strSql.Append(" Values(@Type,@StartTime,@FirstMemberId,@SecondMemberId,@ParentId,@Leave)");
        //    strSql.Append(";select SCOPE_IDENTITY()");
        //    SqlParameter[] parameters = {
        //                new SqlParameter("@Type", SqlDbType.Int,4) ,            
        //                new SqlParameter("@StartTime", SqlDbType.DateTime), 
        //                new SqlParameter("@FirstMemberId", SqlDbType.Int,4),
        //                new SqlParameter("@SecondMemberId", SqlDbType.Int,4),
        //                new SqlParameter("@ParentId", SqlDbType.Int,4),
        //                new SqlParameter("@Leave", SqlDbType.Int,4)

 
              
        //    };

        //    parameters[0].Value = model.Type;
        //    parameters[1].Value = model.StartTime;
        //    parameters[2].Value = model.FirstMemberId;
        //    parameters[3].Value = model.SecondMemberId;
        //    parameters[4].Value = model.ParentId;
        //    parameters[5].Value = model.Leave;
        //    object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
        //    return obj == null ? 0 : Convert.ToInt32(obj);
        
        //}
        #endregion

    }
}
