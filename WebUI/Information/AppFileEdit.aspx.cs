using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.Information
{
    public partial class AppFileEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);

            string AppFileFullDir = ConfigHelper.AppFileVirtualPath;

            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var appVersionBll = new AppVersionBll();
                    var appVersionModel = appVersionBll.GetModel(_id);
                    //if (!string.IsNullOrEmpty(focusFigureModel.LargePicture))
                    //{
                    //    imgLargePic.Src = imgFullDir + focusFigureModel.LargePicture;
                    //    hiLargePic.Value = focusFigureModel.LargePicture;
                    //}
                    //else
                    //{
                    //    imgLargePic.Src = "../images/nonepic_l.jpg";
                    //}
                    if (!string.IsNullOrEmpty(appVersionModel.OS))
                    {
                        txtOS.Value = appVersionModel.OS;
                    }
                    if (!string.IsNullOrEmpty(appVersionModel.AppName))
                    {
                        txtAppName.Value = appVersionModel.AppName;
                    }
                    if (!string.IsNullOrEmpty(appVersionModel.Version))
                    {
                        txtVersion.Value = appVersionModel.Version;
                    }
                    if (!string.IsNullOrEmpty(appVersionModel.VersionNum))
                    {
                        txtVersionNum.Value = appVersionModel.VersionNum;
                    }
                    if (!string.IsNullOrEmpty(appVersionModel.DownURL))
                    {
                        txtDownURL.Value = appVersionModel.DownURL;
                    }
                }
            }
            else
            {
                //if (!string.IsNullOrEmpty(hiSmallPic.Value))
                //{
                //    imgSmallPic.Src = imgFullDir + hiSmallPic.Value;
                //}
                //if (!string.IsNullOrEmpty(hiLargePic.Value))
                //{
                //    imgLargePic.Src = imgFullDir + hiLargePic.Value;
                //}
            }
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOS.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请填写操作系统名称','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtAppName.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请填写应用名称','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtVersion.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请填写版本号','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtVersionNum.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请填写版本编号','warning', '');", true);
                return;
            }

            var appVersionBll = new AppVersionBll();

            var appVersionModel = appVersionBll.GetModel(_id);
            appVersionModel.OS = txtOS.Value.Trim();
            appVersionModel.AppName = txtAppName.Value.Trim();
            appVersionModel.Version = txtVersion.Value.Trim();
            appVersionModel.VersionNum = txtVersionNum.Value.Trim();
            appVersionModel.DownURL = txtDownURL.Value.Trim();
            appVersionModel.UpdateTime = DateTime.Now;
                
            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                    appVersionBll.Update(appVersionModel)
                                        ? "MessageAlert('修改成功','success', '/Information/AppFileManage.aspx?columnId=" + ColumnId + "');"
                                        : "MessageAlert('修改失败','error', '');", true);

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