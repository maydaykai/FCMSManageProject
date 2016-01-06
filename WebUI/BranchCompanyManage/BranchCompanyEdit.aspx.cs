using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.BranchCompanyManage
{
    public partial class BranchCompanyEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            if (_id > 0)
            {
                var model = new BranchCompanyBll().GetBranchCompanyModel(_id);
                txtCompanyName.Value = model.Name;
                txtSetUpDate.Value = model.SetUpDate.ToString("yyyy-MM-dd");
                txtRemark.Value = model.Remark;
            }
        }
        protected void Operate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompanyName.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入公司名称','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtSetUpDate.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入成立日期','warning', '');", true);
                return;
            }
            var bll = new BranchCompanyBll();
            if (_id > 0)
            {
                var model = bll.GetBranchCompanyModel(_id);
                model.Name = txtCompanyName.Value.Trim();
                model.SetUpDate = ConvertHelper.ToDateTime(txtSetUpDate.Value.Trim());
                model.Remark = txtRemark.Value.Trim();
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       bll.UpdateBranchCompany(model)
                                                           ? "MessageAlertChild('修改成功','success', '/MemberManage/BranchCompanyManage.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('修改失败','error', '');", true);

            }
            else
            {
                var model = new BranchCompanyModel()
                {
                    Name = txtCompanyName.Value.Trim(),
                    SetUpDate = ConvertHelper.ToDateTime(txtSetUpDate.Value.Trim()),
                    Remark = txtRemark.Value.Trim()
                };

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       bll.AddBranchCompany(model) > 0
                                           ? "MessageAlertChild('添加成功','success', '/MemberManage/BranchCompanyManage.aspx?columnId=" + ColumnId + "');"
                                           : "MessageAlertChild('添加失败','error', '');", true);
            }
        }
    }
}