using ManageFcmsConn;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsDal
{
    public class AreaDAL
    {
        /// <summary>
        /// 根据条件获取省市
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<AreaModel> getAreaModelList(string strWhere)
        {
            List<AreaModel> list = new List<AreaModel>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Area");
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql.Append(" where " + strWhere);
            }
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString());
                while (dr.Read())
                {
                    AreaModel info = getAreaModelByDr(dr);
                    list.Add(info);
                }
                dr.Close();
            }
            catch
            {

            }
            return list;
        }

        private AreaModel getAreaModelByDr(SqlDataReader dr)
        {
            AreaModel info = new AreaModel();
            info.ID = Convert.ToInt32(dr["ID"]);
            info.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : "";
            info.ParentID = dr["ParentID"] != DBNull.Value ? Convert.ToInt32(dr["ParentID"]) : 0;
            return info;
        }


        public DataTable GetAreList(string strWhere)
        {
            DataTable table = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT a.ID,a.Name AS CityName,b.Name AS ProvinceName FROM dbo.Area a INNER JOIN ( SELECT Name,ID FROM dbo.Area  WHERE ParentID=1) b ON a.ParentID=b.ID");
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql.Append(" where " + strWhere);
            }
            try
            {
                table = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString()).Tables[0];
            }
            catch
            {

            }
            return table;
        }
        
    }
}
