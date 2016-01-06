using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.ReportStatistics
{
    public partial class BondingCoFundReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitFeeTypes();
            }
        }

        private void InitFeeTypes()
        {
            Dictionary<int, string> dicFeeTypes = FeeType.GetBondingCompanyFeeList();
            selFeeType.DataSource = dicFeeTypes;
            selFeeType.DataBind();
            //selFeeType.Items.Insert(0, new ListItem("--费用类型--", string.Join(",", FeeType.GetP2PFeeList().Keys)));
            selFeeType.Items.Insert(0, new ListItem("--费用类型--", string.Join(",", FeeType.GetBondingCompanyFeeList().Keys)));
        }
    }
}