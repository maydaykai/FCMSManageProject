using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
     public  class MemberReferralDal
     {
         public static bool insert(MemberRecommendedModel model)
         { 
            // var strSql = new StringBuilder();
             SqlParameter[] parameters =
                        {
                            new SqlParameter("@RecMemberID", SqlDbType.Int) {Value = model.RecMemberID},
                            new SqlParameter("@RecedMemberID", SqlDbType.Int) {Value = model.RecedMemberID},
                            new SqlParameter("@Bonus", SqlDbType.Int) {Value = 0},
                            new SqlParameter("@BonusPutStatus", SqlDbType.Int) {Value = 0},
                            new SqlParameter("@CreateTime", SqlDbType.DateTime) {Value = DateTime.Now},
                            new SqlParameter("@UpdateTime", SqlDbType.DateTime) {Value = DateTime.Now}
                        };

             var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.Proc_MemberReferral", parameters);
             return obj > 0;
             // return num.Tables[0];
         }
     }
}
