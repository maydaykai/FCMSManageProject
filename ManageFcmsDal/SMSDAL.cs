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
    public class SMSDAL
    {
        /// <summary>
        /// 增加短信
        /// </summary>
        public int addSMS(SMSModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into SMS(");
            strSql.Append("Mobile,SmsContent,SendTime");
            strSql.Append(") values (");
            strSql.Append("@Mobile,@SmsContent,@SendTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@Mobile", SqlDbType.NVarChar,15){Value=model.Mobile} ,            
                        new SqlParameter("@SmsContent", SqlDbType.NVarChar,200){Value=model.SmsContent} ,            
                        new SqlParameter("@SendTime", SqlDbType.DateTime){Value=model.SendTime}                                
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj.ToString());

        }
        /// <summary>
        /// 增加短信
        /// </summary>
        public int addSMS(SqlCommand cmd, SMSModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into SMS(");
            strSql.Append("Mobile,SmsContent,SendTime");
            strSql.Append(") values (");
            strSql.Append("@Mobile,@SmsContent,@SendTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] paras = {
			            new SqlParameter("@Mobile", SqlDbType.NVarChar,15){Value=model.Mobile} ,            
                        new SqlParameter("@SmsContent", SqlDbType.NVarChar,200){Value=model.SmsContent} ,            
                        new SqlParameter("@SendTime", SqlDbType.DateTime){Value=model.SendTime}                                
            };

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql.ToString();
            cmd.Parameters.Clear();
            foreach (var para in paras)
            {
                cmd.Parameters.Add(para);
            }
            var obj = cmd.ExecuteScalar();
            return obj == null ? 0 : Convert.ToInt32(obj.ToString());
        }
        /// <summary>
        /// 增加站内信
        /// </summary>
        public int addMail(MailModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into Mail(");
            strSql.Append("SendUserID,ReceiveUserID,ReadStatus,Title,MailContent,SendTime");
            strSql.Append(") values (");
            strSql.Append("@SendUserID,@ReceiveUserID,@ReadStatus,@Title,@MailContent,@SendTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = 
            { 
                new SqlParameter("@SendUserID", SqlDbType.Int, 4){Value=model.SendUserID},
                new SqlParameter("@ReceiveUserID", SqlDbType.Int, 4){Value=model.ReceiveUserID},
                new SqlParameter("@ReadStatus", SqlDbType.Bit, 1){Value=model.ReadStatus},
                new SqlParameter("@Title", SqlDbType.NVarChar, 64){Value=model.Title},
                new SqlParameter("@MailContent", SqlDbType.NVarChar, 500){Value=model.MailContent},
                new SqlParameter("@SendTime", SqlDbType.DateTime){Value=model.SendTime}
            };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 增加站内信
        /// </summary>
        public int addMail(SqlCommand cmd, MailModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into Mail(");
            strSql.Append("SendUserID,ReceiveUserID,ReadStatus,Title,MailContent,SendTime");
            strSql.Append(") values (");
            strSql.Append("@SendUserID,@ReceiveUserID,@ReadStatus,@Title,@MailContent,@SendTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] paras = 
            { 
                new SqlParameter("@SendUserID", SqlDbType.Int, 4){Value=model.SendUserID},
                new SqlParameter("@ReceiveUserID", SqlDbType.Int, 4){Value=model.ReceiveUserID},
                new SqlParameter("@ReadStatus", SqlDbType.Bit, 1){Value=model.ReadStatus},
                new SqlParameter("@Title", SqlDbType.NVarChar, 64){Value=model.Title},
                new SqlParameter("@MailContent", SqlDbType.NVarChar, 500){Value=model.MailContent},
                new SqlParameter("@SendTime", SqlDbType.DateTime){Value=model.SendTime}
            };
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql.ToString();
            cmd.Parameters.Clear();
            foreach (var para in paras)
            {
                cmd.Parameters.Add(para);
            }
            object obj = cmd.ExecuteScalar();
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
    }
}
