using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System.Data;

namespace WebUI.MemberManage
{
    public partial class QuickBankCardEdit : BasePage
    {
        private int _id;
        private int BankCode;
        private int BankID;
       //  public bool RightUpdate = false;
        protected void Page_Load(object sender, EventArgs e)
        {   
            _id = ConvertHelper.QueryString(Request, "id", 0);

            //权限控制
           // RightUpdate = RightArray.IndexOf("|3|", StringComparison.Ordinal) >= 0;

            if (!IsPostBack)
            {  
                if (_id > 0)
                {
                    var bll = new BankAccountBll();
                    var model = bll.GetBankAccountAuthentModel("BA.ID=" + _id);
                    txtMemberName.Value = new MemberBll().GetModel(model.MemberID).MemberName;
                    txtRealName.Value = new MemberInfoBll().GetModel(model.MemberID).RealName;
                    //txtBankName.Value = model.BankName;
                    txtBankCardNo.Value = model.BankCardNo;
                    ckbEnable.Checked = model.Status < 3;

                    selCurrStatus.DataTextField = "BankName";
                    selCurrStatus.DataValueField = "CodeId";
                    selCurrStatus.DataSource = new BankBll().GetBankTypeList();
                    selCurrStatus.DataBind();
                    selCurrStatus.SelectedIndex = selCurrStatus.Items.IndexOf(selCurrStatus.Items.FindByText(model.BankName));
                }

            }
        }

        //修改
        protected void Operate_Click(object sender, EventArgs e)
        {
            if (_id > 0)
            {
                var bll = new BankAccountBll();
                var model = bll.GetBankAccountAuthentModel("BA.ID=" + _id);
                var cashRecordModel =
                    new CashRecordBLL().getCashRecordModel("MemberID=" + model.MemberID +
                                                           " AND BankAccountType=2 AND BankAccountID=" + _id +
                                                           " AND [Status] < 2");
                if (cashRecordModel != null)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('该卡尚有未审核的提现，无法修改！','error', '');", true);
                }
                var d = selCurrStatus.SelectedValue.Split(',');
                BankCode = Convert.ToInt32(d[1]);
                BankID = Convert.ToInt32(d[0]); 
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       bll.UpdBankAccountAuthentById(_id, BankCode)
                                                           ? "MessageAlertChild('修改成功','success', '/MemberManage/QuickBankAccountManage.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('修改失败','error', '');", true);
              
                new BankCardAuthentBLL().UpdaBankCardAccountByIDExits(BankID, _id);
            }
        }

        //禁用
        protected void Bear_Click(object sender, EventArgs e)
        {
            if (_id > 0)
            {
                var bll = new BankAccountBll();
                var model = bll.GetBankAccountAuthentModel("BA.ID=" + _id);
                var cashRecordModel =
                    new CashRecordBLL().getCashRecordModel("MemberID=" + model.MemberID +
                                                           " AND BankAccountType=2 AND BankAccountID=" + _id +
                                                           " AND [Status] < 2");
                if (cashRecordModel != null)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('该卡尚有未审核的提现，无法修改！','error', '');", true);
                }
                var d = selCurrStatus.SelectedValue.Split(',');
                BankCode = Convert.ToInt32(d[1]);
                BankID = Convert.ToInt32(d[0]);
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       bll.UpdateBankAccountAuthent(_id)
                                                           ? "MessageAlertChild('修改成功','success', '/MemberManage/QuickBankAccountManage.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('修改失败','error', '');", true);

                new BankCardAuthentBLL().UpdaBankCardAccountByIDExits(BankID, _id);
            }
        }
    }
}