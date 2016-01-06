using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.MemberManage
{
    public partial class MemberReferral : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConvertHelper.QueryString(Request, "ID", 0) > 0)
            {
                _id = ConvertHelper.QueryString(Request, "ID", 0);
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            MemberRecommendedModel memberrecoMdel = new MemberRecommendedModel();
            memberrecoMdel.RecMemberID = int.Parse(txtMemberID.Value);
            memberrecoMdel.RecedMemberID = _id;
            ClientScript.RegisterClientScriptBlock(GetType(), "",
                       MemberReferralBll.Insert(memberrecoMdel)
                           ? "MessageAlert('成功。','success', '');"
                           : "MessageAlert('已有推荐人。','error', '');", true);
        }

    }
}