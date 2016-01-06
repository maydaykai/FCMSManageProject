using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class RegistrationDal
    {

        public static DataSet GetList(DateTime dateStart, DateTime endDate, int typeId, string channelType = "", int dateType = 1)
        {
            string procedure = "ProvTotalChannel";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@dateStart", SqlDbType.VarChar,20){Value=dateStart.ToString("yyyy-MM-dd")} ,  
                new SqlParameter("@dateEnd", SqlDbType.VarChar,20){Value=endDate.ToString("yyyy-MM-dd")} , 
                new SqlParameter("@TotalType", SqlDbType.Int){Value=typeId},
                new SqlParameter("@ChannelType",channelType),   
            };
            if (dateType != 0)
            {
                procedure = "dbo.UserChannelTotal";
                parameters = new SqlParameter[] { 
                    new SqlParameter("@dateStart", SqlDbType.VarChar,20){Value=dateStart.ToString("yyyy-MM-dd")} ,  
                    new SqlParameter("@dateEnd", SqlDbType.VarChar,20){Value=endDate.ToString("yyyy-MM-dd")} , 
                    new SqlParameter("@TotalType", SqlDbType.Int){Value=typeId},
                    new SqlParameter("@ChannelType",channelType),   
                    new SqlParameter("@dateType",dateType)
                };                
            }

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, procedure, parameters);
            return ds;

        }
    }
}