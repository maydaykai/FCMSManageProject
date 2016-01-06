using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;

namespace WebUI.Basic
{
    public partial class ProductManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var dimProductScoreBll = new DimProductScoreBLL();

                //dimProductScoreBll.UpdateByIdToField("dbo.DimProductScore", "ScoreItems='你好',FullMarks=6", 1);
            }
        }
    }
}