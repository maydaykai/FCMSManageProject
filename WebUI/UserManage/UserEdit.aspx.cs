using System;
using System.Web;
using System.Web.UI.WebControls;
using ManageFcmsCommon;
using ManageFcmsBll;
using ManageFcmsModel;
using System.Web.Security;

namespace WebUI.UserManage
{
    public partial class UserEdit : BasePage
    {
        private RoleBll _roleBll;
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _roleBll = new RoleBll();
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            if (!IsPostBack)
            {
                BindRoleList();
                BindGcList();
                if (_id > 0)
                {
                    txtUserName.Disabled = true;
                    var fcmsUserBll = new FcmsUserBll();
                    var fcmsUserModel = fcmsUserBll.GetModel(_id);
                    txtUserName.Value = fcmsUserModel.UserName;
                    txtRealName.Value = fcmsUserModel.RealName;
                    txtAnotherName.Value = fcmsUserModel.AnotherName;
                    txtPhone.Value = fcmsUserModel.Phone;
                    txtMobile.Value = fcmsUserModel.Mobile;
                    txtEmail.Value = fcmsUserModel.Email;
                    txtQQ.Value = fcmsUserModel.QQ;
                    chkLock.Checked = fcmsUserModel.IsLock;
                    fcmsUserModel.LastIP = Request.UserHostAddress;
                    fcmsUserModel.LastLoginTime = DateTime.Now;
                    fcmsUserModel.RegTime = DateTime.Now;
                    fcmsUserModel.UpdateTime = DateTime.Now;
                    ControlHelper.SetChecked(ckbRoleList, fcmsUserModel.RoleId, ",");
                    selParentId.Value = fcmsUserModel.ParentID.ToString();
                    rblSex.SelectedValue = fcmsUserModel.Sex.ToString();
                    litRegTime.Text = fcmsUserModel.RegTime.ToString("yyyy-MM-dd HH:mm:ss");
                    litLastLoginTime.Text = fcmsUserModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
                    litLastLoginIP.Text = fcmsUserModel.LastIP;
                    litTimes.Text = fcmsUserModel.Times.ToString();
                }
                else
                {
                    txtUserName.Attributes["class"] = "input_text";
                    imgUserNameLeft.Src = "../images/input_left.png";
                    imgUserNameRight.Src = "../images/input_right.png";
                }

            }
        }

        //绑定角色列表
        private void BindRoleList()
        {
            var ds = _roleBll.GetRoleList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            ckbRoleList.DataSource = ds;
            ckbRoleList.DataValueField = "ID";
            ckbRoleList.DataTextField = "Name";
            ckbRoleList.DataBind();
        }

        //绑定担保公司下拉列表
        private void BindGcList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetGcList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            selParentId.DataSource = ds;
            selParentId.DataValueField = "ID";
            selParentId.DataTextField = "RealName";
            selParentId.DataBind();
            selParentId.Items.Insert(0, new ListItem("--请选择--", "0"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var fcmsUserBll = new FcmsUserBll();
            if (string.IsNullOrEmpty(txtUserName.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('用户名不能为空','warning', '');", true); return;
            }
            if (!RegexHelper.IsUserName(txtUserName.Value.Trim()) || txtUserName.Value.Trim().Length < 5 || txtUserName.Value.Trim().Length > 12)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('用户名长度为5-12位且由26个英文字母或数字组成','warning', '');", true); return;
            }

            if (!txtPwd.Value.Trim().Equals(txtPwdConfirm.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('密码确认错误','warning', '');", true); return;
            }

            //var roleStr = ControlHelper.GetCheckBoxList(ckbRoleList, ",");
            //roleStr = "," + roleStr + ",";
            //if (roleStr.Contains(",6,") || roleStr.Contains(",7,"))
            //{
            //    if (selParentId.SelectedIndex <= 0)
            //    { ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('如果角色包含担保公司岗位必须选择所属担保公司！','warning', '');", true); return; }
            //}

            var pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Value.Trim(), "md5");
            if (_id > 0)
            {
                FcmsUserModel fcmsUserModel = fcmsUserBll.GetModel(_id);
                if (!string.IsNullOrEmpty(txtPwd.Value.Trim()))
                {
                    fcmsUserModel.PassWord = pwd;                   
                    fcmsUserModel.TranPassWord = pwd;
                }
                fcmsUserModel.RealName = txtRealName.Value.Trim();
                fcmsUserModel.AnotherName = txtAnotherName.Value.Trim();
                fcmsUserModel.Phone = txtPhone.Value.Trim();
                fcmsUserModel.Mobile = txtMobile.Value.Trim();
                fcmsUserModel.Email = txtEmail.Value.Trim();
                fcmsUserModel.QQ = txtQQ.Value.Trim();
                fcmsUserModel.IsLock = chkLock.Checked;
                fcmsUserModel.UpdateTime = DateTime.Now;
                fcmsUserModel.RoleId = ControlHelper.GetCheckBoxList(ckbRoleList, ",");
                fcmsUserModel.ParentID = ConvertHelper.ToInt(selParentId.Value);
                fcmsUserModel.Sex = ConvertHelper.ToInt(rblSex.SelectedValue.Trim());
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                           fcmsUserBll.Update(fcmsUserModel)
                               ? "MessageAlert('修改成功。','success', '');"
                               : "MessageAlert('修改失败。','error', '');", true);
            }
            else
            {
                if (fcmsUserBll.Exists(txtUserName.Value.Trim()))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('用户名已存在，请换一个','warning', '');", true); return;
                }
                var fcmsUserModel = new FcmsUserModel
                    {
                        UserName = txtUserName.Value.Trim(),
                        PassWord = pwd,
                        TranPassWord = pwd,
                        RealName = txtRealName.Value.Trim(),
                        AnotherName = txtAnotherName.Value.Trim(),
                        Phone = txtPhone.Value.Trim(),
                        Mobile = txtMobile.Value.Trim(),
                        Email = txtEmail.Value.Trim(),
                        QQ = txtQQ.Value.Trim(),
                        IsLock = chkLock.Checked,
                        LastLoginTime = DateTime.Now,
                        RegTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        RoleId = ControlHelper.GetCheckBoxList(ckbRoleList, ","),
                        ParentID = ConvertHelper.ToInt(selParentId.Value.Trim()),
                        Sex = ConvertHelper.ToInt(rblSex.SelectedValue.Trim()),
                        Times = 0,
                        RelationID = 0
                    };
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                           fcmsUserBll.Add(fcmsUserModel)
                                               ? "MessageAlert('添加成功。','success', '');"
                                               : "MessageAlert('添加失败。','error', '');", true);
            }
        }
    }


}