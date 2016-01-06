using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsCommon;
using ManageFcmsModel;
using ManageFcmsBll;

namespace WebUI
{
    public partial class ManageLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionHelper.Clear();
            }
            txtPassWord.Attributes.Add("onkeydown", "if(event.which || event.keyCode){  " +
                "if ((event.which == 13) || (event.keyCode == 13)) {  " +
                "document.getElementById('" +
               Button1.UniqueID + "').click();return false;}}   " +
                "else {return true}; ");
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            var userName = txtUserName.Value.Trim();
            var passWord = txtPassWord.Value.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入您的用户名和密码。','warning', '');", true); return;
            }
            var fcmsUserBll = new FcmsUserBll();
            var fcmsUserModel = new FcmsUserModel()
            {
                UserName = userName,
                PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(passWord, "md5")
            };

            if (fcmsUserBll.LoginValidate(ref fcmsUserModel))//登录验证
            {
                SessionHelper.Add("FcmsUserId", fcmsUserModel.ID);
                SessionHelper.Add("FcmsUserName", fcmsUserModel.UserName);
                SessionHelper.Add("FcmsRole", fcmsUserModel.RoleId);

                fcmsUserModel.LastIP = HttpContext.Current.Request.UserHostAddress;
                fcmsUserModel.LastLoginTime = DateTime.Now;
                fcmsUserModel.Times += 1;
                fcmsUserModel.UpdateTime = DateTime.Now;

                fcmsUserBll.Update(fcmsUserModel);
                Response.Redirect("Index.aspx");

            }
            else
            {
                SessionHelper.Clear();
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('您的用户名或密码错误。','warning', '');", true);
            }

        }

    }
}