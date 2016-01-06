using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.MemberManage
{
    public partial class MemberAccountManage : BasePage
    {
        protected int _btnOutPut = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsRole("6"))
            {
                _btnOutPut = 0;
            }
            if (!IsPostBack)
            {
                txtTotalDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
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

        //public void ExcelExport_Click(object sender, EventArgs e)
        //{
        //    var filter = " 1=1";
        //    string sUserType = "0";
        //    string uName = "";
        //    int balance = 0;
        //    int accountPayable = 0;
        //    int accountPayableMax = 0;
        //    string dateEnd = "";

        //    sUserType = selSUserType.Value != "" ? selSUserType.Value : "0";
        //    uName = txtName.Value != "" ? txtName.Value : "";
        //    balance = txtBalance.Value != "" ? Convert.ToInt32(txtBalance.Value) : 0;
        //    accountPayable = txtAccountPayable.Value != "" ? Convert.ToInt32(txtAccountPayable.Value) : 0;
        //    accountPayableMax = txtAccountPayableMax.Value != "" ? Convert.ToInt32(txtAccountPayableMax.Value) : accountPayable;
        //    dateEnd = txtTotalDate.Value != "" ? Convert.ToDateTime(txtTotalDate.Value).ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");

        //    if (!string.IsNullOrEmpty(txtName.Value))
        //    {
        //        if (sUserType == "0")
        //        {
        //            filter += " and m.MemberName like '%" + uName + "%'";
        //        }
        //        else
        //        {
        //            filter += " and mi.RealName like '%" + uName + "%'";
        //        }
        //    }
        //    filter += " and m.Balance >= " + balance;
        //    filter += " and dbo.GetMemberAccountPayable(m.id) >= " + accountPayable;

        //    if (accountPayableMax > 0)
        //    {
        //        filter += " and dbo.GetMemberAccountPayable(m.id) < " + accountPayableMax;
        //    }

        //    int pageCount = 0;
        //    var dataset = new MemberBll().GetList(filter, " ID desc", 1, 65535, Convert.ToDateTime(dateEnd), ref pageCount);
        //    var dt = dataset.Tables[0];

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        try
        //        {
        //            ExcelHelper.ExportExcelForDtByNpoi(dt, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls",
        //                                               Server.MapPath("ExcelTeml/mfrTemp.xls"), 1, Title);
        //        }
        //        catch (Exception exx)
        //        {
        //            Log4NetHelper.WriteError(exx);
        //        }
        //    }
        //}
    }
}