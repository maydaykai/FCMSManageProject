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
using NPOI.SS.Formula.Functions;
using System.Text;
using System.Xml;

namespace WebUI.Information
{
    public partial class InformationEdit : BasePage
    {
        private int _id;
        private int _columnId;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            _columnId = ConvertHelper.QueryString(Request, "columnId", 0);

            // XmlHelper xmlHelper = new XmlHelper(Server.MapPath("~/Config/upload.xml"));
            // string remoteDomain = xmlHelper.GetText("upload/remoteDomain");
            // string secLevelDomain = xmlHelper.GetText("upload/secLevelDomain");

            if (!IsPostBack && _columnId > 0)
            {
                var columnBll = new ColumnBll();
                var currentColumn = columnBll.GetModel(_columnId);
                var columns = columnBll.GetChildColumnList(" ID, ParentID, Name ", 1, currentColumn.ParentID);
                DataTable dt = TreeHelper.Modfiy(columns.Tables[0], false, currentColumn.ParentID.ToString());
                dt.Rows.RemoveAt(2);
                selSections.DataSource = dt;
                selSections.DataTextField = "Name";
                selSections.DataValueField = "ID";
                selSections.DataBind();
                selSections.Value = _columnId.ToString();


                //绑定标题小类
                var mediaModelBll = new MediaTypeBll();
                int total = 0;
                DataTable tables = mediaModelBll.GetPageList("ID,Title,LogUrl,CreateTime,CreateUser,LogUrlName", "", "ID Desc", 1, 100, out total);
                SelectMediaType.DataSource = tables;
                SelectMediaType.DataTextField = "Title";
                SelectMediaType.DataValueField = "ID";
                SelectMediaType.DataBind();



                ///_columnId=媒体报道显示小类标题跟logo   trAudit.Style["display"] = "table-row";
                if (_columnId == 138)
                {
                    Categories.Style["display"] = "table-row";
                    Media_logo.Style["display"] = "table-row";
                    SummaryCountId.Style["display"] = "table-row";
                    AuthorId.Style["display"] = "table-row";
                    SourceId.Style["display"] = "table-row";

                }
                if (_id > 0)
                {
                    var informationBll = new InformationBll();
                    InformationModel currentInformation = informationBll.GetModel(_id);
                    txtContent.Value = Server.HtmlDecode(currentInformation.Content);
                    txtPubTime.Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentInformation.PubTime);
                    txtTitle.Value = currentInformation.Title;
                    cbRecommend.Checked = currentInformation.Recommend;
                    txt_ShowDesc.Value = currentInformation.ShowDesc.ToString();
                    txt_SummaryCount.Value = currentInformation.SummaryCount;

                    if (currentInformation.ExtendedContent.Length > 1)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(currentInformation.ExtendedContent);
                        foreach (XmlNode node in doc.SelectNodes("MediaTypeList/List"))
                        {
                            txt_Source.Value = node.Attributes["SourceUrl"].Value;
                            txt_Author.Value = node.Attributes["Author"].Value;
                        }
                    
                    }


                    if (_columnId == 138)
                    {
                        SelectMediaType.Value = currentInformation.MediaTypeId.ToString();
                    }

                    if (!string.IsNullOrEmpty(currentInformation.NewsImage))
                    {
                        imgNewsImg.Src = ConfigHelper.ImgVirtualPath + currentInformation.NewsImage;
                        hiNewsImg.Value = currentInformation.NewsImage;
                    }
                    else
                    {
                        imgNewsImg.Src = "/images/news_con_title.png";
                    }
                    //查询资讯扩展类
                    if (currentInformation.MediaTypeId > 0)
                    {

                        //string NewsImage = "";
                        int curr_value = Convert.ToInt32(SelectMediaType.Value);

                        var model = mediaModelBll.GetModel(curr_value);

                        // string imgFullUrl = EndUrlDir(remoteDomain);
                        // imgFullUrl += EndUrlDir(secLevelDomain);
                        // imgFullUrl += "image/";
                        //imgFullUrl += model.LogUrl;
                        imgNewsImg_logo.Src = ConfigHelper.ImgVirtualPath + model.LogUrl;
                        hiNewsImg_logo.Value = model.LogUrl;
                        hie_source.Value = model.Title;
                    }
                    else
                    {
                        imgNewsImg_logo.Src = "/images/news_con_title.png";
                    }

