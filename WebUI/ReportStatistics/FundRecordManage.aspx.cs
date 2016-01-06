using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.ReportStatistics
{
    public partial class FundRecordManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGcList();
            }
        }
        
        //绑定担保公司下拉列表
        private void BindGcList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetGcList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            selBondingCompany.DataSource = ds;
            selBondingCompany.DataValueField = "RelationID";
            selBondingCompany.DataTextField = "RealName";
            selBondingCompany.DataBind();
            selBondingCompany.Items.Insert(0, new ListItem("--请选择担保公司--", ""));
        }

        //导出Excel
        public void btnOutput_Click(object sender, EventArgs e)
        {
            string payeeType = selPayeeType.Value.Trim();
            string payeeMemberName = txtPayeeMemberName.Value.Trim();
            string partyType = selPartyType.Value.Trim();
            string partyMemberName = txtPartyMemberName.Value.Trim();
            string andor = selAndOr.Value.Trim();
            string bondingCompany = selBondingCompany.Text.Trim();
            string status = selStatus.Value.Trim();
            string feeType = jqxSelectVal.Value.Trim();
            string startTime = txtStartDate.Value.Trim();
            string endTime = txtEndDate.Value.Trim();

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(payeeMemberName))
            {
                filter += " and ";
                if (andor == "1" && !string.IsNullOrEmpty(partyMemberName)) { filter += " ( "; }
                filter += payeeType == "0" ? " M1.MemberName like '%" + payeeMemberName + "%'" : " MI1.RealName like '%" + payeeMemberName + "%'";
            }
            if (!string.IsNullOrEmpty(partyMemberName))
            {
                filter += andor == "0" ? " and " : " or ";

                filter += partyType == "0" ? " M2.MemberName like '%" + partyMemberName + "%'" : " MI2.RealName like '%" + partyMemberName + "%'";
                if (andor == "1" && !string.IsNullOrEmpty(partyMemberName)) { filter += " ) "; }
            }

            if (!string.IsNullOrEmpty(bondingCompany))
            {
                filter += " and L.GuaranteeID=" + bondingCompany;
            }
            if (!string.IsNullOrEmpty(status))
            {
                filter += " and P.Status=" + status;
            }
            if (!string.IsNullOrEmpty(feeType) && !feeType.Equals("-1") && !feeType.Equals("-1,"))
            {
                filter += " and P.FeeType in (" + feeType.Remove(feeType.Length - 1, 1) + ")";
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                filter += " and P.UpdateTime>='" + startTime + "'";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                filter += " and P.UpdateTime<='" + endTime + "'";
            }

            var fundRecordBll = new FundRecordBll();
            var dt = fundRecordBll.GetFundRecordList(filter);
            if (dt == null || dt.Rows.Count <= 0) return;
            try
            {
                ExcelHelper.ExportExcelForDtByNpoi(dt, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls", Server.MapPath("ExcelTeml/FundRecordTemp.xls"), 1, "资金流水明细报表", 1);
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
            }
        }
    }
}