using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.UserManage
{
    public partial class RightEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var rightBll = new RightBll();
                    var rightModel = rightBll.GetModel(_id);
                    txtRightName.Value = rightModel.RightName;
                    ckbRight.Checked = rightModel.Visible;
                }
            }
        }

        //
        protected void Operate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRightName.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入权限名称','warning', '');", true);
                return;
            }

            var rightBll = new RightBll();
            if (_id > 0)
            {
                var rightModel = rightBll.GetModel(_id);
                rightModel.RightName = txtRightName.Value.Trim();
                rightModel.Visible = ckbRight.Checked;
                rightModel.UpdateTime = DateTime.Now;
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       rightBll.Update(rightModel)
                                                           ? "MessageAlertChild('修改成功','success', '/UserManage/RightManage.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('修改失败','error', '');", true);

            }
            else
            {
                var rightModel = new RightModel
                    {
                        UpdateTime = DateTime.Now,
                        CreateTime = DateTime.Now,
                        RightName = txtRightName.Value.Trim(),
                        Visible = ckbRight.Checked
                    };
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       rightBll.Add(rightModel) > 0
                                           ? "MessageAlertChild('添加成功','success', '/UserManage/RightManage.aspx?columnId=" + ColumnId + "');"
                                           : "MessageAlertChild('添加失败','error', '');", true);
            }
        }
    }
}