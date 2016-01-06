using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.MemberManage
{
    public partial class EnterpriseAuthtEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {

            _id = ConvertHelper.QueryString(Request, "ID", 0);

            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var enAuthBll = new EnterpriseAuthentBll();
                    int total;
                    const string field = @" P.[ID],P.[MemberID],P.[EnterpriseName],P.[RegistrationNo],P.[OrganizationCode],P.[LegalName],P.[OperatorRealName],
                                            P.[OperatorIdentityCard],P.[OperatorPhone],P.[AuthentResult],P.[AuthentNumber],P.[ResponseXml],P.[CityID],P.[Address],
                                            P.[ExamStatus],P.[ExpireTime],P.[ApplyTime],P.[UpdateTime],M.MemberName";
                    var dt = enAuthBll.GetPageList(field, " P.ID=" + _id, " P.ID DESC", 1, 1, out total);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtMemberName.Value = dt.Rows[0]["MemberName"].ToString();
                        txtEnterpriseName.Value = dt.Rows[0]["EnterpriseName"].ToString();
                        txtRegistrationNo.Value = dt.Rows[0]["RegistrationNo"].ToString();
                        txtOrganizationCode.Value = dt.Rows[0]["OrganizationCode"].ToString();
                        txtLegalName.Value = dt.Rows[0]["LegalName"].ToString();
                        txtOperatorRealName.Value = dt.Rows[0]["OperatorRealName"].ToString();
                        txtOperatorIdentityCard.Value = dt.Rows[0]["OperatorIdentityCard"].ToString();
                        txtOperatorPhone.Value = dt.Rows[0]["OperatorPhone"].ToString();
                        hdCity.Value = dt.Rows[0]["CityID"].ToString();
                        var areaModel = new AreaBLL().getAreaByID(ConvertHelper.ToInt(dt.Rows[0]["CityID"].ToString()));
                        hdProvince.Value = areaModel == null ? "0" : areaModel.ParentID.ToString();
                        txtAddress.Value = dt.Rows[0]["Address"].ToString();
                        litAnthStatus.Text = dt.Rows[0]["AuthentResult"].ToString().Trim().Equals("1") ? "认证通过" : dt.Rows[0]["AuthentResult"].ToString().Trim().Equals("-1") ? "认证不通过" : "未认证";
                        txtAuthNumber.Value = dt.Rows[0]["AuthentNumber"].ToString();
                        txtExpireTime.Value = ConvertHelper.ToDateTime(dt.Rows[0]["ExpireTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
        }

        protected void btn_Operater_Click(object sender, EventArgs e)
        {
            var enAuthBll = new EnterpriseAuthentBll();
            if (_id > 0)
            {
                var enAuthentModel = enAuthBll.GetModel(_id);
                enAuthentModel.AuthentNumber = ConvertHelper.ToInt(txtAuthNumber.Value.Trim());
                enAuthentModel.CityID = ConvertHelper.ToInt(hdCity.Value.Trim());
                enAuthentModel.Address = txtAddress.Value.Trim();
                enAuthentModel.UpdateTime = DateTime.Now;

                ClientScript.RegisterClientScriptBlock(GetType(), "", enAuthBll.Update(enAuthentModel) ? "MessageAlert('修改成功。','success', '');" : "MessageAlert('修改失败。','error', '');", true);
            }
        }
    }
}