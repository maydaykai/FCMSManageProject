using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.UserMarketing.Marketing_Ex_Person
{
    public partial class DistributionPerson : BasePage
    {
        private Marketing_RoleBLL _roleBll = new Marketing_RoleBLL();
        private Marketing_Ex_PersonBLL _person = new Marketing_Ex_PersonBLL();
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            if (!IsPostBack)
            {
                BindRoleList();
                BindSetList();
                LoadPerson();
            }

        }

        //绑定角色列表
        private void BindRoleList()
        {

            var ds = _roleBll.GetDistributionTable(_id);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                lab_one.InnerText = "暂无人员可以分配";
                return;
            }
            ckbRoleList.DataSource = ds;
            ckbRoleList.DataValueField = "ID";
            ckbRoleList.DataTextField = "Name";
            ckbRoleList.DataBind();
        }

        public void LoadPerson()
        {
            //加载人员列表
            List<Ex_PersonModel> list = _person.GetPersonList(_id);
            StringBuilder htmlstr = new StringBuilder();

            if (list != null && list.Count() > 0)
            {
               
                foreach (var item in list)
                {
                    htmlstr.Append("<tr>");
                    htmlstr.Append("<td style=\"text-align: left; width: 150px;\">" + item.Name + "</td><td style=\"text-align: left; width: 150px;\">" + item.Mobile + "</td>");
                    htmlstr.Append("<td style=\"text-align: left; width: 150px;\">" + item.Age + "</td><td style=\"text-align: left; width: 150px;\">" + (item.Sex == true ? "男" : "女") + "</td>");

                    htmlstr.Append("<td style=\"text-align: left; width: 150px;\">" + item.RoleName + "</td>");
                    htmlstr.Append(" </tr>");
                }
                
            }
            list_html.InnerHtml = htmlstr.ToString();
        }

        /// <summary>
        /// 设置当前的值
        /// </summary>
        public void BindSetList()
        {
            var ds = _roleBll.SetDistributionTable(_id);
            string relust = "";
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                relust = relust + item["PersonId"] + ",";
            }
            relust = relust.Remove(relust.Length - 1);
            ControlHelper.SetChecked(ckbRoleList, relust, ",");
        }

        /// <summary>
        /// 保存当前权限关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Save_Click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<RoleList>");
            string ArrLsit = ControlHelper.GetCheckBoxList(ckbRoleList, ",");
            foreach (string t in ArrLsit.Split(','))
            {
                str.Append("<Role RoleId=\"" + _id + "\" PersonId=\"" + t + "\"></Role>");
            }
            str.Append("</RoleList>");
            int relust = 0;
            if (ArrLsit.Length > 0)
            {
                relust = _roleBll.Ex_PersonToRoleSave(str.ToString(), _id);
            }
            else
            {
                relust = _roleBll.Ex_PersonToRoleSave("", _id);
            }

        }
    }
}