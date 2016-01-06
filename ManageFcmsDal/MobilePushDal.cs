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
    public class MobilePushDal
    {
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(MobilePushModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("IF NOT EXISTS(SELECT * FROM dbo.MobilePush WHERE EventID=@EventID) BEGIN insert into MobilePush(");
            strSql.Append("MessageType,PushTitle,PushContent,EventID,PushStatus,CreateTime,UpdateTime");
            strSql.Append(") values (");
            strSql.Append("@MessageType,@PushTitle,@PushContent,@EventID,@PushStatus,@CreateTime,@UpdateTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY() END ELSE BEGIN SELECT 0 END");
            SqlParameter[] parameters = {
			            new SqlParameter("@MessageType", SqlDbType.Int,4){Value=model.MessageType} ,            
                        new SqlParameter("@PushTitle", SqlDbType.NVarChar,100){Value=model.PushTitle} ,            
                        new SqlParameter("@PushContent", SqlDbType.NVarChar,400){Value=model.PushContent} ,            
                        new SqlParameter("@EventID", SqlDbType.Int,4){Value=model.EventID} ,            
                        new SqlParameter("@PushStatus", SqlDbType.Bit,1){Value=model.PushStatus} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime}             
              
            };

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            return obj == null ? 0 : Convert.ToInt32(obj);
        }


        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MobilePushModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MobilePush set ");

            strSql.Append(" MessageType = @MessageType , ");
            strSql.Append(" PushTitle = @PushTitle , ");
            strSql.Append(" PushContent = @PushContent , ");
            strSql.Append(" EventID = @EventID , ");
            strSql.Append(" PushStatus = @PushStatus , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = @UpdateTime  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@MessageType", SqlDbType.Int,4){Value = model.MessageType} ,            
                        new SqlParameter("@PushTitle", SqlDbType.NVarChar,100){Value = model.PushTitle} ,            
                        new SqlParameter("@PushContent", SqlDbType.NVarChar,400){Value = model.PushContent} ,            
                        new SqlParameter("@EventID", SqlDbType.Int,4){Value = model.EventID} ,            
                        new SqlParameter("@PushStatus", SqlDbType.Bit,1){Value = model.PushStatus} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime}             
              
            };


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
    }
}
