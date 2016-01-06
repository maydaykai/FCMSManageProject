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
    public class BidConfigDal
    {

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(BidConfigModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into BidConfig(");
            strSql.Append("LoanAmount,MinInvestment,MaxInvestment,CreateTime,UpdateTime,EnableStatus");
            strSql.Append(") values (");
            strSql.Append("@LoanAmount,@MinInvestment,@MaxInvestment,@CreateTime,@UpdateTime,@EnableStatus");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value=model.LoanAmount} ,            
                        new SqlParameter("@MinInvestment", SqlDbType.Decimal,9){Value=model.MinInvestment} ,            
                        new SqlParameter("@MaxInvestment", SqlDbType.Decimal,9){Value=model.MaxInvestment} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime} ,            
                        new SqlParameter("@EnableStatus", SqlDbType.Bit,1){Value=model.EnableStatus}                          
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }


        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(BidConfigModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update BidConfig set ");
            strSql.Append(" LoanAmount = @LoanAmount , ");
            strSql.Append(" MinInvestment = @MinInvestment , ");
            strSql.Append(" MaxInvestment = @MaxInvestment , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" EnableStatus = @EnableStatus  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9){Value = model.LoanAmount} ,            
                        new SqlParameter("@MinInvestment", SqlDbType.Decimal,9){Value = model.MinInvestment} ,            
                        new SqlParameter("@MaxInvestment", SqlDbType.Decimal,9){Value = model.MaxInvestment} ,                       
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@EnableStatus", SqlDbType.Bit,1){Value = model.EnableStatus}             
              
            };
            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BidConfigModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, LoanAmount, MinInvestment, MaxInvestment, CreateTime, UpdateTime, EnableStatus  ");
            strSql.Append("  from BidConfig ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};

            var model = new BidConfigModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoanAmount"].ToString() != "")
                {
                    model.LoanAmount = decimal.Parse(ds.Tables[0].Rows[0]["LoanAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinInvestment"].ToString() != "")
                {
                    model.MinInvestment = decimal.Parse(ds.Tables[0].Rows[0]["MinInvestment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxInvestment"].ToString() != "")
                {
                    model.MaxInvestment = decimal.Parse(ds.Tables[0].Rows[0]["MaxInvestment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnableStatus"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["EnableStatus"].ToString() == "1") || (ds.Tables[0].Rows[0]["EnableStatus"].ToString().ToLower() == "true"))
                    {
                        model.EnableStatus = true;
                    }
                    else
                    {
                        model.EnableStatus = false;
                    }
                }
                return model;
            }
            return null;
        }

        /// <summary>
        /// 返回所有投标额度设置
        /// </summary>
        /// <returns></returns>
        public DataSet GetBidConfigList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT [ID],[LoanAmount],[MinInvestment],[MaxInvestment],[CreateTime],[UpdateTime],[EnableStatus] FROM dbo.[BidConfig] ORDER BY LoanAmount asc");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

    }
}
