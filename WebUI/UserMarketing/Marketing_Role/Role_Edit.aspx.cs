using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.UserMarketing.Marketing_Role
{
    public partial class Role_Edit : BasePage
    {
        private int _PartentID = 0;
        private int _Leave = 0;
        private int _Id = 0;
        // private int _pid = 0;
        private Marketing_RoleBLL _roleBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            //参数 PartentID Leave
            _Id = ConvertHelper.QueryString(Request, "Id", 0);
            _PartentID = ConvertHelper.QueryString(Request, "pid", 0);
            _Leave = ConvertHelper.QueryString(Request, "Leave", 0);
            if (!IsPostBack)
            {
                if(_Id>0)
                {
                      _roleBll = new Marketing_RoleBLL();
                      var model = _roleBll.GetRoleNameModelById(_Id);
                      txt_RoleName.Value = model.RoleName;
                     
                }
            }

        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            //添加角色代码
            _roleBll = new Marketing_RoleBLL();
            Marketing_RoleNameModel molde = new Marketing_RoleNameModel();
            molde.RoleName = txt_RoleName.Value;
            molde.RoleCode = string.Format("yx_{0}", DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
            if (_Id == 0)
            {
                molde.Leave = _Leave + 1;
                molde.PartentID = _PartentID;
                molde.Status = 1;
                _roleBll.AddRole(molde);

            }
            else
            {
                molde.ID = _Id;
                _roleBll.UpdateRole(molde);
            }

        }
    }
}