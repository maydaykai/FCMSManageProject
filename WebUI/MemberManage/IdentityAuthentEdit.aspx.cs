using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.MemberManage
{
    public partial class IdentityAuthentEdit : BasePage
    {
        private int _id;
        public bool RightUpdate = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限控制
            RightUpdate = RightArray.IndexOf("|3|", StringComparison.Ordinal) >= 0;

            

            _id = ConvertHelper.QueryString(Request, "ID", 0);

            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var idAuthBll = new IdentityAuthentBll();
                    int total;
                    int pageTotal;
                    var dt = idAuthBll.GetPageList("P.RealName,P.IdentityCard,P.AuthentResult,P.ResponseXml,P.ExpireTime,M.MemberName,M.Mobile", " IdentityAuthent P LEFT JOIN dbo.Member M ON M.ID=P.MemberID", " P.ID=" + _id, " P.ID DESC", 1, 1, out total, out pageTotal);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtMemberName.Value = dt.Rows[0]["MemberName"].ToString();
                        txtRealName.Value = dt.Rows[0]["RealName"].ToString();
                        txtIdentityCard.Value = dt.Rows[0]["IdentityCard"].ToString();
                        txtMobile.Value = dt.Rows[0]["Mobile"].ToString();
                        litAnthStatus.Text = dt.Rows[0]["AuthentResult"].ToString().Trim().Equals("1") ? "认证通过" : dt.Rows[0]["AuthentResult"].ToString().Trim().Equals("-1") ? "认证不通过" : "未认证";
                        txtExpireTime.Value = ConvertHelper.ToDateTime(dt.Rows[0]["ExpireTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    IdentityAuthentModel identityAuthentModel = idAuthBll.GetModel(_id);
                    if (identityAuthentModel.AuthentResult != -1)
                    {
                        Button1.Attributes.Add("disabled", "disabled");
                    }
                }
            }
        }

        protected void btn_Operater_Click(object sender, EventArgs e)
        {
            var idAuthBll = new IdentityAuthentBll();
            if (_id > 0)
            {
                IdentityAuthentModel identityAuthentModel = idAuthBll.GetModel(_id);
                //identityAuthentModel.AuthentNumber = ConvertHelper.ToInt(txtAuthNumber.Value);

                //ClientScript.RegisterClientScriptBlock(GetType(), "", idAuthBll.Update(identityAuthentModel) ? "MessageAlert('修改成功。','success', '');" : "MessageAlert('修改失败。','error', '');", true);//
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            var idAuthBll = new IdentityAuthentBll();
            if (_id > 0)
            {
                IdentityAuthentModel identityAuthentModel = idAuthBll.GetModel(_id);
                ClientScript.RegisterClientScriptBlock(GetType(), "", idAuthBll.Update(identityAuthentModel) ? "MessageAlert('修改成功。','success', '');" : "MessageAlert('修改失败。','error', '');", true);//
            }
        }
    }
}