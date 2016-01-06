using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using System.Data;
namespace WebUI.p2p
{
    public partial class LoanApplyManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              
        }

        public void ExcelExport_Click(object sender, EventArgs e)
        {
            string uName = txtName.Value.Trim();
            //var filter = "";
            //var OrderBy = "ID DESC";
            //if (!string.IsNullOrEmpty(uName))
            //{
            //    string field = (txtName.Value.Trim().Equals("")) ? "M.MemberName" : "MI.RealName";
            //    field += " AND charindex('" + uName.Trim() + "', " + field + ") >0";
            //}
            var memberBll = new MemberBll();
            var dt = memberBll.MemberData("");

            if (dt!=null && dt.Tables.Count >0 && dt.Tables[0].Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dt.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls", Server.MapPath("/ReportStatistics/ExcelTeml/LoanTemp.xls"), 1, Title,3);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
            }
        }


    }
}