using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsCommon;
using ManageFcmsModel;
using ManageFcmsBll;
using ManageFcmsCommon;
using System.IO;
using Image = System.Drawing.Image;

namespace WebUI.p2p
{
    public partial class LoanAuditNew : BasePage
    {
        private int _userId;
        private int _loanId;
        private static LoanModel _loanInfoModel = new LoanModel();
        private static FcmsUserModel _fcmsUserModel = new FcmsUserModel();
        private static LoanModel _loanModel = new LoanModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
            _loanId = Convert.ToInt16(HttpContext.Current.Request["ID"]);
            if (!IsPostBack)
            {
                DataInit();
            }
            else
            {
                FileInit();
                var sumScore = new LoanScoreBll().GetSumScoreList(_loanId);
                lab_SumScore.InnerText = sumScore.Tables[0].Rows[0]["SumScore"].ToString();
                lab_ScoreLevel.InnerText = sumScore.Tables[0].Rows[0]["ScoreLevel"].ToString();
            }

        }
        //绑定担保公司下拉列表
        private void BindGcList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetGcList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_Guarantee.DataSource = ds;
            sel_Guarantee.DataValueField = "RelationID";
            sel_Guarantee.DataTextField = "RealName";
            sel_Guarantee.DataBind();
            sel_Guarantee.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        //绑定借款标类型下拉列表
        private void BindLoanTypeList()
        {
            var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetDimProductTypeList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_LoanScaleType.DataSource = ds;
            sel_LoanScaleType.DataValueField = "ID";
            sel_LoanScaleType.DataTextField = "Name";
            sel_LoanScaleType.DataBind();
            sel_LoanScaleType.Items.Insert(0, new ListItem("--请选择--", "0"));
        }

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

        /// <summary>
        /// 预约投资用户列表 2015-10-15 lxy
        /// </summary>
        private void BindAppointment()
        {
            var list = new LoanBll().GetAppointmentBiddingUserList(_loanId);
            StringBuilder str = new StringBuilder();
            str.Append("<table class=\"editTable\">");
            str.Append("<tr>");
            str.Append("<td>会员名</td>");
            str.Append("<td>预约电话</td>");
            str.Append("<td>预约金额</td>");
            str.Append("<td>可用余额</td>");
            str.Append("<td>申请时间</td>");
            str.Append("</tr>");

            for (var i = 0; i < list.Rows.Count; i++)
            {
                str.Append("<tr>");
                str.Append("<td>" + list.Rows[i]["MemberName"] + "</td>");
                str.Append("<td>" + list.Rows[i]["mobile"] + "</td>");
                str.Append("<td>" + list.Rows[i]["amount"] + "</td>");
                str.Append("<td>" + list.Rows[i]["Balance"] + "</td>");
                str.Append("<td>" + list.Rows[i]["createTime"] + "</td>");
                str.Append("</tr>");
            }
            str.Append("</table>");
            this.tdAppointment.InnerHtml = str.ToString();

            if (list.Rows.Count <= 0)
                this.trAppointment.Visible = false;
        }

