using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.Basic
{
    public partial class BidConfigEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var bidConfigBll = new BidConfigBll();
                    var bidConfigModel = bidConfigBll.GetModel(_id);
                    txtLoanAmount.Value = bidConfigModel.LoanAmount.ToString("0.00");
                    txtMinBidAmount.Value = bidConfigModel.MinInvestment.ToString("0.00");
                    txtMaxBidAmount.Value = bidConfigModel.MaxInvestment.ToString("0.00");
                    ckbEnable.Checked = bidConfigModel.EnableStatus;
                }
            }
        }

        protected void Operate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLoanAmount.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入借款金额','warning', '');", true);
                return;
            }

            var bidConfigBll = new BidConfigBll();
            if (_id > 0)
            {
                var bidConfigModel = bidConfigBll.GetModel(_id);
                bidConfigModel.LoanAmount = ConvertHelper.ToDecimal(txtLoanAmount.Value.Trim());
                bidConfigModel.MinInvestment = ConvertHelper.ToDecimal(txtMinBidAmount.Value.Trim());
                bidConfigModel.MaxInvestment = ConvertHelper.ToDecimal(txtMaxBidAmount.Value.Trim());
                bidConfigModel.UpdateTime = DateTime.Now;
                bidConfigModel.EnableStatus = ckbEnable.Checked;

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       bidConfigBll.Update(bidConfigModel)
                                                           ? "MessageAlertChild('修改成功','success', '/Basic/BidConfigManage.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('修改失败','error', '');", true);

            }
            else
            {
                var bidConfigModel = new BidConfigModel()
                {
                    LoanAmount = ConvertHelper.ToDecimal(txtLoanAmount.Value.Trim()),
                    MinInvestment = ConvertHelper.ToDecimal(txtMinBidAmount.Value.Trim()),
                    MaxInvestment = ConvertHelper.ToDecimal(txtMaxBidAmount.Value.Trim()),
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    EnableStatus = ckbEnable.Checked
                };

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       bidConfigBll.Add(bidConfigModel) > 0
                                           ? "MessageAlertChild('添加成功','success', '/Basic/BidConfigManage.aspx?columnId=" + ColumnId + "');"
                                           : "MessageAlertChild('添加失败','error', '');", true);
            }
        }
    }
}