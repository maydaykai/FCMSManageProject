using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsCommon;

namespace WebUI.MemberManage
{
    public partial class BankCardAuthtQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _ReqSn = ConvertHelper.QueryString(Request, "REQ_SN", "");
            if (!string.IsNullOrEmpty(_ReqSn))
            {
                string resultStr = ManageFcmsCommon.TongLian.TradeQuery.query(_ReqSn);
                lab_result.InnerText = resultStr;
            }
        }
    }
}