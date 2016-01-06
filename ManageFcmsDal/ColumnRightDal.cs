using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsCommon;

namespace ManageFcmsDal
{
    public class ColumnRightDal
    {
        public string GetColumnRightStr(int columnId)
        {
            string rightStr = string.Empty;
            var strSql = new StringBuilder();
            strSql.Append("select RightID ");
            strSql.Append(" FROM [ColumnRight] where ColumnID=" + columnId);

            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (rightStr.Length <= 0)
                    {
                        rightStr = ds.Tables[0].Rows[i]["RightID"].ToString();
                    }
                    else
                    {
                        rightStr += "," + ds.Tables[0].Rows[i]["RightID"];
                    }
                }
            }
            return rightStr;
        }
    }
}
