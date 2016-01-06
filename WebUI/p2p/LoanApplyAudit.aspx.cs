using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Office2010.Excel;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class LoanApplyAudit : BasePage
    {
        private int _id; //LoanApplyID

        protected void Page_Load(object sender, EventArgs e)
        {
            selIncreasingMode.Disabled = true;
            BtnAudit.Visible = false;
            BtnAudit.Enabled = false;
            if (ConvertHelper.QueryString(Request, "ID", 0) > 0)
            {
                _id = ConvertHelper.QueryString(Request, "ID", 0);
            }
            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    InitData();
                }
            }
        }

        protected void button1_ServerClick(object sender, EventArgs e)
        {
            
        }

        private void InitData()
        {
            CurrorStatus.Visible = true;
            AuditStatus.Visible = true;
            AuditReco.Visible = true;
            AreaNote.Visible = true;
            BtnAudit.Visible = true;
            BtnAudit.Enabled = true;
            LoanApplyModel info = new LoanApplyBll().GetLoanApplyModel(_id);
            lab_MemberName.InnerText = info.MemberName;
            lab_province.InnerText = info.ProvinceName;
            lab_city.InnerText = info.CityName;
            lab_loanUseName.InnerText = info.LoanUseName;
            lab_loanTerm.InnerText = info.LoanTerm.ToString();
            lab_loanModel.InnerText = info.BorrowMode == 0 ? "天" : "月";
            lab_loanAmount.InnerText = info.LoanAmount.ToString("0.00");
            lab_createTime.InnerText = string.Format("{0}", info.CreateTime);
            
            lab_RepaymentSource.InnerText = info.RepaymentSource;
            lab_RealName.InnerText = info.RealName;

            selIncreasingMode.Value = info.ExamStatus.ToString(CultureInfo.InvariantCulture);
            litAuditReco.InnerHtml = !string.IsNullOrEmpty(info.AuditRecords) ? (info.AuditRecords.Contains("|")
                                         ? (info.AuditRecords.Split('|')[0] + "</br>" + info.AuditRecords.Split('|')[1])
                                         : info.AuditRecords) : "";
            if (selIncreasingMode.Value == "2" || selIncreasingMode.Value == "3" || selIncreasingMode.Value == "4" || selIncreasingMode.Value == "5" || (RightArray.IndexOf("|9|", StringComparison.Ordinal) == -1 && selIncreasingMode.Value == "0") || (RightArray.IndexOf("|11|", StringComparison.Ordinal) == -1 && selIncreasingMode.Value == "1"))
            {
                BtnAudit.Visible = false;
                BtnAudit.Enabled = false;
            }


            if ((RightArray.IndexOf("|9|", StringComparison.Ordinal) >= 0 && selIncreasingMode.Value == "0") || (RightArray.IndexOf("|11|", StringComparison.Ordinal) >= 0 && selIncreasingMode.Value == "1"))
            {
                BtnAudit.Visible = true;
                BtnAudit.Enabled = true;
            }
        }

        protected void BtnAudit_ServerClick(object sender, EventArgs e)
        {
            string auditStatus = Request.Form["AuditStatus"];

            if (string.IsNullOrEmpty(auditStatus))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择审核状态。','error', '');", true);
                return;
            }
            string note = txtAreaNote.Value.Trim();
            var loanApplyBll = new LoanApplyBll();
            var loanApplyModel = loanApplyBll.GetLoanApplyModel(_id);
            switch (selIncreasingMode.Value)
            {
                case "0"://初审
                    loanApplyModel.ExamStatus = Convert.ToInt32(auditStatus) == 1 ? 1 : 2;
                    loanApplyModel.AuditRecords = (auditStatus == "1" ? "初审通过—" : "初审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）|";
                    break;
                case "1"://复审
                    loanApplyModel.ExamStatus = Convert.ToInt32(auditStatus) == 1 ? 4 : 3;
                    loanApplyModel.AuditRecords = loanApplyModel.AuditRecords + (auditStatus == "1" ? "复审通过—" : "复审不通过—") + (new FcmsUserBll().GetModel(MemberId).UserName) + "—" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "—（审核意见：" + (string.IsNullOrEmpty(note) ? "无" : note) + "）|";
                    break;
            }
            loanApplyModel.UpdateTime = DateTime.Now;
            ClientScript.RegisterClientScriptBlock(GetType(), "", loanApplyBll.UpdateStatus(loanApplyModel) ? "MessageAlert('操作成功。','success', '/p2p/LoanApplyAudit.aspx?" + HttpContext.Current.Request.QueryString + "');" : "MessageAlert('操作异常，请联系系统管理员。','error', '');", true);
        }
    }
}