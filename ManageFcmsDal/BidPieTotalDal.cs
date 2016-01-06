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
    public class BidPieTotalDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int typeId)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@TypeId", SqlDbType.Int){Value=typeId}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcTotalBidPie", parameters);
            return ds;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="typeId">当前类型Id</param>
        /// <param name="EnterType">输入类型 1：指定天数 如昨天 ，7天等 2.输入日期</param>
        /// <param name="BeginTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        public DataSet GetList_version_one(int typeId, int EnterType,  DateTime BeginTime, DateTime EndTime)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@TypeId", SqlDbType.Int){Value=typeId},
                new SqlParameter("@EnterType", SqlDbType.Int){Value=EnterType},
               // new SqlParameter("@DaysValues", SqlDbType.Int){Value=DaysValues},
                new SqlParameter("@BeginTime", SqlDbType.DateTime){Value=BeginTime},
                new SqlParameter("@EndTime", SqlDbType.DateTime){Value=EndTime}

            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "dbo.ProcTotalBidPie_version_01", parameters);
            return ds;
        }
    }
}
