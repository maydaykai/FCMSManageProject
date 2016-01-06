using ManageFcmsConn;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsDal
{
    public class MemberDetailInfoDal
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        {
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MemberDetailInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberDetailInfo set ");

            strSql.Append(" CarLoan = @CarLoan , ");
            strSql.Append(" NativePlace = @NativePlace , ");
            strSql.Append(" JobType = @JobType , ");
            strSql.Append(" JobStatus = @JobStatus , ");
            strSql.Append(" MonthlyIncome = @MonthlyIncome , ");
            strSql.Append(" CompanyName = @CompanyName , ");
            strSql.Append(" WorkCity = @WorkCity , ");
            strSql.Append(" CompanyCategory = @CompanyCategory , ");
            strSql.Append(" CompanySize = @CompanySize , ");
            strSql.Append(" WorkTerm = @WorkTerm , ");
            strSql.Append(" MemberID = @MemberID , ");
            strSql.Append(" CompanyPhone = @CompanyPhone , ");
            strSql.Append(" CompanyAddress = @CompanyAddress , ");
            strSql.Append(" ContactName = @ContactName , ");
            strSql.Append(" ContactRelation = @ContactRelation , ");
            strSql.Append(" ContactPhone = @ContactPhone , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" HighestDegree = @HighestDegree , ");
            strSql.Append(" DomicilePlace = @DomicilePlace , ");
            strSql.Append(" MaritalStatus = @MaritalStatus , ");
            strSql.Append(" Children = @Children , ");
            strSql.Append(" House = @House , ");
            strSql.Append(" HouseLoan = @HouseLoan , ");
            strSql.Append(" Car = @Car  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@CarLoan", SqlDbType.Bit,1){Value = model.CarLoan} ,            
                        new SqlParameter("@NativePlace", SqlDbType.Int,4){Value = model.NativePlace} ,            
                        new SqlParameter("@JobType", SqlDbType.SmallInt,2){Value = model.JobType} ,            
                        new SqlParameter("@JobStatus", SqlDbType.SmallInt,2){Value = model.JobStatus} ,            
                        new SqlParameter("@MonthlyIncome", SqlDbType.SmallInt,2){Value = model.MonthlyIncome} ,            
                        new SqlParameter("@CompanyName", SqlDbType.NVarChar,128){Value = model.CompanyName} ,            
                        new SqlParameter("@WorkCity", SqlDbType.Int,4){Value = model.WorkCity} ,            
                        new SqlParameter("@CompanyCategory", SqlDbType.SmallInt,2){Value = model.CompanyCategory} ,            
                        new SqlParameter("@CompanySize", SqlDbType.SmallInt,2){Value = model.CompanySize} ,            
                        new SqlParameter("@WorkTerm", SqlDbType.SmallInt,2){Value = model.WorkTerm} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value = model.MemberID} ,            
                        new SqlParameter("@CompanyPhone", SqlDbType.VarChar,20){Value = model.CompanyPhone} ,            
                        new SqlParameter("@CompanyAddress", SqlDbType.NVarChar,128){Value = model.CompanyAddress} ,            
                        new SqlParameter("@ContactName", SqlDbType.NVarChar,20){Value = model.ContactName} ,            
                        new SqlParameter("@ContactRelation", SqlDbType.NVarChar,20){Value = model.ContactRelation} ,            
                        new SqlParameter("@ContactPhone", SqlDbType.VarChar,20){Value = model.ContactPhone} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@HighestDegree", SqlDbType.SmallInt,2){Value = model.HighestDegree} ,            
                        new SqlParameter("@DomicilePlace", SqlDbType.Int,4){Value = model.DomicilePlace} ,            
                        new SqlParameter("@MaritalStatus", SqlDbType.SmallInt,2){Value = model.MaritalStatus} ,            
                        new SqlParameter("@Children", SqlDbType.Bit,1){Value = model.Children} ,            
                        new SqlParameter("@House", SqlDbType.Bit,1){Value = model.House} ,            
                        new SqlParameter("@HouseLoan", SqlDbType.Bit,1){Value = model.HouseLoan} ,            
                        new SqlParameter("@Car", SqlDbType.Bit,1){Value = model.Car}             
              
            };


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 用户信息管理更新
        /// </summary>
        public bool LoanPubilshUser(LoanMemberInfoModel loanMemberInfoModel)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value= loanMemberInfoModel.MemberId},
                        new SqlParameter("@HaveCar", SqlDbType.Bit,1){Value= loanMemberInfoModel.HaveCar},
                        new SqlParameter("@WorkingLife", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.WorkingLife},
                        new SqlParameter("@JobStatus", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.JobStatus},
                        new SqlParameter("@Age", SqlDbType.Int,4){Value= loanMemberInfoModel.Age},
                        new SqlParameter("@MaritalStatus", SqlDbType.Int,4){Value= loanMemberInfoModel.MaritalStatus},
                        new SqlParameter("@Sex", SqlDbType.NVarChar,2){Value= loanMemberInfoModel.Sex},
                        new SqlParameter("@DomicilePlace", SqlDbType.Int,4){Value= loanMemberInfoModel.DomicilePlace},
                        new SqlParameter("@MonthlyPay", SqlDbType.NVarChar,20){Value= loanMemberInfoModel.MonthlyPay},
                        new SqlParameter("@HaveHouse", SqlDbType.Bit,1){Value= loanMemberInfoModel.HaveHouse},
                        new SqlParameter("@FamilyNum", SqlDbType.Int,4){Value= loanMemberInfoModel.FamilyNum}
                                            };

            var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_LoanUserInfo", parameters);
            return obj > 0;
            
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LoanMemberInfoModel getLoanMemberInfoModel(int memberId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT m1.ID,m1.MemberId,m1.Age,m1.MaritalStatus,m1.Sex,m1.DomicilePlace,m1.FamilyNum,m1.MonthlyPay,m1.HaveHouse,m1.HaveCar,m1.WorkingLife,m1.JobStatus,m2.RealName,m2.IdentityCard,m2.Telephone ");
            strSql.Append(" FROM MemberInfo m2 LEFT JOIN LoanMemberInfo m1 on m1.MemberId = m2.MemberId  ");
            strSql.Append(" WHERE m2.MemberId=@MemberID");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int,4)
			};
            parameters[0].Value = memberId;

            LoanMemberInfoModel model = new LoanMemberInfoModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = int.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Age"].ToString() != "")
                {
                    model.Age = int.Parse(ds.Tables[0].Rows[0]["Age"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaritalStatus"].ToString() != "")
                {
                    model.MaritalStatus = int.Parse(ds.Tables[0].Rows[0]["MaritalStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DomicilePlace"].ToString() != "")
                {
                    model.DomicilePlace = int.Parse(ds.Tables[0].Rows[0]["DomicilePlace"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FamilyNum"].ToString() != "")
                {
                    model.FamilyNum = int.Parse(ds.Tables[0].Rows[0]["FamilyNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MonthlyPay"].ToString() != "")
                {
                    model.MonthlyPay = ds.Tables[0].Rows[0]["MonthlyPay"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HaveHouse"].ToString() != "")
                {
                    model.HaveHouse = bool.Parse(ds.Tables[0].Rows[0]["HaveHouse"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HaveCar"].ToString() != "")
                {
                    model.HaveCar = bool.Parse(ds.Tables[0].Rows[0]["HaveCar"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkingLife"].ToString() != "")
                {
                    model.WorkingLife = ds.Tables[0].Rows[0]["WorkingLife"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JobStatus"].ToString() != "")
                {
                    model.JobStatus = ds.Tables[0].Rows[0]["JobStatus"].ToString();
                }

                return model;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 企业信息管理更新
        /// </summary>
        public bool LoanEnterprise(LoanEnterpriseMemberInfoModel loanEnterpriseMemberInfoModel)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=loanEnterpriseMemberInfoModel.MemberId},
                        new SqlParameter("@Nature", SqlDbType.NVarChar,20){Value=loanEnterpriseMemberInfoModel.Nature},
                        new SqlParameter("@Size", SqlDbType.NVarChar,20){Value=loanEnterpriseMemberInfoModel.Size},
                        new SqlParameter("@IndustryCategory", SqlDbType.NVarChar,20){Value=loanEnterpriseMemberInfoModel.IndustryCategory},
                        new SqlParameter("@LiveCityID", SqlDbType.Int,4){Value=loanEnterpriseMemberInfoModel.CityId},
                        new SqlParameter("@MainProducts", SqlDbType.NVarChar,20){Value=loanEnterpriseMemberInfoModel.MainProducts},
                        new SqlParameter("@RegisteredCapital", SqlDbType.NVarChar,20){Value=loanEnterpriseMemberInfoModel.RegisteredCapital},
                        new SqlParameter("@BusinessScope", SqlDbType.NVarChar,20){Value=loanEnterpriseMemberInfoModel.BusinessScope},
                        new SqlParameter("@SetUpyear", SqlDbType.NVarChar,20){Value=loanEnterpriseMemberInfoModel.SetUpyear},
                                            };

            var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_LoanEnterprise", parameters);
            return obj > 0;

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberDetailInfoModel GetModel(int memberId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, CarLoan, NativePlace, JobType, JobStatus, MonthlyIncome, CompanyName, WorkCity, CompanyCategory, CompanySize, WorkTerm, MemberID, CompanyPhone, CompanyAddress, ContactName, ContactRelation, ContactPhone, CreateTime, UpdateTime, HighestDegree, DomicilePlace, MaritalStatus, Children, House, HouseLoan, Car  ");
            strSql.Append("  from MemberDetailInfo ");
            strSql.Append(" where MemberID=@MemberID");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int,4)
			};
            parameters[0].Value = memberId;


            MemberDetailInfoModel model = new MemberDetailInfoModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CarLoan"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["CarLoan"].ToString() == "1") || (ds.Tables[0].Rows[0]["CarLoan"].ToString().ToLower() == "true"))
                    {
                        model.CarLoan = true;
                    }
                    else
                    {
                        model.CarLoan = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["NativePlace"].ToString() != "")
                {
                    model.NativePlace = int.Parse(ds.Tables[0].Rows[0]["NativePlace"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JobType"].ToString() != "")
                {
                    model.JobType = int.Parse(ds.Tables[0].Rows[0]["JobType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JobStatus"].ToString() != "")
                {
                    model.JobStatus = int.Parse(ds.Tables[0].Rows[0]["JobStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MonthlyIncome"].ToString() != "")
                {
                    model.MonthlyIncome = int.Parse(ds.Tables[0].Rows[0]["MonthlyIncome"].ToString());
                }
                model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                if (ds.Tables[0].Rows[0]["WorkCity"].ToString() != "")
                {
                    model.WorkCity = int.Parse(ds.Tables[0].Rows[0]["WorkCity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CompanyCategory"].ToString() != "")
                {
                    model.CompanyCategory = int.Parse(ds.Tables[0].Rows[0]["CompanyCategory"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CompanySize"].ToString() != "")
                {
                    model.CompanySize = int.Parse(ds.Tables[0].Rows[0]["CompanySize"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkTerm"].ToString() != "")
                {
                    model.WorkTerm = int.Parse(ds.Tables[0].Rows[0]["WorkTerm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                model.CompanyPhone = ds.Tables[0].Rows[0]["CompanyPhone"].ToString();
                model.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                model.ContactName = ds.Tables[0].Rows[0]["ContactName"].ToString();
                model.ContactRelation = ds.Tables[0].Rows[0]["ContactRelation"].ToString();
                model.ContactPhone = ds.Tables[0].Rows[0]["ContactPhone"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HighestDegree"].ToString() != "")
                {
                    model.HighestDegree = int.Parse(ds.Tables[0].Rows[0]["HighestDegree"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DomicilePlace"].ToString() != "")
                {
                    model.DomicilePlace = int.Parse(ds.Tables[0].Rows[0]["DomicilePlace"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaritalStatus"].ToString() != "")
                {
                    model.MaritalStatus = int.Parse(ds.Tables[0].Rows[0]["MaritalStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Children"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Children"].ToString() == "1") || (ds.Tables[0].Rows[0]["Children"].ToString().ToLower() == "true"))
                    {
                        model.Children = true;
                    }
                    else
                    {
                        model.Children = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["House"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["House"].ToString() == "1") || (ds.Tables[0].Rows[0]["House"].ToString().ToLower() == "true"))
                    {
                        model.House = true;
                    }
                    else
                    {
                        model.House = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["HouseLoan"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["HouseLoan"].ToString() == "1") || (ds.Tables[0].Rows[0]["HouseLoan"].ToString().ToLower() == "true"))
                    {
                        model.HouseLoan = true;
                    }
                    else
                    {
                        model.HouseLoan = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Car"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Car"].ToString() == "1") || (ds.Tables[0].Rows[0]["Car"].ToString().ToLower() == "true"))
                    {
                        model.Car = true;
                    }
                    else
                    {
                        model.Car = false;
                    }
                }

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取线下提现设置
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool GetUnderCashSetting(int memberId)
        {
            const string sql = "SELECT AllowUnderCash FROM dbo.CashSetting WHERE MemberID=@MemberID";
            SqlParameter[] paras =
                {
                    new SqlParameter("@MemberID", SqlDbType.Int, 4) {Value = memberId}
                };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras);
            return obj != null && Convert.ToBoolean(obj);
        }
        /// <summary>
        /// 会员线下提现设置
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="allowUnderCash"></param>
        /// <returns></returns>
        public bool SetMemberUnderCash(int memberId, bool allowUnderCash)
        {
            SqlParameter[] paras =
                {
                    new SqlParameter("@MemberID", SqlDbType.Int, 4) {Value = memberId},
                    new SqlParameter("@AllowUnderCash", SqlDbType.Bit) {Value = allowUnderCash}
                };
            int num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "[dbo].[Proc_UnderCashSetting]", paras);
            return num > 0;
        }
        

        /// <summary>
        /// 会员是否开通线下提现
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="allowUnderCash"></param>
        /// <returns></returns>
        public bool SetMember(int memberId)
        {
            const string sql = "SELECT COUNT(1) FROM dbo.Loan where MemberID=@MemberID AND ExamStatus>8";
            SqlParameter[] paras =
                {
                    new SqlParameter("@MemberID", SqlDbType.Int, 4) {Value = memberId}
                };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras);
            return obj != null && Convert.ToInt32(obj) > 0;
        }
    }
}
