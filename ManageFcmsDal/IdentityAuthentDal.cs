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
    public class IdentityAuthentDal
    {

        /// <summary>
        /// 根据会员ID判断其是否通过实名认证
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool AuthentExists(int memberId)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) from IdentityAuthent");
            strSql.Append(" where ");
            strSql.Append(" MemberID = @MemberID ");
            strSql.Append(" AND AuthentResult = 1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int,4){Value=memberId}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return ConvertHelper.ToInt(obj.ToString()) > 0;
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        {
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public IdentityAuthentModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, ExpireTime, ApplyTime, UpdateTime, MemberID, RealName, IdentityCard, AuthentResult, IdPhoto, ExamStatus, RequestXml  ");
            strSql.Append("  from IdentityAuthent ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};

            var model = new IdentityAuthentModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExpireTime"].ToString() != "")
                {
                    model.ExpireTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExpireTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApplyTime"].ToString() != "")
                {
                    model.ApplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.IdentityCard = ds.Tables[0].Rows[0]["IdentityCard"].ToString();
                if (ds.Tables[0].Rows[0]["AuthentResult"].ToString() != "")
                {
                    model.AuthentResult = int.Parse(ds.Tables[0].Rows[0]["AuthentResult"].ToString());
                }
                model.IdPhoto = ds.Tables[0].Rows[0]["IdPhoto"].ToString();
                if (ds.Tables[0].Rows[0]["ExamStatus"].ToString() != "")
                {
                    model.ExamStatus = int.Parse(ds.Tables[0].Rows[0]["ExamStatus"].ToString());
                }
                model.RequestXml = ds.Tables[0].Rows[0]["RequestXml"].ToString();

                return model;
            }
            return null;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(IdentityAuthentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IdentityAuthent set ");

            strSql.Append(" AuthentResult = @AuthentResult ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@AuthentResult", SqlDbType.Int,4){Value = -2} 
            };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 审核不通过
        /// </summary>
        public bool AuditFail(IdentityAuthentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IdentityAuthent set ");

            strSql.Append(" AuthentResult = @AuthentResult, ExamStatus = @ExamStatus, UpdateTime=getdate()");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@AuthentResult", SqlDbType.Int,4){Value = model.AuthentResult} ,            
                        new SqlParameter("@ExamStatus", SqlDbType.Int,4){Value = model.ExamStatus}
            };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 审核通过
        /// </summary>
        public bool AuditSuccess(IdentityAuthentModel model)
        {
            try
            {
                SqlParameter[] parameters = {          
                    new SqlParameter("@ID", SqlDbType.Int,4){Value=model.ID} ,  
                    new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID} ,            
                    new SqlParameter("@RealName", SqlDbType.NVarChar,20){Value=model.RealName} ,            
                    new SqlParameter("@IdentityCard", SqlDbType.VarChar,20){Value=model.IdentityCard} ,  
                    new SqlParameter("@Sex", SqlDbType.Char, 2) {Value = ConvertHelper.getSexByIDCard(model.IdentityCard)},
                    new SqlParameter("@Birthday", SqlDbType.DateTime) {Value =ConvertHelper.getBirthdayByIDCard(model.IdentityCard)},  
                    new SqlParameter("@CreateTime", SqlDbType.DateTime) {Value = model.ApplyTime}
                };
                var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_IdentityAuthentAuditSuccess", parameters);
                return result != null && ConvertHelper.ToInt(result.ToString()) > 0;
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(ex);
                return false;
            }
        }
    }
}
