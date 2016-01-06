using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebUI.UserMarketing.Marketing_Competence
{
    public partial class EditFunctionOperation : BasePage
    {
        private int _id;
        private int _columnId;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            _columnId = ConvertHelper.QueryString(Request, "columnId", 0);
            if (!IsPostBack)
            {
                BindRoleList();
                if (_id > 0)
                {
                    StartDate();
                }
            }
        }

        //初始化数据
        public void StartDate()
        {
            var _bll = new Marketing_RoleBLL();
            var model = _bll.GetFunctionOperationById(_id);
            txt_OperationName.Value = model.OperationName;
            txt_OperationCode.Value = model.OperationCode;
            ControlHelper.SetChecked(ckbRoleList, model.OperType, ",");
            txt_Remake.Value = model.Remake;

        }

        //绑定角色列表
        private void BindRoleList()
        {
            Marketing_RoleBLL _roleBll = new Marketing_RoleBLL();
            var ds = _roleBll.GetRoleList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            ckbRoleList.DataSource = ds;
            ckbRoleList.DataValueField = "ID";
            ckbRoleList.DataTextField = "RoleName";
            ckbRoleList.DataBind();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            var _bll = new Marketing_RoleBLL();
            Marketing_FunctionOperationModel model = new Marketing_FunctionOperationModel();
            model.OperationName = txt_OperationName.Value;
            model.OperationCode = txt_OperationCode.Value;
            model.Remake = txt_Remake.Value;
            model.OperType = ControlHelper.GetCheckBoxList(ckbRoleList, ",");
            //保存新增功能信息
            if (_id == 0)
            {

                _bll.AddFunctionOperation(model);

            }
            else
            {
                model.Id = _id;
                _bll.UpdateFunctionOperation(model);
            }


        }
    }
}