                    selSections.Value = currentInformation.SectionID.ToString();
                    switch (currentInformation.Status)
                    {
                        case 1: rdStatusY.Checked = true; break;
                        case 2: rdStatusN.Checked = true; break;
                    }
                    trAudit.Style["display"] = "table-row";
                }
                else
                {
                    txtContent.Value = "<span style=\"line-height:2.5;font-family:SimSun;font-size:14px;\">尊敬的融金宝用户：</span><br />" +
"<p style=\"text-indent:2em;\">" +
"	<span style=\"line-height:1.5;font-family:SimSun;font-size:14px;\">公告正文</span> " +
"</p>" +
"<p style=\"text-indent:2em;\">" +
"	<span style=\"line-height:1.5;font-family:SimSun;font-size:14px;\">公告正文</span> " +
"</p>" +
"<p style=\"text-align:right;color:#3E3E3E;text-indent:2em;font-family:Arial, sans-serif;background-color:#FFFFFF;\">" +
"	<span style=\"line-height:2.5;font-family:SimSun;font-size:14px;\">深圳融金宝互联网金融服务有限公司</span> " +
"</p>" +
"<p style=\"text-align:right;color:#3E3E3E;text-indent:2em;font-family:Arial, sans-serif;background-color:#FFFFFF;\">" +
"	<span style=\"line-height:2.5;font-family:SimSun;font-size:14px;\">" + DateTime.Now.Date.ToString("yyyy年MM月dd日") + "</span> </p>";

                    txt_ShowDesc.Value = "0";
                    SelectMediaType.Value = "1";
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
            StringBuilder strxml = new StringBuilder();
            if (string.IsNullOrEmpty(txtTitle.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入资讯标题','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtPubTime.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入发布时间','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtContent.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入资讯正文','warning', '');", true);
                return;
            }
            if (_columnId == 138)
            {
                //Convert.ToInt32(SelectMediaType.Value) <= 0
                if (string.IsNullOrEmpty(SelectMediaType.Value))
                {

                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请先选择标题小类类别','warning', '');", true);
                    return;
                }
                //上传图片
                if (hiNewsImg_logo.Value.Length < 1)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请上传媒体报道新闻的logo','warning', '');", true);
                    return;
                }
                if (txt_Author.Value.Length < 1)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入作者','warning', '');", true);
                    return;
                }
                if (txt_Source.Value.Length < 1)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入来源连接地址','warning', '');", true);
                    return;
                }
                //作者跟来源


