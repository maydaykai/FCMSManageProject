using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using ManageFcmsCommon;
using ManageFcmsConn;

namespace WebUI.HanderAshx.Basic
{
    /// <summary>
    /// LoginMenuHandler 的摘要说明
    /// </summary>
    public class LoginMenuHandler : IHttpHandler, IRequiresSessionState
    {
        private string _fcmsRole=string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/plain";
            if (SessionHelper.Exists("FcmsRole"))
            {
                _fcmsRole = SessionHelper.Get("FcmsRole").ToString();
            }

            context.Response.Write(!SessionHelper.Exists("FcmsUserId") ? "" : GetJsonStr());
        }

        //获取json字符串
        private string GetJsonStr()
        {
            var menuJosn = string.Empty;
            var dtMenu = GetDataTables();
            if (dtMenu != null && dtMenu.Rows.Count > 0)
            {
                menuJosn = "{\"menus\": [" + GetTasksString(0, dtMenu) + "]}";
            }
            return menuJosn;
        }

        //DataTable转换Json字符串 
        private static string GetTasksString(int taskId, DataTable dt)
        {
            var rows = dt.Select("ParentID=" + taskId.ToString(CultureInfo.InvariantCulture));
            if (rows.Length == 0)
                return string.Empty;
            var str = new StringBuilder();
            var rowCount = 0;
            foreach (var row in rows)
            {
                rowCount++;
                str.Append("{");
                for (var i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i != 0) str.Append(",");
                    str.Append("\"" + row.Table.Columns[i].ColumnName + "\"");
                    str.Append(":\"");
                    str.Append(row[i]);
                    str.Append("\"");
                }
                str.Append(",\"menus\":[");
                str.Append(GetTasksString((int)row["menuid"], dt));
                str.Append("]");
                str.Append(rowCount == rows.Count() ? "}" : "},");
            }
            return str.ToString();
        }

        public DataTable GetDataTables()
        {
            string strSql = string.Format("select ID as menuid,ParentID,[name] as menuname,LinkUrl as url,icon from [Column] where ID in (select ColumnID from RoleRight RR left join [Right] R on RR.RightID=R.ID where RR.RightID={1} AND RoleID in(select value from [dbo].[fun_SplitToTable]('{0}',','))group BY ColumnID) AND [Visible] = 1  order by [Sort] DESC,[ID] ASC", _fcmsRole, 1);
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql);
            var dt = ds.Tables[0];
            return dt;
        }

        public DataRow[] SelectRows(DataTable dt, int pid)
        {
            return dt.Select(string.Format("ParentID = {0}", pid));
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}