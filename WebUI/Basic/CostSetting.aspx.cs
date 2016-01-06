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
    public partial class CostSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFeeType();
            }
        }

        private void BindFeeType()
        {
            selFeeType.DataSource = FeeType.GetSetFeeList();
            selFeeType.DataTextField = "Value";
            selFeeType.DataValueField = "Key";
            selFeeType.DataBind();
            selFeeType.Items.Insert(0, new ListItem("--请选择费用类型--", ""));
        }
    }
}