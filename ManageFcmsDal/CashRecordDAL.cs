using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using LLYTPay;
using Newtonsoft.Json;
using com.aipg;
using com.aipg.package;
using ManageFcmsCommon;
using ManageFcmsCommon.TongLian;
using ManageFcmsCommon.LianLian;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class CashRecordDAL
    {
        private static object lockobj = new object();
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " CashRecord C left join BankAccount BA on C.BankAccountID=BA.ID left join Bank B on BA.BankID=B.ID left join Member M on C.MemberID=M.ID left join MemberInfo MI on C.MemberID=MI.MemberID ";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CashRecordModel getCashRecordModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, REQ_SN, RequestXml, ResponseXml, AuditRecords, Remark, ApplicationTime, UpdateTime, MemberID, Type, CashMode, BankAccountID, CashAmount, CashFee, Status, CashStatus , ApplyReason, BankAccountType, CashPayType,WarningRecord,WarningStatus ");
            strSql.Append(" from CashRecord ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            CashRecordModel model = new CashRecordModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["REQ_SN"].ToString() != "")
                {
                    model.REQ_SN = ds.Tables[0].Rows[0]["REQ_SN"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RequestXml"].ToString() != "")
                {
                    model.RequestXml = ds.Tables[0].Rows[0]["RequestXml"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ResponseXml"].ToString() != "")
                {
                    model.ResponseXml = ds.Tables[0].Rows[0]["ResponseXml"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AuditRecords"].ToString() != "")
                {
                    model.AuditRecords = ds.Tables[0].Rows[0]["AuditRecords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ApplicationTime"].ToString() != "")
                {
                    model.ApplicationTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplicationTime"].ToString());
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
                if (ds.Tables[0].Rows[0]["CashMode"].ToString() != "")
                {
                    model.CashMode = int.Parse(ds.Tables[0].Rows[0]["CashMode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankAccountID"].ToString() != "")
                {
                    model.BankAccountID = int.Parse(ds.Tables[0].Rows[0]["BankAccountID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CashAmount"].ToString() != "")
                {
                    model.CashAmount = decimal.Parse(ds.Tables[0].Rows[0]["CashAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CashFee"].ToString() != "")
                {
                    model.CashFee = decimal.Parse(ds.Tables[0].Rows[0]["CashFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CashStatus"].ToString() != "")
                {
                    model.CashStatus = int.Parse(ds.Tables[0].Rows[0]["CashStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApplyReason"].ToString() != "")
                {
                    model.ApplyReason = ds.Tables[0].Rows[0]["ApplyReason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BankAccountType"].ToString() != "")
                {
                    model.BankAccountType = int.Parse(ds.Tables[0].Rows[0]["BankAccountType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CashPayType"].ToString() != "")
                {
                    model.CashPayType = int.Parse(ds.Tables[0].Rows[0]["CashPayType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WarningRecord"].ToString() != "")
                {
                    model.WarningRecord = ds.Tables[0].Rows[0]["WarningRecord"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WarningStatus"].ToString() != "")
                {
                    model.WarningStatus = int.Parse(ds.Tables[0].Rows[0]["WarningStatus"].ToString());
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
        public bool updateCashRecord(CashRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CashRecord set ");

            strSql.Append(" AuditRecords += @AuditRecords , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" CashPayType = @CashPayType , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" Status = @Status ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= model.ID},
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500){Value= model.AuditRecords},
                        new SqlParameter("@Remark", SqlDbType.NVarChar,500){Value= model.Remark},
                        new SqlParameter("@CashPayType", SqlDbType.Int,4){Value= model.CashPayType},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= DateTime.Now},
                        new SqlParameter("@Status", SqlDbType.Int,4){Value= model.Status},
                        };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 更新一条数据(请求报文)
        /// </summary>
        public bool updateCashRecord(int ID, string reqXml)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CashRecord set ");

            strSql.Append(" RequestXml = @RequestXml , ");
            strSql.Append(" UpdateTime = @UpdateTime ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= ID},
                        new SqlParameter("@RequestXml", SqlDbType.NVarChar,-1){Value= reqXml},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= DateTime.Now},
                        };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 提现失败处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool withdrawCashAuditFail(CashRecordModel model)
        {
            SqlParameter[] paras =
            {
                new SqlParameter("@ID", SqlDbType.Int,4){Value= model.ID},
                new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500){Value= model.AuditRecords},
                new SqlParameter("@Remark", SqlDbType.NVarChar,500){Value= model.Remark},
                new SqlParameter("@Status", SqlDbType.Int,4){Value= model.Status},
                new SqlParameter("@ret",SqlDbType.Int,4){Direction = ParameterDirection.ReturnValue} //定义返回值参数
            };
            string sql = "[dbo].[Proc_WithdrawFail]";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, sql, paras);
            int id = Convert.ToInt32(paras[3].Value);
            return id > 0;
        }
        /// <summary>
        /// 复审通过
        /// </summary>
        /// <param name="single"></param>
        /// <returns></returns>
        public bool withdrawCashAuditSuccess(CashRecordModel model, ref string message)
        {
            lock (lockobj)
            {
                try
                {
                    MemberModel memberModel = new MemberDal().GetModel(model.MemberID);
                    MemberInfoModel memberInfoModel = new MemberInfoDal().GetModel(model.MemberID);
                    
                    DetailRequest pbObj = new DetailRequest();
                    if (model.CashMode == 0)
                    {
                        if (model.BankAccountType == 1)//通联
                        {
                            BankAccountModel bankAccountModel =
                                new BankAccountDal().getBankAccountModel("BA.ID=" + model.BankAccountID);
                            if (bankAccountModel != null)
                            {
                                pbObj.AccountNo = bankAccountModel.BankCardNo;
                                pbObj.AccountName = bankAccountModel.AccountHolder;
                                pbObj.BankName = bankAccountModel.BankName;
                                pbObj.BankCode = bankAccountModel.BankCode;
                            }
                        }else if (model.BankAccountType == 2)//连连
                        {
                            BankAccountAuthentModel bankAccountAuthentModel = new BankAccountDal().GetBankAccountAuthentModel("BA.ID=" + model.BankAccountID);
                            if (bankAccountAuthentModel != null)
                            {
                                pbObj.AccountNo = bankAccountAuthentModel.BankCardNo;
                                pbObj.AccountName = memberInfoModel.RealName;
                                pbObj.BankName = bankAccountAuthentModel.BankName;
                                pbObj.BankCode = bankAccountAuthentModel.BankCode;
                            }
                        }
                        pbObj.AccountProp = memberModel.Type == 0 ? AccountProp.Personal : AccountProp.Enterprise;
                    }
                    else
                    {
                        BankAccount_BLModel bankAccountModel = new BankAccount_BLDAL().GetModel(model.BankAccountID);
                        if (bankAccountModel != null)
                        {
                            pbObj.AccountNo = bankAccountModel.BankCardNo;
                            pbObj.AccountName = bankAccountModel.AccountHolder;
                            pbObj.BankName = bankAccountModel.BankName;
                            pbObj.BankCode = bankAccountModel.BankCode;
                            pbObj.AccountProp = bankAccountModel.MemberType == 0 ? AccountProp.Personal : AccountProp.Enterprise;
                        }
                    }
                    pbObj.Sn = "0001";
                    pbObj.AccountType = AccountType.Card;
                    pbObj.Amount = Convert.ToInt64((model.CashAmount * 100).ToString("F0"));
                    pbObj.Currency = Currency.CNY;
                    pbObj.Remark = model.TypeStr;
                    string reqXml = "";
                    Response resp = WithdrawCash.WithdrawCashSync(pbObj, model.REQ_SN, ref reqXml);
                    if (!string.IsNullOrEmpty(reqXml) && updateCashRecord(model.ID, reqXml) && resp != null)
                    {
                        return HandleRetResult(resp, model, ref message);
                    }
                }
                catch (Exception e)
                {
                    Log4NetHelper.WriteError(e);
                }
                return false;
            }
        }
        /// <summary>
        /// 复审通过
        /// </summary>
        /// <param name="single"></param>
        /// <returns></returns>
        public bool withdrawCashAuditSuccessByLL(CashRecordModel model, ref string message)
        {
            lock (lockobj)
            {
                try
                {
                    MemberModel memberModel = new MemberDal().GetModel(model.MemberID);
                    MemberInfoModel memberInfoModel = new MemberInfoDal().GetModel(model.MemberID);

                    if (!(model.CashMode == 0 && model.BankAccountType == 2)) //通联认证的银行卡跟线下提现不能用连连支付
                    {
                        message = "该笔提现不能使用连连支付";
                        return false;
                    }
                    BankAccountAuthentModel bankAccountAuthentModel = new BankAccountDal().GetBankAccountAuthentModel("BA.ID=" + model.BankAccountID);
                    if (bankAccountAuthentModel == null)
                    {
                        message = "未查找到该账户";
                        return false;
                    }
                    SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                    //基本参数
                    sParaTemp.Add("oid_partner", ConfigHelper.OID_PARTNER);
                    sParaTemp.Add("api_version", ConfigHelper.VERSION);
                    sParaTemp.Add("info_order", "会员提现-连连支付");
                    sParaTemp.Add("notify_url", ConfigHelper.NOTIFY_URL);
                    sParaTemp.Add("sign_type", ConfigHelper.SIGN_TYPE);
                    //额外参数
                    sParaTemp.Add("flag_card", memberModel.Type.ToString());//0-对私 1 –对公
                    sParaTemp.Add("acct_name", memberInfoModel.RealName);
                    sParaTemp.Add("card_no", bankAccountAuthentModel.BankCardNo);
                    sParaTemp.Add("no_order", model.REQ_SN);
                    sParaTemp.Add("dt_order", YinTongUtil.getCurrentDateTimeStr());
                    sParaTemp.Add("money_order",model.CashAmount.ToString("F2"));
                    sParaTemp.Add("city_code", bankAccountAuthentModel.CityID);
                    sParaTemp.Add("bank_code", bankAccountAuthentModel.BankCode);
                    sParaTemp.Add("brabank_name", bankAccountAuthentModel.BranchBankName);
                    string sign = YinTongUtil.addSign(sParaTemp, ConfigHelper.TRADER_PRI_KEY, ConfigHelper.MD5_KEY);
                    sParaTemp.Add("sign", sign);
                    string reqJson = YinTongUtil.dictToJson(sParaTemp);
                    updateCashRecord(model.ID, reqJson);
                    string responseJson = WithdrawCashLL.PostJson(ConfigHelper.CASHPAY_URL, reqJson);
                    UpdateTableValue("dbo.CashRecord", "CashPayType=2,ResponseXml=" + responseJson, "ID=" + model.ID);
                    ResponseLLModel resp = JsonConvert.DeserializeObject<ResponseLLModel>(responseJson);
                    
                    if (!string.IsNullOrEmpty(reqJson) && !string.IsNullOrEmpty(responseJson))
                    {
                        if ("0000".Equals(resp.ret_code))
                        {
                            SqlParameter[] paras =
                            {
                                new SqlParameter("@ID", SqlDbType.Int, 4) {Value = model.ID},
                                new SqlParameter("@ResponseXml", SqlDbType.NVarChar,-1) {Value = resp.ret_msg},
                                new SqlParameter("@Remark", SqlDbType.NVarChar,500) {Value = model.Remark ?? ""},
                                new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500) {Value = model.AuditRecords},
                                new SqlParameter("@ret",SqlDbType.Int,4){Direction = ParameterDirection.ReturnValue} //定义返回值参数
                            };
                            string sql = "[dbo].[Proc_WithdrawSuccess]";
                            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, sql, paras);
                            int id = Convert.ToInt32(paras[4].Value);
                            return id > 0;
                        }
                        message = resp.ret_msg;

                    }
                }
                catch (Exception e)
                {
                    Log4NetHelper.WriteError(e);
                }
                return false;
            }
        }
        /// <summary>
        /// 使用单笔实时支付订单
        /// </summary>
        /// <param name="single"></param>
        /// <returns></returns>
        public bool withdrawCashAuditSuccessByQuick(CashRecordModel model, ref string message)
        {
            lock (lockobj)
            {
                try
                {
                    MemberModel memberModel = new MemberDal().GetModel(model.MemberID);
                    MemberInfoModel memberInfoModel = new MemberInfoDal().GetModel(model.MemberID);

                    SingleTradePackage single = new SingleTradePackage();
                    if (model.CashMode == 0)
                    {
                        if (model.BankAccountType == 1)//通联
                        {
                            BankAccountModel bankAccountModel =
                                new BankAccountDal().getBankAccountModel("BA.ID=" + model.BankAccountID);
                            if (bankAccountModel != null)
                            {
                                single.AccountNo = bankAccountModel.BankCardNo;
                                single.AccountName = bankAccountModel.AccountHolder;
                                single.BankName = bankAccountModel.BankName;
                                single.BankCode = bankAccountModel.BankCode;
                            }
                        }
                        else if (model.BankAccountType == 2)//连连
                        {
                            BankAccountAuthentModel bankAccountAuthentModel = new BankAccountDal().GetBankAccountAuthentModel("BA.ID=" + model.BankAccountID);
                            if (bankAccountAuthentModel != null)
                            {
                                single.AccountNo = bankAccountAuthentModel.BankCardNo;
                                single.AccountName = memberInfoModel.RealName;
                                single.BankName = bankAccountAuthentModel.BankName;
                                single.BankCode = bankAccountAuthentModel.BankCode;
                            }
                        }
                        single.AccountProp = memberModel.Type.ToString();
                    }
                    else
                    {
                        BankAccount_BLModel bankAccountModel = new BankAccount_BLDAL().GetModel(model.BankAccountID);
                        if (bankAccountModel != null)
                        {
                            single.AccountNo = bankAccountModel.BankCardNo;
                            single.AccountName = bankAccountModel.AccountHolder;
                            single.BankName = bankAccountModel.BankName;
                            single.BankCode = bankAccountModel.BankCode;
                            single.AccountProp = bankAccountModel.MemberType.ToString();
                        }
                    }
                    single.Sn = "0001";
                    single.Amount = Convert.ToInt64((model.CashAmount * 100).ToString("F0"));
                    single.Remark = model.TypeStr;
                    string reqXml = "";
                    Response resp = WithdrawCash.WithdrawCashSync(single, model.REQ_SN, ref reqXml);
                    if (!string.IsNullOrEmpty(reqXml) && updateCashRecord(model.ID, reqXml) && resp != null)
                    {
                        return HandleRetResult(resp, model, ref message);
                    }
                }
                catch (Exception e)
                {
                    Log4NetHelper.WriteError(e);
                }
                return false;
            }
        }
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <returns></returns>
        public ResponseLLModel GetLLBalance()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            //基本参数
            sParaTemp.Add("oid_partner", ConfigHelper.OID_PARTNER);
            sParaTemp.Add("api_version", ConfigHelper.VERSION);
            sParaTemp.Add("sign_type", ConfigHelper.SIGN_TYPE);
            string sign = YinTongUtil.addSign(sParaTemp, ConfigHelper.TRADER_PRI_KEY, ConfigHelper.MD5_KEY);
            sParaTemp.Add("sign", sign);
            string reqJson = YinTongUtil.dictToJson(sParaTemp);
            string responseJson = WithdrawCashLL.PostJson(ConfigHelper.QUERYBALANCE_URL, reqJson);
            return JsonConvert.DeserializeObject<ResponseLLModel>(responseJson);
        }
        private bool HandleRetResult(Response resp, CashRecordModel model, ref string message)
        {
            if (resp.RetCode.Equals("0000"))
            {
                SqlParameter[] paras =
                {
                    new SqlParameter("@ID", SqlDbType.Int, 4) {Value = model.ID},
                    new SqlParameter("@ResponseXml", SqlDbType.NVarChar,-1) {Value = resp.ToXML()},
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500) {Value = model.Remark ?? ""},
                    new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500) {Value = model.AuditRecords},
                    new SqlParameter("@ret",SqlDbType.Int,4){Direction = ParameterDirection.ReturnValue} //定义返回值参数
                };
                const string sql = "[dbo].[Proc_WithdrawSuccess]";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, sql, paras);
                int id = Convert.ToInt32(paras[4].Value);
                return id > 0;
            }
            message = resp.ErrMsg;
            Log4NetHelper.WriteLog(string.Format("交易流水号：{0}   返回码：{1}   错误文本：{2}", model.REQ_SN, resp.RetCode, resp.ErrMsg));
            return false;
        }
        /// <summary>
        /// 线下提现申请
        /// </summary>
        public object BelowCashApply(int memberId, string accountHolder, int bankId, string bankAccount, decimal cashAmount)
        {
            SqlParameter[] parameters = {                       
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value=memberId} ,                                 
                        new SqlParameter("@AccountHolder", SqlDbType.NVarChar,20){Value=accountHolder} ,            
                        new SqlParameter("@BankId", SqlDbType.Int,4){Value=bankId},
                        new SqlParameter("@BankAccount", SqlDbType.VarChar,30){Value=bankAccount} , 
                        new SqlParameter("@CashAmount", SqlDbType.Decimal){Value=cashAmount} ,
                        new SqlParameter("@ApplyReason",SqlDbType.NVarChar,500){Value = ""}
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_BelowCashApply", parameters);
            return obj;
        }

        /// <summary>
        /// 更新数据(线下提现[初审通过])
        /// </summary>
        public bool BelowCashCheckPass(CashRecordModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update CashRecord set ");
            strSql.Append(" AuditRecords = @AuditRecords , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" Status = @Status ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= model.ID},
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500){Value= model.AuditRecords},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= DateTime.Now},
                        new SqlParameter("@Status", SqlDbType.Int,4){Value= model.Status}
            };

            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 更新数据(线下提现[复审通过])
        /// </summary>
        public object BelowCashReCheckPass(CashRecordModel model)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@CashRecordID", SqlDbType.Int,4){Value= model.ID},
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500){Value= model.AuditRecords}
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_BelowCashReCheckPass", parameters);
            return obj;
        }

        /// <summary>
        /// 更新数据(线下提现[初审不通过/复审不通过])
        /// </summary>
        public object BelowCashNotPass(CashRecordModel model)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@CashRecordID", SqlDbType.Int,4){Value= model.ID},
                        new SqlParameter("@AuditRecords", SqlDbType.NVarChar,500){Value= model.AuditRecords},
                        new SqlParameter("@Status", SqlDbType.Int,4){Value= model.Status}
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_BelowCashNotPass", parameters);
            return obj;
        }

        /// <summary>
        /// 总计
        /// </summary>
        public object Aggregate(string filters)
        {
            string strSql = "Select SUM(CashAmount) FROM dbo.CashRecord C left join Member M on C.MemberID=M.ID where" + filters;

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
        }
        /// <summary>
        /// 提现成功处理(通联轮询/连连异步通知) 表示最终成功
        /// </summary>
        /// <param name="reqSn"></param>
        /// <returns></returns>
        public bool WithdrawCheckSuccess(string reqSn)
        {
            SqlParameter[] paras = {
                        new SqlParameter("@REQ_SN", SqlDbType.VarChar,50){Value= reqSn}
                        };
            string sql = "[dbo].[Proc_WithdrawCheckSuccess]";
            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, sql, paras);
            return result != null && Convert.ToInt32(result.ToString()) > 0;
        }
        /// <summary>
        /// 连连支付提现成功处理+1
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public bool WithdrawSuccessByLL(int MemberID)
        {
            const string sql = "IF((SELECT [Status] FROM dbo.BankAccount_Authent WHERE MemberID=@MemberID AND [Status] < 3) <> 2) BEGIN UPDATE dbo.BankAccount_Authent SET [Status]=2 WHERE MemberID=@MemberID AND [Status] < 3 END";
            SqlParameter[] parameters = new[] 
            {
                new SqlParameter("@MemberID", SqlDbType.Int, 4){Value=MemberID}
            };
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            return rows > 0;
        }
        /// <summary>
        /// 提现失败处理(通联轮询/连连异步通知) 表示最终失败
        /// </summary>
        /// <param name="reqSn"></param>
        /// <returns></returns>
        public bool WithdrawCheckFail(string reqSn)
        {
            SqlParameter[] paras = {
                        new SqlParameter("@REQ_SN", SqlDbType.VarChar,50){Value= reqSn}
                        };
            string sql = "[dbo].[Proc_WithdrawCheckFail]";
            var result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, sql, paras);
            return result != null && Convert.ToInt32(result.ToString()) > 0;
        }
        /// <summary>
        /// 根据条件更新数据库表的内容
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="SetContent">更新内容</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public bool UpdateTableValue(string TableName, string SetContent, string strWhere)
        {
            try
            {
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@TableName", SqlDbType.VarChar, 80),
                    new SqlParameter("@SetContent", SqlDbType.VarChar, 8000),
                    new SqlParameter("@strWhere", SqlDbType.VarChar, 8000),
		        };
                paras[0].Value = TableName;
                paras[1].Value = SetContent;
                paras[2].Value = strWhere;
                int returnval = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.Bas_UpdateTable", paras);
                return returnval > 0;
            }
            catch
            {
                return false;
            }
        }

        #region 处理提现预警状态
        /// <summary>
        /// 处理提现预警状态
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <param name="status">状态：2存疑 3通过</param>
        /// <param name="note">备注</param>
        /// <param name="memberId">操作用户ID</param>
        /// <returns>true false</returns>
        /// <remarks>add 卢侠勇,2015-09-22</remarks>
        public bool ModifyWithdrawWarning(int id, int status, string note, int memberId)
        {
            try
            {
                //string sql = "UPDATE CashRecord SET UpdateTime=GETDATE(),WarningStatus=@status,WarningNote=@note,WarningRecord+=''+case when @status=2 then '存疑' else '通过' end+'-'+(select UserName from FcmsUser where ID=@memberId)+'-'+CONVERT(CHAR(19),GETDATE(),120)+'-备注:'+@note+'|' WHERE ID=@id";
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@id", SqlDbType.Int){Value=id},
                    new SqlParameter("@status", SqlDbType.Int){Value=status},
                    new SqlParameter("@note", SqlDbType.NVarChar,500){Value=note},
                    new SqlParameter("@userId", SqlDbType.Int){Value=memberId}
		        };
                return Convert.ToBoolean(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_WithdrawWarning", paras));
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
