using System.Data.SqlClient;
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
    public class BankAccountDal
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " BankAccount P left join Bank B on P.BankID=B.ID left join Member M on P.MemberID=M.ID left join MemberInfo MI on P.MemberID=MI.MemberID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BankAccountModel getBankAccountModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 BA.*,B.BankName,B.BankCode,B.EnglishName,M.MemberName,MI.RealName");
            strSql.Append(" from BankAccount BA left join Bank B on BA.BankID=B.ID left join Member M on BA.MemberID=M.ID left join MemberInfo MI on BA.MemberID=MI.MemberID");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            BankAccountModel model = new BankAccountModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
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
                if (ds.Tables[0].Rows[0]["BankCardType"].ToString() != "")
                {
                    model.BankCardType = int.Parse(ds.Tables[0].Rows[0]["BankCardType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankID"].ToString() != "")
                {
                    model.BankID = ds.Tables[0].Rows[0]["BankID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BankCardNo"].ToString() != "")
                {
                    model.BankCardNo = ds.Tables[0].Rows[0]["BankCardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.AccountHolder = ds.Tables[0].Rows[0]["RealName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MemberName"].ToString() != "")
                {
                    model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BankName"].ToString() != "")
                {
                    model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BankCode"].ToString() != "")
                {
                    model.BankCode = ds.Tables[0].Rows[0]["BankCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EnglishName"].ToString() != "")
                {
                    model.EnglishName = ds.Tables[0].Rows[0]["EnglishName"].ToString();
                }
                #endregion
                return model;
            }
            return null;
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BankAccountAuthentModel GetBankAccountAuthentModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 BA.ID, BA.MemberID, BA.BankCardNo, BA.BankCode, BA.ProvinceID, BA.CityID, BA.BranchBankName,BA.Prcptcd, BA.Status, BA.CreateTime, BA.UpdateTime, B.BankName, B.EnglishName  ");
            strSql.Append(" from BankAccount_Authent BA left join Bank B on BA.BankCode=B.BankCode");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            BankAccountAuthentModel model = new BankAccountAuthentModel();
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
                if (ds.Tables[0].Rows[0]["BankCardNo"].ToString() != "")
                {
                    model.BankCardNo = ds.Tables[0].Rows[0]["BankCardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BankCode"].ToString() != "")
                {
                    model.BankCode = ds.Tables[0].Rows[0]["BankCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProvinceID"].ToString() != "")
                {
                    model.ProvinceID = ds.Tables[0].Rows[0]["ProvinceID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CityID"].ToString() != "")
                {
                    model.CityID = ds.Tables[0].Rows[0]["CityID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BranchBankName"].ToString() != "")
                {
                    model.BranchBankName = ds.Tables[0].Rows[0]["BranchBankName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Prcptcd"].ToString() != "")
                {
                    model.Prcptcd = ds.Tables[0].Rows[0]["Prcptcd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankName"].ToString() != "")
                {
                    model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EnglishName"].ToString() != "")
                {
                    model.EnglishName = ds.Tables[0].Rows[0]["EnglishName"].ToString();
                }
                #endregion
                return model;
            }
            return null;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBankAccountAuthent(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.BankAccount_Authent set ");
            strSql.Append(" [Status] = 3 ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= ID}
                        };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// id=会员ID
        /// BankCode=银行名称编号
        /// 根据会员Id修改会员信息 
        /// </summary>
        /// ll<returns></returns>
        public bool UpdBankAccountAuthentById(int id, int BankCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.BankAccount_Authent set ");
            strSql.Append(" [BankCode] = @BankCode");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BankCode", SqlDbType.Int,4){Value= BankCode},
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= id}
                        };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters)>0;
        }
      
    }
}
