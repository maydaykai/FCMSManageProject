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
    public class ApplyDeferLoanDAL
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " ApplyDeferLoan A left join Member M on A.MemberID=M.ID left join Loan L on A.LoanID=L.ID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 审核展期申请
        /// </summary>
        /// <param name="model"></param>
        /// <param name="operateID"></param>
        /// <returns></returns>
        public bool auditDeferLoan(ApplyDeferLoanModel model)
        {
            bool flag = false;
            LoanModel loanModel = new LoanDal().GetLoanModel(model.LoanID);
            MemberModel memberModel = new MemberDal().GetModel(model.MemberID);
            SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocal);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            string sql = "update ApplyDeferLoan set [Status]=@Status,AuditRecords+=@AuditRecords,AuditTime=@AuditTime,Remark=@Remark where ID=@ID";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tran;
                SqlParameter[] paras =
                {
                    new SqlParameter("@Status", SqlDbType.SmallInt,2) {Value = model.Status},
                    new SqlParameter("@AuditRecords", SqlDbType.Int,4) {Value = model.AuditRecords},
                    new SqlParameter("@AuditTime", SqlDbType.DateTime) {Value = DateTime.Now},
                    new SqlParameter("@Remark", SqlDbType.NVarChar,250) {Value = model.Remark},
                    new SqlParameter("@ID", SqlDbType.Int, 4) {Value = model.ID}
                };
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                foreach (var para in paras)
                {
                    cmd.Parameters.Add(para);
                }
                int num = cmd.ExecuteNonQuery();
                string sql2 = "";
                SqlParameter[] paras2 = 
                { 
                    new SqlParameter("@ID", SqlDbType.Int, 4) {Value = model.LoanID}
                };
                cmd.CommandText = sql2;
                cmd.Parameters.Clear();
                foreach (var para in paras2)
                {
                    cmd.Parameters.Add(para);
                }
                int num2 = cmd.ExecuteNonQuery();
                #region 发送站内信跟短信
                MailModel mailModel = new MailModel
                {
                    SendUserID = -1,
                    ReceiveUserID = model.MemberID,
                    ReadStatus = false,
                    Title = "",
                    MailContent = "",
                    SendTime = DateTime.Now
                };
                SMSModel SMSModel = new SMSModel
                {
                    Mobile = memberModel.Mobile,
                    SendTime = DateTime.Now,
                    SendTimes = 0,
                    SendLevel = 1,
                    SendStatus = 0,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                if (model.Status == 2)
                {
                    mailModel.Title = "申请展期成功";
                    mailModel.MailContent = "您所申请的借款编号为"+loanModel.LoanNumber+"的展期已申请成功。";
                    SMSModel.SmsContent = "您所申请的借款编号为" + loanModel.LoanNumber + "的展期已申请成功。";
                }
                else if (model.Status == 3)
                {
                    mailModel.Title = "申请展期失败";
                    mailModel.MailContent = "您所申请的借款编号为" + loanModel.LoanNumber + "的展期申请失败。失败原因：" + model.Remark;
                    SMSModel.SmsContent = "您所申请的借款编号为" + loanModel.LoanNumber + "的展期申请失败。失败原因：" + model.Remark;
                }
                int mail = new SMSDAL().addMail(cmd, mailModel);
                int sms = new SMSDAL().addSMS(cmd, SMSModel);
                #endregion
                if (num > 0 && num2 > 0 && mail > 0 && sms > 0)
                {
                    flag = true;
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ApplyDeferLoanModel getApplyDeferLoanModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, Status, Remark, AuditTime, AuditRecords, MemberID, LoanID, ExtensionTerm, ExtensionRate, GuaranteeFee, LoanServiceCharges, ExtensionReason, ApplyTime  ");
            strSql.Append(" from ApplyDeferLoan ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            ApplyDeferLoanModel model = new ApplyDeferLoanModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AuditTime"].ToString() != "")
                {
                    model.AuditTime = DateTime.Parse(ds.Tables[0].Rows[0]["AuditTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditRecords"].ToString() != "")
                {
                    model.AuditRecords = ds.Tables[0].Rows[0]["AuditRecords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoanID"].ToString() != "")
                {
                    model.LoanID = int.Parse(ds.Tables[0].Rows[0]["LoanID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExtensionTerm"].ToString() != "")
                {
                    model.ExtensionTerm = int.Parse(ds.Tables[0].Rows[0]["ExtensionTerm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExtensionRate"].ToString() != "")
                {
                    model.ExtensionRate = decimal.Parse(ds.Tables[0].Rows[0]["ExtensionRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GuaranteeFee"].ToString() != "")
                {
                    model.GuaranteeFee = decimal.Parse(ds.Tables[0].Rows[0]["GuaranteeFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoanServiceCharges"].ToString() != "")
                {
                    model.LoanServiceCharges = decimal.Parse(ds.Tables[0].Rows[0]["LoanServiceCharges"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExtensionReason"].ToString() != "")
                {
                    model.ExtensionReason = ds.Tables[0].Rows[0]["ExtensionReason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ApplyTime"].ToString() != "")
                {
                    model.ApplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplyTime"].ToString());
                }
                #endregion
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
