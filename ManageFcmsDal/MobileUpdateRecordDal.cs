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
    public class MobileUpdateRecordDal
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " MobileUpdateRecord P LEFT JOIN Member M ON P.MemberID=M.ID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileUpdateRecordModel getMobileUpdateRecordModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, AuditRecords, CreateTime, UpdateTime, MemberID, Type, RealName, IdentityCard=LEFT(IdentityCard,6)+'XXXX'+RIGHT(IdentityCard,4), OldMobile, NewMobile, AuditStatus, Remark  ");
            strSql.Append(" from MobileUpdateRecord ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ID desc ");
            MobileUpdateRecordModel model = new MobileUpdateRecordModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditRecords"].ToString() != "")
                {
                    model.AuditRecords = ds.Tables[0].Rows[0]["AuditRecords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IdentityCard"].ToString() != "")
                {
                    model.IdentityCard = ds.Tables[0].Rows[0]["IdentityCard"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OldMobile"].ToString() != "")
                {
                    model.OldMobile = ds.Tables[0].Rows[0]["OldMobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NewMobile"].ToString() != "")
                {
                    model.NewMobile = ds.Tables[0].Rows[0]["NewMobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AuditStatus"].ToString() != "")
                {
                    model.AuditStatus = int.Parse(ds.Tables[0].Rows[0]["AuditStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                #endregion
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool updateMobileUpdateRecord(MobileUpdateRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MobileUpdateRecord set ");

            strSql.Append(" AuditRecords = @AuditRecords , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" OldMobile = @OldMobile , ");
            strSql.Append(" NewMobile = @NewMobile , ");
            strSql.Append(" AuditStatus = @AuditStatus , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value= model.ID},
			            new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500){Value= model.AuditRecords},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= model.UpdateTime},
                        new SqlParameter("@OldMobile", SqlDbType.VarChar,20){Value= model.OldMobile},
                        new SqlParameter("@NewMobile", SqlDbType.VarChar,20){Value= model.NewMobile},
                        new SqlParameter("@AuditStatus", SqlDbType.SmallInt,2){Value= model.AuditStatus},
                        new SqlParameter("@Remark", SqlDbType.NVarChar,500){Value= model.Remark},
                        };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 修改手机号码成功处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool mobileUpdateSuccess(MobileUpdateRecordModel model)
        {
            SqlParameter[] paras = {
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= model.ID},
			            new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500){Value= model.AuditRecords},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= model.UpdateTime},
                        new SqlParameter("@NewMobile", SqlDbType.VarChar,20){Value= model.NewMobile},
                        new SqlParameter("@Remark", SqlDbType.NVarChar,500){Value= model.Remark},
                        };
            string sql = "[dbo].[Proc_MobileUpdateSuccess]";
            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, sql, paras);
            return result != null && Convert.ToInt32(result.ToString()) > 0;
        }
    }
}