        private void DataInit()
        {
            BindGcList();
            BindLoanTypeList();
            BindLoanUseList();
            //BindAuthList();
            //BindAppointment();
            _loanInfoModel = new LoanBll().GetLoanInfoModel(" id =" + _loanId);
            _loanModel = new LoanBll().GetLoanModel(_loanId);
            lab_AuditAmountBig.InnerText =
                ConvertHelper.LowAmountConvertChCap(new LoanBll().GetMemberAuditAmount(_loanModel.MemberID));
            lab_MemberName.InnerText = _loanInfoModel.MemberName;
            lab_LoanNumber.InnerText = _loanInfoModel.LoanNumber;
            txtLoanRate.Value = _loanInfoModel.LoanRate.ToString();
            txtAutoBidScale.Value = _loanInfoModel.AutoBidScale.ToString();
            txt_LoanAmount.Value = _loanInfoModel.LoanAmount.ToString();
            sel_loanUseName.Value = _loanInfoModel.DimLoanUseID.ToString();
            txt_LoanTerm.Value = _loanInfoModel.LoanTerm.ToString();
            lab_loanTerm.InnerText = (_loanInfoModel.BorrowMode == 0 ? "(按天)" : "(按月)");
            decimal fee = _loanInfoModel.LoanAmount * _loanInfoModel.LoanTerm * _loanInfoModel.LoanServiceCharges / 100 / 365;
            lab_daymonth.InnerText = _loanInfoModel.RepaymentMethod == 2 ? "(年化) 服务费：" + fee.ToString("N2") + " 元" : "(按月)";
            sel_repaymentMethod.Value = _loanInfoModel.RepaymentMethod.ToString();
            txtBidAmountMin.Value = _loanInfoModel.BidAmountMin.ToString();
            txtBidAmountMax.Value = _loanInfoModel.BidAmountMax.ToString();
            WdataBidStartTime.Value = _loanInfoModel.BidStratTime == PublicConst.MinDate
                                          ? DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                                          : _loanInfoModel.BidStratTime.ToString("yyyy-MM-dd HH:mm");
            WdataBidEndTime.Value = _loanInfoModel.BidEndTime == PublicConst.MinDate
                                          ? DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                                          : _loanInfoModel.BidEndTime.ToString("yyyy-MM-dd HH:mm");
            lab_province.InnerText = _loanInfoModel.Province;
            lab_city.InnerText = _loanInfoModel.City;
            txtGuaranteeFee.Value = _loanInfoModel.GuaranteeFee.ToString();
            txtLoanServiceCharges.Value = _loanInfoModel.LoanServiceCharges.ToString();
            txtBidServiceCharges.Value = _loanInfoModel.BidServiceCharges.ToString();
            cbNeedGuarantee.Checked = _loanInfoModel.NeedGuarantee;
            cbNeedLoanCharges.Checked = _loanInfoModel.NeedLoanCharges;
            cbNeedBidCharges.Checked = _loanInfoModel.NeedBidCharges;
            lab_examStatus.InnerText = _loanInfoModel.ExamStatusName;
            sel_Guarantee.Value = _loanInfoModel.GuaranteeID.ToString();
            text_LoanTitle.Value = _loanInfoModel.LoanTitle;
            sel_LoanScaleType.Value = _loanInfoModel.LoanTypeID.ToString();
            sel_TradeType.Value = _loanInfoModel.TradeType.ToString();

            text_ContractNo.Value = _loanInfoModel.ContractNo;
            text_Agency.Value = _loanInfoModel.Agency;
            text_LinkmanOne.Value = _loanInfoModel.LinkmanOne;
            text_LinkmanTwo.Value = _loanInfoModel.LinkmanTwo;
            txt_TelOne.Value = _loanInfoModel.TelOne;
            txt_TelTwo.Value = _loanInfoModel.TelTwo;

            //调查信息 wangzhe 20151230
            txt_MonthlyProfit.Value = _loanInfoModel.MonthlyProfit;
            txt_LiabilitiesAmount.Value = _loanInfoModel.LiabilitiesAmount;
            txt_LiabilitiesRatio.Value = _loanInfoModel.LiabilitiesRatio;
            txt_MonthlyControlIncome.Value = _loanInfoModel.MonthlyControlIncome;
            rdo_QualityProfessional.SelectedValue = _loanInfoModel.QualityProfessional.ToString();
            rdo_HouseProperty.SelectedValue = _loanInfoModel.HouseProperty.ToString();
            txt_LitigationSeach.Text = _loanInfoModel.LitigationSeach;
            txt_SocialSecuritySeach.Text = _loanInfoModel.SocialSecuritySeach;
            txt_CreditRecordSeach.Text = _loanInfoModel.CreditRecordSeach;
            txt_ContactSituation.Text = _loanInfoModel.ContactSituation;
            txt_OtherSituation.Text = _loanInfoModel.OtherSituation;


            textReviewComments.Value = "";

            var sumScore = new LoanScoreBll().GetSumScoreList(_loanId);

            lab_SumScore.InnerText = sumScore.Tables[0].Rows[0]["SumScore"].ToString();
            lab_ScoreLevel.InnerText = sumScore.Tables[0].Rows[0]["ScoreLevel"].ToString();

            if (string.IsNullOrEmpty(_loanInfoModel.LoanDescribe))
            {
                var dataset = new ProjectTemplateBll().GetProjectTemplate(Convert.ToInt16(_loanInfoModel.LoanTypeID.ToString()));
                if (dataset == null || dataset.Tables.Count <= 0 || dataset.Tables[0].Rows.Count <= 0)
                {
                }
                else
                {
                    content1.InnerText = dataset.Tables[0].Rows[0]["Template"].ToString();
                }
            }
            else
            {
                content1.InnerText = _loanInfoModel.LoanDescribe;
            }


            FileInit();

            _fcmsUserModel = new FcmsUserBll().GetModel(_userId);

            if (IsRole("9") || IsRole("10") || IsRole("11"))
            {
                txtGuaranteeFee.Attributes.Add("readonly", "true"); txtGuaranteeFee.Attributes["class"] = "input_Disabled";
                //txtBidServiceCharges.Attributes.Add("readonly", "true"); txtBidServiceCharges.Attributes["class"] = "input_Disabled";
                cbNeedLoanCharges.Attributes.Add("disabled", "disabled");
                cbNeedBidCharges.Attributes.Add("disabled", "disabled");
                cbNeedGuarantee.Attributes.Add("disabled", "disabled");

                content1.Attributes.Add("readonly", "true");

            }
            if (_loanModel.ExamStatus == 7 && IsRole("11"))
            {
                txtLoanRate.Attributes.Add("readonly", "true"); txtLoanRate.Attributes["class"] = "input_Disabled";
                sel_Guarantee.Attributes.Add("disabled", "disabled");
                WdataBidStartTime.Attributes.Remove("onclick");
                WdataBidEndTime.Attributes.Remove("onclick");
                WdataBidStartTime.Attributes.Add("readonly", "true"); WdataBidStartTime.Attributes["class"] = "input_Disabled";
                WdataBidEndTime.Attributes.Add("readonly", "true"); WdataBidEndTime.Attributes["class"] = "input_Disabled";
                txtAutoBidScale.Attributes.Add("readonly", "true"); txtAutoBidScale.Attributes["class"] = "input_Disabled";
                txtBidAmountMin.Attributes.Add("readonly", "true"); txtBidAmountMin.Attributes["class"] = "input_Disabled";
                txtBidAmountMax.Attributes.Add("readonly", "true"); txtBidAmountMax.Attributes["class"] = "input_Disabled";
                txtGuaranteeFee.Attributes.Add("readonly", "true"); txtGuaranteeFee.Attributes["class"] = "input_Disabled";
                text_LoanTitle.Attributes.Add("readonly", "true"); text_LoanTitle.Attributes["class"] = "input_Disabled";
                sel_LoanScaleType.Attributes.Add("disabled", "disabled");
                sel_repaymentMethod.Attributes.Add("disabled", "disabled");
                txt_LoanTerm.Attributes.Add("readonly", "true"); txt_LoanTerm.Attributes["class"] = "input_Disabled";
                txt_LoanAmount.Attributes.Add("readonly", "true"); txt_LoanAmount.Attributes["class"] = "input_Disabled";
                sel_loanUseName.Attributes.Add("disabled", "disabled");
                text_ContractNo.Attributes.Add("readonly", "true"); text_ContractNo.Attributes["class"] = "input_Disabled";
                text_LinkmanOne.Attributes.Add("readonly", "true"); text_LinkmanOne.Attributes["class"] = "input_Disabled";
                text_LinkmanTwo.Attributes.Add("readonly", "true"); text_LinkmanTwo.Attributes["class"] = "input_Disabled";
                txt_TelOne.Attributes.Add("readonly", "true"); txt_TelOne.Attributes["class"] = "input_Disabled";
                txt_TelTwo.Attributes.Add("readonly", "true"); txt_TelTwo.Attributes["class"] = "input_Disabled";
                text_Agency.Attributes.Add("readonly", "true"); text_Agency.Attributes["class"] = "input_Disabled";
            }

            if ((_loanModel.ExamStatus == 1 && !IsRole("9")) || (_loanModel.ExamStatus == 3 && !IsRole("10")) || (_loanModel.ExamStatus == 7 && !IsRole("11")) || (_loanModel.ExamStatus != 1 && _loanModel.ExamStatus != 3 && _loanModel.ExamStatus != 7))
            {
                button1.Attributes.Add("disabled", "disabled");
                button1.CssClass = "inputButtonDisabled";

                BtnScore.Attributes.Add("disabled", "disabled");
                BtnScore.CssClass = "inputButtonDisabled";
                Button2.Attributes.Add("disabled", "disabled");
                Button2.CssClass = "inputButtonDisabled";
                BtnDeleteFile.Attributes.Add("disabled", "disabled");
                BtnDeleteFile.CssClass = "inputButtonDisabled";
                BtnAuth.Attributes.Add("disabled", "disabled");
                BtnAuth.CssClass = "inputButtonDisabled";
                Button3.Attributes.Add("disabled", "disabled");
                Button3.CssClass = "inputButtonDisabled";
            }

            //非审核角色不能使用审核按钮
            if (!IsRole("9") && !IsRole("10") && !IsRole("11"))
            {
                button1.Attributes.Add("disabled", "disabled");
                button1.CssClass = "inputButtonDisabled";
                Button2.Attributes.Add("disabled", "disabled");
                Button2.CssClass = "inputButtonDisabled";
                BtnScore.Attributes.Add("disabled", "disabled");
                BtnScore.CssClass = "inputButtonDisabled";
                BtnDeleteFile.Attributes.Add("disabled", "disabled");
                BtnDeleteFile.CssClass = "inputButtonDisabled";
                BtnAuth.Attributes.Add("disabled", "disabled");
                BtnAuth.CssClass = "inputButtonDisabled";
                Button3.Attributes.Add("disabled", "disabled");
                Button3.CssClass = "inputButtonDisabled";
            }

        }


