using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class BiddingView : BasePage
    {
        private int _loanId;
        protected void Page_Load(object sender, EventArgs e)
        {
            _loanId = Convert.ToInt16(HttpContext.Current.Request["ID"]);
            if (!Page.IsPostBack)
            {
                if(_loanId > 0)
                    InitData();
            }
        }

        private void InitData()
        {
            LoanModel model = new LoanBll().GetLoanInfoModel("ID=" + _loanId);
            loanTitle.InnerText = model.LoanTitle;
            loanNumber.InnerText = model.LoanNumber;

            List<BidModel> list = new BidBLL().GetBidRecordList("B.LoanID=" + _loanId);
            Repeater1.DataSource = list;
            Repeater1.DataBind();
        }
    }
}