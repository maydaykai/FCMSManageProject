using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsCommon;

namespace WebUI.MemberManage
{
    public partial class RechargeManage : BasePage
    {
        protected int _btnOffline = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Role.IsRoleNew(RightArray, "2"))
            {
                _btnOffline = 0;
            }
        }
    }
}