                strxml.Append("<MediaTypeList><List");
                strxml.Append(" SourceUrl=\"" + txt_Source.Value + "\" Author=\"" + txt_Author.Value + "\"  Source=\"" + hie_source.Value + "\">");
                strxml.Append("</List> </MediaTypeList>");


            }
            //_columnId=媒体报道显示小类标题跟logo

            var informationBll = new InformationBll();

            //如果设为推荐
            if (cbRecommend.Checked && _columnId == 138)
            {
                informationBll.UpdateAllStatus(_columnId);
            }

            if (_id > 0)
            {

                if (!rdStatusY.Checked && !rdStatusN.Checked)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请审核该资讯','warning', '');", true);
                    return;
                }
                var informationModel = informationBll.GetModel(_id);

                //资讯图片
                if (hiNewsImg.Value.Trim().Length > 0)
                {
                    informationModel.NewsImage = hiNewsImg.Value.Trim();

                }
                //
                informationModel.url = "";
                informationModel.ExtendedContent = strxml.ToString();
                informationModel.PubTime = DateTime.Parse(txtPubTime.Value.Trim());
                informationModel.Recommend = cbRecommend.Checked;
                informationModel.SectionID = int.Parse(selSections.Value.Trim());
                informationModel.Title = txtTitle.Value.Trim();
                informationModel.Content = Server.HtmlEncode(txtContent.Value);
                informationModel.UpdateTime = DateTime.Now;

                informationModel.Image_value = Convert.ToInt32(hie_Image_value.Value.Length > 0 ? hie_Image_value.Value : "0");
                informationModel.SummaryCount = txt_SummaryCount.Value;
                if (!string.IsNullOrEmpty(SelectMediaType.Value))
                {
                    informationModel.MediaTypeId = Convert.ToInt32(SelectMediaType.Value);
                }
                else
                {
                    informationModel.MediaTypeId = 0;
                }
                int relust = 0;
                if (int.TryParse(txt_ShowDesc.Value, out relust))
                {
                    informationModel.ShowDesc = Convert.ToInt32(txt_ShowDesc.Value);
                }
                else
                {
                    informationModel.ShowDesc = 0;
                }

                int status = 0;
                if (rdStatusY.Checked) status = 1;
                else if (rdStatusN.Checked) status = 2;
                else status = 0;
                informationModel.Status = status;
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                     informationBll.Update(informationModel)
                                         ? "MessageAlert('修改成功','success', '/Information/InformationManage.aspx?columnId=" + ColumnId + "');"
                                         : "MessageAlert('修改失败','error', '');", true);
                // 数据推送
                if (informationModel.Status == 1 && informationModel.SectionID == 32)
                {
                    var content = HtmlHelper.DeleteHtml(txtContent.Value);
                    new MobilePushBll().Add(new MobilePushModel { CreateTime = informationModel.PubTime, EventID = informationModel.ID, MessageType = 1, PushContent = content.Length >= 50 ? content.Substring(0, 51) : content, PushStatus = false, PushTitle = informationModel.Title, UpdateTime = DateTime.Now });
                }
            }
            else
            {
                int relust = 0;
                if (int.TryParse(txt_ShowDesc.Value, out relust))
                {
                    relust = Convert.ToInt32(txt_ShowDesc.Value);
                }

                int SelectMediaTypeId = 0;
                int Image_value = 0;
                if (!string.IsNullOrEmpty(SelectMediaType.Value))
                {
                    SelectMediaTypeId = Convert.ToInt32(SelectMediaType.Value);
                }
                if (!string.IsNullOrEmpty(hie_Image_value.Value))
                {
                    Image_value = Convert.ToInt32(hie_Image_value.Value);
                }

                var informationModel = new InformationModel
                {
                    Content = HtmlHelper.ReplaceHtml(txtContent.Value.Trim()),
                    SummaryCount = txt_SummaryCount.Value,
                    NewsImage = hiNewsImg.Value.Trim(),
                    PubTime = DateTime.Parse(txtPubTime.Value.Trim()),
                    Recommend = cbRecommend.Checked,
                    SectionID = int.Parse(selSections.Value),
                    Status = 0,
                    ShowDesc = relust,
                    Title = txtTitle.Value.Trim(),
                    MediaTypeId = SelectMediaTypeId,
                    Image_value = Image_value,
                    UpdateTime = DateTime.Now,
                    url = "",
                    ExtendedContent = strxml.ToString()
                };
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                      informationBll.Add(informationModel) > 0
                                          ? "MessageAlert('添加成功','success', '/Information/InformationManage.aspx?columnId=" + ColumnId + "');"
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            var mediaModelBll = new MediaTypeBll();
            //获得当前选中的值
            int curr_value = Convert.ToInt32(SelectMediaType.Value);

            var model = mediaModelBll.GetModel(curr_value);
           
            if (!string.IsNullOrEmpty(model.LogUrl))
            {
                imgNewsImg_logo.Src = ConfigHelper.ImgVirtualPath + model.LogUrl;
                hiNewsImg_logo.Value = model.LogUrl;
                hie_Image_value.Value = model.ID.ToString();
                hie_source.Value = model.Title;
            }

        }
    }
}