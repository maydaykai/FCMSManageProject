using System;
using System.Data;
using System.Text;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.UserManage
{
    public partial class RoleEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var role = new RoleBll();
                    var roleModel = role.GetModel(_id);
                    txtRoleName.Value = roleModel.Name;
                    txtRoleDescription.Value = roleModel.Description;
                    selStatus.Value = roleModel.Status.ToString();
                    litCreateTime.Text = roleModel.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    var roleRightBll = new RoleRightBll();
                    hdRightList.Value = roleRightBll.GetRoleRightStr(_id);
                }
            }
        }
        protected void Operate_Click(object sender, EventArgs e)
        {

            var roleBll = new RoleBll();
            if (_id > 0)
            {
                var roleModel = roleBll.GetModel(_id);
                roleModel.Name = txtRoleName.Value.Trim();
                roleModel.Description = txtRoleDescription.Value.Trim();
                roleModel.Status = ConvertHelper.ToInt(selStatus.Value.Trim());
                roleModel.UpdateTime = DateTime.Now;
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       roleBll.Update(roleModel, hdRightList.Value.Trim())
                                                           ? "MessageAlert('更新成功。','success', '');"
                                                           : "MessageAlert('更新失败。','error', '');", true);
            }
            else
            {
                var roleModel = new RoleModel();
                roleModel.Name = txtRoleName.Value.Trim();
                roleModel.Description = txtRoleDescription.Value.Trim();
                roleModel.Status = ConvertHelper.ToInt(selStatus.Value.Trim());
                roleModel.CreateTime = DateTime.Now;
                roleModel.UpdateTime = DateTime.Now;
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       roleBll.Add(roleModel, hdRightList.Value.Trim())
                                           ? "MessageAlert('添加成功。','success', '');"
                                           : "MessageAlert('添加失败。','error', '');", true);
            }




        }

        #region 根据DataTable生成Json树结构
        StringBuilder result = new StringBuilder();
        StringBuilder sb = new StringBuilder();
        protected string GetJsonStr()
        {
            var columnBll = new ColumnBll();
            var dataSet = columnBll.GetColumnlList("ID,Name,ParentID", 1);
            var dt = dataSet.Tables[0];
            GetTreeJsonByTable(dt, "id", "name", "ParentID", 0);

            return result.ToString();
        }

        /// <summary>
        /// 根据DataTable生成Json树结构
        /// </summary>
        /// <param name="tabel">数据源</param>
        /// <param name="idCol">ID列</param>
        /// <param name="txtCol">Text列</param>
        /// <param name="rela">关系字段</param>
        /// <param name="pId">父ID</param>
        public void GetTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId)
        {
            result.Append(sb.ToString());
            sb.Clear();
            if (tabel.Rows.Count > 0)
            {
                sb.Append("[");
                string filer = string.Format("{0}='{1}'", rela, pId);
                DataRow[] rows = tabel.Select(filer);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\"");
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            sb.Append(",\"children\":");
                            GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol]);
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                        result.Append(sb.ToString());
                        sb.Clear();
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                result.Append(sb.ToString());
                sb.Clear();
            }
        }
        #endregion

        #region datatable convert
        private DataTable DataSetProcess()
        {
            var columnBll = new ColumnBll();
            var dataSet = columnBll.GetColumnlList("ID,Name,ParentID", 1);
            var dt = dataSet.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = Modfiy(dt);
            }
            return dt;
        }


        private DataTable getTree(DataTable sdt, DataTable pdt, int id, int level)
        {
            const string @join = "<img src=\"../images/join.gif\" border=\"0\"/>";
            const string joinBottom = "<img src=\"../images/joinbottom.gif\" border=\"0\"/>";
            string prefix = join;
            string prefixend = joinBottom;//最后一个
            for (int i = 0; i < level - 1; i++)
            {
                prefix = join + prefix;
                prefixend = joinBottom + prefixend;
            }

            DataRow[] rows = sdt.Select("ParentID = " + id.ToString());
            int countnum = 1;
            foreach (DataRow erow in rows)
            {
                DataRow nrow = pdt.NewRow();
                nrow.ItemArray = erow.ItemArray;
                int i;
                Int32.TryParse(erow["ID"].ToString(), out i);
                if (countnum == rows.Length)
                {
                    DataRow[] lastrows = sdt.Select("ParentID = " + erow["ID"].ToString());
                    if (lastrows.Length <= 0)
                    {
                        nrow["Name"] = prefixend + nrow["Name"];
                    }
                    else
                    {
                        nrow["Name"] = prefix + nrow["Name"];
                    }
                }
                else
                {
                    nrow["Name"] = prefix + nrow["Name"];
                }
                pdt.Rows.Add(nrow);
                countnum++;
                getTree(sdt, pdt, i, level + 1);
            }
            return pdt;
        }


        private DataTable Modfiy(DataTable dt)
        {
            if ((dt == null) || (dt.Rows.Count < 1))
                return dt;
            DataTable dtn = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ParentID"].ToString().Equals("0"))
                {
                    row["Name"] = "<img src=\"../images/plus.gif\" border=\"0\"/><strong>" + row["Name"] + "</strong>";
                    DataRow nrow = dtn.NewRow();
                    nrow.ItemArray = row.ItemArray;
                    dtn.Rows.Add(nrow);
                    int i = -1;
                    Int32.TryParse(row["ID"].ToString(), out i);
                    getTree(dt, dtn, i, 1);
                }
            }
            dtn.Columns.Add("imgStr", Type.GetType("System.String"));
            foreach (DataRow row in dtn.Rows)
            {
                int i = -1;
                Int32.TryParse(row["ID"].ToString(), out i);
                DataRow[] rows = dt.Select("ParentID = " + i.ToString());
                if (rows.Length > 0)
                {
                    row["imgStr"] = "<img src=\"../images/plus.gif\" border=\"0\"/>";
                }
                else
                {
                    row["imgStr"] = "";
                }
            }
            return dtn;
        }
        #endregion
    }

}