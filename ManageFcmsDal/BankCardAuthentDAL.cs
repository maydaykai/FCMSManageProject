using ManageFcmsConn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsModel;
using System.Data.SqlClient;

namespace ManageFcmsDal
{
    public class BankCardAuthentDAL
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " BankCardAuthent P LEFT JOIN dbo.Member M ON M.ID=P.MemberID LEFT JOIN Bank B on P.BankID=B.ID LEFT JOIN MemberInfo MI on M.ID=MI.MemberID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BankCardAuthentModel getBankCardAuthentModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, CreateTime, UpdateTime, VerTimes, BankName, BankCode, RealName, AuthentResult, EnglishName, MemberID, BankCardType, BankID, BankCardNo, Amount, sign, PayType, PayStatus  ");
            strSql.Append(" from vw_BankCardAuthent ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            BankCardAuthentModel model = new BankCardAuthentModel();
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
                if (ds.Tables[0].Rows[0]["VerTimes"].ToString() != "")
                {
                    model.VerTimes = int.Parse(ds.Tables[0].Rows[0]["VerTimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankName"].ToString() != "")
                {
                    model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BankCode"].ToString() != "")
                {
                    model.BankCode = ds.Tables[0].Rows[0]["BankCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AuthentResult"].ToString() != "")
                {
                    model.AuthentResult = int.Parse(ds.Tables[0].Rows[0]["AuthentResult"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnglishName"].ToString() != "")
                {
                    model.EnglishName = ds.Tables[0].Rows[0]["EnglishName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankID"].ToString() != "")
                {
                    model.BankID = int.Parse(ds.Tables[0].Rows[0]["BankID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankCardNo"].ToString() != "")
                {
                    model.BankCardNo = ds.Tables[0].Rows[0]["BankCardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sign"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["sign"].ToString() == "1") || (ds.Tables[0].Rows[0]["sign"].ToString().ToLower() == "true"))
                    {
                        model.sign = true;
                    }
                    else
                    {
                        model.sign = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["PayType"].ToString() != "")
                {
                    model.PayType = int.Parse(ds.Tables[0].Rows[0]["PayType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayStatus"].ToString() != "")
                {
                    model.PayStatus = int.Parse(ds.Tables[0].Rows[0]["PayStatus"].ToString());
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
        /// 认证满三次解锁
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool unlock(int ID)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE dbo.BankCardAuthent SET AuthentResult=-1 WHERE ID=@ID");
            SqlParameter[] parameters = {
                new SqlParameter("@ID", SqlDbType.Int,4){Value = ID}
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
        /// 重置认证状态
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Reset(int ID)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE dbo.BankCardAuthent SET AuthentResult=-2 WHERE ID=@ID");
            SqlParameter[] parameters = {
                new SqlParameter("@ID", SqlDbType.Int,4){Value = ID}
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
        /// BankID=更新的列
        /// ID、=判断条件
        /// 更新BankCardAccount表中的BankID时根据BankAccountAuthent表中的ID判断
        /// ll</summary>
        public void UpdaBankCardAccountByIDExits(int BankID, int ID)
        {
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append(" update dbo.BankCardAuthent set BankID=@BankID where EXISTS ");
            strwhere.Append(" (select 1 from  dbo.BankAccount_Authent where ID=@ID");
            strwhere.Append(" and BankCardAuthent.MemberID=BankAccount_Authent.MemberID");
            strwhere.Append(" and BankCardAuthent.BankCardNo=BankAccount_Authent.BankCardNo)");
            strwhere.Append(" and BankAccountType=2 ");
            SqlParameter[] parm = {
                         new SqlParameter("@BankID", SqlDbType.Int,4){Value= BankID},
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= ID}
                        };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strwhere.ToString(), parm);
        }
    }
}
