using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsModel;
using ManageFcmsCommon;

namespace WebUI.p2p
{
    public partial class LoanManage : BasePage
    {
        private int _userId;
        private FcmsUserModel _fcmsUserModel = new FcmsUserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
            if (!IsPostBack)
            {
                _fcmsUserModel = new FcmsUserBll().GetModel(_userId);
                BindExamStatusList();
                BindGuaranteeList();
                //if (Role.IsRole(_fcmsUserModel.RoleId, "6"))//判断是否担保初审
                //{
                //    sel_Guarantee.Value = _fcmsUserModel.ParentID.ToString();
                //    sel_Guarantee.Attributes.Add("disabled", "disabled");
                //}
                
            }
        }

        //绑定审核状态下拉列表
        private void BindExamStatusList()
        {
            var dimExamStatusBll = new DimExamStatusBll();
            //var ds = dimExamStatusBll.GetDimExamStatusList(Role.ToQuery(_fcmsUserModel.RoleId));
            var ds = dimExamStatusBll.GetDimExamStatusList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_examstatus.DataSource = ds;
            sel_examstatus.DataValueField = "ID";
            sel_examstatus.DataTextField = "ExamStatusName";
            sel_examstatus.DataBind();
            sel_examstatus.Items.Insert(0, new ListItem("--请选择--", "0"));
        }

        //绑定担保公司下拉列表
        private void BindGuaranteeList()
        {
            var guaranteeBll = new GuaranteeBll();
            var ds = guaranteeBll.GetGuaranteeDataSet();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_Guarantee.DataSource = ds;
            sel_Guarantee.DataValueField = "RelationID";
            sel_Guarantee.DataTextField = "GuaranteeName";
            sel_Guarantee.DataBind();
            sel_Guarantee.Items.Insert(0, new ListItem("--请选择担保公司--", "0"));
        }
    }
}