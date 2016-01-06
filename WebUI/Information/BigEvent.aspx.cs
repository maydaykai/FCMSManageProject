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
    public partial class BigEvent : BasePage
    {
        private int _id;
        private int _columnId;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            _columnId = ConvertHelper.QueryString(Request, "columnId", 0);
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

                if (_id > 0)
                {
                    var informationBll = new InformationBll();
                    InformationModel currentInformation = informationBll.GetModel(_id);
                    txtTitle.Value = currentInformation.Title;
                    txt_ShowDesc.Value = currentInformation.ShowDesc.ToString();
                    txt_SummaryCount.Value = currentInformation.SummaryCount;
                    txtUrl.Value = currentInformation.url;
                    cbRecommend.Checked = currentInformation.Recommend;
                    txtPubTime.Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentInformation.PubTime);

                    selSections.Value = currentInformation.SectionID.ToString();
                    switch (currentInformation.Status)
                    {
                        case 1: rdStatusY.Checked = true; break;
                        case 2: rdStatusN.Checked = true; break;
                    }
                    trAudit.Style["display"] = "table-row";
                }
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入事件标题','warning', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(txt_SummaryCount.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入摘要','warning', '');", true);
                return;
            }

            if (!rdStatusY.Checked && !rdStatusN.Checked)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请审核该资讯','warning', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(txtUrl.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入Url','warning', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(txtPubTime.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入发布时间','warning', '');", true);
                return;
            }

            var informationBll = new InformationBll();

            if (_id > 0)
            {
                var informationModel = informationBll.GetModel(_id);

                informationModel.SectionID = int.Parse(selSections.Value.Trim());
                informationModel.Title = txtTitle.Value.Trim();
                informationModel.SummaryCount = txt_SummaryCount.Value.Trim();
                informationModel.ExtendedContent = null;
                informationModel.url = txtUrl.Value.Trim();
                informationModel.PubTime = DateTime.Parse(txtPubTime.Value.Trim());
                informationModel.Recommend = cbRecommend.Checked;
                informationModel.ExtendedContent = "";
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
                                         ? "MessageAlert('修改成功','success', '/Information/BigEventManage.aspx?columnId=" + ColumnId + "');"
                                         : "MessageAlert('修改失败','error', '');", true);
            }
            else
            {
                int relust = 0;
                if (int.TryParse(txt_ShowDesc.Value, out relust))
                {
                    relust = Convert.ToInt32(txt_ShowDesc.Value);
                }

                int Image_value = 0;
                if (!string.IsNullOrEmpty(hie_Image_value.Value))
                {
                    Image_value = Convert.ToInt32(hie_Image_value.Value);
                } 
                
                int status = 0;
                if (rdStatusY.Checked) status = 1;
                else if (rdStatusN.Checked) status = 2;
                else status = 0;

                var informationModel = new InformationModel
                {
                    SummaryCount = txt_SummaryCount.Value,
                    SectionID = int.Parse(selSections.Value),
                    Status = status,
                    ShowDesc = relust,
                    Title = txtTitle.Value.Trim(),
                    Content = "",
                    Recommend =  cbRecommend.Checked,
                    PubTime = DateTime.Parse(txtPubTime.Value.Trim()),
                    UpdateTime = DateTime.Now,
                    Image_value = 0,
                    MediaTypeId = 0,
                    NewsImage="",
                    url=txtUrl.Value.Trim(),
                    ExtendedContent=""
                };
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                      informationBll.Add(informationModel) > 0
                                          ? "MessageAlert('添加成功','success', '/Information/BigEventManage.aspx?columnId=" + ColumnId + "');"
                                          : "MessageAlert('添加失败','error', '');", true);
            }
        }
    }
}