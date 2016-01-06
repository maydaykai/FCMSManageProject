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
    public class ApplyAbandonLoanDAL
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " ApplyAbandonLoan A left join Member M on A.MemberID=M.ID left join Loan L on A.LoanID=L.ID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ApplyAbandonLoanModel getApplyAbandonLoanModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, MemberID, LoanID, ApplyReason, ApplyTime, Status, AuditTime, AuditRemark, AuditRecords  ");
            strSql.Append(" from ApplyAbandonLoan ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            ApplyAbandonLoanModel model = new ApplyAbandonLoanModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoanID"].ToString() != "")
                {
                    model.LoanID = int.Parse(ds.Tables[0].Rows[0]["LoanID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApplyReason"].ToString() != "")
                {
                    model.ApplyReason = ds.Tables[0].Rows[0]["ApplyReason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ApplyTime"].ToString() != "")
                {
                    model.ApplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditTime"].ToString() != "")
                {
                    model.AuditTime = DateTime.Parse(ds.Tables[0].Rows[0]["AuditTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditRemark"].ToString() != "")
                {
                    model.AuditRemark = ds.Tables[0].Rows[0]["AuditRemark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AuditRecords"].ToString() != "")
                {
                    model.AuditRecords = ds.Tables[0].Rows[0]["AuditRecords"].ToString();
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
        /// 审核流标申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool auditAbandonLoan(ApplyAbandonLoanModel model)
        {
            SqlParameter[] paras =
            {
                new SqlParameter("@ID", SqlDbType.Int, 4) {Value = model.ID},
                new SqlParameter("@LoanID", SqlDbType.Int, 4) {Value = model.LoanID},
                new SqlParameter("@Status", SqlDbType.SmallInt,2) {Value = model.Status},
                new SqlParameter("@AuditRemark", SqlDbType.NVarChar,250) {Value = model.AuditRemark},
                new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500) {Value = model.AuditRecords},
                new SqlParameter("@ret",SqlDbType.Int,4){Direction = ParameterDirection.ReturnValue} //定义返回值参数
            };
            string sql = "[dbo].[Proc_AuditAbandonLoan]";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, sql, paras);
            int id = Convert.ToInt32(paras[5].Value);
            return id > 0;
        }
    }
}
