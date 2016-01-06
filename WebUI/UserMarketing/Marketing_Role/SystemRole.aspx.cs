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

namespace WebUI.UserMarketing.Marketing_Role
{
    public partial class SystemRole : System.Web.UI.Page
    {
        private Marketing_RoleBLL _roleBll;
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _roleBll = new Marketing_RoleBLL();
            _id = ConvertHelper.QueryString(Request, "pid", 0);
            if (!IsPostBack)
            {
                BindRoleList();
                BindSetList();
            }

        }

        /// <summary>
        /// 设置当前的值
        /// </summary>
        public void BindSetList()
        {
            int curr_Id = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
            var ds = _roleBll.GetRoleListByUserInfoId(_id, 1);//根据当前角色获得
            string relust = "";
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                relust = relust + item["UserInfoId"] + ",";
            }
            relust = relust.Remove(relust.Length - 1);
            ControlHelper.SetChecked(ckbRoleList, relust, ",");
        }
        //绑定角色列表
        private void BindRoleList()
        {
            var ds = _roleBll.GetSystemRoleList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            ckbRoleList.DataSource = ds;
            ckbRoleList.DataValueField = "ID";
            ckbRoleList.DataTextField = "UserName";
            ckbRoleList.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            //保存当前信息 组合xml <RoleList> <Role UserInfoId="1" RoleNameID="2"> </Role><Role UserInfoId="1" RoleNameID="2"> </Role> </RoleList>
            string ArrLsit = ControlHelper.GetCheckBoxList(ckbRoleList, ",");
            //if (ArrLsit.Split(',').Length >= 2)
            //{
            //    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('只能指定一个角色','warning', '');", true); return;
            //}


            int curr_Id = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
            StringBuilder str = new StringBuilder();
            str.Append("<RoleList>");

            foreach (string t in ArrLsit.Split(','))
            {
                str.Append("<Role UserInfoId=\"" + t + "\" RoleNameID=\"" + _id + "\"></Role>");
            }
            str.Append("</RoleList>");
            int relust = 0;
            if (ArrLsit.Length > 0)
            {
                relust = _roleBll.UserJoinRoleSave(str.ToString(), curr_Id, 1);
            }
            else
            { 
                //取消
               // str = "";
                relust = _roleBll.UserJoinRoleSave("", curr_Id, 1);
            
            }

            if (relust > 0)
            {

                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('保存成功','success', '');", true);
            }




        }
    }
}