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
    public class BankAccount_BLDAL
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BankAccount_BLModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select BL.Id, BL.BankID, BL.AccountHolder, BL.MemberType, BL.BankCardNo, BL.CreateTime,B.BankName,B.BankCode,B.EnglishName ");
            strSql.Append("  from BankAccount_BL BL INNER JOIN dbo.Bank B ON BL.BankID=B.ID ");
            strSql.Append(" WHERE BL.Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4){Value = id}
			};

            var model = new BankAccount_BLModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankID"].ToString() != "")
                {
                    model.BankID = int.Parse(ds.Tables[0].Rows[0]["BankID"].ToString());
                }
                model.AccountHolder = ds.Tables[0].Rows[0]["AccountHolder"].ToString();
                model.BankCardNo = ds.Tables[0].Rows[0]["BankCardNo"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                model.BankCode = ds.Tables[0].Rows[0]["BankCode"].ToString();
                model.EnglishName = ds.Tables[0].Rows[0]["EnglishName"].ToString();
                if (ds.Tables[0].Rows[0]["MemberType"].ToString() != "")
                {
                    model.MemberType = int.Parse(ds.Tables[0].Rows[0]["MemberType"].ToString());
                }

                return model;
            }
            return null;
        }
    }
}
