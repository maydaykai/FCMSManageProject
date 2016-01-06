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
    public class LoanApplyDal
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LoanApplyModel GetLoanApplyModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select la.*,DimLoanUse.LoanUseName,AreaProvince.ID as Province,AreaProvince.Name as ProvinceName,AreaCity.Name as CityName," +
                          " dbo.Member.MemberName,dbo.DimRepaymentMethod.RepaymentMethodName,dbo.DimProductType.Name as ProductTypeName,MemberInfo.RealName,dbo.Member.Mobile " +
                          " from LoanApply la " +
                          " INNER JOIN dbo.DimLoanUse ON la.LoanUseID = dbo.DimLoanUse.ID " +
                          " INNER JOIN dbo.Area AS AreaCity ON la.CityId = AreaCity.ID" +
                          " INNER JOIN dbo.Area AS AreaProvince ON AreaCity.ParentID = AreaProvince.ID " +
                          " INNER JOIN dbo.DimRepaymentMethod on la.RepaymentMethod = dbo.DimRepaymentMethod.ID" +
                          " INNER JOIN dbo.Member ON La.MemberId = Member.ID " +
                          " INNER JOIN MemberInfo on la.MemberID = MemberInfo.MemberID " +
                          " INNER JOIN dbo.DimProductType on la.ProductTypeId = dbo.DimProductType.ID"); 
            strSql.Append(" where La.ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            LoanApplyModel model = new LoanApplyModel();
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (reader.Read())
            {
                model = GetLoanApplyModelByDr(reader);
            }
            reader.Close();
            return model;
        }

        /// <summary>
        /// 获取借款申请列表分页
        /// </summary>
        public IList<LoanApplyModel> GetPagedLoanApplyList(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            var list = new List<LoanApplyModel>();
            string sql1 = "select count(*) as totals from dbo.LoanApply INNER JOIN dbo.Member ON dbo.LoanApply.MemberId = dbo.Member.ID";
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
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY dbo.LoanApply." + OrderBy +
                          ")) AS rownum, dbo.LoanApply.*,dbo.DimLoanUse.LoanUseName,AreaProvince.ID as Province, AreaProvince.Name AS ProvinceName," +
                          " AreaCity.Name AS CityName, dbo.Member.MemberName,dbo.Member.Mobile, MemberInfo.RealName,dbo.DimRepaymentMethod.RepaymentMethodName,dbo.DimProductType.Name as ProductTypeName " +
                          " from dbo.LoanApply " +
                          " INNER JOIN dbo.DimLoanUse ON dbo.LoanApply.LoanUseID = dbo.DimLoanUse.ID " +
                          " INNER JOIN dbo.Area AS AreaCity ON dbo.LoanApply.CityId = AreaCity.ID " +
                          " INNER JOIN dbo.Area AS AreaProvince ON AreaCity.ParentID = AreaProvince.ID " +
                          " INNER JOIN dbo.DimRepaymentMethod on dbo.LoanApply.RepaymentMethod = dbo.DimRepaymentMethod.ID" +
                          " INNER JOIN dbo.Member ON dbo.LoanApply.MemberId = dbo.Member.ID" +
                          " INNER JOIN MemberInfo on dbo.LoanApply.MemberID = MemberInfo.MemberID " +
                          " INNER JOIN dbo.DimProductType on dbo.LoanApply.ProductTypeId = dbo.DimProductType.ID";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " where " + Where;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((PageIndex - 1) * PageSize + 1) + " and " + PageIndex * PageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                LoanApplyModel info = GetLoanApplyModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private LoanApplyModel GetLoanApplyModelByDr(SqlDataReader dr)
        {
            LoanApplyModel model = new LoanApplyModel();
            model.ID = Convert.ToInt32(dr["ID"]);
            model.MemberId = dr["MemberId"] != DBNull.Value ? Convert.ToInt32(dr["MemberId"]) : 0;
            model.MemberName = dr["MemberName"] != DBNull.Value ? Convert.ToString(dr["MemberName"]) : "";
            model.RealName = dr["RealName"] != DBNull.Value ? Convert.ToString(dr["RealName"]) : "";
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0.00M;
            model.LoanUseId = dr["LoanUseId"] != DBNull.Value ? Convert.ToInt32(dr["LoanUseId"]) : 0;
            model.LoanUseName = dr["LoanUseName"] != DBNull.Value ? Convert.ToString(dr["LoanUseName"]) : "";
            model.ProductTypeId = dr["ProductTypeId"] != DBNull.Value ? Convert.ToInt32(dr["ProductTypeId"]) : 0;
            model.ProductTypeName = dr["ProductTypeName"] != DBNull.Value ? Convert.ToString(dr["ProductTypeName"]) : "";
            model.LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.BorrowMode = dr["BorrowMode"] != DBNull.Value ? Convert.ToInt32(dr["BorrowMode"]) : 0;
            model.CityId = dr["CityId"] != DBNull.Value ? Convert.ToInt32(dr["CityId"]) : 0;
            model.CityName = dr["CityName"] != DBNull.Value ? Convert.ToString(dr["CityName"]) : "";
            model.Province = dr["Province"] != DBNull.Value ? Convert.ToInt32(dr["Province"]) : 0;
            model.ProvinceName = dr["ProvinceName"] != DBNull.Value ? Convert.ToString(dr["ProvinceName"]) : "";
            model.ExamStatus = dr["ExamStatus"] != DBNull.Value ? Convert.ToInt32(dr["ExamStatus"]) : 0;
            model.HaveGuaranteeLetter = Convert.ToBoolean(dr["HaveGuaranteeLetter"]);
            model.RepaymentMethod = dr["RepaymentMethod"] != DBNull.Value ? Convert.ToInt32(dr["RepaymentMethod"]) : 0;
            model.RepaymentMethodName = dr["RepaymentMethodName"] != DBNull.Value ? dr["RepaymentMethodName"].ToString() : "";
            model.CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate;
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate;
            model.RepaymentSource = dr["RepaymentSource"] != DBNull.Value ? Convert.ToString(dr["RepaymentSource"]) : "";
            model.AuditRecords = dr["AuditRecords"] != DBNull.Value ? Convert.ToString(dr["AuditRecords"]) : "";
            model.LoanNumber = dr["LoanNumber"] != DBNull.Value ? Convert.ToString(dr["LoanNumber"]) : "";
            model.Mobile = dr["Mobile"] != DBNull.Value ? Convert.ToString(dr["Mobile"]) : "";
            return model;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(LoanApplyModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update dbo.LoanApply set ");
            strSql.Append(" AuditRecords = @AuditRecords , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" ExamStatus = @ExamStatus ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,200){Value = model.AuditRecords} ,
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@ExamStatus", SqlDbType.Int,4){Value = model.ExamStatus}         
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
        /// 获取用户对应未发标借款标号数据列表
        /// </summary>
        public DataSet GetLoanNumberList(int memberId)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID,LoanNumber from dbo.LoanApply where ExamStatus = 4 and MemberId=@MemberId");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberId", SqlDbType.Int,4){Value = memberId}         
            };
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(),parameters);
            return ds;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetLoanApplyProductInfoList(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select l.*,dpi.Name as ProductInfoName ");
            strSql.Append(" FROM LoanApplyProductInfo l inner join dbo.DimProductInfo dpi on l.ProductInfoId = dpi.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }

    }
}
