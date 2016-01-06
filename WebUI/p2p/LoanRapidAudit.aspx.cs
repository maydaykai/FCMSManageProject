using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.p2p
{
    public partial class LoanRapidAudit : BasePage
    {
        private int _id; //LoanRapidID

        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            InitData();
        }

        protected void button1_ServerClick(object sender, EventArgs e)
        {
            LoanRapidModel model = new LoanRapidModel() { ID = _id, Status = 1, UpdateTime = DateTime.Now };
            bool flag = new LoanRapidBLL().UpdateStatus(model);
            if (flag)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核成功','success', '/p2p/LoanRapidManage.aspx');", true);
                button1.Disabled = true;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核失败','error', '');", true);
            }
            
        }

        private void InitData()
        {
            LoanRapidModel info = new LoanRapidBLL().GetLoanRapidModel(_id);
            lab_MemberName.InnerText = info.Name;
            lab_memberPhone.InnerText = info.Phone;
            lab_province.InnerText = info.ProvinceName;
            lab_city.InnerText = info.CityName;
            lab_loanUseName.InnerText = info.LoanUseName;
            lab_loanTerm.InnerText = info.LoanTerm.ToString();
            lab_loanModel.InnerText = info.LoanMode == 0 ? "天" : "月";
            lab_loanAmount.InnerText = info.LoanAmount.ToString("0.00");
            lab_createTime.InnerText = string.Format("{0}", info.CreateTime);
            lab_status.InnerText = info.Status == 0 ? "未审核" : "已审核";
            lab_Describe.InnerText = info.Describe;
            lab_RealName.InnerText = info.RealName;
            if (info.Status == 1)
            {
                button1.Disabled = true;
            }
        }
    }
}