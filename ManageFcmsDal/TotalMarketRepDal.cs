using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;

namespace ManageFcmsDal
{
    public class TotalMarketRepDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime date1, DateTime date2)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@date1", SqlDbType.DateTime){Value=date1},
                new SqlParameter("@date2", SqlDbType.DateTime){Value=date2}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcTotalMarketRep", parameters);
            return ds;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="nexttime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public DataSet GetListNew(DateTime date1, DateTime date2)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@date1", SqlDbType.DateTime),
                new SqlParameter("@date2", SqlDbType.DateTime)
                

            };

            parameters[0].Value = date1;
            parameters[1].Value = date2;
            //parameters[2].Value = nexttime;
            //parameters[3].Value = pageIndex;
            //parameters[4].Value = pageSize;
            //parameters[5].Direction = ParameterDirection.Output;
            //修改ProcTotalMarketRep_Marking
            // DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcTotalMarketRep", parameters);
            string sql = "SELECT * FROM Marking_RoleTable WHERE CONVERT(VARCHAR(10),Createtime,120)>=@date1 AND CONVERT(VARCHAR(10),Createtime,120)<=@date2  ORDER BY RealName desc ";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql, parameters);
            //TotalRows = Convert.ToInt32(parameters[5].Value);
            return ds;
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="nexttime"></param>
        /// <returns></returns>
        public DataSet GetExcelTable(string date1, string date2, string nexttime)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@date1", SqlDbType.VarChar,20),
                new SqlParameter("@date2", SqlDbType.VarChar,20),
                new SqlParameter("@nexttime", SqlDbType.VarChar,20)

            };

            parameters[0].Value = date1;
            parameters[1].Value = date2;
            parameters[2].Value = nexttime;
            //修改ProcTotalMarketRep_Marking
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "ProcTotalMarketRep_Marking_new1", parameters);
            return ds;
        }

        /// <summary>
        /// 查询当前用户的明细
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="nexttime"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public DataSet GetListNewByMemberId(DateTime date1, DateTime date2, string nexttime, string memberName)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@date1", SqlDbType.DateTime),
                new SqlParameter("@date2", SqlDbType.DateTime),
                new SqlParameter("@nexttime", SqlDbType.VarChar,20),
                new SqlParameter("@memberName", SqlDbType.VarChar,20)

            };

            parameters[0].Value = date1;
            parameters[1].Value = date2;
            parameters[2].Value = nexttime;
            parameters[3].Value = memberName;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_TotalMarketRep_Marking_Details", parameters);
            return ds;
        }

    }
}
