using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class AppointmentBiddingUserModify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBingData();
            }
        }

        public void GetBingData()
        {
            var id = Request.QueryString["id"];
            var data = new LoanBll().GetAppointmentBiddingUser(int.Parse(id));
            this.txtMobile.Value = data.Rows[0]["mobile"].ToString();
            this.txtAmount.Value = data.Rows[0]["amount"].ToString();
            this.txtMemberID.Value = data.Rows[0]["memberID"].ToString();
            this.txtMemberName.Value = data.Rows[0]["memberName"].ToString();
            this.spCreateTime.InnerText = data.Rows[0]["createTime"].ToString();
            this.spWarningRecord.InnerHtml = data.Rows[0]["operationRecord"].ToString().Replace("|", "</br>");

            lblRadio4.Visible = false;
            Radio4.Visible = false;
            switch (data.Rows[0]["status"].ToString())
            {
                case "0":
                    Radio4.Disabled = true;
                    Radio1.Checked = true;
                    break;
                case "1":
                    Radio4.Disabled = true;
                    Radio2.Checked = true;
                    break;
                case "2":
                    Radio1.Disabled = true;
                    Radio2.Disabled = true;
                    Radio3.Disabled = true;
                    Radio4.Checked = true;
                    lblRadio4.Visible = true;
                    Radio4.Visible = true;
                    btnSave.Visible = false;
                    break;
                case "3":
                    Radio4.Disabled = true;
                    Radio3.Checked = true;
                    break;
            }
        }

        //提交数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int status = 0;
            if (Radio1.Checked)
                status = 0;
            if (Radio2.Checked)
                status = 1;
            if (Radio3.Checked)
                status = 3;

            int? memberId = null;
            if (!string.IsNullOrEmpty(this.txtMemberID.Value))
                memberId = int.Parse(this.txtMemberID.Value);

            if (new LoanBll().AddAppointmentBiddingUser(int.Parse(Request.QueryString["id"]), memberId, status, this.MemberId, txtNote.Value.Trim()))
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('操作成功','success', location.href);", true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('操作失败','error', location.href);", true);
        }
    }
}