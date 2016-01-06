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
    //AuditHistoryDal
    public partial class AuditHistoryDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AuditHistoryModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AuditHistory(");
            strSql.Append("Process,Result,UserID,AuditTime,Reason,LoanID,ReviewComments");
			strSql.Append(") values (");
            strSql.Append("@Process,@Result,@UserID,getdate(),@Reason,@LoanID,@ReviewComments");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@Process", SqlDbType.Int,4){Value = model.Process} ,            
                        new SqlParameter("@Result", SqlDbType.Bit,1){Value = model.Result} ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4){Value = model.UserID} ,            
                        new SqlParameter("@Reason", SqlDbType.VarChar,50){Value = model.Reason} ,            
                        new SqlParameter("@LoanID", SqlDbType.Int,4){Value = model.LoanID},
                        new SqlParameter("@ReviewComments", SqlDbType.NVarChar,4000){Value = model.ReviewComments}  
              
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
        /// 更新一条数据
        /// </summary>
        public bool Update(AuditHistoryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AuditHistory set ");

            strSql.Append(" Process = @Process , ");
            strSql.Append(" Result = @Result , ");
            strSql.Append(" UserID = @UserID , ");
            strSql.Append(" AuditTime = @AuditTime , ");
            strSql.Append(" Reason = @Reason , ");
            strSql.Append(" LoanID = @LoanID  ");
            strSql.Append(" ReviewComments = @ReviewComments  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@Process", SqlDbType.Int,4){Value = model.Process} ,            
                        new SqlParameter("@Result", SqlDbType.Bit,1){Value = model.Result} ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4){Value = model.UserID} ,            
                        new SqlParameter("@AuditTime", SqlDbType.DateTime){Value = model.AuditTime} ,            
                        new SqlParameter("@Reason", SqlDbType.VarChar,50){Value = model.Reason} ,            
                        new SqlParameter("@LoanID", SqlDbType.Int,4){Value = model.LoanID},
                        new SqlParameter("@ReviewComments", SqlDbType.NVarChar,4000){Value = model.ReviewComments}    
              
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
        /// 得到一个对象实体
        /// </summary>
        public AuditHistoryModel GetAuditHistoryModel(int ID)
        {

            var strSql = new StringBuilder();
            strSql.Append("select ID, Process, Result, UserID, AuditTime, Reason, LoanID, ReviewComments ");
            strSql.Append("  from AuditHistory ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            var model = new AuditHistoryModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Process"].ToString() != "")
                {
                    model.Process = int.Parse(ds.Tables[0].Rows[0]["Process"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Result"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Result"].ToString() == "1") || (ds.Tables[0].Rows[0]["Result"].ToString().ToLower() == "true"))
                    {
                        model.Result = true;
                    }
                    else
                    {
                        model.Result = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditTime"].ToString() != "")
                {
                    model.AuditTime = DateTime.Parse(ds.Tables[0].Rows[0]["AuditTime"].ToString());
                }
                model.Reason = ds.Tables[0].Rows[0]["Reason"].ToString();
                if (ds.Tables[0].Rows[0]["LoanID"].ToString() != "")
                {
                    model.LoanID = int.Parse(ds.Tables[0].Rows[0]["LoanID"].ToString());
                }
                model.ReviewComments = ds.Tables[0].Rows[0]["ReviewComments"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        public AuditHistoryModel GetAuditHistoryModelByLoanId(int loadnId)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, Process, Result, UserID, AuditTime, Reason, LoanID, ReviewComments ");
            strSql.Append("  from AuditHistory ");
            strSql.Append(" where LoanID=@loadnId and Process=4");
            SqlParameter[] parameters = {
					new SqlParameter("@loadnId", SqlDbType.Int,4)
			};
            parameters[0].Value = loadnId;


            var model = new AuditHistoryModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Process"].ToString() != "")
                {
                    model.Process = int.Parse(ds.Tables[0].Rows[0]["Process"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Result"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Result"].ToString() == "1") || (ds.Tables[0].Rows[0]["Result"].ToString().ToLower() == "true"))
                    {
                        model.Result = true;
                    }
                    else
                    {
                        model.Result = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditTime"].ToString() != "")
                {
                    model.AuditTime = DateTime.Parse(ds.Tables[0].Rows[0]["AuditTime"].ToString());
                }
                model.Reason = ds.Tables[0].Rows[0]["Reason"].ToString();
                if (ds.Tables[0].Rows[0]["LoanID"].ToString() != "")
                {
                    model.LoanID = int.Parse(ds.Tables[0].Rows[0]["LoanID"].ToString());
                }
                model.ReviewComments = ds.Tables[0].Rows[0]["ReviewComments"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ah.*,fu.UserName,Ro.Name as ProcessName ");
            strSql.Append(" FROM AuditHistory ah inner join FcmsUser fu on ah.UserID=fu.ID inner join Role Ro on ah.Process=Ro.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by AuditTime ");
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM AuditHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }


    }
}