        private bool CheckData()
        {
            if (!CheckRate("贷款利率", txtLoanRate.Value.Trim()))
            {
                return false;
            }
            else
            {
                if (Convert.ToDecimal(txtLoanRate.Value) == 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('贷款利率不正确！','warning', '');", true);
                    return false;
                }
            }
            if (!CheckRate("自动投标比例", txtAutoBidScale.Value.Trim()))
            {
                return false;
            }
            if (!CheckRate("担保费", txtGuaranteeFee.Value.Trim()))
            {
                return false;
            }
            if (!CheckRate("投资人居间服务费", txtBidServiceCharges.Value.Trim()))
            {
                return false;
            }
            if (!CheckRate("借款人居间服务费", txtLoanServiceCharges.Value.Trim()))
            {
                return false;
            }
            if (Convert.ToDateTime(WdataBidEndTime.Value) < Convert.ToDateTime(WdataBidStartTime.Value))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('竞标截止时间不能小于竞标开始时间！','warning', '');", true);
                return false;
            }
            if (!CheckAmount("最小投资金额", txtBidAmountMin.Value, _loanInfoModel.MinInvestment, _loanInfoModel.MaxInvestment))
            {
                return false;
            }
            if (!CheckAmount("最大投资金额", txtBidAmountMax.Value, _loanInfoModel.MinInvestment, _loanInfoModel.MaxInvestment))
            {
                return false;
            }
            if (Convert.ToDecimal(txtBidAmountMax.Value) < Convert.ToDecimal(txtBidAmountMin.Value))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('最大投资金额不能小于最小投资金额！','warning', '');", true);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证投资额
        /// </summary>
        /// <param name="message"></param>
        /// <param name="nowvalue"></param>
        /// <param name="sysmin"></param>
        /// <param name="sysmax"></param>
        /// <returns></returns>
        private bool CheckAmount(string message, string nowvalue, decimal sysmin, decimal sysmax)
        {
            try
            {
                if (nowvalue == "")
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('" + message + "不能为空！','warning', '');", true);
                    return false;
                }
                if (_loanModel.ExamStatus < 9)
                {
                    if (Convert.ToDecimal(nowvalue) < 100)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                               "MessageAlert('" + message + "小于系统最小投标额！','warning', '');",
                                                               true);
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('" + message + "错误！','warning', '');", true);
                return false;
            }

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

