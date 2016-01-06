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
    public class LoanRapidDAL
    {
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(LoanRapidModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LoanRapid(");
            strSql.Append("CreateTime,UpdateTime,Status,Name,Phone,LoanAmount,LoanUseID,LoanTerm,LoanMode,Province,City,Describe");
            strSql.Append(") values (");
            strSql.Append("@CreateTime,@UpdateTime,@Status,@Name,@Phone,@LoanAmount,@LoanUseID,@LoanTerm,@LoanMode,@Province,@City,@Describe");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value=model.Status} ,            
                        new SqlParameter("@Describe", SqlDbType.NVarChar,50){Value=model.Describe} ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,20){Value=model.Name} ,            
                        new SqlParameter("@Phone", SqlDbType.VarChar,20){Value=model.Phone} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value=model.LoanAmount} ,            
                        new SqlParameter("@LoanUseID", SqlDbType.Int,4){Value=model.LoanUseID} ,            
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value=model.LoanTerm} ,            
                        new SqlParameter("@LoanMode", SqlDbType.Int,4){Value=model.LoanMode} ,            
                        new SqlParameter("@Province", SqlDbType.Int,4){Value=model.Province} ,            
                        new SqlParameter("@City", SqlDbType.Int,4){Value=model.City}             
              
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
        public bool Update(LoanRapidModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LoanRapid set ");

            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Phone = @Phone , ");
            strSql.Append(" LoanAmount = @LoanAmount , ");
            strSql.Append(" LoanUseID = @LoanUseID , ");
            strSql.Append(" LoanTerm = @LoanTerm , ");
            strSql.Append(" LoanMode = @LoanMode , ");
            strSql.Append(" Province = @Province , ");
            strSql.Append(" City = @City,  ");
            strSql.Append(" Describe = @Describe  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value = model.Status} ,            
                        new SqlParameter("@Describe", SqlDbType.NVarChar,50){Value = model.Describe} ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,20){Value = model.Name} ,            
                        new SqlParameter("@Phone", SqlDbType.VarChar,20){Value = model.Phone} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value = model.LoanAmount} ,            
                        new SqlParameter("@LoanUseID", SqlDbType.Int,4){Value = model.LoanUseID} ,            
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value = model.LoanTerm} ,            
                        new SqlParameter("@LoanMode", SqlDbType.Int,4){Value = model.LoanMode} ,            
                        new SqlParameter("@Province", SqlDbType.Int,4){Value = model.Province} ,            
                        new SqlParameter("@City", SqlDbType.Int,4){Value = model.City}             
              
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
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(LoanRapidModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LoanRapid set ");

            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" Status = @Status ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                       
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value = model.Status}         
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LoanRapid ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LoanRapid ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
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
        public LoanRapidModel GetLoanRapidModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select dbo.DimLoanUse.LoanUseName, AreaProvince.Name AS ProvinceName, AreaCity.Name AS CityName, dbo.LoanRapid.LoanTerm, dbo.LoanRapid.LoanMode, 
                      dbo.LoanRapid.CreateTime, dbo.LoanRapid.Status, dbo.LoanRapid.Describe, dbo.LoanRapid.UpdateTime, dbo.LoanRapid.LoanUseID, dbo.LoanRapid.Province, 
                      dbo.LoanRapid.City, dbo.LoanRapid.LoanAmount, dbo.LoanRapid.Phone, dbo.LoanRapid.ID, dbo.LoanRapid.Name, MemberInfo.RealName  ");
            strSql.Append(@"  from dbo.LoanRapid INNER JOIN
                      dbo.DimLoanUse ON dbo.LoanRapid.LoanUseID = dbo.DimLoanUse.ID INNER JOIN
                      dbo.Area AS AreaProvince ON dbo.LoanRapid.Province = AreaProvince.ID INNER JOIN
                      dbo.Area AS AreaCity ON dbo.LoanRapid.City = AreaCity.ID INNER JOIN
                      MemberInfo on dbo.LoanRapid.MemberID = MemberInfo.MemberID ");
            strSql.Append(" where dbo.LoanRapid.ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            LoanRapidModel model = new LoanRapidModel();
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (reader.Read())
            {
                model = GetLoanRapidModelByDr(reader);
            }
            reader.Close();
            return model;
        }

        /// <summary>
        /// 获取快速借贷列表分页
        /// </summary>
        public IList<LoanRapidModel> GetPagedLoanRapidList(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            var list = new List<LoanRapidModel>();
            string sql1 = "select count(*) as totals from LoanRapid";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " where " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                TotalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY dbo.LoanRapid." + OrderBy +
                          ")) AS rownum, dbo.LoanRapid.LoanUseID,dbo.DimLoanUse.LoanUseName, AreaProvince.Name AS ProvinceName," +
                          " AreaCity.Name AS CityName, dbo.LoanRapid.ID, dbo.LoanRapid.Name, dbo.LoanRapid.Phone, dbo.LoanRapid.LoanAmount, dbo.LoanRapid.LoanTerm, dbo.LoanRapid.LoanMode," +
                          " dbo.LoanRapid.CreateTime, dbo.LoanRapid.Status, dbo.LoanRapid.Describe, dbo.LoanRapid.UpdateTime, MemberInfo.RealName " +
                          " from dbo.LoanRapid " +
                          " INNER JOIN dbo.DimLoanUse ON dbo.LoanRapid.LoanUseID = dbo.DimLoanUse.ID " +
                          " INNER JOIN dbo.Area AS AreaProvince ON dbo.LoanRapid.Province = AreaProvince.ID " +
                          " INNER JOIN dbo.Area AS AreaCity ON dbo.LoanRapid.City = AreaCity.ID " +
                          " INNER JOIN MemberInfo on dbo.LoanRapid.MemberID = MemberInfo.MemberID ";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " where " + Where;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((PageIndex - 1) * PageSize + 1) + " and " + PageIndex * PageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                LoanRapidModel info = GetLoanRapidViewByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 根据条件查询指定列定
        /// </summary>
        public IList<LoanRapidModel> GetLoanRapidList(string Where, string OrderBy, ref int TotalRows)
        {
            var list = new List<LoanRapidModel>();
            string sql1 = "select count(*) as totals from LoanRapid";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " where " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                TotalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select dbo.DimLoanUse.LoanUseName, AreaProvince.Name AS ProvinceName, AreaCity.Name AS CityName, dbo.LoanRapid.ID, dbo.LoanRapid.Name, dbo.LoanRapid.Phone," +
                        " dbo.LoanRapid.LoanAmount, dbo.LoanRapid.LoanTerm, dbo.LoanRapid.LoanMode, dbo.LoanRapid.CreateTime, dbo.LoanRapid.Status, dbo.LoanRapid.Describe, " +
                        " dbo.LoanRapid.UpdateTime, MemberInfo.RealName " +
                        " from dbo.LoanRapid " + 
                        " INNER JOIN dbo.DimLoanUse ON dbo.LoanRapid.LoanUseID = dbo.DimLoanUse.ID " + 
                        " INNER JOIN dbo.Area AS AreaProvince ON dbo.LoanRapid.Province = AreaProvince.ID " +
                        " INNER JOIN dbo.Area AS AreaCity ON dbo.LoanRapid.City = AreaCity.ID" +
                        " INNER JOIN MemberInfo on dbo.LoanRapid.MemberID = MemberInfo.MemberID ";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " where " + Where;
            }
            if (!string.IsNullOrEmpty(OrderBy))
            {
                sql2 = sql2 + " order by dbo.LoanRapid." + OrderBy;
            }
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                LoanRapidModel info = GetLoanRapidViewByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private LoanRapidModel GetLoanRapidModelByDr(SqlDataReader dr)
        {
            LoanRapidModel model = new LoanRapidModel();
            model.City = dr["City"] != DBNull.Value ? Convert.ToInt32(dr["City"]) : 0;
            model.Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : "";
            model.Phone = dr["Phone"] != DBNull.Value ? Convert.ToString(dr["Phone"]) : "";
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0.00M;
            model.LoanUseID = dr["LoanUseID"] != DBNull.Value ? Convert.ToInt32(dr["LoanUseID"]) : 0;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.LoanMode = dr["LoanMode"] != DBNull.Value ? Convert.ToInt32(dr["LoanMode"]) : 0;
            model.Province = dr["Province"] != DBNull.Value ? Convert.ToInt32(dr["Province"]) : 0;
            model.Status = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : 0;
            model.CreateTime = dr["LoanTerm"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate;
            model.Describe = dr["Describe"] != DBNull.Value ? Convert.ToString(dr["Describe"]) : "";
            model.ID = Convert.ToInt32(dr["ID"]);
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate;
            model.ProvinceName = dr["ProvinceName"] != DBNull.Value ? Convert.ToString(dr["ProvinceName"]) : "";
            model.CityName = dr["CityName"] != DBNull.Value ? Convert.ToString(dr["CityName"]) : "";
            model.LoanUseName = dr["LoanUseName"] != DBNull.Value ? Convert.ToString(dr["LoanUseName"]) : "";
            model.RealName = dr["RealName"] != DBNull.Value ? Convert.ToString(dr["RealName"]) : "";
            return model;
        }

        private LoanRapidModel GetLoanRapidViewByDr(SqlDataReader dr)
        {
            LoanRapidModel model = new LoanRapidModel();
            model.ID = Convert.ToInt32(dr["ID"]);
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate;
            model.Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : "";
            model.Phone = dr["Phone"] != DBNull.Value ? Convert.ToString(dr["Phone"]) : "";
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0.00M;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.LoanMode = dr["LoanMode"] != DBNull.Value ? Convert.ToInt32(dr["LoanMode"]) : 0;
            model.Status = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : 0;
            model.CreateTime = dr["LoanTerm"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate;
            model.Describe = dr["Describe"] != DBNull.Value ? Convert.ToString(dr["Describe"]) : "";
            model.ProvinceName = dr["ProvinceName"] != DBNull.Value ? Convert.ToString(dr["ProvinceName"]) : "";
            model.CityName = dr["CityName"] != DBNull.Value ? Convert.ToString(dr["CityName"]) : "";
            model.LoanUseName = dr["LoanUseName"] != DBNull.Value ? Convert.ToString(dr["LoanUseName"]) : "";
            model.RealName = dr["RealName"] != DBNull.Value ? Convert.ToString(dr["RealName"]) : "";
            return model;
        }
    }
}
