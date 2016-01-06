using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.Information
{
    public partial class MediaTypeEdit : BasePage
    {

        private int _id;
        private int _columnId;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            _columnId = ConvertHelper.QueryString(Request, "columnId", 0);


            if (!IsPostBack && _columnId > 0)
            {

                if (_id > 0)
                {
                    var mediaModelBll = new MediaTypeBll();

                    MediaTypeModel currentInformation = mediaModelBll.GetModel(_id);
                    txtTitle.Value = currentInformation.LogUrlName;
                    txt_LogUrlValue.Value = currentInformation.LogUrlValue.ToString();
                    if (!string.IsNullOrEmpty(currentInformation.LogUrl))
                    {
                        imgNewsImg.Src = ConfigHelper.ImgVirtualPath + currentInformation.LogUrl;
                        hiNewsImg.Value = currentInformation.LogUrl;
                    }
                    else
                    {
                        imgNewsImg.Src = "/images/news_con_title.png";
                    }

                }
                else
                {
                    txt_LogUrlValue.Value = "0";
                }
                

            }
            if (IsPostBack)
            {
                if (!string.IsNullOrEmpty(hiNewsImg.Value))
                {
                    imgNewsImg.Src = ConfigHelper.ImgVirtualPath + hiNewsImg.Value.Trim();
                }
            }

        }



        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            //提交按钮
            var mediaModelBll = new MediaTypeBll();
            //上传图片
            if (hiNewsImg.Value.Length < 1)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请上传媒体报道新闻的logo','warning', '');", true);
                return;
            }
            MediaTypeModel model = new MediaTypeModel();
            model.Title = txtTitle.Value.ToString();
            model.LogUrlValue = Convert.ToInt32(txt_LogUrlValue.Value.Length <= 0 ? txt_LogUrlValue.Value : "0");
            model.LogUrlName = txtTitle.Value;
            model.LogUrl = hiNewsImg.Value;
            if (_id > 0)
            {
                model.ID = _id;
                //mediaModelBll.Update(model);

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                    mediaModelBll.Update(model)
                                        ? "MessageAlert('修改成功','success', '/Information/MediaTypeManage.aspx?columnId=" + ColumnId + "');"
                                        : "MessageAlert('修改失败','error', '');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                   mediaModelBll.Add(model) > 0
                                      ? "MessageAlert('添加成功','success', '/Information/MediaTypeManage.aspx?columnId=" + ColumnId + "');"
                                      : "MessageAlert('添加失败','error', '');", true);
            }



        }

        private string EndUrlDir(string url)
        {
            if (!url.EndsWith("/"))
            {
                url += "/";
            }
            return url;
        }
    }
}