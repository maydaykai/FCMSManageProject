using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected int MemberId;
        protected int ColumnId;
        protected static string RightArray = string.Empty;
        public bool RightSearch = false;//查询、查看权限

        protected override void OnLoad(EventArgs e)
        {
            ColumnId = ConvertHelper.QueryString(Request, "columnId", 0);
            if (SessionHelper.Exists("FcmsUserId"))
            {
                MemberId = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());

                if (MemberId > 0)
                {
                    if (ColumnId == -1)
                    {
                    }
                    else
                    {
                        if (SessionHelper.Exists("FcmsRole"))
                        {
                            var roleRightBll = new RoleRightBll();
                            RightArray = roleRightBll.GetRightListByRoleIdAndCid(SessionHelper.Get("FcmsRole").ToString(), ColumnId);
                            RightArray = "|" + RightArray + "|";

                            RightSearch = RightArray.IndexOf("|1|", StringComparison.Ordinal) >= 0;
                            if (!RightSearch)//查看
                            {
                                Response.Write("没有查看权限！");
                                Response.End();
                            }
                        }
                    }
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">top.window.location='/ManageLogin.aspx'</script>");
                    Response.End();
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">top.window.location='/ManageLogin.aspx'</script>");
                Response.End();
            }
            base.OnLoad(e);
        }
    }
}