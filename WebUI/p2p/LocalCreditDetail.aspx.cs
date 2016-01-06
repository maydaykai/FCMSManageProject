using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.p2p
{
    public partial class LocalCreditDetail : BasePage
    {
        protected string textareaStr = "";

        //调用业务
        LoadStudentBLL _student = new LoadStudentBLL();
        AreaBLL _arebll = new AreaBLL();
        LoanBll _loan = new LoanBll();
        FcmsUserBll fcmsUserbll = new FcmsUserBll();
        private static LoanModel _loanInfoModel = new LoanModel();
        private static LoanModel _loanModel = new LoanModel();

        #region 声明信息
        #endregion
        private int _loanId = 0;
        private int _memberId = 0;
        int ApplyID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidd_memberId.Value))
            {
                _memberId = Convert.ToInt32(hidd_memberId.Value);
            }
            if (!string.IsNullOrEmpty(hidd_loanId.Value))
            {
                _loanId = Convert.ToInt32(hidd_loanId.Value);
            }
            ApplyID = Convert.ToInt32(Request.QueryString["ID"].ToString());

            if (!IsPostBack)
            {
                LoadList();
                InitInfo();
            }

            if (!string.IsNullOrEmpty(hidd_loanId.Value))
            {
                FileInit();
                var sumScore = new LoanScoreBll().GetSumScoreList(Convert.ToInt32(hidd_loanId.Value));

                lab_SumScore.InnerText = sumScore.Tables[0].Rows[0]["SumScore"].ToString();
                lab_ScoreLevel.InnerText = sumScore.Tables[0].Rows[0]["ScoreLevel"].ToString();
            }
        }

        //初始化借款人信息
        private void InitInfo()
        {
            LocalCreditModel model = new LocalCreditModel();
            model = new LocalCreditBll().GetLocailCreditModel(ApplyID);
            LocalCreditApplyModel applyModel = new LocalCreditBll().GetApplyByID(ApplyID);

            if (model != null)
            {
                hidd_memberId.Value = model.MemberId.ToString();

                //会员信息
                lab_MemberName.InnerText = model.MemberName; //会员账号
                lab_RealName.InnerText = model.RealName; //姓名
                lab_IdentityCard.InnerText = model.IdentityCard; //身份证号
                lab_Sex.InnerText = model.Sex; //性别

                //基本信息
                lab_EducationName.InnerText = Enum.GetName(typeof(Education), model.Education); //学历
                lab_SocialCard.InnerText = model.SocialCard; //社保卡号
                lab_Residence.InnerText = model.Residence; //住宅地址
                lab_ResidenceTelephone.InnerText = model.ResidenceTelephone; //住宅电话
                lab_Mobile.InnerText = model.Mobile; //移动电话
                lab_CompanyNatureName.InnerText = Enum.GetName(typeof(CompanyNature), model.CompanyNature); //单位性质
                lab_CompanyName.InnerText = model.CompanyName; //工作单位名称
                lab_CompanyAddress.InnerText = model.CompanyAddress; //单位地址
                lab_CompanyTelephone.InnerText = model.CompanyTelephone; //单位电话
                lab_Duties.InnerText = model.Duties; //工作职务
                lab_EmergencyName1.InnerText = model.EmergencyName1; //应急联系人1
                lab_Relationship1.InnerText = model.Relationship1; //应急联系人关系1
                lab_ContactNum1.InnerText = model.ContactNum1; //应急联系人电话1
                lab_EmergencyName2.InnerText = model.EmergencyName2; //应急联系人2
                lab_Relationship2.InnerText = model.Relationship2; //应急联系人关系2
                lab_ContactNum2.InnerText = model.ContactNum2; //应急联系人电话2
                lab_EmergencyName3.InnerText = model.EmergencyName3; //应急联系人3
                lab_Relationship3.InnerText = model.Relationship3; //应急联系人关系3
                lab_ContactNum3.InnerText = model.ContactNum3; //应急联系人电话3
                lab_WifeName.InnerText = model.WifeName; //配偶姓名
                lab_IdCard.InnerText = model.IdCard; //配额偶身份证号码
                lab_WifeMobile.InnerText = model.WifeMobile; //配偶移动电话
                lab_WifeIncome.InnerText = model.WifeIncome; //配偶月均收入
                lab_CompanyName2.InnerText = model.CompanyName2; //配偶单位名称
                lab_CompanyAddress2.InnerText = model.CompanyAddress2; //配偶单位地址
                lab_CompanyTelephone2.InnerText = model.CompanyTelephone2; //配偶单位电话
                lab_CompanyNature2.InnerText = Enum.GetName(typeof(CompanyNature), model.CompanyNature2); //配偶单位性质
                lab_WifeDuties.InnerText = model.WifeDuties; //配偶工作职务
                lab_HouseOwner.InnerText = model.HouseOwner; //房产权利人
                lab_HouseNumber.InnerText = model.HouseNumber; //产权编号
                lab_HouseAddress.InnerText = model.HouseAddress; //房产地址
                lab_CarOwner.InnerText = model.CarOwner; //车辆权利人
                lab_CarNumber.InnerText = model.CarNumber; //车辆号码
                lab_CarBrand.InnerText = model.CarBrand; //车辆类型

                //上传资料
                //身份证
                if (model.IDCardAuthen1 != "")
                    lab_IDCardAuthen1.InnerHtml = "<a href='" + model.IDCardAuthen1 + "' target='_blank'>身份证正面</a>";
                if (model.IDCardAuthen2 != "")
                    lab_IDCardAuthen2.InnerHtml = "<a href='" + model.IDCardAuthen2 + "' target='_blank'>身份证反面</a>";
                if (model.IDCardAuthen3 != "")
                    lab_IDCardAuthen3.InnerHtml = "<a href='" + model.IDCardAuthen3 + "' target='_blank'>手持身份证</a>";
                //户口本
                if(model.BookAuthen1!="")
                    lab_BookAuthen1.InnerHtml = "<a href='" + model.BookAuthen1 + "' target='_blank'>户口本首页</a>";
                if (model.BookAuthen2 != "")
                    lab_BookAuthen2.InnerHtml = "<a href='" + model.BookAuthen2 + "' target='_blank'>本人页</a>";
                //社保
                if (model.SocialAuthen != "")
                    lab_SocialAuthen.InnerHtml = "<a href='" + model.SocialAuthen + "' target='_blank'>社保</a>";
                //住址证明
                if (model.ResidenceAuthen != "")
                    lab_ResidenceAuthen.InnerHtml = "<a href='" + model.ResidenceAuthen + "' target='_blank'>住址证明</a>";
                //工作证明
                if (model.WorkAuthen != "")
                    lab_WorkAuthen.InnerHtml = "<a href='" + model.WorkAuthen + "' target='_blank'>工作证明</a>";
                //收入证明
                if (model.BankCard1 != "")
                    lab_BankCard1.InnerHtml = "<a href='" + model.BankCard1 + "' target='_blank'>银行卡正面</a>";
                if (model.BankCard2 != "")
                    lab_BankCard2.InnerHtml = "<a href='" + model.BankCard2 + "' target='_blank'>银行卡反面</a>";
                if (model.BankStreamLine != "")
                    lab_BankStreamLine.InnerHtml = "<a href='" + model.BankStreamLine + "' target='_blank'>银行流水单</a>";
                //征信报告
                if (model.CreditReport != "")
                    lab_CreditReport.InnerHtml = "<a href='" + model.CreditReport + "' target='_blank'>征信报告</a>";
                //信用卡对账单
                if (model.CreditCard != "")
                    lab_CreditCard.InnerHtml = "<a href='" + model.CreditCard + "' target='_blank'>信用卡正面</a>";
                if (model.CreditCardBill != "")
                    lab_CreditCardBill.InnerHtml = "<a href='" + model.CreditCardBill + "' target='_blank'>对账单</a>";
                //学历学位证明
                if (model.EducationAuthen != "")
                    lab_EducationAuthen.InnerHtml = "<a href='" + model.EducationAuthen + "' target='_blank'>学历学位证明</a>";
                //结婚证
                if (model.MarryAuthen != "")
                    lab_MarryAuthen.InnerHtml = "<a href='" + model.MarryAuthen + "' target='_blank'>结婚证</a>";
                //房产证
                if (model.HouseAuthen != "")
                    lab_HouseAuthen.InnerHtml = "<a href='" + model.HouseAuthen + "' target='_blank'>基本信息页</a>";
                if (model.HouseAuthenPage != "")
                    lab_HouseAuthenPage.InnerHtml = "<a href='" + model.HouseAuthenPage + "' target='_blank'>盖章页</a>";

                //调查信息
                txt_MonthlyProfit.Text = model.MonthlyProfit;
                txt_LiabilitiesAmount.Text = model.LiabilitiesAmount;
                txt_LiabilitiesRatio.Text = model.LiabilitiesRatio;
                txt_MonthlyControlIncome.Text = model.MonthlyControlIncome;
                rdo_QualityProfessional.SelectedValue = model.QualityProfessional.ToString();
                rdo_HouseProperty.SelectedValue = model.HouseProperty.ToString();
                txt_LitigationSeach.Text = model.LitigationSeach;
                txt_SocialSecuritySeach.Text = model.SocialSecuritySeach;
                txt_CreditRecordSeach.Text = model.CreditRecordSeach;
                txt_ContactSituation.Text = model.ContactSituation;
                txt_OtherSituation.Text = model.OtherSituation;
                
                //借款信息
                hidd_LoanAmount.Value = model.LoanAmount.ToString("F2");
                if (model.ExamLoanAmount > 0)
                    txt_ApplyAmount.Text = model.ExamLoanAmount.ToString("F2");
                else
                    txt_ApplyAmount.Text = model.LoanAmount.ToString("F2");
                lab_createTime.InnerText = model.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");
                sel_loanUseName.Value = model.LoanUseId.ToString();

                WdataBidStartTime.Attributes.Add("onclick", "return WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' });");
                WdataBidEndTime.Attributes.Add("onclick", "return WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' });");

                //设置默认借款信息
                lab_LoanAmount.InnerText = model.LoanAmount.ToString("F2");
                txtBidAmountMin.Text = "100.00";
                BidAmountMax.Text = model.LoanAmount.ToString("F2");
                WdataBidStartTime.Text = model.CreateTime.ToString("yyyy-MM-dd hh:mm");
                WdataBidEndTime.Text = model.CreateTime.AddDays(3).ToString("yyyy-MM-dd hh:mm");
                txtAutoBidScale.Value = "0";
                txtGuaranteeFee.Value = "1";
                txt_server1.Value = "2";
                txt_server2.Value = "10";
                sel_Guarantee.Value = "243"; //默认金信联担保
                lab_LoanRate.Value = "12";
                lab_LoanTerm.Value = model.LoanTerm.ToString();


                string str = "";

                var ds = new ProjectTemplateBll().GetProjectTemplate(12);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    str = ds.Tables[0].Rows[0]["Template"].ToString();
                }
                hidd_loanUse.Value = sel_loanUseName.Items[sel_loanUseName.SelectedIndex].Text;
                hidd_LoanTerm.Value = model.LoanTerm.ToString();
                hidd_mLoanAount.Value = (Convert.ToDecimal(txt_ApplyAmount.Text) / 10000).ToString();

                str = str.Replace("(*)", "（"+model.LoanTerm.ToString()+"）");
                str = str.Replace("(**)", model.CompanyName);
                str = str.Replace("(***)", sel_loanUseName.Items[sel_loanUseName.SelectedIndex].Text);
                str = str.Replace("(****)", "（" + (model.LoanAmount / 10000).ToString() + "）");
                content1.Value = str;


                //贷款信息
                if (model.LoanId > 0)
                {
                    _loanId = model.LoanId;
                    hidd_loanId.Value = _loanId.ToString();

                    LoanModel loanModel = new LoanBll().GetLoanModel(_loanId);

                    #region 已经生成的借款信息
                    //if (loanModel.AutoBidScale>0)
                    txtAutoBidScale.Value = loanModel.AutoBidScale.ToString();//字段投标比例
                    if (loanModel.BidAmountMin > 0)
                        txtBidAmountMin.Text = loanModel.BidAmountMin.ToString();
                    if (loanModel.BidAmountMax > 0)
                        BidAmountMax.Text = loanModel.BidAmountMax.ToString();
                    WdataBidStartTime.Text = loanModel.BidStratTime.ToString("yyyy-MM-dd HH:mm");
                    WdataBidEndTime.Text = loanModel.BidEndTime.ToString("yyyy-MM-dd HH:mm");
                    if (loanModel.GuaranteeID != 0)
                        sel_Guarantee.Value = loanModel.GuaranteeID.ToString();//担保公司
                    if (loanModel.GuaranteeFee>0)
                        txtGuaranteeFee.Value = loanModel.GuaranteeFee.ToString();//担保费用
                    if (loanModel.LoanServiceCharges>0)
                        txt_server1.Value = loanModel.LoanServiceCharges.ToString();
                    if (loanModel.BidServiceCharges > 0)
                        txt_server2.Value = loanModel.BidServiceCharges.ToString();
                    lab_LoanAmount.InnerText = model.LoanAmount.ToString("F2");
                    lab_LoanRate.Value = model.LoanRate.ToString();//利率
                    lab_LoanTerm.Value = model.LoanTerm.ToString();
                    lab_AuditRecords.InnerText = model.AuditRecords;
                    content1.Value = model.UseDescription;


                    //审核不通过 禁止按钮
                    if (applyModel.ExamStatus == 2 && (new AuditHistoryBll().GetAuditHistoryModel(_loanId) != null))
                    {
                        button1.Attributes.Add("disabled", "disabled");
                        button1.CssClass = "inputButtonDisabled";

                        BtnScore.Attributes.Add("disabled", "disabled");
                        BtnScore.CssClass = "inputButtonDisabled";
                        Button4.Attributes.Add("disabled", "disabled");
                        Button4.CssClass = "inputButtonDisabled";
                        BtnDeleteFile.Attributes.Add("disabled", "disabled");
                        BtnDeleteFile.CssClass = "inputButtonDisabled";
                        BtnAuth.Attributes.Add("disabled", "disabled");
                        BtnAuth.CssClass = "inputButtonDisabled";

                        radio_Audit_loading.Checked = true;
                    }



                    //合同信息
                    text_ContractNo.Value = loanModel.ContractNo;
                    text_Agency.Value = loanModel.Agency;
                    text_LinkmanOne.Value = loanModel.LinkmanOne;
                    txt_TelOne.Value = loanModel.TelOne;
                    txt_TelTwo.Value = loanModel.TelTwo;
                    #endregion



                    #region 禁用按钮属性

                    if ((loanModel.ExamStatus == 1 && !IsRole("9")) || (loanModel.ExamStatus == 3 && !IsRole("10")) || (loanModel.ExamStatus == 7 && !IsRole("11")))
                    {
                        button1.Attributes.Add("disabled", "disabled");
                        button1.CssClass = "inputButtonDisabled";

                        BtnScore.Attributes.Add("disabled", "disabled");
                        BtnScore.CssClass = "inputButtonDisabled";
                        Button4.Attributes.Add("disabled", "disabled");
                        Button4.CssClass = "inputButtonDisabled";
                        BtnDeleteFile.Attributes.Add("disabled", "disabled");
                        BtnDeleteFile.CssClass = "inputButtonDisabled";
                        BtnAuth.Attributes.Add("disabled", "disabled");
                        BtnAuth.CssClass = "inputButtonDisabled";
                    }

                    if (loanModel.ExamStatus >= 5)
                    {
                        button1.Attributes.Add("disabled", "disabled");
                        button1.CssClass = "inputButtonDisabled";

                        BtnScore.Attributes.Add("disabled", "disabled");
                        BtnScore.CssClass = "inputButtonDisabled";
                        Button4.Attributes.Add("disabled", "disabled");
                        Button4.CssClass = "inputButtonDisabled";
                        BtnDeleteFile.Attributes.Add("disabled", "disabled");
                        BtnDeleteFile.CssClass = "inputButtonDisabled";
                        BtnAuth.Attributes.Add("disabled", "disabled");
                        BtnAuth.CssClass = "inputButtonDisabled";
                    }

                    //非审核角色不能使用审核按钮
                    if (!IsRole("9") && !IsRole("10") && !IsRole("11"))
                    {
                        button1.Attributes.Add("disabled", "disabled");
                        button1.CssClass = "inputButtonDisabled";

                        Button4.Attributes.Add("disabled", "disabled");
                        Button4.CssClass = "inputButtonDisabled";

                        BtnScore.Attributes.Add("disabled", "disabled");
                        BtnScore.CssClass = "inputButtonDisabled";
                        BtnDeleteFile.Attributes.Add("disabled", "disabled");
                        BtnDeleteFile.CssClass = "inputButtonDisabled";
                        BtnAuth.Attributes.Add("disabled", "disabled");
                        BtnAuth.CssClass = "inputButtonDisabled";

                    }
                    #endregion
                }

            }
        }

        /// <summary>
        /// 加载下拉列表
        /// </summary>
        public void LoadList()
        {
            //加载借款用途
            var ds = fcmsUserbll.GetDimLoanUseList();
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                sel_loanUseName.DataSource = ds;
                sel_loanUseName.DataValueField = "ID";
                sel_loanUseName.DataTextField = "LoanUseName";
                sel_loanUseName.DataBind();
                sel_loanUseName.Items.Insert(0, new ListItem("--请选择--", "0"));
            }

            //加载年利率
            ListItem item = null;
            for (int i = 9; i <= 18 ; i++)
            {
                item = new ListItem(i + "%", i.ToString());
                lab_LoanRate.Items.Add(item);
            }
            
            //加载贷款期限
            ListItem li = null;
            for (int i = 3; i <= 36; i++)
            {
                li = new ListItem(i + "个月", i.ToString());
                lab_LoanTerm.Items.Add(li);
            }

            //加载担保公司
            var RelationDs = fcmsUserbll.GetGcList();
            if (RelationDs.Tables[0] != null && RelationDs.Tables[0].Rows.Count > 0)
            {
                sel_Guarantee.DataSource = RelationDs;
                sel_Guarantee.DataValueField = "RelationID";
                sel_Guarantee.DataTextField = "RealName";
                sel_Guarantee.DataBind();
                sel_Guarantee.Items.Insert(0, new ListItem("--请选择--", "0"));
            }
        }


        /// <summary>
        /// 判断是否包含权限
        /// </summary>
        /// <param name="role"></param>要判断的权限
        /// <returns></returns>
        public static bool IsRole(string role)
        {
            return (RightArray.IndexOf("|" + role + "|", StringComparison.Ordinal) >= 0);
        }
        protected void BtnAuth_Click(object sender, EventArgs e)
        {
            SaveData();
            InitInfo();
            //认证
            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageWindow(610, 430, '/p2p/LoanAuthInfo.aspx?loanId=" + hidd_loanId.Value + "');", true);
        }


        protected void BtnScore_Click(object sender, EventArgs e)
        {
            var lsb = new LoanScoreBll();
            if (!string.IsNullOrEmpty(hidd_loanId.Value))
            {
                lsb.Add(Convert.ToInt32(hidd_loanId.Value), Convert.ToInt16(12));//添加深户贷款信息,同小微贷一样
            }
            SaveData();
            InitInfo();
            //评分
            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageWindow(590, 360, '/p2p/LoanScore.aspx?loanId=" + hidd_loanId.Value + "');", true);
        }


        /// <summary>
        /// 生成还款信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void button1_Click(object sender, EventArgs e)
        {
            SaveData();

            if (radio_Audit_loading.Checked)
            {
                //审核不通过，将记录写入到记录表
                var auditHistoryModel = new AuditHistoryModel();
                if (!string.IsNullOrEmpty(hidd_loanId.Value))
                {
                    auditHistoryModel.LoanID = Convert.ToInt32(hidd_loanId.Value);
                }
                else
                {
                    auditHistoryModel.LoanID = -1;
                }

                auditHistoryModel.Result = radio_audit.Checked == true ? true : false;
                auditHistoryModel.Reason = "";//
                auditHistoryModel.UserID = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
                bool faulst = false;
                auditHistoryModel.ReviewComments = Request["textReviewComments"];
                _loanModel = new LoanBll().GetLoanModel(_loanId);
                if (_loanModel.ExamStatus == 1 || _loanModel.ExamStatus == 2)
                    auditHistoryModel.Process = 3;
                else if (_loanModel.ExamStatus == 3 || _loanModel.ExamStatus == 4)
                    auditHistoryModel.Process = 4;
                else
                    auditHistoryModel.Process = 4;
                new AuditHistoryBll().Add(auditHistoryModel);

                button1.Attributes.Add("disabled", "disabled");
                button1.CssClass = "inputButtonDisabled";

                BtnScore.Attributes.Add("disabled", "disabled");
                BtnScore.CssClass = "inputButtonDisabled";
                Button4.Attributes.Add("disabled", "disabled");
                Button4.CssClass = "inputButtonDisabled";
                BtnDeleteFile.Attributes.Add("disabled", "disabled");
                BtnDeleteFile.CssClass = "inputButtonDisabled";
                BtnAuth.Attributes.Add("disabled", "disabled");
                BtnAuth.CssClass = "inputButtonDisabled";

                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核成功。','success', '');", true);
                return;
            }

            //参数验证 评分 担保公司
            if (Convert.ToInt32(sel_Guarantee.Value) <= 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择担保公司。','error', '');", true);
                return;
            }

            if (lab_SumScore.InnerText=="" || Convert.ToInt32(lab_SumScore.InnerText) <= 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('评分不能为0。','error', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(Request["txt_server2"]))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('区间服务费不能为空。','error', '');", true);
                return;
            }

            //判断是否生成还款信息
            if (_loanId > 0)
            {
                _loanInfoModel = new LoanBll().GetLoanInfoModel(" id =" + _loanId);
                _loanModel = new LoanBll().GetLoanModel(_loanId);
                //判断是否已经生成借款信息
                if (_loanModel.ExamStatus == 5)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('已经生成借款信息。','error', '');", true);
                    return;
                }

                var loanBll = new LoanBll();
                var auditHistoryModel = new AuditHistoryModel();

                if (!string.IsNullOrEmpty(hidd_loanId.Value))
                {
                    auditHistoryModel.LoanID = Convert.ToInt32(hidd_loanId.Value);
                }
                else
                {
                    auditHistoryModel.LoanID = -1;
                }

                auditHistoryModel.Result = radio_audit.Checked == true ? true : false;
                auditHistoryModel.Reason = "";//
                auditHistoryModel.UserID = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
                bool faulst = false;
                auditHistoryModel.ReviewComments = Request["textReviewComments"];
                #region 平台初审
                if (IsRole("9") && _loanModel.ExamStatus == 1) //平台初审
                {

                    _loanModel.AuthStatus = ControlHelper.GetCheckBoxList(ckbAuthList);
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    //默认初步审核
                    _loanModel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                    //_loanModel.ReleasedRate = Convert.ToDecimal(Request["txtReleasedRate"]);
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);

                    _loanModel.NeedGuarantee = true;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);

                    _loanModel.NeedLoanCharges = Convert.ToDecimal(Request["txt_server1"]) > 0;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txt_server1"]);

                    _loanModel.NeedBidCharges = Convert.ToDecimal(Request["txt_server2"]) > 0; ;
                    _loanModel.BidServiceCharges = Convert.ToDecimal(Request["txt_server2"]);

                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);

                  

                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);

                    _loanModel.RepaymentMethod = 4;
                    _loanModel.ExamStatus = radio_audit.Checked ? 3 : 2; //已经审核
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.LoanTitle = "深户贷";//标题

                    // _loanModel.LoanTypeID = 12;//深户贷
                    _loanModel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                    _loanModel.LoanTypeID = 12;
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["txt_ApplyAmount"]);

                    _loanModel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);

                    var _localModel = new LocalCreditBll().GetApplyByID(ApplyID);
                    string _describe = _localModel.UseDescription;


                    _loanModel.LoanDescribe = _describe;

                    _loanModel.BorrowMode = 1;
                    _loanModel.TradeType = 0;

                    auditHistoryModel.Process = 3; //深户贷初审

                    _loanModel.SumScore = Convert.ToInt16(lab_SumScore.InnerText);
                    _loanModel.ScoreLevel = lab_ScoreLevel.InnerText;
                    //Reason

                    // _loanModel.Reason
                    _loanModel.ContractNo = Request["text_ContractNo"];
                    _loanModel.Agency = Request["text_Agency"];
                    _loanModel.LinkmanOne = Request["text_LinkmanOne"];
                    _loanModel.LinkmanTwo = Request["text_LinkmanTwo"];
                    _loanModel.TelOne = Request["txt_TelOne"];
                    _loanModel.TelTwo = Request["txt_TelTwo"];

                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);


                    faulst = true;
                }
                #endregion
                //平台二审
                #region 平台二审
                if (IsRole("10") && _loanModel.ExamStatus == 3 && faulst == false)
                {
                    _loanModel.AuthStatus = ControlHelper.GetCheckBoxList(ckbAuthList);
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    //默认初步审核
                    _loanModel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                    //_loanModel.ReleasedRate = Convert.ToDecimal(Request["txtReleasedRate"]);
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.NeedGuarantee = true;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);
                    _loanModel.NeedLoanCharges = Convert.ToDecimal(Request["txt_server1"]) > 0;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txt_server1"]);

                    _loanModel.NeedBidCharges = Convert.ToDecimal(Request["txt_server2"]) > 0; ;
                    _loanModel.BidServiceCharges = Convert.ToDecimal(Request["txt_server2"]);

                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);


               

                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);

                    _loanModel.RepaymentMethod = 4;
                    _loanModel.ExamStatus = radio_audit.Checked ? 5 : 4;  //已经审核
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.LoanTitle = "深户贷";//标题

                    // _loanModel.LoanTypeID = 12;//深户贷
                    _loanModel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                    _loanModel.LoanTypeID = 12;
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["txt_ApplyAmount"]);

                    _loanModel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);


                    var _localModel = new LocalCreditBll().GetApplyByID(ApplyID);
                    string _describe = _localModel.UseDescription;


                    _loanModel.LoanDescribe = _describe;

                    _loanModel.BorrowMode = 1;
                    _loanModel.TradeType = 0;

                    auditHistoryModel.Process = 4;//深户贷二审

                    _loanModel.SumScore = Convert.ToInt16(lab_SumScore.InnerText);
                    _loanModel.ScoreLevel = lab_ScoreLevel.InnerText;
                    //Reason

                    // _loanModel.Reason
                    _loanModel.ContractNo = Request["text_ContractNo"];
                    _loanModel.Agency = Request["text_Agency"];
                    _loanModel.LinkmanOne = Request["text_LinkmanOne"];
                    _loanModel.LinkmanTwo = Request["text_LinkmanTwo"];
                    _loanModel.TelOne = Request["txt_TelOne"];
                    _loanModel.TelTwo = Request["txt_TelTwo"];

                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);
                    
                }
                #endregion
                InitInfo();
                #region 审核信息



                #endregion
                //如果添加成功则更新信息

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('不能生成借款信息。','error', '');", true);
            }
            
        }


        //保存贷款数据
        private void SaveData()
        {
            //根据用户Id更改信息
            LocalCreditApplyModel info = new LocalCreditApplyModel();
            decimal LoanAmount = Convert.ToDecimal(Request["txt_ApplyAmount"]);//审批金额
            decimal ApplyAmount = Convert.ToDecimal(hidd_LoanAmount.Value);//借款金额
            info.MemberId = _memberId;
            info.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);
            //sel_loanUseName
            info.LoanUseId = Convert.ToInt32(Request["sel_loanUseName"]);
            info.LoanAmount = ApplyAmount;
            info.ExamLoanAmount = LoanAmount;
            info.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);//利率
            string _describe = Request["content1"];

            if (_describe.IndexOf(hidd_loanUse.Value) > 0)
                _describe = _describe.Replace(hidd_loanUse.Value, sel_loanUseName.Items[sel_loanUseName.SelectedIndex].Text);
            if (_describe.IndexOf("（" + hidd_LoanTerm.Value + "）") > 0)
                _describe = _describe.Replace("（" + hidd_LoanTerm.Value + "）", "（" + info.LoanTerm.ToString() + "）");
            if (_describe.IndexOf("（" + hidd_mLoanAount.Value + "）") > 0)
                _describe = _describe.Replace("（" + hidd_mLoanAount.Value + "）", "（" + (LoanAmount / 10000) + "）");

            info.UseDescription = _describe;

            info.ID = ApplyID;
            if(!string.IsNullOrEmpty(hidd_loanId.Value))
                info.LoanId = Convert.ToInt32(hidd_loanId.Value);
            
            if (radio_audit.Checked)
            {
                info.ExamStatus = 1;
            }
            else if (radio_Audit_loading.Checked)
            {
                info.ExamStatus = 2;
            }

            info.UpdateTime = DateTime.Now;
            //借款信息审核结果
            string fcmsUserName = SessionHelper.Get("FcmsUserName").ToString();
            string curr_boor = string.Format("当前审核人Id为[{0},审核时间[{1}],审核结果[{2}]]", fcmsUserName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), radio_audit.Checked == true ? "审核通过" : "拒绝审核");
            info.AuditRecords = curr_boor;//审核人Id记录
            int relv = 0;

            //调查信息
            LocalCreditModel localModel = new LocalCreditModel();
            localModel.MonthlyProfit = txt_MonthlyProfit.Text;
            localModel.LiabilitiesAmount = txt_LiabilitiesAmount.Text;
            localModel.LiabilitiesRatio = txt_LiabilitiesRatio.Text;
            localModel.MonthlyControlIncome = txt_MonthlyControlIncome.Text;
            localModel.QualityProfessional = Convert.ToInt32(rdo_QualityProfessional.SelectedValue);
            localModel.HouseProperty = Convert.ToInt32(rdo_HouseProperty.SelectedValue);
            localModel.LitigationSeach = txt_LitigationSeach.Text;
            localModel.SocialSecuritySeach = txt_SocialSecuritySeach.Text;
            localModel.CreditRecordSeach = txt_CreditRecordSeach.Text;
            localModel.ContactSituation = txt_ContactSituation.Text;
            localModel.OtherSituation = txt_OtherSituation.Text;
            localModel.LoanApplyId = ApplyID;


            //更新数据

            //如果审核通过则插入借款信息表
            if (radio_audit.Checked)
            {
                if (_loanId == 0)
                {
                    //添加借款标信息
                    #region 借款标信息
                    LoanModel loanmodel = new LoanModel();
                    loanmodel.CityID = 440300;//城市
                    loanmodel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                    loanmodel.MemberID = _memberId;
                    loanmodel.LoanTitle = "深户贷";//借款标题
                    loanmodel.LoanAmount = LoanAmount;//借款金额
                    loanmodel.LoanTypeID = 12;//借款标类型=深户贷
                    loanmodel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                    loanmodel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);//借款期限
                    loanmodel.RepaymentMethod = 4;//还款方式
                    loanmodel.BorrowMode = 1;//按月
                    loanmodel.TradeType = 0;//线上还是线下
                    loanmodel.ID = _loanId;//Id
                    loanmodel.MinInvestment = Convert.ToDecimal(Request["txtBidAmountMin"]); //最小投资金额
                    loanmodel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    loanmodel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    loanmodel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);
                    loanmodel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);
                    #endregion
                    #region 家庭信息
                    LoanMemberInfoModel loanMemberInfoModel = new MemberDetailInfoBll().getLoanMemberInfoModel(_memberId);
                    #endregion
                    #region 企业信息
                    GreenChannelRecordModel greenChannelRecordModel = new GreenChannelRecordModel();
                    greenChannelRecordModel.BranchCompanyID = 0;
                    greenChannelRecordModel.Scale = 0;
                    #endregion
                    relv = _loan.AddLoan(loanmodel, loanMemberInfoModel, greenChannelRecordModel);//添加借款信息
                    if (relv > 0)
                    {
                        info.LoanId = relv;
                        _loanId = relv;
                        hidd_loanId.Value = relv.ToString();
                        //ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('添加成功。','success', '');", true);
                    }
                }
                else
                {
                    var loanmodel = _loan.GetLoanModel(_loanId);
                    loanmodel.CityID = 440300;//城市
                    loanmodel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                    loanmodel.MemberID = _memberId;
                    loanmodel.LoanTitle = "深户贷";//借款标题
                    loanmodel.LoanAmount = LoanAmount;//借款金额
                    loanmodel.LoanTypeID = 12;//借款标类型=深户贷
                    loanmodel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                    loanmodel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);//借款期限
                    loanmodel.RepaymentMethod = 4;//还款方式
                    loanmodel.BorrowMode = 1;//按月
                    loanmodel.TradeType = 0;//线上还是线下
                    loanmodel.ID = _loanId;//Id
                    loanmodel.MinInvestment = Convert.ToDecimal(Request["txtBidAmountMin"]); //最小投资金额
                    loanmodel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    loanmodel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    loanmodel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);
                    loanmodel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);
                    _loan.Update(loanmodel);
                    //ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('已经生成借款信息,不能修改用户借款信息。','error', '');", true);
                }
                //复审去列表审核 生成还款计划
                new LocalCreditBll().ModLocalCreditApplyInfo(info);//更新当前信息
                new LocalCreditBll().UpdateInvestigationInfo(localModel);//更新调查信息
            }
            if (radio_Audit_loading.Checked)
            {
                if (new LocalCreditBll().ModLocalCreditApplyInfo(info))
                {
                    if (_loanId == 0)
                    {
                        //添加借款标信息
                        #region 借款标信息
                        LoanModel loanmodel = new LoanModel();
                        loanmodel.CityID = 440300;//城市
                        loanmodel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                        loanmodel.MemberID = _memberId;
                        loanmodel.LoanTitle = "深户贷";//借款标题
                        loanmodel.LoanAmount = LoanAmount;//借款金额
                        loanmodel.LoanTypeID = 12;//借款标类型=深户贷
                        loanmodel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                        loanmodel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);//借款期限
                        loanmodel.RepaymentMethod = 4;//还款方式
                        loanmodel.BorrowMode = 1;//按月
                        loanmodel.TradeType = 0;//线上还是线下
                        loanmodel.ID = _loanId;//Id
                        loanmodel.MinInvestment = Convert.ToDecimal(Request["txtBidAmountMin"]); //最小投资金额
                        loanmodel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                        loanmodel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                        loanmodel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);
                        loanmodel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);
                        #endregion
                        #region 家庭信息
                        LoanMemberInfoModel loanMemberInfoModel = new MemberDetailInfoBll().getLoanMemberInfoModel(_memberId);
                        #endregion
                        #region 企业信息
                        GreenChannelRecordModel greenChannelRecordModel = new GreenChannelRecordModel();
                        greenChannelRecordModel.BranchCompanyID = 0;
                        greenChannelRecordModel.Scale = 0;
                        #endregion

                        relv = _loan.AddLoan(loanmodel, loanMemberInfoModel, greenChannelRecordModel);//添加借款信息
                        if (relv > 0)
                        {
                            info.LoanId = relv;
                            _loanId = relv;
                            hidd_loanId.Value = relv.ToString();
                            _loanModel = new LoanBll().GetLoanModel(relv);
                            _loanModel.ExamStatus = 2;
                            _loan.Update(_loanModel);

                            //ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('添加成功。','success', '');", true);
                        }
                    }
                    else
                    {
                        _loanModel = _loan.GetLoanModel(_loanId);
                        _loanModel.CityID = 440300;//城市
                        _loanModel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                        _loanModel.MemberID = _memberId;
                        _loanModel.LoanTitle = "深户贷";//借款标题
                        _loanModel.LoanAmount = LoanAmount;//借款金额
                        _loanModel.LoanTypeID = 12;//借款标类型=深户贷
                        _loanModel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                        _loanModel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);//借款期限
                        _loanModel.RepaymentMethod = 4;//还款方式
                        _loanModel.BorrowMode = 1;//按月
                        _loanModel.TradeType = 0;//线上还是线下
                        _loanModel.ID = _loanId;//Id
                        _loanModel.MinInvestment = Convert.ToDecimal(Request["txtBidAmountMin"]); //最小投资金额
                        _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                        _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                        _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);
                        _loanModel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);
                        _loanModel.ExamStatus = 4;
                        _loan.Update(_loanModel);
                        //ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('已经生成借款信息,不能修改用户借款信息。','error', '');", true);
                    }
                    //复审去列表审核 生成还款计划
                    new LocalCreditBll().ModLocalCreditApplyInfo(info);//更新当前信息

                    //ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('修改成功。','success', '');", true);
                }//更新当前信息
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string waterFile = FileUpload1.FileName;//水印文件
            string srcfile = "src/" + FileUpload1.FileName;//原始文件
            if (ApplyID!=0)
            {
                string uploadPath = DESStringHelper.EncryptString(ApplyID.ToString());
                string filePath = ConfigHelper.LoanFilePhysicallPath;
                string fileEx = waterFile.Substring(waterFile.LastIndexOf(".", StringComparison.Ordinal) + 1);
                if (!string.IsNullOrEmpty(waterFile))
                {
                    if (!Directory.Exists(filePath + uploadPath))
                    {
                        Directory.CreateDirectory(filePath + uploadPath);
                    }
                    if (!Directory.Exists(filePath + uploadPath + "/src"))
                    {
                        Directory.CreateDirectory(filePath + uploadPath + "/src");
                    }
                    FileUpload1.SaveAs(filePath + uploadPath + "/" + srcfile);
                    System.Drawing.Image srcImg = System.Drawing.Image.FromFile(filePath + uploadPath + "/" + srcfile);
                    if (fileEx == "jpg" || fileEx == "bmp" || fileEx == "gif")
                    {
                        System.Drawing.Image img = ImageWatermark.CreateWatermark(
                        srcImg,
                        System.Drawing.Image.FromFile(@"D:\files\logo.png"),
                        0.5F,
                        ContentAlignment.BottomRight);
                        img.Save(filePath + uploadPath + "/" + waterFile);//保存水印文件
                        img.Dispose();
                        srcImg.Dispose();
                    }
                    SaveData();
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件上传成功！','warning', '');", true);
                    
                    InitInfo();
                    FileInit();
                }
                else
                {
                    SaveData();
                    
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件上传失败！','warning', '');", true);
                    InitInfo();
                    FileInit();
                }
            }


        }
        protected void BtnDeleteFile_Click(object sender, EventArgs e)
        {
            string myfile = text_FileName.Value;
            string uploadPath = DESStringHelper.EncryptString(ApplyID.ToString());
            string filePath = ConfigHelper.LoanFilePhysicallPath;
            if (!string.IsNullOrEmpty(text_FileName.Value))
            {
                if (File.Exists(filePath + uploadPath + "/" + myfile))
                {
                    File.Delete(filePath + uploadPath + "/" + myfile);
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件删除成功！','warning', '');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件不存在！','warning', '');", true);
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入文件名！','warning', '');", true);
            }
            FileInit();
        }


        private void FileInit()
        {
            tbDirInfo.Rows.Clear();

            string filePath = ConfigHelper.LoanFilePhysicallPath;
            string urlPath = ConfigHelper.LoanFileVirtualPath;
            string strCurrentDir;
            strCurrentDir = filePath + DESStringHelper.EncryptString(ApplyID.ToString());
            FileInfo fi;
            DirectoryInfo di;
            TableCell td;
            TableRow tr;


            string FileName;	 //文件名称
            long FileSize;	 //文件大小
            DateTime FileModify;	 //文件更新时间

            DirectoryInfo dir = new DirectoryInfo(strCurrentDir);
            if (Directory.Exists(strCurrentDir))
            {
                foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
                {
                    FileSize = 0;

                    if (fsi is FileInfo)
                    {
                        //表示当前fsi是文件
                        fi = (FileInfo)fsi;
                        FileName = fi.Name;
                        FileSize = fi.Length;
                        FileModify = fi.LastWriteTime;
                        //通过扩展名来选择文件显示图标
                        //组建新的行
                        tr = new TableRow();

                        td = new TableCell();
                        td.Controls.Add(
                            new LiteralControl("<a href='" + urlPath + dir.Name + "/" + FileName.ToString() +
                                               "' target='_blank'>" + FileName.ToString() + "</a>"));
                        tr.Cells.Add(td);

                        td = new TableCell();
                        td.Controls.Add(new LiteralControl(FileSize.ToString()));
                        tr.Cells.Add(td);

                        td = new TableCell();
                        td.Controls.Add(new LiteralControl(FileModify.ToString()));
                        tr.Cells.Add(td);

                        //td.Controls.Add(
                        //    new LiteralControl("<a href='" + urlPath + dir.Name + "/" + FileName.ToString() +"' target='_blank'><img height=200px width=200px src='" + urlPath + dir.Name + "/" + FileName.ToString() + "' alt='" + FileName.ToString() + "' ></a>"));
                        //tr.Cells.Add(td);

                        //td = new TableCell();
                        //td.Controls.Add(new LiteralControl(FileName.ToString()));
                        //tr.Cells.Add(td);

                        tbDirInfo.Rows.Add(tr);
                    }
                }
            }
        }

        public enum CompanyNature
        {
            国家机关 = 1,
            国有企业 = 2,
            集体企业 = 3,
            有限责任公司 = 4,
            股份有限公司 = 5,
            私营企业 = 6,
            中外合资企业 = 7,
            外商投资企业 = 8,
            其它 = 9
        }

        public enum Education
        {
            初中或以下 = 1,
            高中 = 2,
            中专 = 3,
            大专 = 4,
            大学本科 = 5,
            硕士研究生以上 = 6
        }
    }
}