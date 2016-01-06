using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.Information
{
    public partial class AboutUsEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ColumnBll columnBll = new ColumnBll();
            ColumnModel currentColumn = columnBll.GetModel(base.ColumnId);
            txtTitle.Value = currentColumn.Name;
            if (!IsPostBack)
            {
                AboutUsBll aboutUsBll = new AboutUsBll();
                AboutUsModel model = aboutUsBll.GetModel(base.ColumnId);
                if (model != null)
                {
                    txtTitle.Value = model.Title;
                    txtContent.Value = Server.HtmlDecode(model.Content);
                }
            }
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContent.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入资讯正文','warning', '');", true);
                return;
            }

            AboutUsBll aboutUsBll = new AboutUsBll();
            AboutUsModel model = aboutUsBll.GetModel(base.ColumnId);
            if (model != null)
            {
                model.Content = HtmlHelper.ReplaceHtml(txtContent.Value.Trim());
                model.UpdateTime = DateTime.Now;
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                    aboutUsBll.Update(model)
                                        ? "MessageAlert('修改成功','success', '');"
                                        : "MessageAlert('修改失败','error', '');", true);
            }
            else
            {
                model = new AboutUsModel { Title = txtTitle.Value.Trim(), Content = HtmlHelper.ReplaceHtml(txtContent.Value.Trim()), ColumnID = base.ColumnId, CreateTime = DateTime.Now, UpdateTime = DateTime.Now };
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                    aboutUsBll.Add(model) > 0
                                        ? "MessageAlert('添加成功','success', '');"
                                        : "MessageAlert('添加失败','error', '');", true);
            }
        }
    }
}