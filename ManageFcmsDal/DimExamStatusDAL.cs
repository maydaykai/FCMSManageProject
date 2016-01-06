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
    public class DimExamStatusDal
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DimExamStatusModel> GetDimExamStatusModelList()
        {
            var list = new List<DimExamStatusModel>();
            var strSql = new StringBuilder();
            strSql.Append("select *  from DimExamStatus ");
            //if (!string.IsNullOrEmpty(strWhere))
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
                while (dr.Read())
                {
                    DimExamStatusModel info = getDimExamStatusModelByDr(dr);
                    list.Add(info);
                }
                dr.Close();
            }
            catch
            {

            }
            return list;
        }
        private DimExamStatusModel getDimExamStatusModelByDr(SqlDataReader dr)
        {
            DimExamStatusModel model = new DimExamStatusModel();
            model.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            model.ExamStatusName = dr["ExamStatusName"] != DBNull.Value ? dr["ExamStatusName"].ToString() : "";
            return model;
        }


        /// <summary>
        /// 获取审核状态数据列表
        /// </summary>
        public DataSet GetDimExamStatusList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from dbo.DimExamStatus ");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }
    }
}