                if (!RegexHelper.IsRate(nowvalue.Trim()) || Convert.ToDecimal(nowvalue) < 0 || Convert.ToDecimal(nowvalue) > 100)
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

        private void FileInit()
        {
            tbDirInfo.Rows.Clear();

            string filePath = ConfigHelper.LoanFilePhysicallPath;
            string urlPath = ConfigHelper.LoanFileVirtualPath;
            string strCurrentDir;
            strCurrentDir = filePath + DESStringHelper.EncryptString(_loanId.ToString());
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                return;
            }
            try
            {

                var loanBll = new LoanBll();
                var auditHistoryModel = new AuditHistoryModel();

                auditHistoryModel.LoanID = _loanId;
                auditHistoryModel.Result = radio_audit.Checked == true ? true : false;
                auditHistoryModel.Reason = Request["textReason"];
                auditHistoryModel.UserID = _userId;
                auditHistoryModel.ReviewComments = Request["textReviewComments"];

                _loanModel = new LoanBll().GetLoanModel(_loanId);

                //_loanModel.AuthStatus = ControlHelper.GetCheckBoxList(ckbAuthList);
                _loanModel.AuthStatus = "";




                if (IsRole("9") && _loanModel.ExamStatus == 1) //平台初审
                {
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    if (_loanModel.GuaranteeID == 0 && radio_audit.Checked)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择担保公司！','warning', '');",
                                                               true);
                        return;
                    }
                    _loanModel.RepaymentMethod = Convert.ToInt16(sel_repaymentMethod.Value);
                    if (_loanModel.RepaymentMethod == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择还款方式！','warning', '');",
                                                               true);
                        return;
                    }
                    _loanModel.LoanRate = Convert.ToDecimal(Request["txtLoanRate"]);
                    _loanModel.ReleasedRate = Convert.ToDecimal(Request["txtLoanRate"]);
                    _loanModel.NeedGuarantee = cbNeedGuarantee.Checked;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);
                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["txtBidAmountMax"]);
                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);


                    _loanModel.NeedLoanCharges = cbNeedLoanCharges.Checked;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txtLoanServiceCharges"]);
                    _loanModel.NeedBidCharges = cbNeedBidCharges.Checked;
                    _loanModel.BidServiceCharges = Convert.ToDecimal(Request["txtBidServiceCharges"]);
                    _loanModel.LoanTitle = Request["text_LoanTitle"];
                    _loanModel.LoanTypeID = Convert.ToInt16(sel_LoanScaleType.Value);
                    _loanModel.ExamStatus = radio_audit.Checked ? 3 : 2;
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.DimLoanUseID = Convert.ToInt16(sel_loanUseName.Value);
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["txt_LoanAmount"]);
                    _loanModel.LoanTerm = Convert.ToInt16(Request["txt_LoanTerm"]);
                    _loanModel.LoanDescribe = Request.Form["content1"];

                    _loanModel.RepaymentMethod = Convert.ToInt16(sel_repaymentMethod.Value);
                    _loanModel.BorrowMode = (sel_repaymentMethod.Value == "2" ? 0 : 1);
                    _loanModel.TradeType = Convert.ToInt16(sel_TradeType.Value);

                    _loanModel.SumScore = Convert.ToInt16(lab_SumScore.InnerText);
                    _loanModel.ScoreLevel = lab_ScoreLevel.InnerText;

                    _loanModel.ContractNo = Request["text_ContractNo"];
                    _loanModel.Agency = Request["text_Agency"];
                    _loanModel.LinkmanOne = Request["text_LinkmanOne"];
                    _loanModel.LinkmanTwo = Request["text_LinkmanTwo"];
                    _loanModel.TelOne = Request["txt_TelOne"];
                    _loanModel.TelTwo = Request["txt_TelTwo"];

                    auditHistoryModel.Process = 3;

                    //调查信息保存
                    int _applyID = new LocalCreditBll().GetApplyidByLoanId(_loanId);
                    LocalCreditModel localModel = new LocalCreditModel();
                    localModel.MonthlyProfit = txt_MonthlyProfit.Value;
                    localModel.LiabilitiesAmount = txt_LiabilitiesAmount.Value;
                    localModel.LiabilitiesRatio = txt_LiabilitiesRatio.Value;
                    localModel.MonthlyControlIncome = txt_MonthlyControlIncome.Value;
                    localModel.QualityProfessional = Convert.ToInt32(rdo_QualityProfessional.SelectedValue);
                    localModel.HouseProperty = Convert.ToInt32(rdo_HouseProperty.SelectedValue);
                    localModel.LitigationSeach = txt_LitigationSeach.Text;
                    localModel.SocialSecuritySeach = txt_SocialSecuritySeach.Text;
                    localModel.CreditRecordSeach = txt_CreditRecordSeach.Text;
                    localModel.ContactSituation = txt_ContactSituation.Text;
                    localModel.OtherSituation = txt_OtherSituation.Text;
                    localModel.LoanApplyId = _applyID;

                    new LocalCreditBll().UpdateInvestigationInfo(localModel);//更新调查信息

                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);
                }
                else if (IsRole("10") && _loanModel.ExamStatus == 3) //平台二审
                {
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    if (_loanModel.GuaranteeID == 0 && radio_audit.Checked)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择担保公司！','warning', '');",
                                                               true);
                        return;
                    }

                    _loanModel.RepaymentMethod = Convert.ToInt16(sel_repaymentMethod.Value);
                    if (_loanModel.RepaymentMethod == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择还款方式！','warning', '');",
                                                               true);
                        return;
                    }
                    _loanModel.LoanRate = Convert.ToDecimal(Request["txtLoanRate"]);
                    _loanModel.ReleasedRate = Convert.ToDecimal(Request["txtLoanRate"]);
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.NeedGuarantee = cbNeedGuarantee.Checked;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);
                    _loanModel.NeedLoanCharges = cbNeedLoanCharges.Checked;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txtLoanServiceCharges"]);
                    _loanModel.NeedBidCharges = cbNeedBidCharges.Checked;
                    _loanModel.BidServiceCharges = Convert.ToDecimal(Request["txtBidServiceCharges"]);
                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["txtBidAmountMax"]);
                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);
                    _loanModel.RepaymentMethod = Convert.ToInt16(sel_repaymentMethod.Value);
                    _loanModel.ExamStatus = radio_audit.Checked ? 5 : 4;
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.LoanTitle = Request["text_LoanTitle"];
                    _loanModel.LoanTypeID = Convert.ToInt16(sel_LoanScaleType.Value);
                    _loanModel.DimLoanUseID = Convert.ToInt16(sel_loanUseName.Value);
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["txt_LoanAmount"]);
                    _loanModel.LoanTerm = Convert.ToInt16(Request["txt_LoanTerm"]);
                    _loanModel.LoanDescribe = Request.Form["content1"];

                    _loanModel.BorrowMode = (sel_repaymentMethod.Value == "2" ? 0 : 1);
                    _loanModel.TradeType = Convert.ToInt16(sel_TradeType.Value);

                    _loanModel.SumScore = Convert.ToInt16(lab_SumScore.InnerText);
                    _loanModel.ScoreLevel = lab_ScoreLevel.InnerText;

                    _loanModel.ContractNo = Request["text_ContractNo"];
                    _loanModel.Agency = Request["text_Agency"];
                    _loanModel.LinkmanOne = Request["text_LinkmanOne"];
                    _loanModel.LinkmanTwo = Request["text_LinkmanTwo"];
                    _loanModel.TelOne = Request["txt_TelOne"];
                    _loanModel.TelTwo = Request["txt_TelTwo"];

                    auditHistoryModel.Process = 4;

                    //调查信息保存
                    int _applyID = new LocalCreditBll().GetApplyidByLoanId(_loanId);
                    LocalCreditModel localModel = new LocalCreditModel();
                    localModel.MonthlyProfit = txt_MonthlyProfit.Value;
                    localModel.LiabilitiesAmount = txt_LiabilitiesAmount.Value;
                    localModel.LiabilitiesRatio = txt_LiabilitiesRatio.Value;
                    localModel.MonthlyControlIncome = txt_MonthlyControlIncome.Value;
                    localModel.QualityProfessional = Convert.ToInt32(rdo_QualityProfessional.SelectedValue);
                    localModel.HouseProperty = Convert.ToInt32(rdo_HouseProperty.SelectedValue);
                    localModel.LitigationSeach = txt_LitigationSeach.Text;
                    localModel.SocialSecuritySeach = txt_SocialSecuritySeach.Text;
                    localModel.CreditRecordSeach = txt_CreditRecordSeach.Text;
                    localModel.ContactSituation = txt_ContactSituation.Text;
                    localModel.OtherSituation = txt_OtherSituation.Text;
                    localModel.LoanApplyId = _applyID;

                    new LocalCreditBll().UpdateInvestigationInfo(localModel);//更新调查信息

                    bool res = loanBll.UpdateTran(_loanModel, auditHistoryModel);
                    //添加微信发标提醒
                    if (res)
                    {
                        LoanModel lm = loanBll.GetLoanModel(_loanModel.ID);
                        loanBll.AddWeixinNoticeMessage(lm);
                    }
                    string alert = res ? "MessageAlert('审核成功。','success', '');" : "MessageAlert('审核失败。','error', '');";
                    ClientScript.RegisterClientScriptBlock(GetType(), "", alert, true);
                }
                else if (IsRole("11") && _loanModel.ExamStatus == 7) //平台复审
                {
                    _loanModel.ExamStatus = radio_audit.Checked ? 9 : 8;

                    auditHistoryModel.Process = 5;

                    //调查信息保存
                    int _applyID = new LocalCreditBll().GetApplyidByLoanId(_loanId);
                    LocalCreditModel localModel = new LocalCreditModel();
                    localModel.MonthlyProfit = txt_MonthlyProfit.Value;
                    localModel.LiabilitiesAmount = txt_LiabilitiesAmount.Value;
                    localModel.LiabilitiesRatio = txt_LiabilitiesRatio.Value;
                    localModel.MonthlyControlIncome = txt_MonthlyControlIncome.Value;
                    localModel.QualityProfessional = Convert.ToInt32(rdo_QualityProfessional.SelectedValue);
                    localModel.HouseProperty = Convert.ToInt32(rdo_HouseProperty.SelectedValue);
                    localModel.LitigationSeach = txt_LitigationSeach.Text;
                    localModel.SocialSecuritySeach = txt_SocialSecuritySeach.Text;
                    localModel.CreditRecordSeach = txt_CreditRecordSeach.Text;
                    localModel.ContactSituation = txt_ContactSituation.Text;
                    localModel.OtherSituation = txt_OtherSituation.Text;
                    localModel.LoanApplyId = _applyID;

                    new LocalCreditBll().UpdateInvestigationInfo(localModel);//更新调查信息

                    string message = "";
                    if (_loanModel.ExamStatus == 9)
                    {
                        if (loanBll.BuildPlanTran(_loanModel, auditHistoryModel))
                        {
                            if (sel_LoanScaleType.Value == "5")
                            {
                                bool flag = new ProcRepaymentBll().Add(_loanModel.ID, 1, ref message);
                                if (flag)
                                {
                                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                   GenerationContract(_loanId)
                                                                       ? "MessageAlert('审核还款成功且合同生成成功!','success', '');"
                                                                       : "MessageAlert('审核还款成功，合同生成失败," + message + "!','error', '');",
                                                                   true);
                                }
                                else
                                {
                                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                   GenerationContract(_loanId)
                                                                       ? "MessageAlert('审核放款成功且合同生成成功," + message + "!','success', '');"
                                                                       : "MessageAlert('审核放款成功，合同生成失败," + message + "!','error', '');",
                                                                   true);
                                }
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                   GenerationContract(_loanId)
                                                                       ? "MessageAlert('审核放款成功且合同生成成功!','success', '');"
                                                                       : "MessageAlert('审核放款成功，合同生成失败!','error', '');",
                                                                   true);
                            }

                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('审核失败!还款账户余额不足!','error', '');",
                                                                   true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);
                    }
                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
            }
            DataInit();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string waterFile = FileUpload1.FileName;//水印文件
            string srcfile = "src/" + FileUpload1.FileName;//原始文件
            string uploadPath = DESStringHelper.EncryptString(_loanId.ToString());
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
                Image srcImg = Image.FromFile(filePath + uploadPath + "/" + srcfile);
                if (fileEx == "jpg" || fileEx == "bmp" || fileEx == "gif" || fileEx == "png")
                {
                    Image img = ImageWatermark.CreateWatermark(
                    srcImg,
                    Image.FromFile(@"D:\files\logo.png"),
                    0.5F,
                    ContentAlignment.BottomRight);
                    img.Save(filePath + uploadPath + "/" + waterFile);//保存水印文件
                    img.Dispose();
                    srcImg.Dispose();
                    //if (File.Exists(filePath + uploadPath + "/" + srcfile))//删除原文件
                    //{
                    //    File.Delete(filePath + uploadPath + "/" + srcfile);
                    //}
                }
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件上传成功！','warning', '');", true);
                FileInit();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件上传失败！','warning', '');", true);
            }

        }

        protected void btnLookMemberInfo_Click(object sender, EventArgs e)
        {
            string urlstr = "../MemberManage/MemberGeneralInfo.aspx?ID=" + _loanModel.MemberID + "&columnId=" + ColumnId;

            Page.RegisterClientScriptBlock("open", "<script language='javascript'>MessageWindow(800, 250, '" + urlstr + "')</script>");
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

        //生成合同
        private bool GenerationContract(int loanId)
        {
            try
            {
                var loanBll = new LoanBll();
                var item = loanBll.GetLoanInfoModel("ID=" + loanId);

                if (18 <= item.LoanTypeID && item.LoanTypeID <= 21)
                {
                    //典当小贷合同
                    SignCert.SignCertApp.Instance.AddBuyBackContractTask(loanId);
                }
                else
                {    //普通借款标
                    SignCert.SignCertApp.Instance.AddLoanGuaranteeContractTask(loanId);
                }

                return true;

                #region 不再使用
                //var memberInfo = new MemberInfoBll().GetModel(item.MemberID);
                //var loanContractTemp = "";

                //if (18 <= item.LoanTypeID && item.LoanTypeID <= 21)
                //{
                //    #region  典当小贷合同

                //    loanContractTemp = HtmlHelper.ReadHtmlFile(Server.MapPath(item.LoanTypeID == 20 ? @"/ContractTemp/jxxdContract.html" : @"/ContractTemp/jxddContract.html"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanNumber_Start-->", "<!--LoanNumber_End-->", item.LoanNumber);
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--Borrower_Start-->", "<!--Borrower_End-->", memberInfo.RealName);
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanUserName_Start-->", "<!--LoanUserName_End-->", item.MemberName.Substring(0, 2) + "***" + item.MemberName.Substring(item.MemberName.Length - 2, 2));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingOrganizationCode_Start-->", "<!--BondingOrganizationCode_End-->", memberInfo.IdentityCard);

                //    var bidBll = new BidBLL();
                //    var bidList = bidBll.GetBidListByLoanId(item.ID);
                //    var sb = new StringBuilder();
                //    if (bidList != null)
                //    {
                //        foreach (var bidModel in bidList)
                //        {
                //            sb.Append("<tr><td>" + bidModel.MemberName.Substring(0, 1) + "*****" + bidModel.MemberName.Substring(bidModel.MemberName.Length - 1, 1) + "</td><td>" + bidModel.IdentityCard.Replace(bidModel.IdentityCard.Substring(0, bidModel.IdentityCard.Length - 3), "************") + "</td><td style=\"text-align: right; padding-right: 30px;\">" + bidModel.BidAmount.ToString("N2") + "</td></tr>");
                //        }
                //    }
                //    var repaymentDate = item.ReviewTime;
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LenderList_Start-->", "<!--LenderList_End-->", sb.ToString());
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanAmountUpp_Start-->", "<!--LoanAmountUpp_End-->", ConvertHelper.LowAmountConvertChCap(item.LoanAmount));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanAmountLow_Start-->", "<!--LoanAmountLow_End-->", item.LoanAmount.ToString("N2"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LastRepayDate_Start-->", "<!--LastRepayDate_End-->", (item.BorrowMode == 0) ? repaymentDate.AddDays(item.LoanTerm).ToString("yyyy-MM-dd") : repaymentDate.AddMonths(item.LoanTerm).ToString("yyyy-MM-dd"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanRate_Start-->", "<!--LoanRate_End-->", item.LoanRate.ToString(CultureInfo.InvariantCulture));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Chapter_Start-->", "<!--BondingCompany_Chapter_End-->", "<div class=\"seal-" + item.GuaranteeID + "\">&nbsp;</div>");
                //    var repaymentPlan = new StringBuilder();
                //    var repaymentPlanBll = new RepaymentPlanBll();
                //    var dt = repaymentPlanBll.GetRepaymentPlanByLoanID(loanId);
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            repaymentPlan.Append("<tr><td>" + dr["PeNumber"] + "</td><td>" + dr["RePayDate"] + "</td><td>" + dr["RePrincipal"] + "</td><td>" + dr["ReInterest"] + "</td><td>" + dr["RemainAmount"] + "</td></tr>");
                //        }
                //    }
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentPlan_Start-->", "<!--RepaymentPlan_End-->", repaymentPlan.ToString());

                //    #endregion
                //}
                //else
                //{
                //    #region 普通借款标
                //    var guaranteeMemberInfo = new MemberInfoBll().GetModel(item.GuaranteeID);
                //    loanContractTemp = HtmlHelper.ReadHtmlFile(Server.MapPath(@"/ContractTemp/LoanContract.html"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanNumber_Start-->", "<!--LoanNumber_End-->", item.LoanNumber);
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--Borrower_Start-->", "<!--Borrower_End-->", memberInfo.RealName.Replace(memberInfo.RealName, "***"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanUserName_Start-->", "<!--LoanUserName_End-->", item.MemberName.Substring(0, 1) + "*****" + item.MemberName.Substring(item.MemberName.Length - 1, 1));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanCode_Start-->", "<!--LoanCode_End-->", memberInfo.IdentityCard.Replace(memberInfo.IdentityCard.Substring(0, memberInfo.IdentityCard.Length - 3), "************"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingOrganizationCode_Start-->", "<!--BondingOrganizationCode_End-->", guaranteeMemberInfo.IdentityCard);
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Start-->", "<!--BondingCompany_End-->", guaranteeMemberInfo.RealName);
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Chapter_Start-->", "<!--BondingCompany_Chapter_End-->", "<div class=\"seal-" + item.GuaranteeID + "\">&nbsp;</div>");

                //    var bidBll = new BidBLL();
                //    var bidList = bidBll.GetBidListByLoanId(item.ID);
                //    var sb = new StringBuilder();
                //    if (bidList != null)
                //    {
                //        foreach (var bidModel in bidList)
                //        {
                //            sb.Append("<tr><td>" + bidModel.MemberName.Substring(0, 1) + "*****" + bidModel.MemberName.Substring(bidModel.MemberName.Length - 1, 1) + "</td><td>" + bidModel.IdentityCard.Replace(bidModel.IdentityCard.Substring(0, bidModel.IdentityCard.Length - 3), "************") + "</td><td style=\"text-align: right; padding-right: 30px;\">" + bidModel.BidAmount.ToString("N2") + "</td></tr>");
                //        }
                //    }
                //    var repaymentDate = item.ReviewTime;
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LenderList_Start-->", "<!--LenderList_End-->", sb.ToString());
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanAmountUpp_Start-->", "<!--LoanAmountUpp_End-->", ConvertHelper.LowAmountConvertChCap(item.LoanAmount));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanAmountLow_Start-->", "<!--LoanAmountLow_End-->", item.LoanAmount.ToString("N2"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanTerm_Start-->", "<!--LoanTerm_End-->", item.LoanTerm + item.BorrowModeStr);
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanStartDate_Start-->", "<!--LoanStartDate_End-->", repaymentDate.ToString("yyyy年MM月dd日"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanEndDate_Start-->", "<!--LoanEndDate_End-->", (item.BorrowMode == 0) ? repaymentDate.AddDays(item.LoanTerm).ToString("yyyy年MM月dd日") : repaymentDate.AddMonths(item.LoanTerm).ToString("yyyy年MM月dd日"));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanRate_Start-->", "<!--LoanRate_End-->", item.LoanRate.ToString(CultureInfo.InvariantCulture));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanPurpose_Start-->", "<!--LoanPurpose_End-->", item.LoanUseName);
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentMethod_Start-->", "<!--RepaymentMethod_End-->", item.RepaymentMethod.ToString(CultureInfo.InvariantCulture));
                //    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentDate_Start-->", "<!--RepaymentDate_End-->", (item.BorrowMode == 0) ? "—" : repaymentDate.Day.ToString(CultureInfo.InvariantCulture));
                //    #endregion
                //}
                //return HtmlHelper.WriteHtmlFile(loanContractTemp, ConfigHelper.ContractPhysicallPath + item.ReviewTime.ToString("yyyy-MM-dd"), item.LoanNumber + ".html");
                #endregion

            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return false;
            }
        }

        protected void BtnScore_Click(object sender, EventArgs e)
        {
            var lsb = new LoanScoreBll();
            lsb.Add(_loanId, Convert.ToInt16(sel_LoanScaleType.Value));

            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageWindow(590, 360, '/p2p/LoanScore.aspx?loanId=" + _loanId + "');", true);
        }

        protected void BtnAuth_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageWindow(590, 360, '/p2p/LoanAuthInfo.aspx?loanId=" + _loanId + "');", true);
        }

        protected void BtnDeleteFile_Click(object sender, EventArgs e)
        {
            string myfile = text_FileName.Value;
            string uploadPath = DESStringHelper.EncryptString(_loanId.ToString());
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

        protected void BtnTempSave(object sender, EventArgs e)
        {
            _loanModel = new LoanBll().GetLoanModel(_loanId);

            _loanModel.LoanTypeID = Convert.ToInt16(sel_LoanScaleType.Value);
            _loanModel.ContractNo = Request["text_ContractNo"];
            _loanModel.Agency = Request["text_Agency"];
            _loanModel.LinkmanOne = Request["text_LinkmanOne"];
            _loanModel.LinkmanTwo = Request["text_LinkmanTwo"];
            _loanModel.TelOne = Request["txt_TelOne"];
            _loanModel.TelTwo = Request["txt_TelTwo"];
            _loanModel.LoanDescribe = Request.Form["content1"];

            //调查信息保存
            int _applyID = new LocalCreditBll().GetApplyidByLoanId(_loanId);
            LocalCreditModel localModel = new LocalCreditModel();
            localModel.MonthlyProfit = txt_MonthlyProfit.Value;
            localModel.LiabilitiesAmount = txt_LiabilitiesAmount.Value;
            localModel.LiabilitiesRatio = txt_LiabilitiesRatio.Value;
            localModel.MonthlyControlIncome = txt_MonthlyControlIncome.Value;
            localModel.QualityProfessional = Convert.ToInt32(rdo_QualityProfessional.SelectedValue);
            localModel.HouseProperty = Convert.ToInt32(rdo_HouseProperty.SelectedValue);
            localModel.LitigationSeach = txt_LitigationSeach.Text;
            localModel.SocialSecuritySeach = txt_SocialSecuritySeach.Text;
            localModel.CreditRecordSeach = txt_CreditRecordSeach.Text;
            localModel.ContactSituation = txt_ContactSituation.Text;
            localModel.OtherSituation = txt_OtherSituation.Text;
            localModel.LoanApplyId = _applyID;

            new LocalCreditBll().UpdateInvestigationInfo(localModel);//更新调查信息

            var loanBll = new LoanBll();
            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateLoanDescribe(_loanModel)
                                                               ? "MessageAlert('借款标详情保存成功。','success', '');"
                                                               : "MessageAlert('借款标详情保存失败。','error', '');", true);
            DataInit();
        }
    }
}