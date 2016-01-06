using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsCommon
{
    public class TreeHelper
    {
        private static DataTable GetTree(DataTable sdt, DataTable pdt, int id, int level, bool grid)
        {
            string @join = "<img src=\"../images/join.gif\" border=\"0\"/>";
            string joinBottom = "<img src=\"../images/joinbottom.gif\" border=\"0\"/>";
            if (!grid)
            {
                join = "├";
                joinBottom = "└";
            }
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
                GetTree(sdt, pdt, i, level + 1, grid);
            }
            return pdt;
        }


        /// <summary>
        /// 生成递归树
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="grid">是否在grid显示</param>
        /// <returns></returns>
        public static DataTable Modfiy(DataTable dt, bool grid, string rootID="0")
        {
            if ((dt == null) || (dt.Rows.Count < 1))
                return dt;
            DataTable dtn = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ParentID"].ToString().Equals(rootID))
                {
                    if (grid)
                    {
                        row["Name"] = "<img src=\"../images/plus.gif\" border=\"0\"/><strong>" + row["Name"] + "</strong>";
                    }
                    DataRow nrow = dtn.NewRow();
                    nrow.ItemArray = row.ItemArray;
                    dtn.Rows.Add(nrow);
                    int i = -1;
                    Int32.TryParse(row["ID"].ToString(), out i);
                    GetTree(dt, dtn, i, 1, grid);
                }
            }
            if (grid)
            {
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
            }
            return dtn;
        }
    }
}
