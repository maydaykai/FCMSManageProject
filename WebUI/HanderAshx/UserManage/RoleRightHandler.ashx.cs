using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.UserManage
{
    /// <summary>
    /// RoleRightHandler 的摘要说明
    /// </summary>
    public class RoleRightHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            context.Response.Write(GetJson());
        }


        private static Object GetJson()
        {
            var columnBll = new ColumnBll();
            var dataSet = columnBll.GetColumnlList("ID,Name,ParentID", 1);
            var dt = dataSet.Tables[0];
            dt.Columns.Add("ckbList", Type.GetType("System.String"));
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dsColRight = columnBll.GetColRightList(ConvertHelper.ToInt(dt.Rows[i]["ID"].ToString()));
                var dtColRight = dsColRight.Tables[0];
                foreach (DataRow dr in dtColRight.Rows)
                {
                    dt.Rows[i]["ckbList"] += "<input id=\"ckb_"+dr["ColumnID"]+"_"+ dr["RightID"] + "\" name=\"" + dr["ColumnID"] + "\" type=\"checkbox\" value=\"" + dr["RightID"] + "\" runat=\"server\" />" + dr["RightName"];
                }
                if (ConvertHelper.ToInt(dt.Rows[i]["ParentID"].ToString()) == 0)
                {
                    dt.Rows[i]["Name"] = "<b>" + dt.Rows[i]["Name"] + "</b>";
                }
            }
            string s = JsonHelper.DataTableToJson(dt);
            return s;
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