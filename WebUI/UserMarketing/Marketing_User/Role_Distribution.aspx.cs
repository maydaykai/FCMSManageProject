using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.UserMarketing.Marketing_User
{
    public partial class Role_Distribution : System.Web.UI.Page
    {
        private Marketing_RoleBLL _roleBll;

        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _roleBll = new Marketing_RoleBLL();
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            //加载角色
            if (!IsPostBack)
            {
                BindRoleList();
                BindSetList();
            }

        }
        //设置值GetRoleListByUserInfoId
        /// <summary>
        /// 设置当前的值
        /// </summary>
        public void BindSetList()
        {
            var ds = _roleBll.GetRoleListByUserInfoId(_id,0);
            string relust = "";
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                relust = relust + item["RoleNameId"] + ",";
            }
            relust = relust.Remove(relust.Length - 1);
            ControlHelper.SetChecked(ckbRoleList, relust, ",");
        }

        //绑定角色列表
        private void BindRoleList()
        {
            var ds = _roleBll.GetRoleList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            ckbRoleList.DataSource = ds;
            ckbRoleList.DataValueField = "ID";
            ckbRoleList.DataTextField = "RoleName";
            ckbRoleList.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string ArrLsit = ControlHelper.GetCheckBoxList(ckbRoleList, ",");
            if (ArrLsit.Split(',').Length > 1)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('只允许分配一个角色','warning', '');", true); return;
            }

            //保存当前信息 组合xml <RoleList> <Role UserInfoId="1" RoleNameID="2"> </Role><Role UserInfoId="1" RoleNameID="2"> </Role> </RoleList>
            StringBuilder str = new StringBuilder();
            str.Append("<RoleList>");
          
            foreach (string t in ArrLsit.Split(','))
            {
                str.Append("<Role UserInfoId=\"" + _id + "\" RoleNameID=\"" + t + "\"></Role>");
            }
            str.Append("</RoleList>");
            int relust = 0;
            if (ArrLsit.Length > 0)
            {
                relust = _roleBll.UserJoinRoleSave(str.ToString(), _id, 0);
            }
            else
            {
                relust = _roleBll.UserJoinRoleSave("", _id, 0);
            }

            
 
        }


    }
}