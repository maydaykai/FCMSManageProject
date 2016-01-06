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
    public class LoadStudentDAL
    {
        public DataTable GetLoan_StudentInfo(int Id)
        {
            //查询学生信息
            StringBuilder strSql = new StringBuilder();
            StringBuilder fiede = new StringBuilder();
            fiede.Append(" ID,MemberID,Sex, ISNULL(Age,0) AS Age,House,Car,JobStatus,WorkYear,MaritalStatus, ");
            fiede.Append(" ISNULL(NativePlaceProvince,0) AS NativePlaceProvince ,ISNULL(NativePlaceCity,0) AS NativePlaceCity,");
            fiede.Append(" ISNULL(PlaceResidenceProvince,0) AS PlaceResidenceProvince,ISNULL(PlaceResidenceCity,0) PlaceResidenceCity,PlaceResidenceDetailed,");
            fiede.Append("  AccountLocationProvince,AccountLocationCity,AccountLocationDetailed,ISNULL(CollegeProvince,0) CollegeProvince,  ISNULL(CollegeCity,0) CollegeCity,");
            fiede.Append("  UniversityName,Professional,EnrollmentYear, GraduationDate,Education,StudentID,MonthlyLivingExpenses,ISNULL(FamilySize,0) FamilySize,");
            fiede.Append("   RelativeContactMethod,FriendContactMethod,PositiveIdentityCard,NegativeIdentityCard,StudentIDCard,");
            fiede.Append("     StudentInformationScreenshot,CreateTime,UpdateTime, StudentInformationalipay,Mobile,HeadsetIdentityCard , StudentInformationlastYearAlipay");
            strSql.Append("SELECT " + fiede.ToString() + " FROM dbo.StudentInfo where MemberID=@id");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@id", SqlDbType.Int) {Value = Id}
                };
            var table = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return table.Tables[0];


        }


        /// <summary>
        /// 查询借款信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetLoan_StudentLoanApplyInfo(int Id)
        {
            //查询学生信息
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM dbo.StudentLoanApply where id=@id");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@id", SqlDbType.Int) {Value = Id}
                };
            var table = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return table.Tables[0];


        }

        /// <summary>
        /// 编辑借款信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateStudentLoanApplyInfo(StudentLoanApply model)
        {
            bool relust = false;
            //修改 借款信息
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  StudentLoanApply set ");
            strSql.Append(" LoanUseId=@LoanUseId,LoanAmount=@LoanAmount,LoanRate=@LoanRate,LoanTerm=@LoanTerm,ExamStatus=@ExamStatus,UseDescription=@UseDescription");
            strSql.Append(",AuditRecords=@AuditRecords,UpdateTime=@UpdateTime,LoanId=@LoanId");
            strSql.Append(" where  id=@id ");

            SqlParameter[] parameters =
                {
                    new SqlParameter("@LoanUseId", SqlDbType.Int) {Value = model.LoanUseId},
                    new SqlParameter("@LoanAmount", SqlDbType.Decimal) {Value = model.LoanAmount},
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

        //插入借款标表更新 loanId
    }
}
