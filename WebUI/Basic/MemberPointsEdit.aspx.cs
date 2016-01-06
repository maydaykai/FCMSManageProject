using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.Basic
{
    public partial class MemberPointsEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var bll = new MemberPointsBLL();
                    var model = bll.GetMemberPointsModel(_id);
                    txtName.Value = model.Name;
                    txtInterestManageFee.Value = model.InterestManageFee.ToString("0.00");
                    txtMinScore.Value = model.MinScore.ToString();
                }
            }
        }

        protected void Operate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入会员积分等级名称','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtInterestManageFee.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入利息管理费率','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtMinScore.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入对应积分','warning', '');", true);
                return;
            }

            var bll = new MemberPointsBLL();
            if (_id > 0)
            {
                var model = bll.GetMemberPointsModel(_id);
                model.Name = txtName.Value.Trim();
                model.InterestManageFee = ConvertHelper.ToDecimal(txtInterestManageFee.Value.Trim());
                model.MinScore = ConvertHelper.ToInt(txtMinScore.Value.Trim());

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       bll.UpdateMemberPoints(model)
                                                           ? "MessageAlertChild('修改成功','success', '/Basic/MemberPointsSetting.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('修改失败','error', '');", true);

            }
            else
            {
                var model = new DimMemberPointsModel()
                {
                    Name = txtName.Value.Trim(),
                    InterestManageFee = ConvertHelper.ToDecimal(txtInterestManageFee.Value.Trim()),
                    MinScore = ConvertHelper.ToInt(txtMinScore.Value.Trim())
                };

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       bll.AddMemberPoints(model) > 0
                                           ? "MessageAlertChild('添加成功','success', '/Basic/MemberPointsSetting.aspx?columnId=" + ColumnId + "');"
                                           : "MessageAlertChild('添加失败','error', '');", true);
            }
        }
    }
}