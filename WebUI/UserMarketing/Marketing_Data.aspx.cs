using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.UserMarketing
{
    public partial class Marketing_Data : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRoleId();
            }

        }

        //活动当前角色
        public void GetRoleId()
        {
            //当前登录用户Id
            int curr_Id = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString()); 
            //根据当前角色Id查询所属角色
            Marketing_RoleBLL _bll = new Marketing_RoleBLL();
            DataTable table = _bll.GetMarkingRoleListByUserInfoId(curr_Id, 1).Tables[0];
            if (table == null || table.Rows.Count <= 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请先指定当前系统用户角色','warning', '');", true);
                return;

            }
            int MarketingId = Convert.ToInt32(table.Rows[0]["RoleNameId"]);//角色Id
            int UserInfoId = Convert.ToInt32(table.Rows[0]["UserInfoId"]);//用户Id
            //加载列表
            marketingId.Value = MarketingId.ToString();
            userId.Value = UserInfoId.ToString();

        }

    }
}