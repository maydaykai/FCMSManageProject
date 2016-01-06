using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.UserManage
{
    public partial class ChanagePwd : System.Web.UI.Page
    {
        FcmsUserBll _userBll;
        FcmsUserModel _userModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            _userBll = new FcmsUserBll();
            if (SessionHelper.Exists("FcmsUserId"))
            {
                int memberId = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
                _userModel = _userBll.GetModel(memberId);
            }
            else
            {
                Response.Redirect("~/ManageLogin.aspx");
            }
            if (!IsPostBack)
            {
                txtUserName.Value = _userModel.UserName;
            }
        }

        public void Operater_Btn_Click(object sender, EventArgs e)
        {
            string oldPwd = txtOldPwd.Value.Trim();
            string newPwd = txtNewPwd.Value.Trim();
            string configPwd = txtConfirmPwd.Value.Trim();

            if (string.IsNullOrEmpty(oldPwd))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入您的原密码.','warning', '');", true);
                return;
            }

            if (!_userModel.PassWord.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(oldPwd, "md5")))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('原密码不正确.','warning', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(newPwd))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请您重新设置一个新密码.','warning', '');", true);
                return;
            }

            if (newPwd.Equals(oldPwd))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('新设置的密码不能跟原密码一样.','warning', '');", true);
                return;
            }

            if (!configPwd.Equals(newPwd))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('确认新密码不一致.','warning', '');", true);
                return;
            }

            _userModel.PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(newPwd, "md5");

            ClientScript.RegisterClientScriptBlock(GetType(), "", _userBll.Update(_userModel) ? "MessageAlert('密码修改成功，请重新登录.','success', '');" : "MessageAlert('修改失败，请联系系统管理员.','error', '');", true);
        }

    }
}