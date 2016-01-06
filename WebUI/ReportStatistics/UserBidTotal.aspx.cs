using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.ReportStatistics
{
    public partial class UserBidTotal : BasePage
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
    }
}