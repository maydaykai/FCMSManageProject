using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class GuaranteeNoAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGcList();
        }
        //绑定担保公司下拉列表
        private void BindGcList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetGcList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_guaranteeId.DataSource = ds;
            sel_guaranteeId.DataValueField = "ID";
            sel_guaranteeId.DataTextField = "RealName";
            sel_guaranteeId.DataBind();
            sel_guaranteeId.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var guaranteeNo = txtGuaranteeNo.Value;
            var guaranteeId = int.Parse(Request.Form["sel_guaranteeId"]);
            if (guaranteeId <= 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择担保公司！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(guaranteeNo))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入担保函号！','warning', '');", true);
                return;
            }
            GuaranteeNoModel model = new GuaranteeNoModel
                {
                    GuaranteeId=guaranteeId,
                    GuaranteeNo=guaranteeNo,
                    UpdateTime=DateTime.Now
                };
            int id = new GuaranteeNoBll().AddGuaranteeNo(model);
            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                   id > 0
                                                       ? "MessageAlert('添加担保函号成功！','success', '');"
                                                       : "MessageAlert('添加担保函号失败！','error', '');", true);
        }
    }
}