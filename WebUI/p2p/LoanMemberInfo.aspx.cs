using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class LoanMemberInfo : BasePage
    {


        public int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConvertHelper.QueryString(Request, "ID", 0) > 0)
            {
                _id = ConvertHelper.QueryString(Request, "ID", 0);
                if (!IsPostBack)
                {
                    LoadData(_id);
                }
            }
        }

        private void LoadData(int id)
        {
            string sql = "SELECT * FROM dbo.CreditLine where ID = @ID ";
                  var dt = CreditLineBLL.GetUserInfo(id ,sql);

                  txtCompanyAddress.Value = ObjectToString(dt.Rows[0]["CreditLine"]);
                  txtCreditnumber.Value = ObjectToString(dt.Rows[0]["CardNumber"]);
                  txtCard.Value = ObjectToString(dt.Rows[0]["CreditNumber"]);
            txt_identitycard.Value = ObjectToString(dt.Rows[0]["IdentityCard"]);
        }


        private string ObjectToString(object obj)
        {
            return obj == null ? "" : obj.ToString();
        }


    }
}