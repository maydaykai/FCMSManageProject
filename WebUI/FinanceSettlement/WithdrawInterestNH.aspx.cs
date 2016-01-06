using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.FinanceSettlement
{
    public partial class WithdrawInterestNH : BasePage
    {
        protected int _btnOutPut = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsRole("6"))
            {
                _btnOutPut = 0;
            }
        }

        /// <summary>
        /// 判断是否包含权限
        /// </summary>
        /// <param name="role"></param>要判断的权限
        /// <returns></returns>
        public static bool IsRole(string role)
        {
            return (RightArray.IndexOf("|" + role + "|", StringComparison.Ordinal) >= 0);
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            FundRecordBll frb = new FundRecordBll();
            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                  frb.ExeProcWithdrawInterestNH()
                                                                       ? "<script type='text/javascript'>alert('过期收益收回成功！');</script>"
                                                                       : "<script type='text/javascript'>alert('操作失败！');</script>");
        }

        public void ExcelExport_Click(object sender, EventArgs e)
        {
            var dataset = new MemberBll().GetWithdrawInterestNhList();
            var dt = dataset.Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    ExcelHelper.ExportExcelForDtByNpoi(dt, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls",
                                                       Server.MapPath("ExcelTeml/wiNHTemp.xls"), 1, Title,3);
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
            }
        }
    }
}