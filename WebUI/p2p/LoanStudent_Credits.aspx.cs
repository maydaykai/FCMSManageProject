using ManageFcmsBll;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.p2p
{
    public partial class LoanStudent_Credits : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindProvinceList();
            }

        }

        private void BindProvinceList()
        {
          
        }



    }
}