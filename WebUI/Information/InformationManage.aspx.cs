using ManageFcmsBll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.Information
{
    public partial class InformationManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //绑定标题小类
                var mediaModelBll = new MediaTypeBll();
                int total = 0;

                DataTable tables = mediaModelBll.GetPageList("ID,Title,LogUrl,CreateTime,CreateUser,LogUrlName", "", "ID asc", 1, 100, out total);

                DataRow dr = tables.NewRow();
                dr["ID"] = "-1";
                dr["Title"] = "请选择媒体报道类型";
                dr["LogUrl"] = "-1";
                dr["CreateTime"] = DateTime.Now;
                dr["CreateUser"] = "-1";
                dr["LogUrlName"] = "-1";

                tables.Rows.InsertAt(dr, 0); 

                SelectMediaType.DataSource = tables;
                SelectMediaType.DataTextField = "Title";
                SelectMediaType.DataValueField = "ID";
                SelectMediaType.DataBind();



            }
        }
    }
}