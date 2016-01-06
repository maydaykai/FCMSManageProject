using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.ReportStatistics
{
    public partial class MemberFondRecord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void ExcelExport_Click(object sender, EventArgs e)
        {
           
            var filter = " 1=1 ";
            string uName = txtName.Value.Trim();
            string seachName = "";
            if (uName.ToUpper() == "RJB777")
            {
                seachName = "P.UpdateTime";
            }
            else
            {
                seachName = "P.CreateTime";
            }
            if (!string.IsNullOrEmpty(txtStartDate.Value))
            {
                filter += " and " + seachName + ">='" + txtStartDate.Value + "'";
            }
            if (!string.IsNullOrEmpty(txtEndDate.Value))
            {
                filter += " and " + seachName + "<=DATEADD(DAY,1,'" + txtEndDate.Value + "')";
            }
             
            int memberId = new MemberBll().GetMemberId(uName);
            int total;
            var dataset = new ProcFundReportBll().GetList(memberId, filter, 1, 65535, seachName + " asc, ID asc", out total);
            var dt = dataset.Tables[0];

            dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "FeeTypeString" });
            dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "StatusString" });
            foreach (DataRow dr in dt.Rows)
            {
                int feeType = dr["FeeType"] != null ? Convert.ToInt32(dr["FeeType"]) : -1;
                if (feeType == -1)
                {
                    dr["FeeTypeString"] = "";
                }
                else
                {
                    dr["FeeTypeString"] = FeeType.GetNameByType((FeeType.FeeTypeEnum)feeType);
                }
                int status = dr["Status"] != null ? Convert.ToInt32(dr["Status"]) : -1;
                switch (status) {
                    case 1:
                        dr["StatusString"] = "正常";
                        break;
                    case 2:
                        dr["StatusString"] += "冻结";
                        break;
                    case 3:
                        dr["StatusString"] += "作废";
                        break;
                }
                        

            }
            DataTable dtNew = dt.DefaultView.ToTable(false, new string[] { "RowID", "FeeTypeString", "Amount1", "MemberBalance", "StatusString", "CreateTime", "Description" });

            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dtNew, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls",
                                                       Server.MapPath("ExcelTeml/mfrTemp.xls"), 1, Title,3);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
            }
        }
    }
}