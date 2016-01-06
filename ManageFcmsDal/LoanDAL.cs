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
    public class LoanDal
    {
        /// <summary>
        /// 增加一条借贷数据
        /// </summary>
        public int AddLoan(LoanModel model, LoanMemberInfoModel loanMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel)
        {
            SqlParameter[] parameters = {                                    
                        new SqlParameter("@CityID", SqlDbType.Int,4){Value=model.CityID} , 
                        new SqlParameter("@DimLoanUseID", SqlDbType.Int,4){Value=model.DimLoanUseID} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID} ,                                                                                                                                            
                        new SqlParameter("@LoanTitle", SqlDbType.NVarChar,50){Value=model.LoanTitle} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value=model.LoanAmount} ,            
                        new SqlParameter("@LoanTypeID", SqlDbType.Int,4){Value=model.LoanTypeID} ,            
                        new SqlParameter("@LoanRate", SqlDbType.Decimal,9){Value=model.LoanRate} ,                        
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value=model.LoanTerm} ,            
                        new SqlParameter("@RepaymentMethod", SqlDbType.Int,4){Value=model.RepaymentMethod} ,            
                        new SqlParameter("@BorrowMode", SqlDbType.Int,4){Value=model.BorrowMode},
                        new SqlParameter("@TradeType", SqlDbType.Int,4){Value=model.TradeType},
                        new SqlParameter("@LoanApplyId", SqlDbType.Int,4){Value=model.ID},
                        new SqlParameter("@HaveCar", SqlDbType.Bit,1){Value= loanMemberInfoModel.HaveCar},
                        new SqlParameter("@WorkingLife", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.WorkingLife},
                        new SqlParameter("@JobStatus", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.JobStatus},
                        new SqlParameter("@Age", SqlDbType.Int,4){Value= loanMemberInfoModel.Age},
                        new SqlParameter("@MaritalStatus", SqlDbType.Int,4){Value= loanMemberInfoModel.MaritalStatus},
                        new SqlParameter("@Sex", SqlDbType.NVarChar,2){Value= loanMemberInfoModel.Sex},
                        new SqlParameter("@DomicilePlace", SqlDbType.Int,4){Value= loanMemberInfoModel.DomicilePlace},
                        new SqlParameter("@MonthlyPay", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.MonthlyPay},
                        new SqlParameter("@HaveHouse", SqlDbType.Bit,1){Value= loanMemberInfoModel.HaveHouse},
                        new SqlParameter("@FamilyNum", SqlDbType.Int,4){Value= loanMemberInfoModel.FamilyNum},
                        new SqlParameter("@BranchCompanyID", SqlDbType.Int,4){Value= greenChannelRecordModel.BranchCompanyID},
                        new SqlParameter("@AutoBidScale", SqlDbType.Decimal,9){Value= model.AutoBidScale},
                        new SqlParameter("@BidStratTime", SqlDbType.DateTime){Value= model.BidStratTime},
                        new SqlParameter("@BidEndTime", SqlDbType.DateTime){Value= model.BidEndTime},
                        new SqlParameter("@BidAmountMax", SqlDbType.Decimal,9){Value= model.BidAmountMax},
                        new SqlParameter("@Scale", SqlDbType.Decimal,9){Value=greenChannelRecordModel.Scale}
            };
             
            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_LoanAddNew", parameters);
            return obj == null ? 0 : int.Parse(obj.ToString());
        }

        public int AddLoan(LoanModel model, LoanMemberInfoModel loanMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel, AppointmentLoan appointmentLoan)
        {
            SqlParameter[] parameters = {                                    
                        new SqlParameter("@CityID", SqlDbType.Int,4){Value=model.CityID} ,                                 
                        new SqlParameter("@DimLoanUseID", SqlDbType.Int,4){Value=model.DimLoanUseID} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID} ,                                                                                                                                            
                        new SqlParameter("@LoanTitle", SqlDbType.NVarChar,50){Value=model.LoanTitle} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value=model.LoanAmount} ,            
                        new SqlParameter("@LoanTypeID", SqlDbType.Int,4){Value=model.LoanTypeID} ,            
                        new SqlParameter("@LoanRate", SqlDbType.Decimal,9){Value=model.LoanRate} ,                        
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value=model.LoanTerm} ,            
                        new SqlParameter("@RepaymentMethod", SqlDbType.Int,4){Value=model.RepaymentMethod} ,            
                        new SqlParameter("@BorrowMode", SqlDbType.Int,4){Value=model.BorrowMode},
                        new SqlParameter("@TradeType", SqlDbType.Int,4){Value=model.TradeType},
                        new SqlParameter("@LoanApplyId", SqlDbType.Int,4){Value=model.ID},
                        new SqlParameter("@HaveCar", SqlDbType.Bit,1){Value= loanMemberInfoModel.HaveCar},
                        new SqlParameter("@WorkingLife", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.WorkingLife},
                        new SqlParameter("@JobStatus", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.JobStatus},
                        new SqlParameter("@Age", SqlDbType.Int,4){Value= loanMemberInfoModel.Age},
                        new SqlParameter("@MaritalStatus", SqlDbType.Int,4){Value= loanMemberInfoModel.MaritalStatus},
                        new SqlParameter("@Sex", SqlDbType.NVarChar,2){Value= loanMemberInfoModel.Sex},
                        new SqlParameter("@DomicilePlace", SqlDbType.Int,4){Value= loanMemberInfoModel.DomicilePlace},
                        new SqlParameter("@MonthlyPay", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.MonthlyPay},
                        new SqlParameter("@HaveHouse", SqlDbType.Bit,1){Value= loanMemberInfoModel.HaveHouse},
                        new SqlParameter("@FamilyNum", SqlDbType.Int,4){Value= loanMemberInfoModel.FamilyNum},
                        new SqlParameter("@BranchCompanyID", SqlDbType.Int,4){Value= greenChannelRecordModel.BranchCompanyID},
                        new SqlParameter("@Scale", SqlDbType.Decimal,9){Value=greenChannelRecordModel.Scale},
                        new SqlParameter("@isRjk", SqlDbType.Int){Value=appointmentLoan==null?0:1},
                        new SqlParameter("@isAppointment", SqlDbType.Int){Value=appointmentLoan==null?0:appointmentLoan.isAppointment},
                        new SqlParameter("@endTime", SqlDbType.DateTime){Value=appointmentLoan==null?DateTime.Now:appointmentLoan.endTime},
                        new SqlParameter("@biddingTime", SqlDbType.DateTime){Value=appointmentLoan==null?DateTime.Now:appointmentLoan.biddingTime},
                        new SqlParameter("@BidStratTime", SqlDbType.DateTime){Value=model.BidStratTime},
                        new SqlParameter("@BidEndTime", SqlDbType.DateTime){Value=model.BidEndTime}
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_LoanAddNew2", parameters);
            return obj == null ? 0 : int.Parse(obj.ToString());
        }




        /// <summary>
        /// 企业会员借款申请
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanEnterpriseMemberInfoModel"></param>
        /// <param name="greenChannelRecordModel"></param>
        /// <returns></returns>
        public int AddLoanEnterprise(LoanModel model, LoanEnterpriseMemberInfoModel loanEnterpriseMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel)
        {
            SqlParameter[] parameters = {                                    
                        new SqlParameter("@CityID", SqlDbType.Int,4){Value=model.CityID} ,                                 
                        new SqlParameter("@DimLoanUseID", SqlDbType.Int,4){Value=model.DimLoanUseID} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID} ,                                                                                                                                            
                        new SqlParameter("@LoanTitle", SqlDbType.NVarChar,50){Value=model.LoanTitle} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value=model.LoanAmount} ,            
                        new SqlParameter("@LoanTypeID", SqlDbType.Int,4){Value=model.LoanTypeID} ,            
                        new SqlParameter("@LoanRate", SqlDbType.Decimal,9){Value=model.LoanRate} ,                        
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value=model.LoanTerm} ,            
                        new SqlParameter("@RepaymentMethod", SqlDbType.Int,4){Value=model.RepaymentMethod} ,            
                        new SqlParameter("@BorrowMode", SqlDbType.Int,4){Value=model.BorrowMode},
                        new SqlParameter("@TradeType", SqlDbType.Int,4){Value=model.TradeType},
                        new SqlParameter("@LoanApplyId", SqlDbType.Int,4){Value=model.ID},
                        new SqlParameter("@SetUpyear", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.SetUpyear},
                        new SqlParameter("@Nature", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.Nature},
                        new SqlParameter("@Size", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.Size},
                        new SqlParameter("@IndustryCategory", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.IndustryCategory},
                        new SqlParameter("@LiveCityID", SqlDbType.Int,4){Value= loanEnterpriseMemberInfoModel.CityId},
                        new SqlParameter("@MainProducts", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.MainProducts},
                        new SqlParameter("@RegisteredCapital", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.RegisteredCapital},
                        new SqlParameter("@BusinessScope", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.BusinessScope},
                        new SqlParameter("@BranchCompanyID", SqlDbType.Int,4){Value= greenChannelRecordModel.BranchCompanyID},
                        new SqlParameter("@Scale", SqlDbType.Decimal,9){Value=greenChannelRecordModel.Scale}
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_EnterpriseLoanAdd", parameters);
            return obj == null ? 0 : int.Parse(obj.ToString());
        }

        /// <summary>
        /// 企业会员借款申请
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanEnterpriseMemberInfoModel"></param>
        /// <param name="greenChannelRecordModel"></param>
        /// <returns></returns>
        public int AddLoanEnterprise(LoanModel model, LoanEnterpriseMemberInfoModel loanEnterpriseMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel, AppointmentLoan appointmentLoan)
        {
            SqlParameter[] parameters = {                                    
                        new SqlParameter("@CityID", SqlDbType.Int,4){Value=model.CityID} ,                                 
                        new SqlParameter("@DimLoanUseID", SqlDbType.Int,4){Value=model.DimLoanUseID} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID} ,                                                                                                                                            
                        new SqlParameter("@LoanTitle", SqlDbType.NVarChar,50){Value=model.LoanTitle} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value=model.LoanAmount} ,            
                        new SqlParameter("@LoanTypeID", SqlDbType.Int,4){Value=model.LoanTypeID} ,            
                        new SqlParameter("@LoanRate", SqlDbType.Decimal,9){Value=model.LoanRate} ,                        
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value=model.LoanTerm} ,            
                        new SqlParameter("@RepaymentMethod", SqlDbType.Int,4){Value=model.RepaymentMethod} ,            
                        new SqlParameter("@BorrowMode", SqlDbType.Int,4){Value=model.BorrowMode},
                        new SqlParameter("@TradeType", SqlDbType.Int,4){Value=model.TradeType},
                        new SqlParameter("@LoanApplyId", SqlDbType.Int,4){Value=model.ID},
                        new SqlParameter("@SetUpyear", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.SetUpyear},
                        new SqlParameter("@Nature", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.Nature},
                        new SqlParameter("@Size", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.Size},
                        new SqlParameter("@IndustryCategory", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.IndustryCategory},
                        new SqlParameter("@LiveCityID", SqlDbType.Int,4){Value= loanEnterpriseMemberInfoModel.CityId},
                        new SqlParameter("@MainProducts", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.MainProducts},
                        new SqlParameter("@RegisteredCapital", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.RegisteredCapital},
                        new SqlParameter("@BusinessScope", SqlDbType.NVarChar,50){Value= loanEnterpriseMemberInfoModel.BusinessScope},
                        new SqlParameter("@BranchCompanyID", SqlDbType.Int,4){Value= greenChannelRecordModel.BranchCompanyID},
                        new SqlParameter("@Scale", SqlDbType.Decimal,9){Value=greenChannelRecordModel.Scale},
                        new SqlParameter("@isRjk", SqlDbType.Int){Value=appointmentLoan==null?0:1},
                        new SqlParameter("@isAppointment", SqlDbType.Int){Value=appointmentLoan==null?0:appointmentLoan.isAppointment},
                        new SqlParameter("@endTime", SqlDbType.DateTime){Value=appointmentLoan==null?DateTime.Now:appointmentLoan.endTime},
                        new SqlParameter("@biddingTime", SqlDbType.DateTime){Value=appointmentLoan==null?DateTime.Now:appointmentLoan.biddingTime},
                        new SqlParameter("@BidStratTime", SqlDbType.DateTime){Value=model.BidStratTime},
                        new SqlParameter("@BidEndTime", SqlDbType.DateTime){Value=model.BidEndTime}
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_EnterpriseLoanAdd_New", parameters);
            return obj == null ? 0 : int.Parse(obj.ToString());
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LoanModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update Loan set ");

            strSql.Append(" TradeType = @TradeType , ");
            strSql.Append(" BidAmountMin = @BidAmountMin , ");
            strSql.Append(" BidAmountMax = @BidAmountMax , ");
            strSql.Append(" BidStratTime = @BidStratTime , ");
            strSql.Append(" BidEndTime = @BidEndTime , ");
            strSql.Append(" Bond = @Bond , ");
            strSql.Append(" CityID = @CityID , ");
            strSql.Append(" ExamStatus = @ExamStatus , ");
            strSql.Append(" BiddingProcess = @BiddingProcess , ");
            strSql.Append(" MemberID = @MemberID , ");
            strSql.Append(" DimLoanUseID = @DimLoanUseID , ");
            strSql.Append(" BidCount = @BidCount , ");
            strSql.Append(" GuaranteeID = @GuaranteeID , ");
            strSql.Append(" ReviewTime = @ReviewTime , ");
            strSql.Append(" RepaymentLastTime = @RepaymentLastTime , ");
            strSql.Append(" ContractNo = @ContractNo , ");
            strSql.Append(" FullScaleTime = @FullScaleTime , ");
            strSql.Append(" UnderTime = @UnderTime , ");
            strSql.Append(" ContractGenerTime = @ContractGenerTime , ");
            strSql.Append(" LoanNumber = @LoanNumber , ");
            strSql.Append(" NeedGuarantee = @NeedGuarantee , ");
            strSql.Append(" GuaranteeFee = @GuaranteeFee , ");
            strSql.Append(" NeedLoanCharges = @NeedLoanCharges , ");
            strSql.Append(" LoanServiceCharges = @LoanServiceCharges , ");
            strSql.Append(" NeedBidCharges = @NeedBidCharges , ");
            strSql.Append(" BidServiceCharges = @BidServiceCharges , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = getdate() , ");
            strSql.Append(" LoanDescribe = @LoanDescribe ,  ");
            //strSql.Append(" TimeStamp = @TimeStamp , ");
            strSql.Append(" LoanAmount = @LoanAmount , ");
            strSql.Append(" LoanRate = @LoanRate , ");
            strSql.Append(" ReleasedRate = @ReleasedRate , ");
            strSql.Append(" LoanTerm = @LoanTerm , ");
            strSql.Append(" RepaymentMethod = @RepaymentMethod , ");
            strSql.Append(" BorrowMode = @BorrowMode ,  ");
            strSql.Append(" SumScore = @SumScore ,  ");
            strSql.Append(" AutoBidScale = @AutoBidScale ,  ");
            strSql.Append(" ScoreLevel = @ScoreLevel  ");

            strSql.Append(" where ID=@ID and TimeStamp = @TimeStamp ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} , 
                        new SqlParameter("@TimeStamp", SqlDbType.Int,4){Value = model.TimeStamp} , 
                        new SqlParameter("@TradeType", SqlDbType.Int,4){Value = model.TradeType} ,            
                        new SqlParameter("@BidAmountMin", SqlDbType.Decimal,9){Value = model.BidAmountMin} ,            
                        new SqlParameter("@BidAmountMax", SqlDbType.Decimal,9){Value = model.BidAmountMax} ,            
                        new SqlParameter("@BidStratTime", SqlDbType.DateTime){Value = model.BidStratTime} ,            
                        new SqlParameter("@BidEndTime", SqlDbType.DateTime){Value = model.BidEndTime} ,            
                        new SqlParameter("@Bond", SqlDbType.Decimal,9){Value = model.Bond} ,            
                        new SqlParameter("@CityID", SqlDbType.Int,4){Value = model.CityID} ,            
                        new SqlParameter("@ExamStatus", SqlDbType.Int,4){Value = model.ExamStatus} ,            
                        new SqlParameter("@BiddingProcess", SqlDbType.Decimal,9){Value = model.BiddingProcess} ,            
                        new SqlParameter("@DimLoanUseID", SqlDbType.Int,4){Value = model.DimLoanUseID} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value = model.MemberID} ,            
                        new SqlParameter("@BidCount", SqlDbType.Int,4){Value = model.BidCount} ,            
                        new SqlParameter("@GuaranteeID", SqlDbType.Int,4){Value = model.GuaranteeID} ,            
                        new SqlParameter("@ReviewTime", SqlDbType.DateTime){Value = model.ReviewTime} ,            
                        new SqlParameter("@RepaymentLastTime", SqlDbType.DateTime){Value = model.RepaymentLastTime} ,            
                        new SqlParameter("@ContractNo", SqlDbType.VarChar,20){Value = model.ContractNo} ,            
                        new SqlParameter("@FullScaleTime", SqlDbType.DateTime){Value = model.FullScaleTime} ,            
                        new SqlParameter("@UnderTime", SqlDbType.DateTime){Value = model.UnderTime} ,            
                        new SqlParameter("@ContractGenerTime", SqlDbType.DateTime){Value = model.ContractGenerTime} ,            
                        new SqlParameter("@NeedGuarantee", SqlDbType.Bit,1){Value = model.NeedGuarantee} ,            
                        new SqlParameter("@GuaranteeFee", SqlDbType.Decimal,9){Value = model.GuaranteeFee} ,            
                        new SqlParameter("@LoanNumber", SqlDbType.VarChar,20){Value = model.LoanNumber} ,            
                        new SqlParameter("@NeedLoanCharges", SqlDbType.Bit,1){Value = model.NeedLoanCharges} ,            
                        new SqlParameter("@LoanServiceCharges", SqlDbType.Decimal,9){Value = model.LoanServiceCharges} ,            
                        new SqlParameter("@NeedBidCharges", SqlDbType.Bit,1){Value = model.NeedBidCharges} ,            
                        new SqlParameter("@BidServiceCharges", SqlDbType.Decimal,9){Value = model.BidServiceCharges} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@LoanDescribe", SqlDbType.Text){Value = model.LoanDescribe} ,
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value = model.LoanAmount} ,            
                        new SqlParameter("@LoanRate", SqlDbType.Decimal,9){Value = model.LoanRate} ,            
                        new SqlParameter("@ReleasedRate", SqlDbType.Decimal,9){Value = model.ReleasedRate} ,            
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value = model.LoanTerm} ,            
                        new SqlParameter("@RepaymentMethod", SqlDbType.Int,4){Value = model.RepaymentMethod} ,            
                        new SqlParameter("@BorrowMode", SqlDbType.Int,4){Value = model.BorrowMode} ,
                        new SqlParameter("@SumScore", SqlDbType.Int,4){Value = model.SumScore} ,
                        new SqlParameter("@AutoBidScale", SqlDbType.Decimal,9){Value = model.AutoBidScale} ,
                        new SqlParameter("@ScoreLevel", SqlDbType.VarChar,10){Value = model.ScoreLevel} 
                        
              
            };

            try
            {
                int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
                return rows > 0;
            }
            catch
            {
                return false;
            }
        }


        public bool Update_Student(LoanModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update Loan set BidStratTime=@BidStratTime,BidEndTime=@BidEndTime,BidAmountMin=@BidAmountMin,BidAmountMax=@BidAmountMax, ");
            strSql.Append(" AutoBidScale=@AutoBidScale,GuaranteeFee=@GuaranteeFee,LoanServiceCharges=@LoanServiceCharges,BidServiceCharges=@BidServiceCharges,GuaranteeID=@GuaranteeID");
            strSql.Append(" where ID=@ID");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4),
                        new SqlParameter("@BidStratTime", SqlDbType.DateTime),
                        new SqlParameter("@BidEndTime", SqlDbType.DateTime),
                        new SqlParameter("@BidAmountMin", SqlDbType.Decimal),
                        new SqlParameter("@BidAmountMax", SqlDbType.Decimal),
                        new SqlParameter("@AutoBidScale", SqlDbType.Decimal),
                        new SqlParameter("@GuaranteeFee", SqlDbType.Decimal),
                        new SqlParameter("@LoanServiceCharges", SqlDbType.Decimal),
                        new SqlParameter("@BidServiceCharges", SqlDbType.Decimal),
                         new SqlParameter("@GuaranteeID", SqlDbType.Int),
                                         };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.BidStratTime;
            parameters[2].Value = model.BidEndTime;
            parameters[3].Value = model.BidAmountMin;
            parameters[4].Value = model.BidAmountMax;
            parameters[5].Value = model.AutoBidScale;
            parameters[6].Value = model.GuaranteeFee;
            parameters[7].Value = model.LoanServiceCharges;
            parameters[8].Value = model.BidServiceCharges;
            parameters[9].Value = model.GuaranteeID;


            try
            {
                int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
                return rows > 0;
            }
            catch
            {
                return false;
            }


        }

        public bool Review_student(int loandId, int memberId, decimal LoanAmount, int LoanTerm, string LoanDescribe)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@loandId", SqlDbType.Int,4),
                        new SqlParameter("@memberId", SqlDbType.Int,4),
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal),
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4),
                        new SqlParameter("@LoanDescribe", SqlDbType.Text)

             
            };
            parameters[0].Value = loandId;
            parameters[1].Value = memberId;
            parameters[2].Value = LoanAmount;
            parameters[3].Value = LoanTerm;
            parameters[4].Value = LoanDescribe;
            try
            {
                int rows = SqlHelper.ExecuteNonQueryVal(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_ReviewLoadn", parameters);
                return rows > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据(事物：带审核历史插入)
        /// </summary>
        public bool UpdateTran(LoanModel model, AuditHistoryModel auditModel)
        {
            var strSql = new StringBuilder();
            strSql.Append(" DECLARE @ErrorTotal int ");
            strSql.Append(" set @ErrorTotal = 0 ");
            strSql.Append("Begin tran ");

            strSql.Append("update Loan set ");

            if (model.ExamStatus == 5)
            {
                strSql.Append(" LoanNumber = dbo.GetLoanNumber(@BidStratTime) , ");
            }
            strSql.Append(" TradeType = @TradeType , ");
            strSql.Append(" BidAmountMin = @BidAmountMin , ");
            strSql.Append(" BidAmountMax = @BidAmountMax , ");
            strSql.Append(" BidStratTime = @BidStratTime , ");
            strSql.Append(" BidEndTime = @BidEndTime , ");
            strSql.Append(" Bond = @Bond , ");
            strSql.Append(" CityID = @CityID , ");
            strSql.Append(" ExamStatus = @ExamStatus , ");
            strSql.Append(" BiddingProcess = @BiddingProcess , ");
            strSql.Append(" MemberID = @MemberID , ");
            strSql.Append(" DimLoanUseID = @DimLoanUseID , ");
            strSql.Append(" BidCount = @BidCount , ");
            strSql.Append(" GuaranteeID = @GuaranteeID , ");
            strSql.Append(" ReviewTime = @ReviewTime , ");
            strSql.Append(" RepaymentLastTime = @RepaymentLastTime , ");
            strSql.Append(" ContractNo = @ContractNo , ");
            strSql.Append(" FullScaleTime = @FullScaleTime , ");
            strSql.Append(" UnderTime = @UnderTime , ");
            strSql.Append(" ContractGenerTime = @ContractGenerTime , ");
            strSql.Append(" NeedGuarantee = @NeedGuarantee , ");
            strSql.Append(" GuaranteeFee = @GuaranteeFee , ");
            strSql.Append(" NeedLoanCharges = @NeedLoanCharges , ");
            strSql.Append(" LoanServiceCharges = @LoanServiceCharges , ");
            strSql.Append(" NeedBidCharges = @NeedBidCharges , ");
            strSql.Append(" BidServiceCharges = @BidServiceCharges , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = getdate() , ");
            strSql.Append(" LoanDescribe = @LoanDescribe ,  ");
            //strSql.Append(" TimeStamp = @TimeStamp , ");
            strSql.Append(" LoanAmount = @LoanAmount , ");
            strSql.Append(" LoanRate = @LoanRate , ");
            strSql.Append(" ReleasedRate = @ReleasedRate , ");
            strSql.Append(" LoanTerm = @LoanTerm , ");
            strSql.Append(" RepaymentMethod = @RepaymentMethod , ");
            strSql.Append(" BorrowMode = @BorrowMode,  ");
            strSql.Append(" AutoBidScale = @AutoBidScale,  ");
            strSql.Append(" LoanTitle = @LoanTitle,  ");
            strSql.Append(" LoanTypeID = @LoanTypeID,  ");
            strSql.Append(" AuthStatus = @AuthStatus,  ");
            strSql.Append(" SumScore = @SumScore ,  ");
            strSql.Append(" ScoreLevel = @ScoreLevel,  ");
            strSql.Append(" Agency = @Agency,  ");
            strSql.Append(" LinkmanOne = @LinkmanOne,  ");
            strSql.Append(" TelOne = @TelOne,  ");
            strSql.Append(" LinkmanTwo = @LinkmanTwo,  ");
            strSql.Append(" TelTwo = @TelTwo  ");

            strSql.Append(" where ID=@ID and TimeStamp = @TimeStamp ");

            strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");

            strSql.Append(" insert into AuditHistory(");
            strSql.Append("Process,Result,UserID,AuditTime,Reason,LoanID,ReviewComments ");
            strSql.Append(") values (");
            strSql.Append("@Process,@Result,@UserID,getdate(),@Reason,@LoanID,@ReviewComments ");
            strSql.Append(") ");

            strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");


            //if (",2,4,6,8".IndexOf(',' + model.ExamStatus.ToString() + ',') > 0)
            //{
            //    strSql.Append(" exec Proc_AbandonLoanCommon @LoanID ");
            //    strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");
            //}
            #region #
            //if (model.ExamStatus == 5)
            //{
            //    strSql.Append("  DECLARE @SQL NVARCHAR(MAX),@MemberAllVar NVARCHAR(MAX)='',@MemberVar NVARCHAR(MAX),@HelisLoanAmount decimal(18, 2)=0,@amount_ decimal(18, 2)=0");
            //    strSql.Append(" SET @SQL=(SELECT AL.appointmentId FROM AppointmentLoan AL WHERE AL.loanID=@ID)");
            //    strSql.Append(" IF OBJECT_ID('tempdb.dbo.#Data') IS NOT NULL");
            //    strSql.Append(" DROP TABLE #Data");
            //    strSql.Append(" CREATE TABLE #Data");
            //    strSql.Append(" (");
            //    strSql.Append(" id INT");
            //    strSql.Append(" ,memberID INT");
            //    strSql.Append(" ,amount decimal(18, 2)");
            //    strSql.Append(" )");

            //    strSql.Append(" INSERT INTO #Data EXEC ('SELECT A.id,A.memberID,A.amount FROM AppointmentBiddingUser A,Member M WHERE A.memberID=M.ID AND A.id IN ('+@SQL+')')");

            //    strSql.Append(" select @MemberAllVar+=cast(id as varchar(20))+','+cast(memberID as varchar(20))+','+cast(amount as varchar(20))+'|' from #Data");
            //    strSql.Append(" while(charindex('|',@MemberAllVar) > 0)");
            //    strSql.Append(" BEGIN");
            //    strSql.Append(" set @MemberVar = left(@MemberAllVar,charindex('|',@MemberAllVar) - 1)");
            //    strSql.Append(" set @MemberAllVar = right(@MemberAllVar,len(@MemberAllVar) - charindex('|',@MemberAllVar))");

            //    strSql.Append(" DECLARE @id_ INT,@memberID_ INT,@balance_ decimal(18, 2),@PayeeBalance_ decimal(18, 2)");
            //    strSql.Append(" set @id_ = left(@MemberVar,charindex(',',@MemberVar) - 1)");
            //    strSql.Append(" set @MemberVar = right(@MemberVar,len(@MemberVar) - charindex(',',@MemberVar))");
            //    strSql.Append(" set @memberID_ = left(@MemberVar,charindex(',',@MemberVar) - 1)");
            //    strSql.Append(" set @MemberVar = right(@MemberVar,len(@MemberVar) - charindex(',',@MemberVar))");
            //    strSql.Append(" set @amount_ = cast(@MemberVar as decimal(18,2))");

            //    strSql.Append(" IF (SELECT M.Balance FROM Member M WHERE M.ID=@memberID_)>=@amount_");
            //    strSql.Append(" BEGIN");
            //    strSql.Append(" SELECT @PayeeBalance_=Balance FROM Member M WHERE M.ID=@MemberID");

            //    strSql.Append(" UPDATE Member SET @balance_=Balance-=@amount_,UpdateTime=GETDATE() WHERE ID=@memberID_");
            //    strSql.Append(" set @ErrorTotal += @@ERROR");

            //    strSql.Append(" INSERT INTO Bidding(BidType,MemberID,LoanID,BidAmount,BidStatus,CreateTime,UpdateTime) VALUES(1,@memberID_,@ID,@amount_,1,GETDATE(),GETDATE())");
            //    strSql.Append(" set @ErrorTotal += @@ERROR");

            //    strSql.Append(" INSERT INTO FundRecord(PayeeMemberID,PartyMemberID,FeeType,Amount,PayeeBalance,PartyBalance,Status,LoanID,RelationID,Description,CreateTime,UpdateTime)");
            //    strSql.Append(" VALUES(@MemberID,@memberID_,2,@amount_,@PayeeBalance_,@balance_,2,@ID,@@identity,'竞标',GETDATE(),GETDATE())");
            //    strSql.Append(" set @ErrorTotal += @@ERROR");

            //    strSql.Append(" SET @HelisLoanAmount+=@amount_");
            //    strSql.Append(" END");
            //    strSql.Append(" IF @HelisLoanAmount=@LoanAmount");
            //    strSql.Append(" BEGIN");
            //    strSql.Append(" UPDATE Loan SET ExamStatus=7,FullScaleTime=GETDATE(),BiddingProcess=100,BidCount = BidCount + 1,UpdateTime=GETDATE() WHERE ID=@ID");
            //    strSql.Append(" set @ErrorTotal += @@ERROR");
            //    strSql.Append(" END");
            //    strSql.Append(" ELSE");
            //    strSql.Append(" BEGIN");
            //    strSql.Append(" UPDATE Loan SET BiddingProcess=CASE WHEN CAST(((LoanAmount-@HelisLoanAmount+@amount_) - @amount_) AS DECIMAL(18,2))/CAST(LoanAmount AS DECIMAL(18,2))>0 AND CAST(((LoanAmount-@HelisLoanAmount+@amount_) - @amount_) AS DECIMAL(18,2))/CAST(LoanAmount AS DECIMAL(18,2))<0.0001 THEN 99.99 ELSE (1- ((LoanAmount-@HelisLoanAmount+@amount_) - @amount_)/LoanAmount) * 100 end,");
            //    strSql.Append(" BidCount = BidCount + 1,UpdateTime=GETDATE() WHERE ID=@ID");
            //    strSql.Append(" set @ErrorTotal += @@ERROR");
            //    strSql.Append(" END");
            //    strSql.Append(" UPDATE AppointmentBiddingUser SET status=2,operationId=@UserID,operationTime=GETDATE(),operationNote='已处理',operationRecord+='已处理-'+(SELECT F.UserName FROM FcmsUser F WHERE F.ID=@UserID)+'-'+CONVERT(CHAR(19),GETDATE(),120)+'-备注:已发标系统自动投资|' WHERE id=@id_");
            //    strSql.Append(" set @ErrorTotal += @@ERROR");
            //    strSql.Append(" END");
            //    strSql.Append(" DROP TABLE #Data");
            //}
            #endregion
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
                        new SqlParameter("@TimeStamp", SqlDbType.Int,4){Value = model.TimeStamp} , 
                        new SqlParameter("@TradeType", SqlDbType.Int,4){Value = model.TradeType} ,            
                        new SqlParameter("@BidAmountMin", SqlDbType.Decimal,9){Value = model.BidAmountMin} ,            
                        new SqlParameter("@BidAmountMax", SqlDbType.Decimal,9){Value = model.BidAmountMax} ,            
                        new SqlParameter("@BidStratTime", SqlDbType.DateTime){Value = model.BidStratTime} ,            
                        new SqlParameter("@BidEndTime", SqlDbType.DateTime){Value = model.BidEndTime} ,            
                        new SqlParameter("@Bond", SqlDbType.Decimal,9){Value = model.Bond} ,            
                        new SqlParameter("@CityID", SqlDbType.Int,4){Value = model.CityID} ,            
                        new SqlParameter("@ExamStatus", SqlDbType.Int,4){Value = model.ExamStatus} ,            
                        new SqlParameter("@BiddingProcess", SqlDbType.Decimal,9){Value = model.BiddingProcess} ,            
                        new SqlParameter("@DimLoanUseID", SqlDbType.Int,4){Value = model.DimLoanUseID} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value = model.MemberID} ,            
                        new SqlParameter("@BidCount", SqlDbType.Int,4){Value = model.BidCount} ,            
                        new SqlParameter("@GuaranteeID", SqlDbType.Int,4){Value = model.GuaranteeID} ,            
                        new SqlParameter("@ReviewTime", SqlDbType.DateTime){Value = model.ReviewTime} ,            
                        new SqlParameter("@RepaymentLastTime", SqlDbType.DateTime){Value = model.RepaymentLastTime} ,            
                        new SqlParameter("@ContractNo", SqlDbType.VarChar,20){Value = model.ContractNo} ,            
                        new SqlParameter("@FullScaleTime", SqlDbType.DateTime){Value = model.FullScaleTime} ,            
                        new SqlParameter("@UnderTime", SqlDbType.DateTime){Value = model.UnderTime} ,            
                        new SqlParameter("@ContractGenerTime", SqlDbType.DateTime){Value = model.ContractGenerTime} ,            
                        new SqlParameter("@NeedGuarantee", SqlDbType.Bit,1){Value = model.NeedGuarantee} ,            
                        new SqlParameter("@GuaranteeFee", SqlDbType.Decimal,9){Value = model.GuaranteeFee} ,            
                        new SqlParameter("@NeedLoanCharges", SqlDbType.Bit,1){Value = model.NeedLoanCharges} ,            
                        new SqlParameter("@LoanServiceCharges", SqlDbType.Decimal,9){Value = model.LoanServiceCharges} ,            
                        new SqlParameter("@NeedBidCharges", SqlDbType.Bit,1){Value = model.NeedBidCharges} ,            
                        new SqlParameter("@BidServiceCharges", SqlDbType.Decimal,9){Value = model.BidServiceCharges} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@LoanDescribe", SqlDbType.Text){Value = model.LoanDescribe} ,
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value = model.LoanAmount} ,            
                        new SqlParameter("@LoanRate", SqlDbType.Decimal,9){Value = model.LoanRate} ,            
                        new SqlParameter("@ReleasedRate", SqlDbType.Decimal,9){Value = model.ReleasedRate} ,            
                        new SqlParameter("@LoanTerm", SqlDbType.Int,4){Value = model.LoanTerm} ,            
                        new SqlParameter("@RepaymentMethod", SqlDbType.Int,4){Value = model.RepaymentMethod} ,            
                        new SqlParameter("@BorrowMode", SqlDbType.Int,4){Value = model.BorrowMode},
                        new SqlParameter("@AutoBidScale", SqlDbType.Decimal,9){Value = model.AutoBidScale},
                        new SqlParameter("@LoanTitle", SqlDbType.NVarChar,50){Value = model.LoanTitle},   
                        new SqlParameter("@LoanTypeID", SqlDbType.Int,4){Value = model.LoanTypeID} ,
                        //new SqlParameter("@DimLoanUseID", SqlDbType.Int,4){Value = model.DimLoanUseID} ,,
                        new SqlParameter("@AuthStatus", SqlDbType.VarChar,50){Value = model.AuthStatus} ,

                        new SqlParameter("@Agency", SqlDbType.VarChar,20){Value = model.Agency} ,  
                        new SqlParameter("@LinkmanOne", SqlDbType.VarChar,20){Value = model.LinkmanOne} ,  
                        new SqlParameter("@TelOne", SqlDbType.VarChar,20){Value = model.TelOne} ,  
                        new SqlParameter("@LinkmanTwo", SqlDbType.VarChar,20){Value = model.LinkmanTwo} ,  
                        new SqlParameter("@TelTwo", SqlDbType.VarChar,20){Value = model.TelTwo} ,  

                        new SqlParameter("@Process", SqlDbType.Int,4){Value = auditModel.Process} ,            
                        new SqlParameter("@Result", SqlDbType.Bit,1){Value = auditModel.Result} ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4){Value = auditModel.UserID} ,            
                        new SqlParameter("@Reason", SqlDbType.VarChar,50){Value = auditModel.Reason} ,            
                        new SqlParameter("@LoanID", SqlDbType.Int,4){Value = auditModel.LoanID},
                        new SqlParameter("@ReviewComments", SqlDbType.NVarChar,4000){Value = auditModel.ReviewComments},
                        new SqlParameter("@SumScore", SqlDbType.Int,4){Value = model.SumScore} ,
                        new SqlParameter("@ScoreLevel", SqlDbType.VarChar,10){Value = model.ScoreLevel} 
                        
                        
              
            };

            try
            {
                int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
                return rows > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 生成还款计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BuildPlan(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("dbo.ProcBuildPlan");
            SqlParameter[] parameters = {
                        new SqlParameter("@LoanID", SqlDbType.Int,4){Value = id}        
             
            };
            try
            {
                int rows = SqlHelper.ExecuteNonQueryVal(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, strSql.ToString(), parameters);
                return rows > 0;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 生成还款计划(事物：带审核历史记录插入)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BuildPlanTran(LoanModel model, AuditHistoryModel auditModel)
        {
            var strSql = new StringBuilder();
            strSql.Append(" DECLARE @ErrorTotal int ");
            strSql.Append(" DECLARE @ReturnValue int ");
            strSql.Append(" DECLARE @ReturnBuildPlan int ");
            strSql.Append(" set @ErrorTotal = 0 ");

            strSql.Append("Begin tran ");
            if (model.ExamStatus == 8)
            {
                strSql.Append(" exec @ReturnValue = Proc_AbandonLoanCommon @LoanID ");
                strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");
            }
            else
            {
                strSql.Append(" exec @ReturnBuildPlan = dbo.ProcBuildPlan @LoanID ");
                strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");
            }

            //strSql.Append(" if (@ReturnBuildPlan = 1) ");
            //strSql.Append(" Begin ");
            strSql.Append("insert into AuditHistory(");
            strSql.Append("Process,Result,UserID,AuditTime,Reason,LoanID,ReviewComments");
            strSql.Append(") values (");
            strSql.Append("@Process,@Result,@UserID,getdate(),@Reason,@LoanID,@ReviewComments");
            strSql.Append(") ");
            //strSql.Append("End ");

            strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");

            if (model.LoanTypeID == 5) //秒标
            {
                strSql.Append(" exec dbo.ProcRepayment @LoanID,1,0,'' ");
                strSql.Append(" set @ErrorTotal = @ErrorTotal + @@ERROR ");
            }

            strSql.Append(" if @@ERROR>0 ");
            strSql.Append(" begin ");
            strSql.Append(" rollback TransAction ");
            strSql.Append(" end ");
            strSql.Append(" Else ");
            strSql.Append(" Begin ");
            strSql.Append(" Commit TransAction ");
            strSql.Append(" End ");

            SqlParameter[] parameters = {
                        new SqlParameter("@LoanID", SqlDbType.Int,4){Value = model.ID},
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value = model.MemberID} , 
                        new SqlParameter("@Bond", SqlDbType.Decimal,9){Value = model.Bond} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value = model.LoanAmount} ,            

                        new SqlParameter("@Process", SqlDbType.Int,4){Value = auditModel.Process} ,            
                        new SqlParameter("@Result", SqlDbType.Bit,1){Value = auditModel.Result} ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4){Value = auditModel.UserID} ,            
                        new SqlParameter("@Reason", SqlDbType.VarChar,50){Value = auditModel.Reason} ,            
                        new SqlParameter("@ReviewComments", SqlDbType.NVarChar,4000){Value = auditModel.ReviewComments} 
             
            };
            try
            {
                //int rows = SqlHelper.ExecuteNonQueryVal(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, strSql.ToString(), parameters);
                int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
                return rows > 0;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 得到一个借贷对象实体
        /// </summary>
        public LoanModel GetLoanModel(int id)
        {

            var strSql = new StringBuilder();
            strSql.Append("select top 1 Loan.ID,RepaymentMethodName, LoanTitle,LoanTypeID,TradeType, BidAmountMin, BidAmountMax, BidStratTime, BidEndTime, Bond, CityID, ExamStatus, BiddingProcess, MemberID, DimLoanUseID, BidCount, GuaranteeID, ReviewTime, RepaymentLastTime, ContractNo, FullScaleTime, UnderTime, ContractGenerTime, LoanNumber, NeedGuarantee, GuaranteeFee, NeedLoanCharges, LoanServiceCharges, NeedBidCharges, BidServiceCharges, CreateTime, UpdateTime, LoanAmount, LoanRate, ReleasedRate, LoanTerm, RepaymentMethod, BorrowMode,LoanDescribe, CAST(dbo.Loan.TimeStamp AS bigint) AS TimeStamp,AutoBidScale,SumScore,ScoreLevel  ");
            strSql.Append(" from Loan left join [DimRepaymentMethod] d on d.ID=loan.RepaymentMethod ");
            strSql.Append(" where Loan.ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value=id}
			};

            var model = new LoanModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值

                model.ID = ds.Tables[0].Rows[0]["ID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                model.RepaymentMethodName = ds.Tables[0].Rows[0]["RepaymentMethodName"] != DBNull.Value ? ds.Tables[0].Rows[0]["RepaymentMethodName"].ToString() : "";
                model.LoanTitle = ds.Tables[0].Rows[0]["LoanTitle"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanTitle"].ToString() : "";
                model.LoanTypeID = ds.Tables[0].Rows[0]["LoanTypeID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LoanTypeID"]) : 0;
                model.TradeType = ds.Tables[0].Rows[0]["TradeType"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TradeType"]) : 0;
                model.BidAmountMin = ds.Tables[0].Rows[0]["BidAmountMin"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BidAmountMin"]) : 0;
                model.BidAmountMax = ds.Tables[0].Rows[0]["BidAmountMax"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BidAmountMax"]) : 0;
                model.BidStratTime = ds.Tables[0].Rows[0]["BidStratTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["BidStratTime"]) : PublicConst.MinDate;
                model.BidEndTime = ds.Tables[0].Rows[0]["BidEndTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["BidEndTime"]) : PublicConst.MinDate;
                model.Bond = ds.Tables[0].Rows[0]["Bond"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["Bond"]) : 0;
                model.CityID = ds.Tables[0].Rows[0]["CityID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["CityID"]) : 0;
                model.ExamStatus = ds.Tables[0].Rows[0]["ExamStatus"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ExamStatus"]) : 0;
                model.BiddingProcess = ds.Tables[0].Rows[0]["BiddingProcess"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BiddingProcess"]) : 0;
                model.MemberID = ds.Tables[0].Rows[0]["MemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["MemberID"]) : 0;
                model.DimLoanUseID = ds.Tables[0].Rows[0]["DimLoanUseID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["DimLoanUseID"]) : 0;
                model.BidCount = ds.Tables[0].Rows[0]["BidCount"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["BidCount"]) : 0;
                model.GuaranteeID = ds.Tables[0].Rows[0]["GuaranteeID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["GuaranteeID"]) : 0;
                model.ReviewTime = ds.Tables[0].Rows[0]["ReviewTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["ReviewTime"]) : PublicConst.MinDate;
                model.RepaymentLastTime = ds.Tables[0].Rows[0]["RepaymentLastTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["RepaymentLastTime"]) : PublicConst.MinDate;
                model.ContractNo = ds.Tables[0].Rows[0]["ContractNo"] != DBNull.Value ? ds.Tables[0].Rows[0]["ContractNo"].ToString() : "";
                model.FullScaleTime = ds.Tables[0].Rows[0]["FullScaleTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["FullScaleTime"]) : PublicConst.MinDate;
                model.UnderTime = ds.Tables[0].Rows[0]["UnderTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UnderTime"]) : PublicConst.MinDate;
                model.ContractGenerTime = ds.Tables[0].Rows[0]["ContractGenerTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["ContractGenerTime"]) : PublicConst.MinDate;
                model.LoanNumber = ds.Tables[0].Rows[0]["LoanNumber"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanNumber"].ToString() : "";
                model.NeedGuarantee = ds.Tables[0].Rows[0]["NeedGuarantee"] != DBNull.Value ? Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedGuarantee"]) : false;
                model.GuaranteeFee = ds.Tables[0].Rows[0]["GuaranteeFee"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["GuaranteeFee"]) : 0;
                model.NeedLoanCharges = ds.Tables[0].Rows[0]["NeedLoanCharges"] != DBNull.Value ? Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedLoanCharges"]) : false;
                model.LoanServiceCharges = ds.Tables[0].Rows[0]["LoanServiceCharges"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["LoanServiceCharges"]) : 0;
                model.NeedBidCharges = ds.Tables[0].Rows[0]["NeedBidCharges"] != DBNull.Value ? Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedBidCharges"]) : false;
                model.BidServiceCharges = ds.Tables[0].Rows[0]["BidServiceCharges"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BidServiceCharges"]) : 0;
                model.CreateTime = ds.Tables[0].Rows[0]["CreateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateTime"]) : PublicConst.MinDate;
                model.UpdateTime = ds.Tables[0].Rows[0]["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdateTime"]) : PublicConst.MinDate;
                model.LoanAmount = ds.Tables[0].Rows[0]["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["LoanAmount"]) : 0;
                model.LoanRate = ds.Tables[0].Rows[0]["LoanRate"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["LoanRate"]) : 0;
                model.ReleasedRate = ds.Tables[0].Rows[0]["ReleasedRate"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["ReleasedRate"]) : 0;
                model.LoanTerm = ds.Tables[0].Rows[0]["LoanTerm"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LoanTerm"]) : 0;
                model.RepaymentMethod = ds.Tables[0].Rows[0]["RepaymentMethod"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["RepaymentMethod"]) : 0;
                model.BorrowMode = ds.Tables[0].Rows[0]["BorrowMode"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["BorrowMode"]) : 0;
                model.LoanDescribe = ds.Tables[0].Rows[0]["LoanDescribe"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanDescribe"].ToString() : "";
                model.TimeStamp = Convert.ToInt64(ds.Tables[0].Rows[0]["TimeStamp"]);
                model.AutoBidScale = ds.Tables[0].Rows[0]["AutoBidScale"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["AutoBidScale"]) : 0;
                model.SumScore = ds.Tables[0].Rows[0]["SumScore"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["SumScore"]) : 0;
                model.ScoreLevel = ds.Tables[0].Rows[0]["ScoreLevel"] != DBNull.Value ? ds.Tables[0].Rows[0]["ScoreLevel"].ToString() : "";
                #endregion
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取借贷列表分页
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public List<LoanModel> GetPageLoanModel(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            var list = new List<LoanModel>();
            string sql1 = "select count(*) as totals from Loan";
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
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + OrderBy + ")) AS rownum, * from Loan";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " where " + Where;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((PageIndex - 1) * PageSize + 1) + " and " + PageIndex * PageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                LoanModel info = getLoanModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private LoanModel getLoanModelByDr(SqlDataReader dr)
        {
            var model = new LoanModel();
            model.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            model.TradeType = dr["TradeType"] != DBNull.Value ? Convert.ToInt32(dr["TradeType"]) : 0;
            model.BidAmountMin = dr["BidAmountMin"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmountMin"]) : 0;
            model.BidAmountMax = dr["BidAmountMax"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmountMax"]) : 0;
            model.BidStratTime = dr["BidStratTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidStratTime"]) : PublicConst.MinDate;
            model.BidEndTime = dr["BidEndTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidEndTime"]) : PublicConst.MinDate;
            model.Bond = dr["Bond"] != DBNull.Value ? Convert.ToDecimal(dr["Bond"]) : 0;
            model.CityID = dr["CityID"] != DBNull.Value ? Convert.ToInt32(dr["CityID"]) : 0;
            model.ExamStatus = dr["ExamStatus"] != DBNull.Value ? Convert.ToInt32(dr["ExamStatus"]) : 0;
            model.BiddingProcess = dr["BiddingProcess"] != DBNull.Value ? Convert.ToDecimal(dr["BiddingProcess"]) : 0;
            model.MemberID = dr["MemberID"] != DBNull.Value ? Convert.ToInt32(dr["MemberID"]) : 0;
            model.DimLoanUseID = dr["DimLoanUseID"] != DBNull.Value ? Convert.ToInt32(dr["DimLoanUseID"]) : 0;
            model.BidCount = dr["BidCount"] != DBNull.Value ? Convert.ToInt32(dr["BidCount"]) : 0;
            model.GuaranteeID = dr["GuaranteeID"] != DBNull.Value ? Convert.ToInt32(dr["GuaranteeID"]) : 0;
            model.ReviewTime = dr["ReviewTime"] != DBNull.Value ? Convert.ToDateTime(dr["ReviewTime"]) : PublicConst.MinDate;
            model.RepaymentLastTime = dr["RepaymentLastTime"] != DBNull.Value ? Convert.ToDateTime(dr["RepaymentLastTime"]) : PublicConst.MinDate;
            model.ContractNo = dr["ContractNo"] != DBNull.Value ? dr["ContractNo"].ToString() : "";
            model.FullScaleTime = dr["FullScaleTime"] != DBNull.Value ? Convert.ToDateTime(dr["FullScaleTime"]) : PublicConst.MinDate;
            model.UnderTime = dr["UnderTime"] != DBNull.Value ? Convert.ToDateTime(dr["UnderTime"]) : PublicConst.MinDate;
            model.ContractGenerTime = dr["ContractGenerTime"] != DBNull.Value ? Convert.ToDateTime(dr["ContractGenerTime"]) : PublicConst.MinDate;
            model.LoanNumber = dr["LoanNumber"] != DBNull.Value ? dr["LoanNumber"].ToString() : "";
            model.NeedGuarantee = dr["NeedGuarantee"] != DBNull.Value ? Convert.ToBoolean(dr["NeedGuarantee"]) : false;
            model.GuaranteeFee = dr["GuaranteeFee"] != DBNull.Value ? Convert.ToDecimal(dr["GuaranteeFee"]) : 0;
            model.NeedLoanCharges = dr["NeedLoanCharges"] != DBNull.Value ? Convert.ToBoolean(dr["NeedLoanCharges"]) : false;
            model.LoanServiceCharges = dr["LoanServiceCharges"] != DBNull.Value ? Convert.ToDecimal(dr["LoanServiceCharges"]) : 0;
            model.NeedBidCharges = dr["NeedBidCharges"] != DBNull.Value ? Convert.ToBoolean(dr["NeedBidCharges"]) : false;
            model.BidServiceCharges = dr["BidServiceCharges"] != DBNull.Value ? Convert.ToDecimal(dr["BidServiceCharges"]) : 0;
            model.CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate;
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate;
            //model.TimeStamp = dr["TimeStamp"] != DBNull.Value ? Convert.ToDateTime(dr["TimeStamp"]) : PublicConst.MinDate;
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0;
            model.LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0;
            model.ReleasedRate = dr["ReleasedRate"] != DBNull.Value ? Convert.ToDecimal(dr["ReleasedRate"]) : 0;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.RepaymentMethod = dr["RepaymentMethod"] != DBNull.Value ? Convert.ToInt32(dr["RepaymentMethod"]) : 0;
            model.BorrowMode = dr["BorrowMode"] != DBNull.Value ? Convert.ToInt32(dr["BorrowMode"]) : 0;
            model.LoanDescribe = dr["LoanDescribe"] != DBNull.Value ? dr["LoanDescribe"].ToString() : "";
            model.TimeStamp = Convert.ToInt64(dr["TimeStamp"]);
            return model;
        }

        public List<LoanModel> GetPageLoanManageModel(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var list = new List<LoanModel>();
            string sql1 = "select count(*) as totals from vw_LoanManage where (LoanTypeID<>23 OR (LoanTypeID=23 AND ExamStatus>5))";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sql1 = sql1 + " and " + whereStr;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum,* from vw_LoanManage where (LoanTypeID<>23 OR (LoanTypeID=23 AND ExamStatus>5))";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sql2 = sql2 + " and " + whereStr;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((currentPage - 1) * pageSize + 1) + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                LoanModel info = getLoanManageModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private LoanModel getLoanManageModelByDr(SqlDataReader dr)
        {
            var model = new LoanModel();
            model.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            model.MemberName = dr["MemberName"] != DBNull.Value ? dr["MemberName"].ToString() : "";
            model.RealName = dr["RealName"] != DBNull.Value ? dr["RealName"].ToString() : "";
            model.LoanNumber = dr["LoanNumber"] != DBNull.Value ? dr["LoanNumber"].ToString() : "";
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.ReviewTime = dr["ReviewTime"] != DBNull.Value ? Convert.ToDateTime(dr["ReviewTime"]) : PublicConst.MinDate;
            model.RepaymentLastTime = dr["RepaymentLastTime"] != DBNull.Value ? Convert.ToDateTime(dr["RepaymentLastTime"]) : PublicConst.MinDate;
            model.EndRePayTime = dr["EndRePayTime"] != DBNull.Value ? Convert.ToDateTime(dr["EndRePayTime"]) : PublicConst.MinDate;
            model.LoanUseName = dr["LoanUseName"] != DBNull.Value ? dr["LoanUseName"].ToString() : "";
            model.FullScaleTime = dr["FullScaleTime"] != DBNull.Value ? Convert.ToDateTime(dr["FullScaleTime"]) : PublicConst.MinDate;
            model.BorrowMode = dr["BorrowMode"] != DBNull.Value ? Convert.ToInt32(dr["BorrowMode"]) : 0;
            model.BidStratTime = dr["BidStratTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidStratTime"]) : PublicConst.MinDate;
            model.BidEndTime = dr["BidEndTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidEndTime"]) : PublicConst.MinDate;
            model.BidCount = dr["BidCount"] != DBNull.Value ? Convert.ToInt32(dr["BidCount"]) : 0;
            model.ContractGenerTime = dr["ContractGenerTime"] != DBNull.Value ? Convert.ToDateTime(dr["ContractGenerTime"]) : PublicConst.MinDate;
            model.CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate;
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate;
            model.UnderTime = dr["UnderTime"] != DBNull.Value ? Convert.ToDateTime(dr["UnderTime"]) : PublicConst.MinDate;
            model.BiddingProcess = dr["BiddingProcess"] != DBNull.Value ? Convert.ToDecimal(dr["BiddingProcess"]) : 0;
            model.LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0;
            model.MemberID = dr["MemberID"] != DBNull.Value ? Convert.ToInt32(dr["MemberID"]) : 0;
            model.RegTime = PublicConst.MinDate;
            model.SoonStatus = dr["SoonStatus"] != DBNull.Value ? Convert.ToInt32(dr["SoonStatus"]) : 0;
            model.ExamStatus = dr["ExamStatus"] != DBNull.Value ? Convert.ToInt32(dr["ExamStatus"]) : 0;
            model.ExamStatusName = dr["ExamStatusName"] != DBNull.Value ? dr["ExamStatusName"].ToString() : "";
            model.GuaranteeID = dr["GuaranteeID"] != DBNull.Value ? Convert.ToInt32(dr["GuaranteeID"]) : 0;
            model.GuaranteeName = dr["GuaranteeName"] != DBNull.Value ? dr["GuaranteeName"].ToString() : "";
            model.RepaymentMethodName = dr["RepaymentMethodName"] != DBNull.Value ? dr["RepaymentMethodName"].ToString() : "";
            model.LoanTermInfo = (dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0).ToString() + (model.BorrowMode == 0 ? " 天" : " 个月");
            model.TimeStamp = Convert.ToInt64(dr["TimeStamp"]);
            model.SwitchAutoRepayment = Convert.ToBoolean(dr["SwitchAutoRepayment"]);
            model.SwitchBuildOverdueFee = Convert.ToBoolean(dr["SwitchBuildOverdueFee"]);
            model.SwitchAutoPass = Convert.ToBoolean(dr["SwitchAutoPass"]);
            model.Mobile = dr["Mobile"] != DBNull.Value ? dr["Mobile"].ToString() : "";

            model.SumScore = dr["SumScore"] != DBNull.Value ? Convert.ToInt32(dr["SumScore"]) : 0;
            model.ScoreLevel = dr["ScoreLevel"] != DBNull.Value ? dr["ScoreLevel"].ToString() : "";
            return model;
        }

        public List<LoanModel> GetLoanInfoListModel(string Where)
        {
            var list = new List<LoanModel>();
            string sql1 = "select *,dbo.GetMemberNameByID(Loan.MemberID) as MemberName,dbo.GetProvinceID(Loan.CityID) as ProvinceID from Loan";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " where " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            while (reader.Read())
            {
                LoanModel info = getLoanInfoModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        public LoanModel GetLoanInfoModel(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select top 1 *  ");
            strSql.Append(" from vw_LoanInfo ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            var model = new LoanModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.ID = ds.Tables[0].Rows[0]["ID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                model.TradeType = ds.Tables[0].Rows[0]["TradeType"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["TradeType"]) : 0;
                model.BidAmountMin = ds.Tables[0].Rows[0]["BidAmountMin"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BidAmountMin"]) : 0;
                model.BidAmountMax = ds.Tables[0].Rows[0]["BidAmountMax"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BidAmountMax"]) : 0;
                model.BidStratTime = ds.Tables[0].Rows[0]["BidStratTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["BidStratTime"]) : PublicConst.MinDate;
                model.BidEndTime = ds.Tables[0].Rows[0]["BidEndTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["BidEndTime"]) : PublicConst.MinDate;
                model.Bond = ds.Tables[0].Rows[0]["Bond"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["Bond"]) : 0;
                model.CityID = ds.Tables[0].Rows[0]["CityID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["CityID"]) : 0;
                model.ExamStatus = ds.Tables[0].Rows[0]["ExamStatus"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ExamStatus"]) : 0;
                model.BiddingProcess = ds.Tables[0].Rows[0]["BiddingProcess"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BiddingProcess"]) : 0;
                model.MemberID = ds.Tables[0].Rows[0]["MemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["MemberID"]) : 0;
                model.DimLoanUseID = ds.Tables[0].Rows[0]["DimLoanUseID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["DimLoanUseID"]) : 0;
                model.BidCount = ds.Tables[0].Rows[0]["BidCount"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["BidCount"]) : 0;
                model.GuaranteeID = ds.Tables[0].Rows[0]["GuaranteeID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["GuaranteeID"]) : 0;
                model.ReviewTime = ds.Tables[0].Rows[0]["ReviewTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["ReviewTime"]) : PublicConst.MinDate;
                model.RepaymentLastTime = ds.Tables[0].Rows[0]["RepaymentLastTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["RepaymentLastTime"]) : PublicConst.MinDate;
                model.ContractNo = ds.Tables[0].Rows[0]["ContractNo"] != DBNull.Value ? ds.Tables[0].Rows[0]["ContractNo"].ToString() : "";
                model.FullScaleTime = ds.Tables[0].Rows[0]["FullScaleTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["FullScaleTime"]) : PublicConst.MinDate;
                model.UnderTime = ds.Tables[0].Rows[0]["UnderTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UnderTime"]) : PublicConst.MinDate;
                model.ContractGenerTime = ds.Tables[0].Rows[0]["ContractGenerTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["ContractGenerTime"]) : PublicConst.MinDate;
                model.LoanNumber = ds.Tables[0].Rows[0]["LoanNumber"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanNumber"].ToString() : "";
                model.NeedGuarantee = ds.Tables[0].Rows[0]["NeedGuarantee"] != DBNull.Value && Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedGuarantee"]);
                model.GuaranteeFee = ds.Tables[0].Rows[0]["GuaranteeFee"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["GuaranteeFee"]) : 0;
                model.NeedLoanCharges = ds.Tables[0].Rows[0]["NeedLoanCharges"] != DBNull.Value && Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedLoanCharges"]);
                model.LoanServiceCharges = ds.Tables[0].Rows[0]["LoanServiceCharges"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["LoanServiceCharges"]) : 0;
                model.NeedBidCharges = ds.Tables[0].Rows[0]["NeedBidCharges"] != DBNull.Value && Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedBidCharges"]);
                model.BidServiceCharges = ds.Tables[0].Rows[0]["BidServiceCharges"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["BidServiceCharges"]) : 0;
                model.CreateTime = ds.Tables[0].Rows[0]["CreateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateTime"]) : PublicConst.MinDate;
                model.UpdateTime = ds.Tables[0].Rows[0]["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdateTime"]) : PublicConst.MinDate;
                model.LoanAmount = ds.Tables[0].Rows[0]["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["LoanAmount"]) : 0;
                model.LoanRate = ds.Tables[0].Rows[0]["LoanRate"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["LoanRate"]) : 0;
                model.ReleasedRate = ds.Tables[0].Rows[0]["ReleasedRate"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["ReleasedRate"]) : 0;
                model.LoanTerm = ds.Tables[0].Rows[0]["LoanTerm"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LoanTerm"]) : 0;
                model.RepaymentMethod = ds.Tables[0].Rows[0]["RepaymentMethod"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["RepaymentMethod"]) : 0;
                model.BorrowMode = ds.Tables[0].Rows[0]["BorrowMode"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["BorrowMode"]) : 0;
                model.ProvinceID = ds.Tables[0].Rows[0]["ProvinceID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ProvinceID"]) : 0;

                model.MemberName = ds.Tables[0].Rows[0]["MemberName"] != DBNull.Value ? ds.Tables[0].Rows[0]["MemberName"].ToString() : "";
                model.City = ds.Tables[0].Rows[0]["City"] != DBNull.Value ? ds.Tables[0].Rows[0]["City"].ToString() : "";
                model.Province = ds.Tables[0].Rows[0]["Province"] != DBNull.Value ? ds.Tables[0].Rows[0]["Province"].ToString() : "";
                model.RegTime = ds.Tables[0].Rows[0]["RegTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["RegTime"]) : PublicConst.MinDate;
                model.LoanUseName = ds.Tables[0].Rows[0]["LoanUseName"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanUseName"].ToString() : "";

                model.BorrowModeName = ds.Tables[0].Rows[0]["BorrowModeName"] != DBNull.Value ? ds.Tables[0].Rows[0]["BorrowModeName"].ToString() : "";
                model.ExamStatusName = ds.Tables[0].Rows[0]["ExamStatusName"] != DBNull.Value ? ds.Tables[0].Rows[0]["ExamStatusName"].ToString() : "";
                model.TradeTypeName = ds.Tables[0].Rows[0]["TradeTypeName"] != DBNull.Value ? ds.Tables[0].Rows[0]["TradeTypeName"].ToString() : "";
                model.RepaymentMethodName = ds.Tables[0].Rows[0]["RepaymentMethodName"] != DBNull.Value ? ds.Tables[0].Rows[0]["RepaymentMethodName"].ToString() : "";
                model.GuaranteeName = ds.Tables[0].Rows[0]["GuaranteeName"] != DBNull.Value ? ds.Tables[0].Rows[0]["GuaranteeName"].ToString() : "";
                model.LoanDescribe = ds.Tables[0].Rows[0]["LoanDescribe"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanDescribe"].ToString() : "";
                model.TimeStamp = Convert.ToInt64(ds.Tables[0].Rows[0]["TimeStamp"]);

                model.MinInvestment = ds.Tables[0].Rows[0]["MinInvestment"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["MinInvestment"]) : 0;
                model.MaxInvestment = ds.Tables[0].Rows[0]["MaxInvestment"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["MaxInvestment"]) : 0;
                model.AutoBidScale = ds.Tables[0].Rows[0]["AutoBidScale"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["AutoBidScale"]) : 0;

                model.LoanTitle = ds.Tables[0].Rows[0]["LoanTitle"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanTitle"].ToString() : "";
                model.LoanTypeID = ds.Tables[0].Rows[0]["LoanTypeID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LoanTypeID"]) : 0;
                model.LoanScaleType = ds.Tables[0].Rows[0]["LoanScaleType"] != DBNull.Value ? ds.Tables[0].Rows[0]["LoanScaleType"].ToString() : "";
                model.AuthStatus = ds.Tables[0].Rows[0]["AuthStatus"] != DBNull.Value ? ds.Tables[0].Rows[0]["AuthStatus"].ToString() : "";

                model.SumScore = ds.Tables[0].Rows[0]["SumScore"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["SumScore"]) : 0;
                model.ScoreLevel = ds.Tables[0].Rows[0]["ScoreLevel"] != DBNull.Value ? ds.Tables[0].Rows[0]["ScoreLevel"].ToString() : "";

                model.Agency = ds.Tables[0].Rows[0]["Agency"] != DBNull.Value ? ds.Tables[0].Rows[0]["Agency"].ToString() : "";
                model.LinkmanOne = ds.Tables[0].Rows[0]["LinkmanOne"] != DBNull.Value ? ds.Tables[0].Rows[0]["LinkmanOne"].ToString() : "";
                model.LinkmanTwo = ds.Tables[0].Rows[0]["LinkmanTwo"] != DBNull.Value ? ds.Tables[0].Rows[0]["LinkmanTwo"].ToString() : "";
                model.TelOne = ds.Tables[0].Rows[0]["TelOne"] != DBNull.Value ? ds.Tables[0].Rows[0]["TelOne"].ToString() : "";
                model.TelTwo = ds.Tables[0].Rows[0]["TelTwo"] != DBNull.Value ? ds.Tables[0].Rows[0]["TelTwo"].ToString() : "";

                model.MonthlyProfit = ds.Tables[0].Rows[0]["Income"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["Income"]) : "";
                model.LiabilitiesAmount = ds.Tables[0].Rows[0]["LiabilitiesTotalAmount"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["LiabilitiesTotalAmount"]) : "";
                model.LiabilitiesRatio = ds.Tables[0].Rows[0]["LiabilitiesDebt"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["LiabilitiesDebt"]) : "";
                model.MonthlyControlIncome = ds.Tables[0].Rows[0]["CanUsedIncome"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["CanUsedIncome"]) : "";
                model.QualityProfessional = ds.Tables[0].Rows[0]["HighDuties"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["HighDuties"]) : 0;
                model.HouseProperty = ds.Tables[0].Rows[0]["HaveHouse"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["HaveHouse"]) : 0;
                model.LitigationSeach = ds.Tables[0].Rows[0]["LitigationQuery"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["LitigationQuery"]) : "";
                model.SocialSecuritySeach = ds.Tables[0].Rows[0]["SocialQuery"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["SocialQuery"]) : "";
                model.CreditRecordSeach = ds.Tables[0].Rows[0]["CreditRecordQuery"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["CreditRecordQuery"]) : "";
                model.ContactSituation = ds.Tables[0].Rows[0]["EmergencyExplain"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["EmergencyExplain"]) : "";
                model.OtherSituation = ds.Tables[0].Rows[0]["OtherSituation"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["OtherSituation"]) : "";

                #endregion
                return model;
            }
            else
            {
                return null;
            }
        }

        private LoanModel getLoanInfoModelByDr(SqlDataReader dr)
        {
            var model = new LoanModel();
            model.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            model.MemberName = dr["MemberName"] != DBNull.Value ? dr["MemberName"].ToString() : "";
            model.TradeType = dr["TradeType"] != DBNull.Value ? Convert.ToInt32(dr["TradeType"]) : 0;
            model.BidAmountMin = dr["BidAmountMin"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmountMin"]) : 0;
            model.BidAmountMax = dr["BidAmountMax"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmountMax"]) : 0;
            model.BidStratTime = dr["BidStratTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidStratTime"]) : PublicConst.MinDate;
            model.BidEndTime = dr["BidEndTime"] != DBNull.Value ? Convert.ToDateTime(dr["BidEndTime"]) : PublicConst.MinDate;
            model.Bond = dr["Bond"] != DBNull.Value ? Convert.ToDecimal(dr["Bond"]) : 0;
            model.CityID = dr["CityID"] != DBNull.Value ? Convert.ToInt32(dr["CityID"]) : 0;
            model.ExamStatus = dr["ExamStatus"] != DBNull.Value ? Convert.ToInt32(dr["ExamStatus"]) : 0;
            model.BiddingProcess = dr["BiddingProcess"] != DBNull.Value ? Convert.ToDecimal(dr["BiddingProcess"]) : 0;
            model.MemberID = dr["MemberID"] != DBNull.Value ? Convert.ToInt32(dr["MemberID"]) : 0;
            model.DimLoanUseID = dr["DimLoanUseID"] != DBNull.Value ? Convert.ToInt32(dr["DimLoanUseID"]) : 0;
            model.BidCount = dr["BidCount"] != DBNull.Value ? Convert.ToInt32(dr["BidCount"]) : 0;
            model.GuaranteeID = dr["GuaranteeID"] != DBNull.Value ? Convert.ToInt32(dr["GuaranteeID"]) : 0;
            model.ReviewTime = dr["ReviewTime"] != DBNull.Value ? Convert.ToDateTime(dr["ReviewTime"]) : PublicConst.MinDate;
            model.RepaymentLastTime = dr["RepaymentLastTime"] != DBNull.Value ? Convert.ToDateTime(dr["RepaymentLastTime"]) : PublicConst.MinDate;
            model.ContractNo = dr["ContractNo"] != DBNull.Value ? dr["ContractNo"].ToString() : "";
            model.FullScaleTime = dr["FullScaleTime"] != DBNull.Value ? Convert.ToDateTime(dr["FullScaleTime"]) : PublicConst.MinDate;
            model.UnderTime = dr["UnderTime"] != DBNull.Value ? Convert.ToDateTime(dr["UnderTime"]) : PublicConst.MinDate;
            model.ContractGenerTime = dr["ContractGenerTime"] != DBNull.Value ? Convert.ToDateTime(dr["ContractGenerTime"]) : PublicConst.MinDate;
            model.LoanNumber = dr["LoanNumber"] != DBNull.Value ? dr["LoanNumber"].ToString() : "";
            model.NeedGuarantee = dr["NeedGuarantee"] != DBNull.Value ? Convert.ToBoolean(dr["NeedGuarantee"]) : false;
            model.GuaranteeFee = dr["GuaranteeFee"] != DBNull.Value ? Convert.ToDecimal(dr["GuaranteeFee"]) : 0;
            model.NeedLoanCharges = dr["NeedLoanCharges"] != DBNull.Value ? Convert.ToBoolean(dr["NeedLoanCharges"]) : false;
            model.LoanServiceCharges = dr["LoanServiceCharges"] != DBNull.Value ? Convert.ToDecimal(dr["LoanServiceCharges"]) : 0;
            model.NeedBidCharges = dr["NeedBidCharges"] != DBNull.Value ? Convert.ToBoolean(dr["NeedBidCharges"]) : false;
            model.BidServiceCharges = dr["BidServiceCharges"] != DBNull.Value ? Convert.ToDecimal(dr["BidServiceCharges"]) : 0;
            model.CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate;
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate;
            model.LoanAmount = dr["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dr["LoanAmount"]) : 0;
            model.LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0;
            model.ReleasedRate = dr["ReleasedRate"] != DBNull.Value ? Convert.ToDecimal(dr["ReleasedRate"]) : 0;
            model.LoanTerm = dr["LoanTerm"] != DBNull.Value ? Convert.ToInt32(dr["LoanTerm"]) : 0;
            model.RepaymentMethod = dr["RepaymentMethod"] != DBNull.Value ? Convert.ToInt32(dr["RepaymentMethod"]) : 0;
            model.BorrowMode = dr["BorrowMode"] != DBNull.Value ? Convert.ToInt32(dr["BorrowMode"]) : 0;
            model.ProvinceID = dr["ProvinceID"] != DBNull.Value ? Convert.ToInt32(dr["ProvinceID"]) : 0;
            model.LoanDescribe = dr["LoanDescribe"] != DBNull.Value ? dr["LoanDescribe"].ToString() : "";
            model.TimeStamp = Convert.ToInt64(dr["TimeStamp"]);

            model.SumScore = dr["SumScore"] != DBNull.Value ? Convert.ToInt32(dr["SumScore"]) : 0;
            model.ScoreLevel = dr["ScoreLevel"] != DBNull.Value ? dr["ScoreLevel"].ToString() : "";
            return model;
        }

        /// <summary>
        /// 获取会员当前未还贷款金额
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public decimal GetMemberSurAmount(int memberID)
        {
            var strSql = new StringBuilder();
            strSql.Append("select isnull(sum(SurPrincipal+SurReInterest+SurOverInterest),0) as SurAmount from RepaymentPlan where status<2 and LoanMemberID = " + memberID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToDecimal(ds.Tables[0].Rows[0]["SurAmount"]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取会员审批中的申请金额
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public decimal GetMemberAuditAmount(int memberID)
        {
            var strSql = new StringBuilder();
            strSql.Append("select isnull(sum(LoanAmount),0) as AuditAmount from Loan where ExamStatus in (1,3,5,7,9,13,15) and MemberID = " + memberID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToDecimal(ds.Tables[0].Rows[0]["AuditAmount"]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 平台项目汇总
        /// </summary>
        public DataTable SysProjectSummary(int currentPage, int pageSize, string filter, out int total)
        {
            var dataTable = new DataTable();
            var conn = new SqlConnection(SqlHelper.ConnectionStringLocal);
            var pCurrentPage = new SqlParameter("@CurrentPage", SqlDbType.Int);
            var pPageSize = new SqlParameter("@PageSize", SqlDbType.Int);
            var pFilter = new SqlParameter("@Filter", SqlDbType.NVarChar, 200);
            var pTotal = new SqlParameter("@RecordCount", SqlDbType.Int);

            using (var dAdapter = new SqlDataAdapter())
            {
                var sqlCommand = new SqlCommand
                {
                    Connection = conn,
                    CommandTimeout = 300,
                    CommandText = "Proc_SysProjectSummary",
                    CommandType = CommandType.StoredProcedure
                };
                //参数赋值
                pCurrentPage.Value = currentPage;
                pPageSize.Value = pageSize;
                pFilter.Value = filter;
                pTotal.Direction = ParameterDirection.Output;
                //添加参数
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add(pCurrentPage);
                sqlCommand.Parameters.Add(pPageSize);
                sqlCommand.Parameters.Add(pFilter);
                sqlCommand.Parameters.Add(pTotal);
                try
                {
                    conn.Open();
                    dAdapter.SelectCommand = sqlCommand;
                    dAdapter.Fill(dataTable);
                    total = Convert.ToInt32(pTotal.Value);
                }
                catch (Exception e)
                {
                    total = 0;
                    Log4NetHelper.WriteError(e);
                }
                finally
                {
                    sqlCommand.Dispose();
                    conn.Close();
                }
            }
            return dataTable;
        }
        /// <summary>
        /// 开启/关闭 开关
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool OnOrOffSwitch(int type, int id, int status)
        {
            string column = "";
            switch (type)
            {
                case 1:
                    column = "SwitchAutoRepayment";
                    break;
                case 2:
                    column = "SwitchBuildOverdueFee";
                    break;
                case 3:
                    column = "SwitchAutoPass";
                    break;
            }
            string sql = "UPDATE dbo.Loan SET " + column + "=@Value WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[] 
            {
                new SqlParameter("@Value", SqlDbType.Bit){Value=status},
                new SqlParameter("@ID", SqlDbType.Int, 4){Value=id}
            };
            int num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            return num > 0;
        }
        /// <summary>
        /// 获取借款标会员/企业信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetUserInfo(int memberId, string sql)
        {
            SqlParameter[] paras =
                {
                    new SqlParameter("@MemberID", SqlDbType.Int,4){Value=memberId}
                };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras);
            return ds != null ? ds.Tables[0] : null;
        }

        /// <summary>
        /// 临时保存借款标描述
        /// </summary>
        public bool UpdateLoanDescribe(LoanModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update Loan set ");

            strSql.Append(" LoanDescribe = @LoanDescribe,  ");
            strSql.Append(" ContractNo = @ContractNo , ");
            strSql.Append(" Agency = @Agency,  ");
            strSql.Append(" LinkmanOne = @LinkmanOne,  ");
            strSql.Append(" TelOne = @TelOne,  ");
            strSql.Append(" LinkmanTwo = @LinkmanTwo,  ");
            strSql.Append(" TelTwo = @TelTwo,  ");
            strSql.Append(" LoanTypeID = @LoanTypeID  ");

            strSql.Append(" where ID=@ID and TimeStamp = @TimeStamp ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} , 
                        new SqlParameter("@TimeStamp", SqlDbType.Int,4){Value = model.TimeStamp} , 
                        new SqlParameter("@LoanDescribe", SqlDbType.Text){Value = model.LoanDescribe},
                        new SqlParameter("@ContractNo", SqlDbType.VarChar,20){Value = model.ContractNo} ,   
                        new SqlParameter("@Agency", SqlDbType.VarChar,20){Value = model.Agency} ,  
                        new SqlParameter("@LinkmanOne", SqlDbType.VarChar,20){Value = model.LinkmanOne} ,  
                        new SqlParameter("@TelOne", SqlDbType.VarChar,20){Value = model.TelOne} ,  
                        new SqlParameter("@LinkmanTwo", SqlDbType.VarChar,20){Value = model.LinkmanTwo} ,  
                        new SqlParameter("@TelTwo", SqlDbType.VarChar,20){Value = model.TelTwo} ,  
                        new SqlParameter("@LoanTypeID", SqlDbType.Int,4){Value = model.LoanTypeID}
            };

            try
            {
                int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
                return rows > 0;
            }
            catch
            {
                return false;
            }
        }

        #region 获取预约用户列表
        /// <summary>
        /// 获取预约用户列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRows">总记录数</param>
        /// <returns>列表数据</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUserList(string filter, int pageIndex, int pageSize, ref int totalRows)
        {
            string sql1 = "SELECT COUNT(A.id) as totals FROM AppointmentBiddingUser A,Member M WHERE A.memberID=M.ID" + filter;
            var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                totalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY CreateTime)) AS rownum,A.*,M.MemberName memberName,M.Balance balance,(SELECT F.RealName FROM MemberInfo F WHERE F.MemberID=A.memberID) realName FROM AppointmentBiddingUser A,Member M WHERE A.memberID=M.ID" + filter;
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((pageIndex - 1) * pageSize + 1) + " and " + pageIndex * pageSize;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2).Tables[0];
        }
        #endregion

        #region 获取预约信息
        /// <summary>
        /// 获取预约信息
        /// </summary>
        /// <param name="id">预约ID</param>
        /// <returns>预约信息</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUser(int id)
        {
            string sql = "SELECT *,(SELECT M.MemberName FROM Member M WHERE M.ID=A.memberID) memberName FROM AppointmentBiddingUser A WHERE a.id=" + id;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql).Tables[0];
        }
        #endregion

        #region 修改预约用户数据
        /// <summary>
        /// 修改预约用户数据
        /// </summary>
        /// <param name="id">预约id</param>
        /// <param name="memberId">用户id</param>
        /// <param name="status">状态</param>
        /// <param name="operationId">操作员id</param>
        /// <param name="operationNote">操作备注</param>
        /// <returns>true 成功 false 失败</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public bool AddAppointmentBiddingUser(int id, int? memberId, int status, int operationId, string operationNote)
        {
            string sql = "UPDATE AppointmentBiddingUser SET memberID=@memberId,status=@status,operationId=@operationId,operationTime=GETDATE(),operationNote=''+@operationNote+'',operationRecord+=''+case when @status=0 then '核实中' when @status=1 then '核实通过' else '核实不通过' end+'-'+(SELECT F.UserName FROM FcmsUser F WHERE F.ID=@operationId)+'-'+CONVERT(CHAR(19),GETDATE(),120)+'-备注:'+@operationNote+'|' WHERE id=@id";
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            if (memberId == null)
                parameters[1] = new SqlParameter("@memberId", SqlDbType.Int) { Value = DBNull.Value };
            else
                parameters[1] = new SqlParameter("@memberId", SqlDbType.Int) { Value = memberId.Value };
            parameters[2] = new SqlParameter("@status", SqlDbType.Int) { Value = status };
            parameters[3] = new SqlParameter("@operationId", SqlDbType.Int) { Value = operationId };
            parameters[4] = new SqlParameter("@operationNote", SqlDbType.NVarChar, 500) { Value = operationNote };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters) > 0;
        }
        #endregion

        #region 获取已确定预约用户信息列表
        /// <summary>
        /// 获取已确定预约用户信息列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRows">总记录数</param>
        /// <param name="filter">查询条件</param>
        /// <returns>列表数据</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUserList(int pageIndex, int pageSize, ref int totalRows, string filter)
        {
            string sql1 = "SELECT COUNT(A.id) as totals FROM AppointmentBiddingUser A WHERE status=1" + filter;
            var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                totalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY CreateTime)) AS rownum,A.id,A.memberID,A.amount,A.mobile,CONVERT(CHAR(10),A.createTime,120) createTime,(CASE WHEN M.Balance>=A.amount THEN A.amount ELSE M.Balance END) Balance,M.MemberName FROM AppointmentBiddingUser A,Member M WHERE A.memberID=M.ID AND A.status=1" + filter;
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((pageIndex - 1) * pageSize + 1) + " and " + pageIndex * pageSize;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2).Tables[0];
        }
        #endregion

        #region 根据借款ID获取预约用户信息列表
        /// <summary>
        /// 根据借款ID获取预约用户信息列表
        /// </summary>
        /// <param name="loanId">借款ID</param>
        /// <returns>列表数据</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUserList(int loanId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DECLARE @SQL NVARCHAR(MAX)");
            sql.Append(" SET @SQL=(SELECT AL.appointmentId FROM AppointmentLoan AL WHERE AL.loanID=" + loanId + ")");
            sql.Append(" EXEC ('SELECT A.id,A.memberID,A.amount,A.mobile,CONVERT(CHAR(10),A.createTime,120) createTime,(CASE WHEN M.Balance>=A.amount THEN A.amount ELSE M.Balance END) Balance,M.MemberName FROM AppointmentBiddingUser A,Member M WHERE A.memberID=M.ID AND A.id IN ('+@SQL+')')");
            var data = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
            return data == null ? new DataTable() : data.Tables[0];
        }
        #endregion
        /// <summary>
        /// 微信发标提醒
        /// </summary>
        /// <param name="lm"></param>
        public void AddWeixinNoticeMessage(LoanModel lm)
        {
            string sql = string.Empty;
            sql = "INSERT INTO [RJBDB].[dbo].[WeixinNoticeMessage]([LoanID],[LoanNumber],[LoanTitle],[LoanTypeID],[LoanAmount],[LoanRate],[LoanTerm],[BorrowMode],[BidStratTime],[SendTime],[RepaymentMethodName])"
                + " VALUES (@LoanID,@LoanNumber,@LoanTitle,@LoanTypeID,@LoanAmount,@LoanRate,@LoanTerm,@BorrowMode,@BidStratTime,@SendTime,@RepaymentMethodName)";
            SqlParameter[] paras =
                {
                    new SqlParameter("@LoanID", SqlDbType.Int){Value=lm.ID},
                    new SqlParameter("@LoanNumber", SqlDbType.VarChar){Value=lm.LoanNumber},
                    new SqlParameter("@LoanTitle", SqlDbType.VarChar,50){Value=(string.IsNullOrEmpty(lm.LoanTitle)?"":lm.LoanTitle)},
                    new SqlParameter("@LoanTypeID", SqlDbType.Int){Value=lm.LoanTypeID},
                    new SqlParameter("@LoanAmount", SqlDbType.Decimal){Value=lm.LoanAmount},
                    new SqlParameter("@LoanRate", SqlDbType.Decimal){Value=lm.LoanRate},
                    new SqlParameter("@LoanTerm", SqlDbType.Int){Value=lm.LoanTerm},
                    new SqlParameter("@BorrowMode", SqlDbType.Int){Value=lm.BorrowMode},
                    new SqlParameter("@BidStratTime", SqlDbType.DateTime){Value=lm.BidStratTime},
                    new SqlParameter("@SendTime", SqlDbType.DateTime){Value=lm.BidStratTime},
                    new SqlParameter("@RepaymentMethodName", SqlDbType.VarChar){Value=lm.RepaymentMethodName}
                };
            int res = 0;

            TimeSpan fb = lm.BidStratTime - DateTime.Now;
            if (fb.TotalMinutes > 15)//15分钟内只添加一条
            {
                sql += ", (@LoanID,@LoanNumber,@LoanTitle,@LoanTypeID,@LoanAmount,@LoanRate,@LoanTerm,@BorrowMode,@BidStratTime,@SendTime2,@RepaymentMethodName)";
                SqlParameter[] paras2 =
                {
                    new SqlParameter("@LoanID", SqlDbType.Int){Value=lm.ID},
                    new SqlParameter("@LoanNumber", SqlDbType.VarChar){Value=lm.LoanNumber},
                    new SqlParameter("@LoanTitle", SqlDbType.VarChar,50){Value=(string.IsNullOrEmpty(lm.LoanTitle)?"":lm.LoanTitle)},
                    new SqlParameter("@LoanTypeID", SqlDbType.Int){Value=lm.LoanTypeID},
                    new SqlParameter("@LoanAmount", SqlDbType.Decimal){Value=lm.LoanAmount},
                    new SqlParameter("@LoanRate", SqlDbType.Decimal){Value=lm.LoanRate},
                    new SqlParameter("@LoanTerm", SqlDbType.Int){Value=lm.LoanTerm},
                    new SqlParameter("@BorrowMode", SqlDbType.Int){Value=lm.BorrowMode},
                    new SqlParameter("@BidStratTime", SqlDbType.DateTime){Value=lm.BidStratTime},
                    new SqlParameter("@SendTime", SqlDbType.DateTime){Value=lm.BidStratTime},
                    new SqlParameter("@SendTime2", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@RepaymentMethodName", SqlDbType.VarChar){Value=lm.RepaymentMethodName}
                };
                res = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras2);
            }
            else
            {
                res = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras);
            }
        }
    }
}
