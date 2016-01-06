using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCMSModel;
using ManageFcmsCommon;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class CreditAssignmentDal
    {
        /// <summary>
        /// 获取债权转让分页
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public List<CreditAssignmentModel> GetPageCreditAssignmentModel(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            List<CreditAssignmentModel> list = new List<CreditAssignmentModel>();
            string sql1 = "select count(*) as totals from CreditAssignment C left join Member M on C.MemberID=M.ID left join MemberInfo MI on C.MemberID=MI.MemberID left join Loan L on C.OldLoanId=L.ID";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " where " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                TotalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + OrderBy + ")) AS rownum, C.*, M.MemberName, MI.RealName, L.LoanNumber OldLoanNumber, L.LoanTerm, L.BorrowMode,dbo.GetFreezeAmountByLoanId(C.ID) as FreezeAmount from CreditAssignment C left join Member M on C.MemberID=M.ID left join MemberInfo MI on C.MemberID=MI.MemberID left join Loan L on C.OldLoanId=L.ID";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " where " + Where;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + (PageIndex - 1) * PageSize + " and " + PageIndex * PageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                CreditAssignmentModel info = getCreditAssignmentModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获得债权转让标详细信息
        /// </summary>
        public CreditAssignmentDetailModel GetCreditAssignmentDetail(int creditAssignmentId)
        {
            var parameters = new[] 
            { 
                new SqlParameter("@CreditAssignmentID", SqlDbType.Int, 4){Value=creditAssignmentId}
            };

            var model = new CreditAssignmentDetailModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.Proc_CreditAssignmentDetail", parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值

                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OldBidId"].ToString() != "")
                {
                    model.OldBidId = int.Parse(ds.Tables[0].Rows[0]["OldBidId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OldLoanId"].ToString() != "")
                {
                    model.OldLoanId = int.Parse(ds.Tables[0].Rows[0]["OldLoanId"].ToString());
                    model.LoanIdSec = DESStringHelper.EncryptString(ds.Tables[0].Rows[0]["OldLoanId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BidStartTime"].ToString() != "")
                {
                    model.BidStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["BidStartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BidEndTime"].ToString() != "")
                {
                    model.BidEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["BidEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BidAmountMin"].ToString() != "")
                {
                    model.BidAmountMin = decimal.Parse(ds.Tables[0].Rows[0]["BidAmountMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BidAmountMax"].ToString() != "")
                {
                    model.BidAmountMax = decimal.Parse(ds.Tables[0].Rows[0]["BidAmountMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExamStatus"].ToString() != "")
                {
                    model.ExamStatus = int.Parse(ds.Tables[0].Rows[0]["ExamStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BiddingProcess"].ToString() != "")
                {
                    model.BiddingProcess = decimal.Parse(ds.Tables[0].Rows[0]["BiddingProcess"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BidCount"].ToString() != "")
                {
                    model.BidCount = int.Parse(ds.Tables[0].Rows[0]["BidCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TransferRate"].ToString() != "")
                {
                    model.TransferRate = decimal.Parse(ds.Tables[0].Rows[0]["TransferRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                model.ContractNo = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                if (ds.Tables[0].Rows[0]["ContractGenerTime"].ToString() != "")
                {
                    model.ContractGenerTime = DateTime.Parse(ds.Tables[0].Rows[0]["ContractGenerTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FullScaleTime"].ToString() != "")
                {
                    model.FullScaleTime = DateTime.Parse(ds.Tables[0].Rows[0]["FullScaleTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                model.LoanTitle = ds.Tables[0].Rows[0]["LoanTitle"].ToString();
                model.LoanNumber = ds.Tables[0].Rows[0]["LoanNumber"].ToString();
                if (ds.Tables[0].Rows[0]["LoanAmount"].ToString() != "")
                {
                    model.LoanAmount = decimal.Parse(ds.Tables[0].Rows[0]["LoanAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DiscountRate"].ToString() != "")
                {
                    model.DiscountRate = decimal.Parse(ds.Tables[0].Rows[0]["DiscountRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RealLoanAmount"].ToString() != "")
                {
                    model.RealLoanAmount = decimal.Parse(ds.Tables[0].Rows[0]["RealLoanAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoanRate"].ToString() != "")
                {
                    model.LoanRate = decimal.Parse(ds.Tables[0].Rows[0]["LoanRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UseDays"].ToString() != "")
                {
                    model.UseDays = int.Parse(ds.Tables[0].Rows[0]["UseDays"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RemainedDays"].ToString() != "")
                {
                    model.RemainedDays = ds.Tables[0].Rows[0]["RemainedDays"].ToString();
                }
                model.LoanTerm = ds.Tables[0].Rows[0]["LoanTerm"].ToString();
                model.RepaymentMethodName = ds.Tables[0].Rows[0]["RepaymentMethodName"].ToString();
                model.AssignmentReInterest = ds.Tables[0].Rows[0]["AssignmentReInterest"] != DBNull.Value ? ConvertHelper.ToDecimal(ds.Tables[0].Rows[0]["AssignmentReInterest"].ToString()) : 0;
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"] != DBNull.Value ? ds.Tables[0].Rows[0]["MemberName"].ToString().Substring(0, 1) + "*****" + ds.Tables[0].Rows[0]["MemberName"].ToString().Substring(ds.Tables[0].Rows[0]["MemberName"].ToString().Length - 1, 1) : "";
                model.BiddingTimeRemain = ds.Tables[0].Rows[0]["BiddingTimeRemain"].ToString();
                model.ExamStatusName = ds.Tables[0].Rows[0]["ExamStatusName"].ToString();
                model.SurplusTransferAmount = ds.Tables[0].Rows[0]["SurplusTransferAmount"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["SurplusTransferAmount"]) : 0;
                model.CurrentPaymentDate = ds.Tables[0].Rows[0]["CurrentPaymentDate"].ToString();
                return model;
                #endregion
            }
            return null;
        }

        private CreditAssignmentModel getCreditAssignmentModelByDr(SqlDataReader dr)
        {
            CreditAssignmentModel model = new CreditAssignmentModel
                {
                    ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                    RemainedDays = dr["RemainedDays"] != DBNull.Value ? dr["RemainedDays"].ToString() : "",
                    OldBidId = dr["OldBidId"] != DBNull.Value ? Convert.ToInt32(dr["OldBidId"]) : 0,
                    OldLoanId = dr["OldLoanId"] != DBNull.Value ? Convert.ToInt32(dr["OldLoanId"]) : 0,
                    BidStartTime =
                        dr["BidStartTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidStartTime"]) : DateTime.MinValue,
                    BidEndTime = dr["BidEndTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidEndTime"]) : DateTime.MinValue,
                    BidAmountMin = dr["BidAmountMin"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmountMin"]) : 0,
                    BidAmountMax = dr["BidAmountMax"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmountMax"]) : 0,
                    ExamStatus = dr["ExamStatus"] != DBNull.Value ? Convert.ToInt32(dr["ExamStatus"]) : 0,
                    BiddingProcess = dr["BiddingProcess"] != DBNull.Value ? Convert.ToDecimal(dr["BiddingProcess"]) : 0,
                    BidCount = dr["BidCount"] != DBNull.Value ? Convert.ToInt32(dr["BidCount"]) : 0,
                    MemberID = dr["MemberID"] != DBNull.Value ? Convert.ToInt32(dr["MemberID"]) : 0,
                    TransferRate = dr["TransferRate"] != DBNull.Value ? Convert.ToDecimal(dr["TransferRate"]) : 0,
                    ContractNo = dr["ContractNo"] != DBNull.Value ? dr["ContractNo"].ToString() : "",
                    ContractGenerTime =
                        dr["ContractGenerTime"] != DBNull.Value
                            ? Convert.ToDateTime(dr["ContractGenerTime"])
                            : DateTime.MinValue,
                    FullScaleTime =
                        dr["FullScaleTime"] != DBNull.Value ? Convert.ToDateTime(dr["FullScaleTime"]) : DateTime.MinValue,
                    CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : DateTime.MinValue,
                    UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : DateTime.MinValue,
                    LoanTitle = dr["LoanTitle"] != DBNull.Value ? dr["LoanTitle"].ToString() : "",
                    LoanNumber = dr["LoanNumber"] != DBNull.Value ? dr["LoanNumber"].ToString() : "",
                    LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0,
                    DiscountRate = dr["DiscountRate"] != DBNull.Value ? Convert.ToDecimal(dr["DiscountRate"]) : 0,
                    RealLoanAmount = dr["RealLoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["RealLoanAmount"]) : 0,
                    LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0,
                    UseDays = dr["UseDays"] != DBNull.Value ? Convert.ToInt32(dr["UseDays"]) : 0,
                    MemberName = dr["MemberName"] != DBNull.Value ? dr["MemberName"].ToString() : "",
                    RealName = dr["RealName"] != DBNull.Value ? dr["RealName"].ToString() : "",
                    OldLoanNumber = dr["OldLoanNumber"] != DBNull.Value ? dr["OldLoanNumber"].ToString() : "",
                    LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0,
                    BorrowMode = dr["BorrowMode"] != DBNull.Value ? Convert.ToInt32(dr["BorrowMode"]) : 0,
                    FreezeAmount = dr["FreezeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["FreezeAmount"]) : 0
                };
            return model;
        }
    }
}
