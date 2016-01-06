using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.MemberManage
{
    public partial class MemberGeneralInfo : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            if (!IsPostBack)
            {
                LoadData(_id);
            }
        }

        private void LoadData(int id)
        {
            // 会员基本信息加载
            MemberInfoBll _memberInfoBll = new MemberInfoBll();
            int total;
            DataTable dt = _memberInfoBll.GetPageList(" P.RealName, P.IdentityCard, P.Sex, P.Province, P.City, P.Address, P.Telephone, P.Fax", " MemberInfo P", "P.MemberID=" + id, "  P.ID DESC", 1, 1, out total);
            if (dt.Rows.Count > 0)
            {
                txtRealName.Value = ObjectToString(dt.Rows[0]["RealName"]);
                txtIdentityCard.Value = ObjectToString(dt.Rows[0]["IdentityCard"]);
                hdProvince.Value = ObjectToString(dt.Rows[0]["Province"]);
                hdCity.Value = ObjectToString(dt.Rows[0]["City"]);
                txtAddress.Value = ObjectToString(dt.Rows[0]["Address"]);
                txtTelephone.Value = ObjectToString(dt.Rows[0]["Telephone"]);
                txtFax.Value = ObjectToString(dt.Rows[0]["Fax"]);
            }
        }

        private string ObjectToString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}