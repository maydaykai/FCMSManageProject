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
   public class CreditLineDAL
    {
       public static bool LoanCreditLineser(CreditLineModel creditLineModel)
       {
           SqlParameter[] parameters = {
                        new SqlParameter("@ID",SqlDbType.Int){Value = creditLineModel.ID}, 
                        new SqlParameter("@CreditLine", SqlDbType.Decimal){Value= creditLineModel.CreditLine},
                        new SqlParameter("@CardNumber", SqlDbType.NVarChar,10){Value= creditLineModel.CardNumber},
                        new SqlParameter("@CreditNumber", SqlDbType.NVarChar,20){Value= creditLineModel.CreditNumber},
                        new SqlParameter("@IdentityCard", SqlDbType.NVarChar,20){Value= creditLineModel.IdentityCard},
                        new SqlParameter("@OpUid",SqlDbType.Int){Value = creditLineModel.OpUid} 
                                            };
           var obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_LoanCreditLine", parameters);
           return obj > 0;

       }


       /// <summary>
       /// 获取信息
       /// </summary>
       /// <param name="id"></param>
       /// <param name="sql"></param>
       /// <returns></returns>
       public static DataTable GetUserInfo(int ID, string sql)
       {
           SqlParameter[] paras =
                {
                    new SqlParameter("@ID", SqlDbType.Int,4){Value=ID}
                };
           DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, paras);
           return ds != null ? ds.Tables[0] : null;
       }

        /// <summary>
        /// 数据库分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
       public static DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
       {
           int totalPage;
           const string tables = " CreditLine C";
           return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
       } 
       //查询学生贷分页信息
       public static DataTable Getpage_StudentList(string tableName,string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
       {

           int totalPage;
           //const string tables = " CreditLine C";
           return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tableName, filters, sortStr, currentPage, pageSize, out total, out totalPage);
       }


       /// <summary>
       /// 查询导出数据
       /// </summary>
       /// <param name="tableName"></param>
       /// <param name="fields"></param>
       /// <param name="filters"></param>
       /// <param name="sortStr"></param>
       /// <returns></returns>
       public static DataTable Getpage_StudentTable(string tableName, string fields, string filters, string sortStr)
       {
           StringBuilder sbSql2 = new StringBuilder();
           sbSql2.Append("select ").Append(fields).Append(" from ").Append(tableName).Append(" where ").Append(filters).Append(" order by ").Append(sortStr);
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

    }
}
