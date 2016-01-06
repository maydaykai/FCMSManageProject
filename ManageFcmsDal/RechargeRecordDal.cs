using System.Data.SqlClient;
using System.Globalization;
using ManageFcmsCommon;
using ManageFcmsConn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class RechargeRecordDal
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " RechargeRecord P left join Member M on P.MemberID=M.ID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 总计
        /// </summary>
        public object Aggregate(string filters)
        {
            string strSql = "Select SUM(Amount) FROM RechargeRecord P left join Member M on P.MemberID=M.ID where" + filters;

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
        }

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(RechargeRecordModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into RechargeRecord(");
            strSql.Append(" MemberID,Type,RechargeChannel,OrderNumber,MerchantOrderNo,Amount,RechargeFee,Status,ApplicationTime");
            strSql.Append(") values (");
            strSql.Append(" @MemberID,@Type,@RechargeChannel,@OrderNumber,@MerchantOrderNo,@Amount,@RechargeFee,@Status,@ApplicationTime");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {                                            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID} ,            
                        new SqlParameter("@Type", SqlDbType.Int,4){Value=model.Type} ,            
                        new SqlParameter("@RechargeChannel", SqlDbType.Int,4){Value=model.RechargeChannel} ,            
                        new SqlParameter("@OrderNumber", SqlDbType.VarChar,50){Value=model.OrderNumber} ,            
                        new SqlParameter("@MerchantOrderNo", SqlDbType.VarChar,50){Value=model.MerchantOrderNo} ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9){Value=model.Amount} ,            
                        new SqlParameter("@RechargeFee", SqlDbType.Decimal,9){Value=model.RechargeFee} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value=model.Status},
                        new SqlParameter("@ApplicationTime", SqlDbType.DateTime){Value=model.ApplicationTime}
            };
            var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return ConvertHelper.ToInt(obj.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(RechargeRecordModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update RechargeRecord set ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" AuditStatus = @AuditStatus , ");
            strSql.Append(" AuditRecords = @AuditRecords , ");
            strSql.Append(" MemberID = @MemberID , ");
            strSql.Append(" Status = @Status  ");
            strSql.Append(" where ID=@ID");
            if (model.AuditStatus == 1 || model.AuditStatus == 2)
            {
                strSql.Append(" AND AuditStatus=0");
            }
            else if (model.AuditStatus == 3)
            {
                strSql.Append(" AND AuditStatus=1");
            }


            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,                       
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@AuditStatus", SqlDbType.Int,4){Value = model.AuditStatus} ,            
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,200){Value = model.AuditRecords} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value = model.MemberID} ,                                            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value = model.Status}             
            };

            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;

        }

        /// <summary>
        /// 线下充值复审通过事务处理
        /// </summary>
        public int RechargeBelowLine(RechargeRecordModel model)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@RechargeID", SqlDbType.Int,4){Value = model.ID} ,                                              
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,200){Value = model.AuditRecords}                                                               
            };

            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_RechargeBelowLine", parameters);

            return ConvertHelper.ToInt(result.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RechargeRecordModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, RequestXml, ResponseXml, ApplicationTime, UpdateTime, AuditStatus, AuditRecords, MemberID, Type, RechargeChannel, OrderNumber, MerchantOrderNo, Amount, RechargeFee, Status  ");
            strSql.Append("  from RechargeRecord ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};

            var model = new RechargeRecordModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.RequestXml = ds.Tables[0].Rows[0]["RequestXml"].ToString();
                model.ResponseXml = ds.Tables[0].Rows[0]["ResponseXml"].ToString();
                if (ds.Tables[0].Rows[0]["ApplicationTime"].ToString() != "")
                {
                    model.ApplicationTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplicationTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditStatus"].ToString() != "")
                {
                    model.AuditStatus = int.Parse(ds.Tables[0].Rows[0]["AuditStatus"].ToString());
                }
                model.AuditRecords = ds.Tables[0].Rows[0]["AuditRecords"].ToString();
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RechargeChannel"].ToString() != "")
                {
                    model.RechargeChannel = int.Parse(ds.Tables[0].Rows[0]["RechargeChannel"].ToString());
                }
                model.OrderNumber = ds.Tables[0].Rows[0]["OrderNumber"].ToString();
                model.MerchantOrderNo = ds.Tables[0].Rows[0]["MerchantOrderNo"].ToString();
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RechargeFee"].ToString() != "")
                {
                    model.RechargeFee = decimal.Parse(ds.Tables[0].Rows[0]["RechargeFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                return model;
            }
            return null;
        }

    }
}
