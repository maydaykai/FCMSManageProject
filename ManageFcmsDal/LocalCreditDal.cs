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
    public class LocalCreditDal
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LocalCreditModel GetLocailCreditModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM v_LocalCreditLoanApply WHERE ID = @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            LocalCreditModel model = new LocalCreditModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    model = GetLocailCreditModelByDr(dr);
                }
            }
            
            return model;
        }

        /// <summary>
        /// 获取借款申请列表分页
        /// </summary>
        public IList<LocalCreditModel> GetPagedLoanApplyList(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            var list = new List<LocalCreditModel>();
            string sql1 = "select count(*) as totals FROM v_LocalCreditLoanApply";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " WHERE " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                TotalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + OrderBy + ")) AS rownum, * FROM v_LocalCreditLoanApply ";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " WHERE " + Where;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((PageIndex - 1) * PageSize + 1) + " and " + PageIndex * PageSize;

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    LocalCreditModel info = GetLocailCreditModelByDr(dr);
                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取借款申请列表分页
        /// </summary>
        public DataTable GetPagedLoanApplyList(string Where)
        {
            DataTable dt = null;
            var list = new List<LocalCreditModel>();
            string sql1 = "select MemberName,RealName,LoanNumber,Mobile,LoanAmount,CONVERT(VARCHAR(5),LoanTerm) + (CASE WHEN BorrowMode = 0 THEN '天' ELSE '月' END),LoanUseName,CreateTime,CASE WHEN ExamStatus = 0 THEN '初审中' WHEN ExamStatus = 1 THEN '复审中' WHEN ExamStatus = 2 THEN '初审不通过' WHEN ExamStatus = 3 THEN '初审不通过' WHEN ExamStatus = 4 THEN '复审通过' WHEN ExamStatus = 5 THEN '已发标' END ExamStatus FROM v_LocalCreditLoanApply";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " WHERE " + Where;
            }
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        private LocalCreditModel GetLocailCreditModelByDr(DataRow dr)
        {
            LocalCreditModel model = new LocalCreditModel();
            model.ID = Convert.ToInt32(dr["ID"]);
            model.MemberId = dr["MemberId"] != DBNull.Value ? Convert.ToInt32(dr["MemberId"]) : 0;
            model.LoanNumber = dr["LoanNumber"] != DBNull.Value ? Convert.ToString(dr["LoanNumber"]) : "";
            model.LoanId = dr["LoanId"] != DBNull.Value ? Convert.ToInt32(dr["LoanId"]) : 0;
            model.MemberName = dr["MemberName"] != DBNull.Value ? Convert.ToString(dr["MemberName"]) : "";
            model.RealName = dr["RealName"] != DBNull.Value ? Convert.ToString(dr["RealName"]) : "";
            model.IdentityCard = dr["IdentityCard"] != DBNull.Value ? Convert.ToString(dr["IdentityCard"]) : "";
            model.Education = dr["Education"] != DBNull.Value ? Convert.ToInt32(dr["Education"]) : 0;
            model.SocialCard = dr["SocialCard"] != DBNull.Value ? Convert.ToString(dr["SocialCard"]) : "";
            model.Residence = dr["Residence"] != DBNull.Value ? Convert.ToString(dr["Residence"]) : "";
            model.ResidenceTelephone = dr["ResidenceTelephone"] != DBNull.Value ? Convert.ToString(dr["ResidenceTelephone"]) : "";
            model.Mobile = dr["Mobile"] != DBNull.Value ? Convert.ToString(dr["Mobile"]) : "";
            model.CompanyNature = dr["CompanyNature"] != DBNull.Value ? Convert.ToInt32(dr["CompanyNature"]) : 0;
            model.CompanyName = dr["CompanyName"] != DBNull.Value ? Convert.ToString(dr["CompanyName"]) : "";
            model.CompanyAddress = dr["CompanyAddress"] != DBNull.Value ? Convert.ToString(dr["CompanyAddress"]) : "";
            model.CompanyTelephone = dr["CompanyTelephone"] != DBNull.Value ? Convert.ToString(dr["CompanyTelephone"]) : "";
            model.Duties = dr["Duties"] != DBNull.Value ? Convert.ToString(dr["Duties"]) : "";
            model.EmergencyName1 = dr["EmergencyName1"] != DBNull.Value ? Convert.ToString(dr["EmergencyName1"]) : "";
            model.Relationship1 = dr["Relationship1"] != DBNull.Value ? Convert.ToString(dr["Relationship1"]) : "";
            model.ContactNum1 = dr["ContactNum1"] != DBNull.Value ? Convert.ToString(dr["ContactNum1"]) : "";
            model.EmergencyName2 = dr["EmergencyName2"] != DBNull.Value ? Convert.ToString(dr["EmergencyName2"]) : "";
            model.Relationship2 = dr["Relationship2"] != DBNull.Value ? Convert.ToString(dr["Relationship2"]) : "";
            model.ContactNum2 = dr["ContactNum2"] != DBNull.Value ? Convert.ToString(dr["ContactNum2"]) : "";
            model.EmergencyName3 = dr["EmergencyName3"] != DBNull.Value ? Convert.ToString(dr["EmergencyName3"]) : "";
            model.Relationship3 = dr["Relationship3"] != DBNull.Value ? Convert.ToString(dr["Relationship3"]) : "";
            model.ContactNum3 = dr["ContactNum3"] != DBNull.Value ? Convert.ToString(dr["ContactNum3"]) : "";
            model.WifeName = dr["WifeName"] != DBNull.Value ? Convert.ToString(dr["WifeName"]) : "";
            model.IdCard = dr["IdCard"] != DBNull.Value ? Convert.ToString(dr["IdCard"]) : "";
            model.WifeMobile = dr["WifeMobile"] != DBNull.Value ? Convert.ToString(dr["WifeMobile"]) : "";
            model.WifeIncome = dr["WifeIncome"] != DBNull.Value ? Convert.ToString(dr["WifeIncome"]) : "";
            model.CompanyName2 = dr["CompanyName2"] != DBNull.Value ? Convert.ToString(dr["CompanyName2"]) : "";
            model.CompanyAddress2 = dr["CompanyAddress2"] != DBNull.Value ? Convert.ToString(dr["CompanyAddress2"]) : "";
            model.CompanyTelephone2 = dr["CompanyTelephone2"] != DBNull.Value ? Convert.ToString(dr["CompanyTelephone2"]) : "";
            model.WifeDuties = dr["WifeDuties"] != DBNull.Value ? Convert.ToString(dr["WifeDuties"]) : "";
            model.CompanyNature2 = dr["CompanyNature2"] != DBNull.Value ? Convert.ToInt32(dr["CompanyNature2"]) : 0;
            model.HouseOwner = dr["HouseOwner"] != DBNull.Value ? Convert.ToString(dr["HouseOwner"]) : "";
            model.HouseNumber = dr["HouseNumber"] != DBNull.Value ? Convert.ToString(dr["HouseNumber"]) : "";
            model.HouseAddress = dr["HouseAddress"] != DBNull.Value ? Convert.ToString(dr["HouseAddress"]) : "";
            model.CarOwner = dr["CarOwner"] != DBNull.Value ? Convert.ToString(dr["CarOwner"]) : "";
            model.CarNumber = dr["CarNumber"] != DBNull.Value ? Convert.ToString(dr["CarNumber"]) : "";
            model.CarBrand = dr["CarBrand"] != DBNull.Value ? Convert.ToString(dr["CarBrand"]) : "";
            model.IDCardAuthen1 = dr["IDCardAuthen1"] != DBNull.Value ? Convert.ToString(dr["IDCardAuthen1"]) : "";
            model.IDCardAuthen2 = dr["IDCardAuthen2"] != DBNull.Value ? Convert.ToString(dr["IDCardAuthen2"]) : "";
            model.IDCardAuthen3 = dr["IDCardAuthen3"] != DBNull.Value ? Convert.ToString(dr["IDCardAuthen3"]) : "";
            model.BookAuthen1 = dr["BookAuthen1"] != DBNull.Value ? Convert.ToString(dr["BookAuthen1"]) : "";
            model.BookAuthen2 = dr["BookAuthen2"] != DBNull.Value ? Convert.ToString(dr["BookAuthen2"]) : "";
            model.SocialAuthen = dr["SocialAuthen"] != DBNull.Value ? Convert.ToString(dr["SocialAuthen"]) : "";
            model.WorkAuthen = dr["WorkAuthen"] != DBNull.Value ? Convert.ToString(dr["WorkAuthen"]) : "";
            model.BankCard1 = dr["BankCard1"] != DBNull.Value ? Convert.ToString(dr["BankCard1"]) : "";
            model.BankCard2 = dr["BankCard2"] != DBNull.Value ? Convert.ToString(dr["BankCard2"]) : "";
            model.BankStreamLine = dr["BankStreamLine"] != DBNull.Value ? Convert.ToString(dr["BankStreamLine"]) : "";
            model.CreditReport = dr["CreditReport"] != DBNull.Value ? Convert.ToString(dr["CreditReport"]) : "";
            model.ResidenceAuthen = dr["ResidenceAuthen"] != DBNull.Value ? Convert.ToString(dr["ResidenceAuthen"]) : "";
            //model.ResidenceOther = dr["ResidenceOther"] != DBNull.Value ? Convert.ToString(dr["ResidenceOther"]) : "";
            model.CreditCard = dr["CreditCard"] != DBNull.Value ? Convert.ToString(dr["CreditCard"]) : "";
            model.CreditCardBill = dr["CreditCardBill"] != DBNull.Value ? Convert.ToString(dr["CreditCardBill"]) : "";
            model.EducationAuthen = dr["EducationAuthen"] != DBNull.Value ? Convert.ToString(dr["EducationAuthen"]) : "";
            model.MarryAuthen = dr["MarryAuthen"] != DBNull.Value ? Convert.ToString(dr["MarryAuthen"]) : "";
            model.HouseAuthen = dr["HouseAuthen"] != DBNull.Value ? Convert.ToString(dr["HouseAuthen"]) : "";
            model.HouseAuthenPage = dr["HouseAuthenPage"] != DBNull.Value ? Convert.ToString(dr["HouseAuthenPage"]) : "";
            model.LoanUseId = dr["LoanUseId"] != DBNull.Value ? Convert.ToInt32(dr["LoanUseId"]) : 0;
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0.00M;
            model.ExamLoanAmount = dr["ExamLoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["ExamLoanAmount"]) : 0.00M;
            model.LoanUseName = dr["LoanUseName"] != DBNull.Value ? Convert.ToString(dr["LoanUseName"]) : "";
            model.LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0.00M;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.RepaymentMethod = dr["RepaymentMethod"] != DBNull.Value ? Convert.ToInt32(dr["RepaymentMethod"]) : 0;
            model.BorrowMode = dr["BorrowMode"] != DBNull.Value ? Convert.ToInt32(dr["BorrowMode"]) : 0;
            model.ExamStatus = dr["ExamStatus"] != DBNull.Value ? Convert.ToInt32(dr["ExamStatus"]) : 0;
            model.UseDescription = dr["UseDescription"] != DBNull.Value ? Convert.ToString(dr["UseDescription"]) : "";
            model.AuditRecords = dr["AuditRecords"] != DBNull.Value ? Convert.ToString(dr["AuditRecords"]) : "";
            model.CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : DateTime.MinValue;
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : DateTime.MinValue;

            model.MonthlyProfit = dr["Income"] != DBNull.Value ? Convert.ToString(dr["Income"]) : "";
            model.LiabilitiesAmount = dr["LiabilitiesTotalAmount"] != DBNull.Value ? Convert.ToString(dr["LiabilitiesTotalAmount"]) : "";
            model.LiabilitiesRatio = dr["LiabilitiesDebt"] != DBNull.Value ? Convert.ToString(dr["LiabilitiesDebt"]) : "";
            model.MonthlyControlIncome = dr["CanUsedIncome"] != DBNull.Value ? Convert.ToString(dr["CanUsedIncome"]) : "";
            model.QualityProfessional = dr["HighDuties"] != DBNull.Value ? Convert.ToInt32(dr["HighDuties"]) : 0;
            model.HouseProperty = dr["HaveHouse"] != DBNull.Value ? Convert.ToInt32(dr["HaveHouse"]) : 0;
            model.LitigationSeach = dr["LitigationQuery"] != DBNull.Value ? Convert.ToString(dr["LitigationQuery"]) : "";
            model.SocialSecuritySeach = dr["SocialQuery"] != DBNull.Value ? Convert.ToString(dr["SocialQuery"]) : "";
            model.CreditRecordSeach = dr["CreditRecordQuery"] != DBNull.Value ? Convert.ToString(dr["CreditRecordQuery"]) : "";
            model.ContactSituation = dr["EmergencyExplain"] != DBNull.Value ? Convert.ToString(dr["EmergencyExplain"]) : "";
            model.OtherSituation = dr["OtherSituation"] != DBNull.Value ? Convert.ToString(dr["OtherSituation"]) : "";

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
        /// 更新调查信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateInvestigationInfo(LocalCreditModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update LocalCreditInfo set ");
            strSql.Append(" Income = @Income , ");
            strSql.Append(" LiabilitiesTotalAmount = @LiabilitiesTotalAmount, ");
            strSql.Append(" LiabilitiesDebt = @LiabilitiesDebt, ");
            strSql.Append(" CanUsedIncome = @CanUsedIncome, ");
            strSql.Append(" HighDuties = @HighDuties, ");
            strSql.Append(" HaveHouse = @HaveHouse, ");
            strSql.Append(" LitigationQuery = @LitigationQuery, ");
            strSql.Append(" SocialQuery = @SocialQuery, ");
            strSql.Append(" CreditRecordQuery = @CreditRecordQuery, ");
            strSql.Append(" EmergencyExplain = @EmergencyExplain, ");
            strSql.Append(" OtherSituation = @OtherSituation ");
            strSql.Append(" where LoanApplyId=@LoanApplyId ");
            SqlParameter[] parameters = {
			            new SqlParameter("@Income", SqlDbType.Int,4){Value = model.MonthlyProfit} ,            
                        new SqlParameter("@LiabilitiesTotalAmount", SqlDbType.VarChar,50){Value = model.LiabilitiesAmount} ,
                        new SqlParameter("@LiabilitiesDebt", SqlDbType.VarChar,50){Value = model.LiabilitiesRatio} ,
                        new SqlParameter("@CanUsedIncome", SqlDbType.VarChar,50){Value = model.MonthlyControlIncome} ,
                        new SqlParameter("@HighDuties", SqlDbType.Int,4){Value = model.QualityProfessional} ,
                        new SqlParameter("@HaveHouse", SqlDbType.Int,4){Value = model.HouseProperty} ,
                        new SqlParameter("@LitigationQuery", SqlDbType.VarChar,500){Value = model.LitigationSeach} ,
                        new SqlParameter("@SocialQuery", SqlDbType.VarChar,500){Value = model.SocialSecuritySeach} ,
                        new SqlParameter("@CreditRecordQuery", SqlDbType.VarChar,500){Value = model.CreditRecordSeach} ,
                        new SqlParameter("@EmergencyExplain", SqlDbType.VarChar,500){Value = model.ContactSituation} ,
                        new SqlParameter("@OtherSituation", SqlDbType.VarChar,500){Value = model.OtherSituation},  
                        new SqlParameter("@LoanApplyId", SqlDbType.Int,4){Value = model.LoanApplyId}
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
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
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

        /// <summary>
        /// 得到一个申请贷款对象
        /// </summary>
        public LocalCreditApplyModel GetApplyByID(int applyID)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM LocalCreditLoanApply WHERE ID = @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = applyID;
            LocalCreditApplyModel model = new LocalCreditApplyModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    model = GetApplyModel(dr);
                }
            }

            return model;
        }

        private LocalCreditApplyModel GetApplyModel(DataRow dr)
        {
            LocalCreditApplyModel model = new LocalCreditApplyModel();
            model.ID = Convert.ToInt32(dr["ID"]);
            model.MemberId = dr["MemberId"] != DBNull.Value ? Convert.ToInt32(dr["MemberId"]) : 0;
            model.LoanUseId = dr["LoanUseId"] != DBNull.Value ? Convert.ToInt32(dr["LoanUseId"]) : 0;
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0.00M;
            model.LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0.00M;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.RepaymentMethod = dr["RepaymentMethod"] != DBNull.Value ? Convert.ToInt32(dr["RepaymentMethod"]) : 0;
            model.BorrowMode = dr["BorrowMode"] != DBNull.Value ? Convert.ToInt32(dr["BorrowMode"]) : 0;
            model.ExamStatus = dr["ExamStatus"] != DBNull.Value ? Convert.ToInt32(dr["ExamStatus"]) : 0;
            model.UseDescription = dr["UseDescription"] != DBNull.Value ? Convert.ToString(dr["UseDescription"]) : "";
            model.AuditRecords = dr["AuditRecords"] != DBNull.Value ? Convert.ToString(dr["AuditRecords"]) : "";
            model.LoanId = dr["LoanId"] != DBNull.Value ? Convert.ToInt32(dr["LoanId"]) : 0;
            model.CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : DateTime.MinValue;
            model.CreateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : DateTime.MinValue;
            return model;
        }

        /// <summary>
        /// 通过贷款ID找到申请ID
        /// </summary>
        /// <returns></returns>
        public int GetApplyidByLoanId(int LoanID)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT ID FROM LocalCreditLoanApply WHERE LoanID = @LoanID");
            SqlParameter[] parameters = {
					new SqlParameter("@LoanID", SqlDbType.Int,4)
			};
            parameters[0].Value = LoanID;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters));

        }

        /// <summary>
        /// 编辑借款信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModLocalCreditApplyInfo(LocalCreditApplyModel model)
        {
            bool relust = false;
            //修改 借款信息
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LocalCreditLoanApply set ");
            strSql.Append(" LoanUseId=@LoanUseId,LoanAmount=@LoanAmount,ExamLoanAmount=@ExamLoanAmount,LoanRate=@LoanRate,LoanTerm=@LoanTerm,ExamStatus=@ExamStatus,UseDescription=@UseDescription");
            strSql.Append(",AuditRecords=@AuditRecords,UpdateTime=@UpdateTime,LoanId=@LoanId");
            strSql.Append(" where  id=@id ");

            SqlParameter[] parameters =
                {
                    new SqlParameter("@LoanUseId", SqlDbType.Int) {Value = model.LoanUseId},
                    new SqlParameter("@LoanAmount", SqlDbType.Decimal) {Value = model.LoanAmount},
                    new SqlParameter("@ExamLoanAmount", SqlDbType.Decimal) {Value = model.ExamLoanAmount},
                    new SqlParameter("@LoanRate", SqlDbType.Decimal) {Value = model.LoanRate},
                    new SqlParameter("@LoanTerm", SqlDbType.Int) {Value = model.LoanTerm},
                    new SqlParameter("@ExamStatus", SqlDbType.Int) {Value = model.ExamStatus},
                    new SqlParameter("@UseDescription", SqlDbType.VarChar,400) {Value = model.UseDescription},
                    new SqlParameter("@AuditRecords", SqlDbType.VarChar,400) {Value = model.AuditRecords},
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime) {Value = model.UpdateTime},
                    new SqlParameter("@LoanId", SqlDbType.Int) {Value = model.LoanId},
                    new SqlParameter("@id", SqlDbType.Int) {Value = model.ID}
                };

            try
            {
                int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
                relust = rows > 0;
            }
            catch
            {
                relust = false;
            }

            return relust;
        }
    }
}
