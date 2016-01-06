using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ManageFcmsCommon;
using ManageFcmsBll;
using ManageFcmsDal;
using ManageFcmsModel;

namespace WebUI.Basic
{
    public partial class ParameterConfiguration : BasePage
    {
        private ParameterSetBll _parameterSetBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            _parameterSetBll = new ParameterSetBll();
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            var list = _parameterSetBll.GetParameterSetList();
            Repeater1.DataSource = list;
            Repeater1.DataBind();
        }

        //保存
        protected void btnOK_Click(object sender, EventArgs e)
        {
            var list = (from RepeaterItem item in Repeater1.Items
                        let hidId = item.FindControl("hidID") as HiddenField
                        where hidId != null
                        let txtValue = item.FindControl("txtValue") as HtmlInputText
                        select new ParameterSetModel()
                            {
                                ID = ConvertHelper.ToInt(hidId.Value),
                                ParameterValue = ConvertHelper.ToDecimal(txtValue.Value)
                            }).ToList();

            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       _parameterSetBll.Update(list)
                                           ? "MessageAlert('更新成功。','success', '/Basic/ParameterConfiguration.aspx?columnId=" + ColumnId + "');"
                                           : "MessageAlert('更新失败。','error', '');", true);

        }

        //行绑定事件，设置数据格式
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hidType = e.Item.FindControl("hidType") as HiddenField;
                if (hidType != null)
                {
                    HtmlInputText txtValue = (e.Item.FindControl("txtValue") as HtmlInputText);
                    if (txtValue != null)
                    {
                        string txtvalue = txtValue.Value;
                        string value = txtvalue;

                        int type = ConvertHelper.ToInt(hidType.Value);
                        switch (type)
                        {
                            case 1:
                                value = ConvertHelper.ToDecimal(txtvalue).ToString("0");
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                value = ConvertHelper.ToDecimal(txtvalue).ToString("0.00");
                                break;
                        }
                        txtValue.Value = value;
                    }
                }
            }
        }
    }
}