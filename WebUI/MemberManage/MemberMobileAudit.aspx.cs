using System;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.MemberManage
{
    public partial class MemberMobileAudit : BasePage
    {
        private int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Id = ConvertHelper.QueryString(Request, "ID", 0);
            if (!IsPostBack)
            {
                if (Id > 0)
                {
                    MobileUpdateRecordModel model = new MobileUpdateRecordBll().getMobileUpdateRecordModel("ID=" + Id);
                    txtMemberName.Value = new MemberBll().GetModel(model.MemberID).MemberName;
                    txtRealName.Value = model.RealName;
                    txtIdentityCard.Value = model.IdentityCard;
                    txtOldMobile.Value = model.OldMobile;
                    txtNewMobile.Value = model.NewMobile;
                    auditRemark.Value = model.Remark;
                    AuditRecords.InnerText = model.AuditRecords;
                    if (model.AuditStatus == -1 || model.AuditStatus == 2)
                    {
                        Tr_AuditRecords.Visible = true;
                        Btn_Audit.Visible = false;
                        Btn_Audit.Enabled = false;
                    }
                    else
                    {
                        Tr_AuditStatus.Visible = true;
                    }
                }
            }
        }

        protected void Audit_Click(object sender, EventArgs e)
        {
            string auditStatus = Request.Form["AuditStatus"];
            if (string.IsNullOrEmpty(auditStatus))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择审核状态。','warning', location.href);", true);
                return;
            }
            MobileUpdateRecordModel model = new MobileUpdateRecordBll().getMobileUpdateRecordModel("ID=" + Id);
            if (model.AuditStatus == -1 || model.AuditStatus == 2)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('该记录已审核。','warning', location.href);", true);
                return;
            }
            model.UpdateTime = DateTime.Now;
            model.Remark = model.Remark ?? "";
            bool flag = false;
            if (auditStatus.Equals("1"))
            {
                model.AuditRecords = "审核通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" +
                                     DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                flag = new MobileUpdateRecordBll().mobileUpdateSuccess(model);
            }
            else
            {
                model.AuditStatus = -1;
                model.AuditRecords = "审核不通过—" + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" +
                                     DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                flag = new MobileUpdateRecordBll().updateMobileUpdateRecord(model);
            }
            if (flag)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核成功。','success', location.href);", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核失败。','error', location.href);", true);
            }
        }
    }
}