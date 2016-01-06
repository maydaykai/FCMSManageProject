using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.p2p
{
    public partial class PublishLoan : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }
            else
            {
                //if (sel_LoanScaleType.Value == "5")
                //{
                //    txt_LoanTerm.Value = "1";
                //    txt_LoanTerm.Attributes.Add("readonly", "true"); 
                //    txt_LoanTerm.Attributes["class"] = "input_Disabled";
                //    sel_repaymentMethod.Value = "3";
                //    sel_repaymentMethod.Attributes.Add("disabled", "disabled");
                //}
                //else
                //{
                //    sel_repaymentMethod.Attributes.Remove("disabled");
                //    txt_LoanTerm.Attributes.Remove("readonly");
                //    txt_LoanTerm.Attributes["class"] = "fl input_text";
                //}
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                return;
            }
            try
            {
                var loanModel = new LoanModel();
                var loanBll = new LoanBll();
                loanModel.CityID = int.Parse(Request.Form["sel_city"]);
                loanModel.MemberID = int.Parse(txtMemberID.Value);
                loanModel.DimLoanUseID = int.Parse(sel_loanUseName.Value);
                loanModel.LoanAmount = decimal.Parse(txt_LoanAmount.Value);
                loanModel.LoanRate = decimal.Parse(txtLoanRate.Value);
                loanModel.LoanTerm = int.Parse(txt_LoanTerm.Value);
                loanModel.RepaymentMethod = int.Parse(sel_repaymentMethod.Value);
                loanModel.BorrowMode = (sel_repaymentMethod.Value == "2" ? 0 : 1);
                loanModel.LoanTitle = text_LoanTitle.Value;
                loanModel.LoanTypeID = int.Parse(sel_LoanScaleType.Value);
                loanModel.TradeType = int.Parse(sel_TradeType.Value);
                loanModel.BidStratTime = DateTime.Now;
                loanModel.BidEndTime = DateTime.Now.AddDays(1);
                loanModel.BidAmountMax = decimal.Parse(txt_LoanAmount.Value);

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                       loanBll.AddLoan(loanModel,null,null) > 0
                                                                           ? "MessageAlert('借款申请成功!','success', '');"
                                                                           : "MessageAlert('借款申请失败!','error', '');",
                                                                       true);
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
            }

        }

        //绑定借款标类型下拉列表
        private void BindLoanTypeList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetDimLoanScaleTypeList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_LoanScaleType.DataSource = ds;
            sel_LoanScaleType.DataValueField = "ID";
            sel_LoanScaleType.DataTextField = "LoanScaleType";
            sel_LoanScaleType.DataBind();
            sel_LoanScaleType.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        //绑定借款用途下拉列表
        private void BindLoanUseList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetDimLoanUseList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_loanUseName.DataSource = ds;
            sel_loanUseName.DataValueField = "ID";
            sel_loanUseName.DataTextField = "LoanUseName";
            sel_loanUseName.DataBind();
            sel_loanUseName.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        //绑定还款方式
        private void BindRepaymentMethodList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetDimRepaymentMethodList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_repaymentMethod.DataSource = ds;
            sel_repaymentMethod.DataValueField = "ID";
            sel_repaymentMethod.DataTextField = "RepaymentMethodName";
            sel_repaymentMethod.DataBind();
            sel_repaymentMethod.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        
        private void BindProvinceList()
        {
            var areaBll = new AreaBLL();
            List<AreaModel> ls = areaBll.getProvinceList();
            sel_province.DataSource = ls;
            sel_province.DataValueField = "ID";
            sel_province.DataTextField = "Name";
            sel_province.DataBind();
            sel_province.Items.Insert(0, new ListItem("--请选择--", "0"));
        }

        private void DataInit()
        {
            BindLoanTypeList();
            BindLoanUseList();
            BindRepaymentMethodList();
            BindProvinceList();
        }

        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtMemberID.Value))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择借款会员！','warning', '');", true);
                return false;
            }
            var memberModel = new MemberModel();
            var memberBll = new MemberBll();
            memberModel = memberBll.GetModel(int.Parse(txtMemberID.Value));
            if (memberModel.CompleStatus != 4)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('该会员未填写详细信息！不能申请借款！','warning', '');", true);
                return false;
            }
            if (sel_LoanScaleType.Value == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择借款标类型！','warning', '');", true);
                return false;
            }
            if (sel_loanUseName.Value == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择借款用途！','warning', '');", true);
                return false;
            }
            if (sel_repaymentMethod.Value == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择还款方式！','warning', '');", true);
                return false;
            }
            if (sel_province.Value == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择省份！','warning', '');", true);
                return false;
            }
            if (Request.Form["sel_city"] == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择城市！','warning', '');", true);
                return false;
            }

            if (string.IsNullOrEmpty(text_LoanTitle.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入借款标题！','warning', '');", true);
                return false;
            }
            if (string.IsNullOrEmpty(txt_LoanTerm.Value))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入借款期限！','warning', '');", true);
                return false;
            }
            if (sel_repaymentMethod.Value == "2" && int.Parse(txt_LoanTerm.Value) > 30)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('借款期限不能超过30天！','warning', '');", true);
                return false;
            }

            if (string.IsNullOrEmpty(txt_LoanAmount.Value))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入借款金额！','warning', '');", true);
                return false;
            }
            if (string.IsNullOrEmpty(txtLoanRate.Value))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入借款年利率！','warning', '');", true);
                return false;
            }
            
            if (!CheckRate("借款利率", txtLoanRate.Value.Trim()))
            {
                return false;
            }

            if (!RegexHelper.IsAmount(txt_LoanAmount.Value))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入有效的借款金额！','warning', '');", true);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证费率
        /// </summary>
        /// <param name="message"></param>
        /// <param name="nowvalue"></param>
        /// <returns></returns>
        private bool CheckRate(string message, string nowvalue)
        {
            try
            {
                if (nowvalue == "")
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('" + message + "不能为空！','warning', '');", true);
                    return false;
                }

                if (!RegexHelper.IsRate(nowvalue.Trim()) || Convert.ToDecimal(nowvalue) <= 0 || Convert.ToDecimal(nowvalue) > 100)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('" + message + "不正确！','warning', '');", true);
                    return false;
                }

                return true;
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('" + message + "错误！','warning', '');", true);
                return false;
            }
        }
    }
}