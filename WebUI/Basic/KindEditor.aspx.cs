using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsCommon;

namespace WebUI.Basic
{
    public partial class KindEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(HttpContext.Current.Server.HtmlDecode(HtmlHelper.ReplaceHtml(Request.Form["content1"])));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write(HttpContext.Current.Server.HtmlDecode(HtmlHelper.ReplaceHtml(Request.Form["content"])));
            Response.Write(HttpContext.Current.Server.HtmlDecode(HtmlHelper.ReplaceHtml(Request.Form["content1"])));
        }
    }
}