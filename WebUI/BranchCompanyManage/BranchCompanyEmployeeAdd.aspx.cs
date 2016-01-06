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
    public partial class BranchCompanyEmployeeAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button_Click(object sender, EventArgs e)
        {
            var memberId = txtMemberID.Value;
            var branchCompanyId = txtBranchCompanyID.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择会员。','warning','');", true);
                return;
            }
            if (string.IsNullOrEmpty(branchCompanyId))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择分公司。','warning','');", true);
                return;
            }
            var bll = new BranchCompanyBll();
            if (bll.GetBranchCompanyMemberModel(ConvertHelper.ToInt(memberId)) != null)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('该会员已分配分公司。','warning','');", true);
                return;
            }
            var model = new BranchCompanyMemberModel
                {
                    MemberID = ConvertHelper.ToInt(memberId),
                    BranchCompanyID = ConvertHelper.ToInt(branchCompanyId),
                    CreateTime = DateTime.Now
                };
            var id = new BranchCompanyBll().AddBranchCompanyMember(model);
            ClientScript.RegisterClientScriptBlock(GetType(), "", id > 0
                                                                      ? "MessageAlert('添加成功。','success', '/BranchCompanyManage/BranchCompanyEmployee.aspx?columnId=" + ColumnId + "');"
                                                                      : "MessageAlert('添加失败。','error', '');", true);
        }
    }
}