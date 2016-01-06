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
    public class StudentInfoDAL
    {
      


        /// <summary>
        /// 更新
        /// </summary>
        public bool UpdateByMemberId(StudentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [RJBDB].[dbo].[StudentInfo] SET ");
            strSql.Append(" [Age] = @Age,");
            strSql.Append("[Sex] = @Sex");
            strSql.Append(" ,[Mobile] = @Mobile");
            //strSql.Append(" ,[NativePlaceProvince] = @NativePlaceProvince");
            //strSql.Append(",[NativePlaceCity] = @NativePlaceCity");
            strSql.Append(",[PlaceResidenceProvince] = @PlaceResidenceProvince");
            strSql.Append(",[PlaceResidenceCity] = @PlaceResidenceCity");
            strSql.Append(" ,[PlaceResidenceDetailed] = @PlaceResidenceDetailed");
            strSql.Append(" ,[AccountLocationProvince] = @AccountLocationProvince");
            strSql.Append(" ,[AccountLocationCity] = @AccountLocationCity");
            strSql.Append(" ,[AccountLocationDetailed] = @AccountLocationDetailed");
            //strSql.Append(" ,[CollegeProvince] = @CollegeProvince");
            //strSql.Append(" ,[CollegeCity] = @CollegeCity");
            strSql.Append(" ,[UniversityName] = @UniversityName");
            strSql.Append(" ,[Professional] = @Professional");
            strSql.Append(" ,[EnrollmentYear] = @EnrollmentYear");
            //strSql.Append(" ,[GraduationDate] = @GraduationDate");
            strSql.Append(" ,[Education] = @Education");
            //strSql.Append(" ,[StudentID] = @StudentID");
            //strSql.Append(" ,[MonthlyLivingExpenses] = @MonthlyLivingExpenses");
            //strSql.Append(" ,[FamilySize] = @FamilySize");
            strSql.Append(" ,[RelativeContactMethod] = @RelativeContactMethod");
            strSql.Append(" ,[FriendContactMethod] = @FriendContactMethod");
            strSql.Append(" ,[PositiveIdentityCard] = @PositiveIdentityCard");
            strSql.Append(" ,[NegativeIdentityCard] = @NegativeIdentityCard");
            strSql.Append(" ,[StudentIDCard] = @StudentIDCard");
            strSql.Append(" ,[StudentInformationScreenshot] = @StudentInformationScreenshot");
            strSql.Append(" ,[StudentInformationalipay] = @StudentInformationalipay");
            strSql.Append(" ,[UpdateTime] = @UpdateTime");
            strSql.Append(" ,[StudentInformationlastYearAlipay] = @StudentInformationlastYearAlipay");
            strSql.Append(" ,[HeadsetIdentityCard] = @HeadsetIdentityCard");
            //strSql.Append(" ,[House] = @House");
            //strSql.Append(" ,[Car] = @Car");
            //strSql.Append(" ,[JobStatus] = @JobStatus");
            //strSql.Append(" ,[WorkYear] = @WorkYear");
            //strSql.Append(" ,[MaritalStatus] = @MaritalStatus");
            strSql.Append(" WHERE MemberID=@MemberID");

            SqlParameter[] parameters = {           
			            new SqlParameter("@Sex",SqlDbType.NVarChar,10){Value=model.Sex} ,            
                        new SqlParameter("@Mobile",SqlDbType.VarChar){Value=model.Mobile} ,  
                        new SqlParameter("@Age",SqlDbType.Int){Value=model.Age} ,  
                        //new SqlParameter("@NativePlaceProvince",SqlDbType.Int,4){Value=model.NativePlaceProvince} ,            
                        //new SqlParameter("@NativePlaceCity",SqlDbType.Int,4){Value=model.NativePlaceCity} ,            
                        new SqlParameter("@PlaceResidenceProvince",SqlDbType.Int,4){Value=model.PlaceResidenceProvince} ,            
                        new SqlParameter("@PlaceResidenceCity",SqlDbType.Int,4){Value=model.PlaceResidenceCity} ,            
                        new SqlParameter("@PlaceResidenceDetailed",SqlDbType.NVarChar,150){Value=model.PlaceResidenceDetailed} ,            
                        new SqlParameter("@AccountLocationProvince",SqlDbType.Int,4){Value=model.AccountLocationProvince} ,            
                        new SqlParameter("@AccountLocationCity",SqlDbType.Int,4){Value=model.AccountLocationCity} ,            
                        new SqlParameter("@AccountLocationDetailed", SqlDbType.NVarChar,500){Value=model.AccountLocationDetailed} ,            
                        //new SqlParameter("@CollegeProvince",SqlDbType.Int,4){Value=model.CollegeProvince} ,            
                        //new SqlParameter("@CollegeCity",SqlDbType.Int,4){Value=model.CollegeCity} ,            
                        new SqlParameter("@UniversityName",SqlDbType.NVarChar,100){Value=model.UniversityName} ,            
                        new SqlParameter("@Professional",SqlDbType.NVarChar,50){Value=model.Professional} ,            
                        new SqlParameter("@EnrollmentYear",SqlDbType.VarChar,50){Value=model.EnrollmentYear} ,            
                        //new SqlParameter("@GraduationDate",SqlDbType.VarChar,50){Value=model.GraduationDate} ,            
                        new SqlParameter("@Education",SqlDbType.NVarChar,20){Value=model.Education} ,            
                        //new SqlParameter("@StudentID",SqlDbType.VarChar,20){Value=model.StudentID??""} ,            
                        //new SqlParameter("@MonthlyLivingExpenses",SqlDbType.Decimal){Value=model.MonthlyLivingExpenses} ,            
                        //new SqlParameter("@FamilySize",SqlDbType.Int,4){Value=model.FamilySize} ,            
                        new SqlParameter("@RelativeContactMethod",SqlDbType.NVarChar,1000){Value=model.RelativeContactMethod} ,            
                        new SqlParameter("@FriendContactMethod",SqlDbType.NVarChar,1000){Value=model.FriendContactMethod} ,            
                        new SqlParameter("@PositiveIdentityCard",SqlDbType.VarChar,500){Value=model.PositiveIdentityCard??""},
                        new SqlParameter("@NegativeIdentityCard",SqlDbType.VarChar,500){Value=model.NegativeIdentityCard??""},
                        new SqlParameter("@StudentIDCard", SqlDbType.VarChar,500){Value=model.StudentIDCard??""},
                        new SqlParameter("@StudentInformationScreenshot", SqlDbType.VarChar,500){Value=model.StudentInformationScreenshot??""},
                        new SqlParameter("@StudentInformationalipay", SqlDbType.VarChar,500){Value=model.StudentInformationalipay??""},                        
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime,1){Value=model.UpdateTime},
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID},
                        new SqlParameter("@StudentInformationlastYearAlipay", SqlDbType.VarChar){Value=model.StudentInformationlastYearAlipay},
                        new SqlParameter("@HeadsetIdentityCard", SqlDbType.VarChar){Value=model.HeadsetIdentityCard}
                        //new SqlParameter("@House", SqlDbType.Bit){Value=model.House},
                        //new SqlParameter("@Car", SqlDbType.Bit){Value=model.Car},
                        //new SqlParameter("@JobStatus", SqlDbType.NVarChar,50){Value=model.JobStatus},
                        //new SqlParameter("@WorkYear", SqlDbType.Int,4){Value=model.WorkYear},
                        //new SqlParameter("@MaritalStatus", SqlDbType.Bit){Value=model.MaritalStatus}
            };
            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
    }
}
