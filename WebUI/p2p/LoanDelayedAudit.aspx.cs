using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.p2p
{
    public partial class LoanDelayedAudit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);

        }

        protected void OK_Click(object sender, EventArgs e)
        {
            var loanDelayedBll = new LoanDelayedBll();
            var loanDelayedModel = loanDelayedBll.GetModel(_id);

            loanDelayedModel.AuditStatus = 1;

            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       loanDelayedBll.Update(loanDelayedModel)
                                                           ? "MessageAlertChild('审核成功','success', '../p2p/LoanDelayedManage.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('审核失败','error', '');", true);
        }

        protected void NO_Click(object sender, EventArgs e)
        {
            var loanDelayedBll = new LoanDelayedBll();
            var loanDelayedModel = loanDelayedBll.GetModel(_id);

            loanDelayedModel.AuditStatus = 2;

            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       loanDelayedBll.Update(loanDelayedModel)
                                                           ? "MessageAlertChild('审核成功','success', '../p2p/LoanDelayedManage.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('审核失败','error', '');", true);
        }
    }
}