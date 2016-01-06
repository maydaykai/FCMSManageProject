using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.MemberManage
{
    public partial class QuickBankAccountManage : BasePage
    {
        public bool RightUpdate = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //权限控制
                RightUpdate = RightArray.IndexOf("|3|", StringComparison.Ordinal) >= 0;
            }
        }
    }
}