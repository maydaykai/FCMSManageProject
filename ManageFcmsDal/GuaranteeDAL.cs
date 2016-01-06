using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    //Guarantee
    public partial class GuaranteeDal
    {
        public List<GuaranteeModel> GetGuaranteeListModel()
        {
            var list = new List<GuaranteeModel>();
            string sql2 = "select RelationID,RealName as GuaranteeName from FcmsUser where parentID=0 and charindex(','+ltrim(11)+',',','+RoleID+',')>0 AND IsLock=0";
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                GuaranteeModel info = getGuaranteeListModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private GuaranteeModel getGuaranteeListModelByDr(SqlDataReader dr)
        {
            var model = new GuaranteeModel();
            model.ID = dr["RelationID"] != DBNull.Value ? Convert.ToInt32(dr["RelationID"]) : 0;
            model.GuaranteeName = dr["GuaranteeName"] != DBNull.Value ? dr["GuaranteeName"].ToString() : "";

            return model;
        }

        public DataSet GetGuaranteeDataSet()
        {
            var strSql = new StringBuilder();
            strSql.Append("select RelationID,RealName as GuaranteeName from FcmsUser where parentID=0 and charindex(','+ltrim(8)+',',','+RoleID+',')>0 AND IsLock=0");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 担保公司剩余应还本金
        /// </summary>
        /// <returns></returns>
        public DataSet GuaranteeRePrincipalTotal()
        {
            string str = "select mi.RealName,SUM(r.RePrincipal) as RePrincipal from dbo.RepaymentPlan r inner join dbo.loan l on r.loanid=l.id inner join dbo.MemberInfo mi on l.GuaranteeID=mi.MemberID where r.Status<2 and r.OverStatus<>5 group by mi.RealName";
            return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, str);
        }

    }
}
