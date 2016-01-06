using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsModel;
using ManageFcmsCommon;

namespace ManageFcmsDal
{
    public class MemberDal
    {
        //获取账户余额
        public decimal GetBalance(int Id)
        {  
            string sql = "select [Balance] from Member where ID=@ID";
            SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, new SqlParameter("@ID", Id));
            decimal balance = 0;
            if (sdr.Read())
            {
                balance = decimal.Parse(sdr["Balance"].ToString());
            }
            sdr.Close();
            return balance;
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = "Member p left join MemberInfo m on  p.ID = m.MemberID left join dbo.BranchCompanyMember BM on p.ID=BM.MemberID left join dbo.DimBranchCompany BC on BM.BranchCompanyID=BC.ID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        } 

        public DataTable GetPageList1(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = "Member P left join MemberInfo M on P.ID=M.MemberID left join Coupon c on c.UseID = p.ID ";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 按条件获得会员列表
        /// </summary>
        public DataTable GetMemberList(string fields, string filters, string sortStr, out int total)
        {
            const string tables = "Member p left join MemberInfo m on  p.ID = m.MemberID";
            string sql1 = "select count(*) as totals from " + tables;
            if (!string.IsNullOrEmpty(filters))
            {
                sql1 = sql1 + " where " + filters;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                total = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            else
            {
                total = 0;
            }
            reader.Close();

            StringBuilder sbSql2 = new StringBuilder();
            sbSql2.Append("select ").Append(fields).Append(" from ").Append(tables).Append(" where ").Append(filters).Append(" order by ").Append(sortStr);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sbSql2.ToString(), null);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, UpdateTime, IsLocked, LastLoginTime, CreditPoint, MemberPoint, Times, RecoCode, Balance, Type, CompleStatus, MemberName, MemberLevel, VIPStartTime, VIPEndTime, Icon, TimeStamp, PassWord, TranPassWord, Mobile, Email, LastIP, IsDisable, RegTime  ");
            strSql.Append("  from Member ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            MemberModel model = new MemberModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                else
                {
                    model.UpdateTime = PublicConst.MinDate;
                }
                if (ds.Tables[0].Rows[0]["IsLocked"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsLocked"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsLocked"].ToString().ToLower() == "true"))
                    {
                        model.IsLocked = true;
                    }
                    else
                    {
                        model.IsLocked = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }
                else
                {
                    model.LastLoginTime = PublicConst.MinDate;
                }
                if (ds.Tables[0].Rows[0]["CreditPoint"].ToString() != "")
                {
                    model.CreditPoint = int.Parse(ds.Tables[0].Rows[0]["CreditPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberPoint"].ToString() != "")
                {
                    model.MemberPoint = int.Parse(ds.Tables[0].Rows[0]["MemberPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Times"].ToString() != "")
                {
                    model.Times = int.Parse(ds.Tables[0].Rows[0]["Times"].ToString());
                }
                model.RecoCode = ds.Tables[0].Rows[0]["RecoCode"].ToString();
                if (ds.Tables[0].Rows[0]["Balance"].ToString() != "")
                {
                    model.Balance = decimal.Parse(ds.Tables[0].Rows[0]["Balance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CompleStatus"].ToString() != "")
                {
                    model.CompleStatus = int.Parse(ds.Tables[0].Rows[0]["CompleStatus"].ToString());
                }
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                if (ds.Tables[0].Rows[0]["MemberLevel"].ToString() != "")
                {
                    model.MemberLevel = int.Parse(ds.Tables[0].Rows[0]["MemberLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VIPStartTime"].ToString() != "")
                {
                    model.VIPStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["VIPStartTime"].ToString());
                }
                else
                {
                    model.VIPStartTime = PublicConst.MinDate;
                }
                if (ds.Tables[0].Rows[0]["VIPEndTime"].ToString() != "")
                {
                    model.VIPEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["VIPEndTime"].ToString());
                }
                else
                {
                    model.VIPEndTime = PublicConst.MinDate;
                }
                model.Icon = ds.Tables[0].Rows[0]["Icon"].ToString();
                model.PassWord = ds.Tables[0].Rows[0]["PassWord"].ToString();
                model.TranPassWord = ds.Tables[0].Rows[0]["TranPassWord"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.LastIP = ds.Tables[0].Rows[0]["LastIP"].ToString();
                if (ds.Tables[0].Rows[0]["IsDisable"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDisable"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDisable"].ToString().ToLower() == "true"))
                    {
                        model.IsDisable = true;
                    }
                    else
                    {
                        model.IsDisable = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["RegTime"].ToString() != "")
                {
                    model.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["RegTime"].ToString());
                }
                else
                {
                    model.RegTime = PublicConst.MinDate;
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MemberModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Member set ");

            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" IsLocked = @IsLocked , ");
            strSql.Append(" LastLoginTime = @LastLoginTime , ");
            strSql.Append(" CreditPoint = @CreditPoint , ");
            strSql.Append(" MemberPoint = @MemberPoint , ");
            strSql.Append(" Times = @Times , ");
            strSql.Append(" RecoCode = @RecoCode , ");
            strSql.Append(" Balance = @Balance , ");
            strSql.Append(" Type = @Type , ");
            strSql.Append(" CompleStatus = @CompleStatus , ");
            strSql.Append(" MemberName = @MemberName , ");
            strSql.Append(" MemberLevel = @MemberLevel , ");
            strSql.Append(" VIPStartTime = @VIPStartTime , ");
            strSql.Append(" VIPEndTime = @VIPEndTime , ");
            strSql.Append(" Icon = @Icon , ");
            strSql.Append(" PassWord = @PassWord , ");
            strSql.Append(" TranPassWord = @TranPassWord , ");
            strSql.Append(" Mobile = @Mobile , ");
            strSql.Append(" Email = @Email , ");
            strSql.Append(" LastIP = @LastIP , ");
            strSql.Append(" IsDisable = @IsDisable , ");
            strSql.Append(" RegTime = @RegTime ,  ");
            strSql.Append(" AllowWithdraw = @AllowWithdraw,  ");
            strSql.Append(" IsMarket = @IsMarket  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@IsLocked", SqlDbType.Bit,1){Value = model.IsLocked} ,            
                        new SqlParameter("@LastLoginTime", SqlDbType.DateTime){Value = model.LastLoginTime} ,            
                        new SqlParameter("@CreditPoint", SqlDbType.Int,4){Value = model.CreditPoint} ,            
                        new SqlParameter("@MemberPoint", SqlDbType.Int,4){Value = model.MemberPoint} ,            
                        new SqlParameter("@Times", SqlDbType.Int,4){Value = model.Times} ,            
                        new SqlParameter("@RecoCode", SqlDbType.VarChar,10){Value = model.RecoCode} ,            
                        new SqlParameter("@Balance", SqlDbType.Decimal,9){Value = model.Balance} ,            
                        new SqlParameter("@Type", SqlDbType.Int,4){Value = model.Type} ,            
                        new SqlParameter("@CompleStatus", SqlDbType.Int,4){Value = model.CompleStatus} ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar,12){Value = model.MemberName} ,            
                        new SqlParameter("@MemberLevel", SqlDbType.Int,4){Value = model.MemberLevel} ,            
                        new SqlParameter("@VIPStartTime", SqlDbType.DateTime){Value = model.VIPStartTime} ,            
                        new SqlParameter("@VIPEndTime", SqlDbType.DateTime){Value = model.VIPEndTime} ,            
                        new SqlParameter("@Icon", SqlDbType.VarChar,150){Value = model.Icon} ,                     
                        new SqlParameter("@PassWord", SqlDbType.VarChar,50){Value = model.PassWord} ,            
                        new SqlParameter("@TranPassWord", SqlDbType.VarChar,50){Value = model.TranPassWord} ,            
                        new SqlParameter("@Mobile", SqlDbType.VarChar,20){Value = model.Mobile} ,            
                        new SqlParameter("@Email", SqlDbType.NVarChar,50){Value = model.Email} ,            
                        new SqlParameter("@LastIP", SqlDbType.NVarChar,50){Value = model.LastIP} ,            
                        new SqlParameter("@IsDisable", SqlDbType.Bit,1){Value = model.IsDisable} ,            
                        new SqlParameter("@RegTime", SqlDbType.DateTime){Value = model.RegTime} ,
                        new SqlParameter("@AllowWithdraw", SqlDbType.Bit,1){Value = model.AllowWithdraw},
                        new SqlParameter("@IsMarket", SqlDbType.Bit,1){Value = model.IsMarket}
            };


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(MemberModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into Member(");
            strSql.Append("RegTime,UpdateTime,IsLocked,LastLoginTime,CreditPoint,MemberPoint,Times,RecoCode,Type,CompleStatus,MemberName,MemberLevel,VIPStartTime,VIPEndTime,Icon,TimeStamp,PassWord,TranPassWord,Mobile,Email,Balance,LastIP,IsDisable");
            strSql.Append(") values (");
            strSql.Append("@RegTime,@UpdateTime,@IsLocked,@LastLoginTime,@CreditPoint,@MemberPoint,@Times,@RecoCode,@Type,@CompleStatus,@MemberName,@MemberLevel,@VIPStartTime,@VIPEndTime,@Icon,@TimeStamp,@PassWord,@TranPassWord,@Mobile,@Email,@Balance,@LastIP,@IsDisable");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@RegTime", SqlDbType.DateTime){Value=model.RegTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime} ,            
                        new SqlParameter("@IsLocked", SqlDbType.Bit,1){Value=model.IsLocked} ,            
                        new SqlParameter("@LastLoginTime", SqlDbType.DateTime){Value=model.LastLoginTime} ,            
                        new SqlParameter("@CreditPoint", SqlDbType.Int,4){Value=model.CreditPoint} ,            
                        new SqlParameter("@MemberPoint", SqlDbType.Int,4){Value=model.MemberPoint} ,            
                        new SqlParameter("@Times", SqlDbType.Int,4){Value=model.Times} ,            
                        new SqlParameter("@RecoCode", SqlDbType.VarChar,10){Value=model.RecoCode} ,            
                        new SqlParameter("@Type", SqlDbType.Int,4){Value=model.Type} ,            
                        new SqlParameter("@CompleStatus", SqlDbType.Int,4){Value=model.CompleStatus} ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar,12){Value=model.MemberName} ,            
                        new SqlParameter("@MemberLevel", SqlDbType.Int,4){Value=model.MemberLevel} ,            
                        new SqlParameter("@VIPStartTime", SqlDbType.DateTime){Value=model.VIPStartTime} ,            
                        new SqlParameter("@VIPEndTime", SqlDbType.DateTime){Value=model.VIPEndTime} ,            
                        new SqlParameter("@Icon", SqlDbType.VarChar,150){Value=model.Icon} ,            
                        new SqlParameter("@TimeStamp", SqlDbType.Timestamp,8){Value=model.TimeStamp} ,            
                        new SqlParameter("@PassWord", SqlDbType.VarChar,50){Value=model.PassWord} ,            
                        new SqlParameter("@TranPassWord", SqlDbType.VarChar,50){Value=model.TranPassWord} ,            
                        new SqlParameter("@Mobile", SqlDbType.VarChar,20){Value=model.Mobile} ,            
                        new SqlParameter("@Email", SqlDbType.NVarChar,50){Value=model.Email} ,            
                        new SqlParameter("@Balance", SqlDbType.Decimal,9){Value=model.Balance} ,            
                        new SqlParameter("@LastIP", SqlDbType.NVarChar,50){Value=model.LastIP} ,            
                        new SqlParameter("@IsDisable", SqlDbType.Bit,1){Value=model.IsDisable}             
              
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : ConvertHelper.ToInt(obj.ToString());

        }

        /// <summary>
        /// 获得获取会员账户信息数据列表
        /// </summary>
        public DataSet GetList(string filter, string orderBy, int currentPage, int pageSize, DateTime dateEnd, ref int rowsCount)
        {
            var list = new List<LoanModel>();
            string sql1 = "select count(*) as totals from dbo.Member m " +
                          "  left outer join dbo.MemberInfo mi on m.ID=mi.MemberID " +
                          "  left outer join dbo.MemberRecommended mr on m.ID = mr.RecedMemberID " +
                          "  left outer join dbo.Member m1 on mr.RecMemberID = m1.ID " +
                          "  left outer join dbo.MemberInfo mi1 on mr.RecMemberID=mi1.MemberID " +
                          "  left outer join dbo.City c on left(mi.identitycard,6) = cast(c.ID as varchar(10)) " +
                          "  where m.Type = 0 and m.IsDisable = 0 and m.IsLocked = 0 ";
            if (!string.IsNullOrEmpty(filter))
            {
                sql1 = sql1 + " and " + filter;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum,m.MemberName,mi.RealName,m.Mobile,m.Balance,dbo.GetMemberFreezeAmount(m.id) as FreezeAmount,dbo.GetMemberAccountPayable(m.id) as AccountPayable,dbo.GetMemberAccountPayableDate(m.id,'" + dateEnd + "') as AccountPayableDate,mi.IdentityCard,m.regtime,m1.MemberName as RecMemberName,mi1.RealName as RecRealName,m1.Mobile as RecMobile,mi.Address,c.Province + c.City as ProvinceCity from dbo.Member m " +
                          "  left outer join dbo.MemberInfo mi on m.ID=mi.MemberID " +
                          "  left outer join dbo.MemberRecommended mr on m.ID = mr.RecedMemberID " +
                          "  left outer join dbo.Member m1 on mr.RecMemberID = m1.ID " +
                          "  left outer join dbo.MemberInfo mi1 on mr.RecMemberID=mi1.MemberID " +
                          "  left outer join dbo.City c on left(mi.identitycard,6) = cast(c.ID as varchar(10)) " +
                          "  where m.Type = 0 and m.IsDisable = 0 and m.IsLocked = 0 ";
            if (!string.IsNullOrEmpty(filter))
            {
                sql2 = sql2 + " and " + filter;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((currentPage - 1) * pageSize + 1) + " and " + currentPage * pageSize;
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
        }

        /// <summary>
        /// 统计汇总分页
        /// </summary>
        public DataTable SummaryStatistics(string storedProcedure, int currentPage, int pageSize, string filter, out int total)
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
                    CommandTimeout = 0,
                    CommandText = storedProcedure,
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
                    conn.Close();
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 获得异常提现预警列表
        /// </summary>
        public DataSet GetWithdrawAlertList(int limitDays, decimal percent)
        {
            SqlParameter[] parameters =
                {
                    new SqlParameter("@limitDays", SqlDbType.Int,4) {Value = limitDays},
                    new SqlParameter("@percent", SqlDbType.Decimal,9) {Value = percent}
                };
            string sql2 = "select m.ID as MemberId,m.MemberName,mi.RealName,m.Mobile,a.RechargeAmount,a.BidAmount,a.withdrawAmount,withdrawAmount / RechargeAmount * 100 AS scale from ( " +
                          "  select PartyMemberID,SUM(case when FeeType = 0 then Amount else 0 end) as RechargeAmount,SUM(case when FeeType = 2 then Amount else 0 end) as BidAmount, SUM(case when FeeType = 3 then Amount else 0 end) as withdrawAmount " +
                          "  from dbo.FundRecord where FeeType in (0,3) " +
                          "  and datediff(dd,CreateTime,GETDATE()) < @limitDays " +
                          "  group by PartyMemberID) a " +
                          "  inner join dbo.Member m on a.PartyMemberID = m.ID " +
                          "  inner join dbo.MemberInfo mi on a.PartyMemberID = mi.MemberID " +
                          " where withdrawAmount > RechargeAmount * @percent and RechargeAmount > 0";
            sql2 = "Select * from (" + sql2 + ") tmp  ";
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2, parameters);
        }

        /// <summary>
        /// 获得异常提现预警交易明细列表
        /// </summary>
        public DataSet GetWithdrawAlertDetailList(int limitDays, decimal percent, int memberId)
        {
            SqlParameter[] parameters =
                {
                    new SqlParameter("@MemberId", SqlDbType.Int,4) {Value = memberId},
                    new SqlParameter("@limitDays", SqlDbType.Int,4) {Value = limitDays},
                    new SqlParameter("@percent", SqlDbType.Decimal,9) {Value = percent}
                };
            string sql2 = "  select m.MemberName,mi.RealName,f.Amount,f.FeeType,f.UpdateTime " +
                "  from dbo.FundRecord f " +
                "  inner join dbo.Member m on f.PartyMemberID = m.ID " +
                "  inner join dbo.MemberInfo mi on f.PartyMemberID = mi.MemberID " +
                " where f.FeeType in (0,3) " +
                "  and datediff(dd,f.CreateTime,GETDATE()) < @limitDays " +
                " and f.PartyMemberID = @MemberId";
            sql2 = "Select * from (" + sql2 + ") tmp  ";
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2, parameters);
        }

        /// <summary>
        /// 会员推荐关系
        /// </summary>
        public DataTable GetMemberRecommend(int pageSize, int currentPage, string filter, string strOrderBy, out int recordCount)
        {
            var parameters = new[]
            {                
                new SqlParameter("@pageSize", SqlDbType.Int, 4){Value=pageSize},
                new SqlParameter("@currentPage", SqlDbType.Int, 4){Value=currentPage},
                new SqlParameter("@strWhere", SqlDbType.NVarChar){Value=filter},
                new SqlParameter("@strOrderBy", SqlDbType.NVarChar){Value=strOrderBy},
                new SqlParameter("@pageCount", SqlDbType.Int, 4){Direction=ParameterDirection.Output},
                new SqlParameter("@recordCount", SqlDbType.Int, 4){Direction=ParameterDirection.Output}
            };

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.Proc_MemberRecommend", parameters);
            recordCount = (int)parameters[5].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据会员名获取会员ID
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public int GetMemberId(string memberName)
        {
            string sql = "select [ID] from Member where MemberName=@MemberName";
            SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, new SqlParameter("@MemberName", memberName));
            int memberId = -2;
            if (sdr.Read())
            {
                memberId = int.Parse(sdr["ID"].ToString());
            }
            sdr.Close();
            return memberId;
        }

        public DataSet GetWithdrawInterestNhList()
        {
            string sql2 =
                "select b.MemberId " +
                "  from dbo.Bidding b  " +
                "  INNER JOIN dbo.Loan l ON b.LoanID = l.ID AND l.LoanTypeID = 5 AND l.TradeType = 1 " +
                "  WHERE b.MemberID IN (SELECT UseID FROM dbo.Coupon WHERE DATEDIFF(dd,EndTime,GETDATE()) > 0 AND UseID > 0 AND LockAmount = 100) " +
                "  AND b.MemberID NOT IN (SELECT memberid FROM dbo.Bidding WHERE BidStatus = 1 AND CALoanID = 0 GROUP BY memberid HAVING(COUNT(MemberID))>1) ";

            sql2 = "Select m.MemberName,mi.RealName,dbo.GetLockAmount(tmp.MemberId) as LockAmount,m.RegTime,m.LastLoginTime from (" + sql2 + ") tmp inner join dbo.Member m on tmp.MemberId = m.ID inner join dbo.MemberInfo mi on tmp.MemberId = Mi.MemberId   ";
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
        }

        public DataSet Summary(string Where)
        {
            string sql2 = @"Select 
                  Member.MemberName,MemberInfo.RealName,  Member.Mobile, L.LoanAmount, 
                 LoanTerm= (case when L.BorrowMode=0 then CONVERT(varchar(10),L.LoanTerm)+'天' when L.BorrowMode=1 then CONVERT(varchar(10),L.LoanTerm)+'个月' end),
                DimLoanUse.LoanUseName, 
                  DimRepaymentMethod.RepaymentMethodName,BorrowMode=case when L.BorrowMode=0 then '按天' when L.BorrowMode=1 then '按月' END,
                  ES.ExamStatusName ,Pro=(AreaProvince.Name) , City=(AreaCity.Name),L.CreateTime,L.RepaymentSource
                 from dbo.LoanApply L
                 Left JOIN dbo.DimLoanUse ON L.LoanUseID = dbo.DimLoanUse.ID  
                 Left JOIN dbo.Area AS AreaCity ON L.CityId = AreaCity.ID  
                 Left JOIN dbo.Area AS AreaProvince ON AreaCity.ParentID = AreaProvince.ID  
                Left JOIN dbo.DimRepaymentMethod on L.RepaymentMethod = dbo.DimRepaymentMethod.ID 
                Left JOIN dbo.Member ON L.MemberId = dbo.Member.ID 
                Left JOIN dbo.DimExamStatus ES ON L.ExamStatus = ES.ID
                Left JOIN MemberInfo on L.MemberID = MemberInfo.MemberID  
                Left JOIN dbo.DimProductType on L.ProductTypeId = dbo.DimProductType.ID ";

            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
        }

        /// <summary>
        /// 融金宝日报表统计
        /// </summary>
        public DataTable DailyReport(DateTime nowDate)
        {
            var parameters = new[]
            {                
                new SqlParameter("@StatisticsDate", SqlDbType.DateTime){ Value = nowDate },
            };

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.Proc_RJB_DailyReport", parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 会员资金汇总
        /// </summary>
        public DataTable MemberFundSummary(DateTime nowDate)
        {
            const string strSql = "SELECT[MemberBalance],[DueInPrincipal],[DueInInterest],[DueInOverdueInterest],[SstillPrincipal],[SstillInterest],[SstillOverdueInterest],[SstillLoanServingFee],[I_I_Principal],[I_I_Interest],[I_I_OverdueInterest],[I_I_Penalty],[I_I_RecommendIncentives],[I_P_Bidding],[I_P_InvestorServingFee],[L_I_LoanAmount],[L_I_ReGuaranteeFee],[L_I_ReLoanServingFee],[I_F_FrozenBid],[I_F_FrozenInterest],[L_P_LoanAmount],[L_P_Interest],[L_P_OverdueInterest],[L_P_OverduePayment],[L_P_PenaltyByBid],[L_P_LoanServingFee],[L_P_PenaltyBySys],[O_I_Recharge],[O_I_Settlement],[O_I_RedEnvelope],[O_I_ActivitiesAwardFroz],[O_I_ActivitiesAward],[O_P_CashAmount],[O_P_RechargeFee],[O_P_CashFee],[O_P_VipFee],[O_F_Recharge],[O_F_Settlement],[StatisticalTime] FROM dbo.MemberFundSummary WHERE DATEDIFF(DAY,StatisticalTime,@NowDateTime)=0";

            var parameters = new[]
            {                
                new SqlParameter("@NowDateTime", SqlDbType.DateTime){ Value = nowDate },
            };

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 运营日报表平台资金存量
        /// </summary>
        public DataTable PlatformCapitalStock(DateTime nowDate)
        {
            const string strSql = "SELECT [MemberBalance],[SpecialAccount],[PlatformAccount],[BidFreezingTotal],[PurchaseFrozenAmount],[WithdrawCashFrozenAmount],[StatisticalTime] FROM dbo.PlatformCapitalStock WHERE DATEDIFF(DAY,StatisticalTime,@NowDateTime)=0";

            var parameters = new[]
            {                
                new SqlParameter("@NowDateTime", SqlDbType.DateTime){ Value = nowDate },
            };

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql, parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 会员账户可用余额统计
        /// </summary>
        public object MemberBalanceTatal(string filters)
        {
            string strSql = "SELECT SUM(Balance) FROM Member p left join MemberInfo m on  p.ID = m.MemberID WHERE p.ID>0 AND " + filters;

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
        }
        /// <summary>
        /// 短信数据分页
        /// </summary>
        public DataTable GetPagePrivateSmsList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " dbo.SMS ";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        #region 
        //检测用户名是否存在 true为不存在
        public bool UserNameExists(string userName)
        {
            var strSql = new StringBuilder();
            SqlParameter[] parameters =
                {
                    new SqlParameter("@MemberName", SqlDbType.VarChar) {Value = userName}
                };
            strSql.Append("select count(1) from [member] where [MemberName]=@MemberName");
            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters));
            return result <= 0;
        }
        /// <summary>
        /// 会员注册（个人、企业）
        /// </summary>
        public int Add(MemberModel model, string reCode)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@MemberName", SqlDbType.NVarChar, 12) {Value = model.MemberName},
                new SqlParameter("@PassWord", SqlDbType.VarChar, 50) {Value = model.PassWord},
                new SqlParameter("@TranPassWord", SqlDbType.VarChar, 50) {Value = model.TranPassWord},
                new SqlParameter("@Mobile", SqlDbType.VarChar, 20) {Value = model.Mobile},
                new SqlParameter("@Email", SqlDbType.NVarChar, 50) {Value = ""},
                new SqlParameter("@LastIP", SqlDbType.NVarChar, 50) {Value = ""},
                new SqlParameter("@Type", SqlDbType.Int, 4) {Value = 0},
                new SqlParameter("@Icon", SqlDbType.VarChar, 150) {Value = "1.jpg"},
                new SqlParameter("@RecommendCode", SqlDbType.VarChar, 10) {Value = reCode},
                new SqlParameter("@Channel", SqlDbType.VarChar, 10) {Value = 0},
                new SqlParameter("@ChannelRemark", SqlDbType.VarChar, 10) {Value = ""}
            };
            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_MemberAdd", parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 增加一条数据  认证通过
        /// </summary>
        public bool AddIdentityAuthent(IdentityAuthentModel model)
        {
            SqlParameter[] parameters = {          
                new SqlParameter("@MemberID", SqlDbType.Int,4){Value=model.MemberID} ,            
                new SqlParameter("@RealName", SqlDbType.NVarChar,20){Value=model.RealName} ,            
                new SqlParameter("@IdentityCard", SqlDbType.VarChar,20){Value=model.IdentityCard} ,  
                new SqlParameter("@Sex", SqlDbType.Char, 2) {Value = ConvertHelper.getSexByIDCard(model.IdentityCard)},
                new SqlParameter("@Birthday", SqlDbType.DateTime) {Value =ConvertHelper.getBirthdayByIDCard(model.IdentityCard)},  
                new SqlParameter("@CreateTime", SqlDbType.DateTime) {Value = DateTime.Now},               
                new SqlParameter("@ExpireTime", SqlDbType.DateTime){Value=DateTime.Now.AddYears(10)} ,
                new SqlParameter("@AuthentResult", SqlDbType.Int,4){Value=1} ,            
                new SqlParameter("@IdPhoto", SqlDbType.NVarChar,500){Value=""} ,            
                new SqlParameter("@ExamStatus", SqlDbType.Int,4){Value=1} ,            
                new SqlParameter("@RequestXml", SqlDbType.NVarChar,-1){Value=""}  ,        
		        new SqlParameter("@ResponseXml", SqlDbType.NVarChar,-1){Value=""}   
            };
            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_IdentityAuthentSuccess", parameters);
            return result != null && ConvertHelper.ToInt(result.ToString()) > 0;
        }
        /// <summary>
        /// VIP申请
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool ApplyVip(int memberId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@MemberID", SqlDbType.Int, 4) {Value = memberId},
            };
            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_VipApply", parameters);
            return result != null && ConvertHelper.ToInt(result.ToString()) == 1;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ProcInvestModel model, ref string message)
        {
            SqlParameter[] parameters = {
			            new SqlParameter("@LoanID", SqlDbType.Int,4) ,            
                        new SqlParameter("@LoanMemberID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BidMemberID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BidAmount", SqlDbType.Decimal,9) ,           
                        new SqlParameter("@CouponCode", SqlDbType.VarChar,20) ,           
                        new SqlParameter("@message", SqlDbType.NVarChar,50)             
              
            };

            parameters[0].Value = model.LoanID;
            parameters[1].Value = model.LoanMemberID;
            parameters[2].Value = model.BidMemberID;
            parameters[3].Value = model.BidAmount;
            parameters[4].Value = model.CouponCode;
            parameters[5].Direction = ParameterDirection.Output;
            int num = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcInvest", parameters);
            message = parameters[5].Value.ToString();
            return num > 0;
        }
        /// <summary>
        /// 根据memberId获取优惠码
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public string GetCouponCodeByMemberId(int memberId)
        {
            string sql = "SELECT CouponCode FROM dbo.Coupon WHERE UseID=@MemberID";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MemberID", SqlDbType.Int, 4) {Value = memberId},
            };
            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            return result != null ? result.ToString() : "";
        }
        /// <summary>
        /// 查询IdentityNum表可用身份证条数
        /// </summary>
        /// <returns></returns>
        public int GetIdentityNumCount()
        {
            const string sql = "SELECT COUNT(*) FROM dbo.IdentityNum WHERE Status=0";
            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql);
            return Convert.ToInt32(result);
        }
        /// <summary>
        /// 获取IdentityNum表可用身份证datatable
        /// </summary>
        /// <returns></returns>
        public DataTable GetIdentityNumDt(int count)
        {
            string sql = "SELECT TOP " + count + " * FROM dbo.IdentityNum WHERE Status=0";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql);
            return ds != null ? ds.Tables[0] : null;
        }
        /// <summary>
        /// 修改为已使用
        /// </summary>
        /// <returns></returns>
        public bool UpdateIdentityNumByID(int ID)
        {
            const string sql = "UPDATE dbo.IdentityNum SET Status=1 WHERE ID=@ID";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID", SqlDbType.Int, 4) {Value = ID},
            };
            var result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            return result > 0;
        }
        /// <summary>
        /// 检测身份证是否存在
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns></returns>
        public bool IdCardExists(string idCard)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MemberInfo where IdentityCard=@IdentityCard");
            SqlParameter[] parameters =
            {
                new SqlParameter("@IdentityCard", SqlDbType.VarChar, 20) {Value = idCard}
            };
            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters));
            return result > 0;
        }
        #endregion

        #region 获取广告渠道列表
        /// <summary>
        /// 获取广告渠道列表
        /// </summary>
        /// <returns>渠道列表</returns>
        /// <remarks>add 卢侠勇,2015-08-27</remarks>
        public DataTable GetChannel()
        {
            string sql = "select * from DimChannel";
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, null).Tables[0];
        }
        #endregion

        #region 根据用户名获取用户信息
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="memberName">用户名</param>
        /// <returns>用户信息</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable Getmember(string memberName)
        {
            string sql = "SELECT * FROM Member M WHERE M.MemberName='" + memberName + "'";
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql).Tables[0];
        }
        #endregion
    }
}
