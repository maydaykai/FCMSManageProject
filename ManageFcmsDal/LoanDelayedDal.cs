using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public partial class LoanDelayedDal
    {
            
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManageFcmsModel.LoanDelayedModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DECLARE @ErrorTotal int ");
            strSql.Append(" set @ErrorTotal = 0 ");

            strSql.Append("Begin tran ");
            strSql.Append("update LoanDelayed set ");

            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" AuditTime = @AuditTime , ");
            strSql.Append(" LoanID = @LoanID , ");
            strSql.Append(" LoanNumber = @LoanNumber , ");
            strSql.Append(" LoanMemberID = @LoanMemberID , ");
            strSql.Append(" BidStartTime = @BidStartTime , ");
            strSql.Append(" BidEndTime = @BidEndTime , ");
            strSql.Append(" NewBidEndTime = @NewBidEndTime , ");
            strSql.Append(" AuditUserID = @AuditUserID , ");
            strSql.Append(" AuditStatus = @AuditStatus  ");
            strSql.Append(" where ID=@ID ");

            strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");

            strSql.Append("update LoanDelayed set ");
            strSql.Append(" BidEndTime = @BidEndTime, ");
            strSql.Append(" UpdateTime = getdate() ");
            strSql.Append(" where ID=@LoanID ");

            strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");

            strSql.Append(" if @@ERROR>0 ");
            strSql.Append(" begin ");
            strSql.Append(" rollback TransAction ");
            strSql.Append(" end ");
            strSql.Append(" Else ");
            strSql.Append(" Begin ");
            strSql.Append(" Commit TransAction ");
            strSql.Append(" End ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@AuditTime", SqlDbType.DateTime){Value = model.AuditTime} ,            
                        new SqlParameter("@LoanID", SqlDbType.Int,4){Value = model.LoanID} ,            
                        new SqlParameter("@LoanNumber", SqlDbType.VarChar,20){Value = model.LoanNumber} ,            
                        new SqlParameter("@LoanMemberID", SqlDbType.Int,4){Value = model.LoanMemberID} ,            
                        new SqlParameter("@BidStartTime", SqlDbType.DateTime){Value = model.BidStartTime} ,            
                        new SqlParameter("@BidEndTime", SqlDbType.DateTime){Value = model.BidEndTime} ,            
                        new SqlParameter("@NewBidEndTime", SqlDbType.DateTime){Value = model.NewBidEndTime} ,            
                        new SqlParameter("@AuditUserID", SqlDbType.Int,4){Value = model.AuditUserID} ,            
                        new SqlParameter("@AuditStatus", SqlDbType.Int,4){Value = model.AuditStatus}             
              
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
        public ManageFcmsModel.LoanDelayedModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ld.*,m.MemberName,fu.UserName,Case AuditStatus when 0 then '未审核' when 1 then '通过' when 2 then '未通过' end as AuditStatusName  ");
            strSql.Append("  from LoanDelayed ld inner join Member m on ld.LoanMemberID = m.ID left join FcmsUser fu on ld.AuditUserID = fu.ID ");
            strSql.Append(" where ld.ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            ManageFcmsModel.LoanDelayedModel model = new ManageFcmsModel.LoanDelayedModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                model.CreateTime = ds.Tables[0].Rows[0]["CreateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateTime"]) : PublicConst.MinDate;
                model.AuditTime = ds.Tables[0].Rows[0]["AuditTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["AuditTime"]) : PublicConst.MinDate;
                model.LoanID = ds.Tables[0].Rows[0]["LoanID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LoanID"]) : 0;
                model.LoanNumber = ds.Tables[0].Rows[0]["LoanNumber"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["LoanNumber"]) : "";
                model.LoanMemberID = ds.Tables[0].Rows[0]["LoanMemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LoanMemberID"]) : 0;
                model.BidStartTime = ds.Tables[0].Rows[0]["BidStartTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["BidStartTime"]) : PublicConst.MinDate;
                model.BidEndTime = ds.Tables[0].Rows[0]["BidEndTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["BidEndTime"]) : PublicConst.MinDate;
                model.NewBidEndTime = ds.Tables[0].Rows[0]["NewBidEndTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["NewBidEndTime"]) : PublicConst.MinDate;
                model.AuditUserID = ds.Tables[0].Rows[0]["AuditUserID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["AuditUserID"]) : 0;
                model.AuditStatus = ds.Tables[0].Rows[0]["AuditStatus"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["AuditStatus"]) : 0;
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["MemberName"]) : "";
                model.UserName = ds.Tables[0].Rows[0]["UserName"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["UserName"]) : "";
                model.AuditStatusName = ds.Tables[0].Rows[0]["AuditStatusName"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["AuditStatusName"]) : "";

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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,m.MemberName,fu.UserName,Case AuditStatus when 0 then '未审核' when 1 then '通过' when 2 then '不通过' end as AuditStatusName ");
            strSql.Append(" FROM LoanDelayed ld inner join Member m on ld.LoanMemberID = m.ID left join FcmsUser fu on ld.AuditUserID = fu.ID  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
        }

    }
}
