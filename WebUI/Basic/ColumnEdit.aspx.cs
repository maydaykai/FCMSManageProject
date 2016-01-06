using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsModel;
using ManageFcmsCommon;
using ManageFcmsBll;

namespace WebUI.Basic
{
    public partial class ColumnEdit : BasePage
    {
        private RightBll _rightBll;
        private ColumnRightBll _columnRightBll;
        /* _id 栏目ID */
        private int _pid, _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _rightBll = new RightBll();
            _columnRightBll = new ColumnRightBll();
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            _pid = ConvertHelper.QueryString(Request, "pid", 0);
            if (!IsPostBack)
            {
                var ds = _rightBll.GetRightList(1);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ckbRightList.DataSource = ds;
                    ckbRightList.DataValueField = "ID";
                    ckbRightList.DataTextField = "RightName";
                    ckbRightList.DataBind();
                }
                if (_id > 0)
                {
                    var colnmnBll = new ColumnBll();
                    var columnModel = colnmnBll.GetModel(_id);
                    string rightStr = _columnRightBll.GetColumnRightStr(_id);
                    ControlHelper.SetChecked(ckbRightList, rightStr, ",");
                    txtName.Value = columnModel.Name;
                    txtLinkUrl.Value = columnModel.LinkUrl;
                    txtICon.Value = columnModel.ICon;
                    txtSort.Value = columnModel.Sort.ToString();
                    chkVisible.Checked = columnModel.Visible;
                    txtDescription.Value = columnModel.Description;
                }
            }
        }

        protected void Operator_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入栏目名称','warning', '');", true);
                return;
            }
            var colnmnBll = new ColumnBll();
            if (_id > 0) //修改
            {
                var columnModel = colnmnBll.GetModel(_id);
                columnModel.ID = _id;
                columnModel.Name = txtName.Value.Trim();
                columnModel.LinkUrl = txtLinkUrl.Value.Trim();
                columnModel.ICon = txtICon.Value.Trim();
                columnModel.Sort = ConvertHelper.ToInt(txtSort.Value.Trim());
                columnModel.Visible = chkVisible.Checked;
                columnModel.Description = txtDescription.Value.Trim();
                var rightGroup = ControlHelper.GetCheckBoxList(ckbRightList, ",");
                var add = colnmnBll.Update(columnModel, rightGroup);
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       add > 0
                                                           ? "MessageAlert('修改成功。','success', '');"
                                                           : "MessageAlert('修改失败。','error', '');", true);
            }
            else
            {
                var columnModel = new ColumnModel
                    {
                        Name = txtName.Value.Trim(),
                        ParentID = _pid,
                        LinkUrl = txtLinkUrl.Value.Trim(),
                        ICon = txtICon.Value.Trim(),
                        Sort = ConvertHelper.ToInt(txtSort.Value.Trim()),
                        Visible = chkVisible.Checked,
                        Description = txtDescription.Value.Trim()
                    };
                var rightGroup = ControlHelper.GetCheckBoxList(ckbRightList, ",");
                var add = colnmnBll.Add(columnModel, rightGroup);
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       add > 0
                                                           ? "MessageAlert('添加成功。','success', '');"
                                                           : "MessageAlert('添加失败。','error', '');", true);
            }
        }
    }
}