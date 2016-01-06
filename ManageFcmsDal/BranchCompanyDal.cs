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
    public class BranchCompanyDal
    {
        #region 分公司管理
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddBranchCompany(BranchCompanyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DimBranchCompany(");
            strSql.Append("Name,SetUpDate,Remark");
            strSql.Append(") values (");
            strSql.Append("@Name,@SetUpDate,@Remark");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Name", SqlDbType.NVarChar,50){Value= model.Name},
                        new SqlParameter("@SetUpDate", SqlDbType.Date){Value= model.SetUpDate},
                        new SqlParameter("@Remark", SqlDbType.NVarChar,500){Value= model.Remark},
                        };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBranchCompany(BranchCompanyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DimBranchCompany set ");

            strSql.Append(" Name = @Name , ");
            strSql.Append(" SetUpDate = @SetUpDate , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int){Value= model.ID},
			            new SqlParameter("@Name", SqlDbType.NVarChar,50){Value= model.Name},
                        new SqlParameter("@SetUpDate", SqlDbType.Date){Value= model.SetUpDate},
                        new SqlParameter("@Remark", SqlDbType.NVarChar,500){Value= model.Remark},
                        };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BranchCompanyModel GetBranchCompanyModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, Name, SetUpDate, Remark  ");
            strSql.Append(" from DimBranchCompany ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            BranchCompanyModel model = new BranchCompanyModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SetUpDate"].ToString() != "")
                {
                    model.SetUpDate = DateTime.Parse(ds.Tables[0].Rows[0]["SetUpDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                #endregion
                return model;
            }
            return null;
        }
        /// <summary>
        /// 获取所有分公司
        /// </summary>
        /// <returns></returns>
        public DataSet GetBranchCompanyList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT [ID],[Name],[SetUpDate],[Remark] FROM [dbo].[DimBranchCompany] ORDER BY SetUpDate asc");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }
        #endregion
        #region 分公司账号
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BranchCompanyMemberModel GetBranchCompanyMemberModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, MemberID, BranchCompanyID, CreateTime  ");
            strSql.Append(" from BranchCompanyMember ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            BranchCompanyMemberModel model = new BranchCompanyMemberModel();
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
                if (ds.Tables[0].Rows[0]["BranchCompanyID"].ToString() != "")
                {
                    model.BranchCompanyID = int.Parse(ds.Tables[0].Rows[0]["BranchCompanyID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                #endregion
                return model;
            }
            return null;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddBranchCompanyMember(BranchCompanyMemberModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BranchCompanyMember(");
            strSql.Append("MemberID,BranchCompanyID,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@BranchCompanyID,@CreateTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int,4){Value= model.MemberID},
                        new SqlParameter("@BranchCompanyID", SqlDbType.Int,4){Value= model.BranchCompanyID},
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value= model.CreateTime},
                        };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            //修改该用户  不允许提现
            const string sql = "UPDATE dbo.Member SET AllowWithdraw=0 WHERE ID=@MemberID";
            SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 获取锁定金额(属于分公司的账号)
        /// </summary>
        /// <returns></returns>
        public decimal GetBranchCompanyLockAmount(int MemberID)
        {
            const string sql = "SELECT ISNULL(SUM(L.LoanAmount * G.Scale / 100 / B.Times),0) FROM dbo.GreenChannelRecord G INNER JOIN BranchCompanyMember B ON(G.BranchCompanyID=B.MemberID) LEFT JOIN dbo.Loan L ON G.LoanID=L.ID WHERE G.BranchCompanyID=@MemberID AND L.ExamStatus IN (1,3,5,7,9)";
            SqlParameter[] paras = {
                new SqlParameter("@MemberID",SqlDbType.Int){Value=MemberID},
            };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras);
            return obj != null ? Convert.ToDecimal(obj) : 0;
        }
        #endregion
    }
}
