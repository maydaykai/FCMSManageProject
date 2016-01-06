using System;
using System.Collections.Generic;
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

namespace WebUI.p2p
{
    public partial class LoanAudit : BasePage
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
            var ds = fcmsUserbll.GetDimLoanScaleTypeList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_LoanScaleType.DataSource = ds;
            sel_LoanScaleType.DataValueField = "ID";
            sel_LoanScaleType.DataTextField = "LoanScaleType";
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

        private void DataInit()
        {
            BindGcList();
            BindLoanTypeList();
            BindLoanUseList();
            BindAuthList();
            _loanInfoModel = new LoanBll().GetLoanInfoModel(" id =" + _loanId);
            _loanModel = new LoanBll().GetLoanModel(_loanId);
            lab_SurAmount.InnerText = (new LoanBll().GetMemberSurAmount(_loanModel.MemberID)).ToString("N") + " 元";
            lab_AuditAmount.InnerText = (new LoanBll().GetMemberAuditAmount(_loanModel.MemberID)).ToString("N") + " 元";
            lab_AuditAmountBig.InnerText =
                ConvertHelper.LowAmountConvertChCap(new LoanBll().GetMemberAuditAmount(_loanModel.MemberID));
            lab_MemberName.InnerText = _loanInfoModel.MemberName;
            lab_LoanNumber.InnerText = _loanInfoModel.LoanNumber;
            txtLoanRate.Value = _loanInfoModel.LoanRate.ToString();
            txtReleasedRate.Value = _loanInfoModel.ReleasedRate.ToString();
            txtAutoBidScale.Value = _loanInfoModel.AutoBidScale.ToString();
            //lab_LoanAmount.InnerText = _loanInfoModel.LoanAmount.ToString("N");
            txt_LoanAmount.Value = _loanInfoModel.LoanAmount.ToString();
            //lab_loanUseName.InnerText = _loanInfoModel.LoanUseName;
            sel_loanUseName.Value = _loanInfoModel.DimLoanUseID.ToString();
            txt_LoanTerm.Value = _loanInfoModel.LoanTerm.ToString();
            lab_loanTerm.InnerText = (_loanInfoModel.BorrowMode == 0 ? "(按天)" : "(按月)");
            decimal fee = _loanInfoModel.LoanAmount * _loanInfoModel.LoanTerm * _loanInfoModel.LoanServiceCharges / 100 / 365;
            lab_daymonth.InnerText = _loanInfoModel.RepaymentMethod == 2 ? "(按天) 服务费：" + fee.ToString("N2") + " 元" : "(按月)";
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
            content1.InnerText = _loanInfoModel.LoanDescribe;
            text_LoanTitle.Value = _loanInfoModel.LoanTitle;
            sel_LoanScaleType.Value = _loanInfoModel.LoanTypeID.ToString();
            ControlHelper.SetChecked(ckbAuthList, _loanInfoModel.AuthStatus);
            sel_TradeType.Value = _loanInfoModel.TradeType.ToString();
            FileInit();

            _fcmsUserModel = new FcmsUserBll().GetModel(_userId);

            //if (sel_LoanScaleType.Value == "5")
            //{
            //    txt_LoanTerm.Attributes.Add("readonly", "true"); txt_LoanTerm.Attributes["class"] = "input_Disabled";
            //    sel_repaymentMethod.Attributes.Add("disabled", "disabled");
            //}
            if (IsRole("9") || IsRole("10") || IsRole("11"))
            {
                txtGuaranteeFee.Attributes.Add("readonly", "true"); txtGuaranteeFee.Attributes["class"] = "input_Disabled";
                //txtLoanServiceCharges.Attributes.Add("readonly", "true"); txtLoanServiceCharges.Attributes["class"] = "input_Disabled";
                txtBidServiceCharges.Attributes.Add("readonly", "true"); txtBidServiceCharges.Attributes["class"] = "input_Disabled";
                cbNeedLoanCharges.Attributes.Add("disabled", "disabled");
                cbNeedBidCharges.Attributes.Add("disabled", "disabled");
                cbNeedGuarantee.Attributes.Add("disabled", "disabled");

                content1.Attributes.Add("readonly", "true");

            }
            if (_loanModel.ExamStatus == 7 && IsRole("11"))
            {
                txtLoanRate.Attributes.Add("readonly", "true"); txtLoanRate.Attributes["class"] = "input_Disabled";
                txtReleasedRate.Attributes.Add("readonly", "true"); txtReleasedRate.Attributes["class"] = "input_Disabled";
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
            }

            if ((_loanModel.ExamStatus == 1 && !IsRole("9")) || (_loanModel.ExamStatus == 3 && !IsRole("10")) || (_loanModel.ExamStatus == 7 && !IsRole("11")) || (_loanModel.ExamStatus != 1 && _loanModel.ExamStatus != 3 && _loanModel.ExamStatus != 7))
            {
                button1.Attributes.Add("disabled", "disabled");
                button1.CssClass = "inputButtonDisabled";
                Button2.Enabled = false;
            }
            //当前角色需对应当前审核状态，审核按钮才能用
            //if ((IsRole("9") && _loanModel.ExamStatus != 1) || (IsRole("10") && _loanModel.ExamStatus != 3) || (IsRole("11") && _loanModel.ExamStatus != 7))
            //{
            //    button1.Attributes.Add("disabled", "disabled");
            //    button1.CssClass = "inputButtonDisabled";
            //    Button2.Enabled = false;
            //}

            //非审核角色不能使用审核按钮
            if (!IsRole("9") && !IsRole("10") && !IsRole("11"))
            {
                button1.Attributes.Add("disabled", "disabled");
                button1.CssClass = "inputButtonDisabled";
                Button2.Enabled = false;
            }

        }

        private void BindAuthList()
        {
            var ds = AuthColumn.GetAuthColumnList();
            if (ds == null || ds.Count <= 0) return;
            ckbAuthList.DataSource = ds;
            ckbAuthList.DataValueField = "Key";
            ckbAuthList.DataTextField = "Value";
            ckbAuthList.DataBind();
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
            if (!CheckRate("即将发布利率", txtReleasedRate.Value.Trim()))
            {
                return false;
            }
            else
            {
                if (Convert.ToDecimal(txtReleasedRate.Value) == 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('即将发布利率不正确！','warning', '');", true);
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
            try
            {
                string filePath = ConfigHelper.LoanFilePhysicallPath;
                string urlPath = ConfigHelper.LoanFileVirtualPath;
                string strCurrentDir;
                strCurrentDir = filePath + _loanInfoModel.LoanNumber;
                FileInfo fi;
                DirectoryInfo di;
                TableCell td;
                TableRow tr;

                string FileName;	 //文件名称
                string FileExt;	 //文件扩展名
                long FileSize;	 //文件大小
                DateTime FileModify;	 //文件更新时间

                DirectoryInfo dir = new DirectoryInfo(strCurrentDir);
                if (Directory.Exists(strCurrentDir))
                {
                    foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
                    {

                        FileName = "";
                        FileExt = "";
                        FileSize = 0;

                        if (fsi is FileInfo)
                        {
                            //表示当前fsi是文件
                            fi = (FileInfo)fsi;
                            FileName = fi.Name;
                            FileExt = fi.Extension;
                            FileSize = fi.Length;
                            FileModify = fi.LastWriteTime;
                            //通过扩展名来选择文件显示图标

                        }
                        else
                        {
                            //当前为目录
                            di = (DirectoryInfo)fsi;
                            FileName = di.Name;
                            FileModify = di.LastWriteTime;

                        }
                        //组建新的行
                        tr = new TableRow();

                        td = new TableCell();
                        td.Controls.Add(new LiteralControl("<a href='" + urlPath + dir.Name + "/" + FileName.ToString() + "' target='_blank'>" + FileName.ToString() + "</a>"));
                        tr.Cells.Add(td);

                        td = new TableCell();
                        td.Controls.Add(new LiteralControl(FileSize.ToString()));
                        tr.Cells.Add(td);

                        td = new TableCell();
                        td.Controls.Add(new LiteralControl(FileModify.ToString()));
                        tr.Cells.Add(td);

                        tbDirInfo.Rows.Add(tr);
                    }
                }
            }
            catch (Exception)
            {

                throw;
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

                _loanModel.AuthStatus = ControlHelper.GetCheckBoxList(ckbAuthList);

                
                

                //if (Role.IsRole(_fcmsUserModel.RoleId, "3") && _loanModel.ExamStatus == 1)//平台初审
                if (IsRole("9") && _loanModel.ExamStatus == 1) //平台初审
                {
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    if (_loanModel.GuaranteeID == 0 && radio_audit.Checked == true)
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
                    _loanModel.ReleasedRate = Convert.ToDecimal(Request["txtReleasedRate"]);
                    _loanModel.NeedGuarantee = cbNeedGuarantee.Checked == true ? true : false;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);
                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["txtBidAmountMax"]);
                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);

                    
                    _loanModel.NeedLoanCharges = cbNeedLoanCharges.Checked == true ? true : false;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txtLoanServiceCharges"]);
                    _loanModel.LoanTitle = Request["text_LoanTitle"];
                    _loanModel.LoanTypeID = Convert.ToInt16(sel_LoanScaleType.Value);
                    _loanModel.ExamStatus = radio_audit.Checked == true ? 3 : 2;
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.DimLoanUseID = Convert.ToInt16(sel_loanUseName.Value);
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["txt_LoanAmount"]);
                    _loanModel.LoanTerm = Convert.ToInt16(Request["txt_LoanTerm"]);
                    _loanModel.LoanDescribe = Request.Form["content1"];

                    _loanModel.RepaymentMethod = Convert.ToInt16(sel_repaymentMethod.Value);
                    _loanModel.BorrowMode = (sel_repaymentMethod.Value == "2" ? 0 : 1);
                    _loanModel.TradeType = Convert.ToInt16(sel_TradeType.Value);

                    auditHistoryModel.Process = 3;

                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);
                }
                else if (IsRole("10") && _loanModel.ExamStatus == 3) //平台二审
                {
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    if (_loanModel.GuaranteeID == 0 && radio_audit.Checked == true)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择担保公司！','warning', '');",
                                                               true);
                        return;
                    }

                    //if (sel_LoanScaleType.Value == "5")
                    //{
                    //    sel_repaymentMethod.Value = "3";
                    //}

                    _loanModel.RepaymentMethod = Convert.ToInt16(sel_repaymentMethod.Value);
                    if (_loanModel.RepaymentMethod == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择还款方式！','warning', '');",
                                                               true);
                        return;
                    }
                    _loanModel.LoanRate = Convert.ToDecimal(Request["txtLoanRate"]);
                    _loanModel.ReleasedRate = Convert.ToDecimal(Request["txtReleasedRate"]);
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.NeedGuarantee = cbNeedGuarantee.Checked == true ? true : false;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);
                    _loanModel.NeedLoanCharges = cbNeedLoanCharges.Checked == true ? true : false;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txtLoanServiceCharges"]);
                    _loanModel.NeedBidCharges = cbNeedBidCharges.Checked == true ? true : false;
                    _loanModel.BidServiceCharges = Convert.ToDecimal(Request["txtBidServiceCharges"]);
                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["txtBidAmountMax"]);
                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);
                    _loanModel.RepaymentMethod = Convert.ToInt16(sel_repaymentMethod.Value);
                    _loanModel.ExamStatus = radio_audit.Checked == true ? 5 : 4;
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.LoanTitle = Request["text_LoanTitle"];
                    _loanModel.LoanTypeID = Convert.ToInt16(sel_LoanScaleType.Value);
                    _loanModel.DimLoanUseID = Convert.ToInt16(sel_loanUseName.Value);
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["txt_LoanAmount"]);
                    _loanModel.LoanTerm = Convert.ToInt16(Request["txt_LoanTerm"]);
                    _loanModel.LoanDescribe = Request.Form["content1"];

                    _loanModel.BorrowMode = (sel_repaymentMethod.Value == "2" ? 0 : 1);
                    _loanModel.TradeType = Convert.ToInt16(sel_TradeType.Value);

                    auditHistoryModel.Process = 4;

                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);

                }
                else if (IsRole("11") && _loanModel.ExamStatus == 7) //平台复审
                {
                    _loanModel.ExamStatus = radio_audit.Checked == true ? 9 : 8;

                    auditHistoryModel.Process = 5;
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
                            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('借款人账户余额不足以支付居间服务费!','error', '');",
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
            string myfile = FileUpload1.FileName;
            string uploadPath = _loanInfoModel.LoanNumber;
            string filePath = ConfigHelper.LoanFilePhysicallPath;
            if (myfile != null)
            {
                if (!Directory.Exists(filePath + uploadPath))
                {
                    Directory.CreateDirectory(filePath + uploadPath);
                }
                FileUpload1.SaveAs(filePath + uploadPath + "/" + myfile);

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
                var memberInfo = new MemberInfoBll().GetModel(item.MemberID);
                var guaranteeMemberInfo = new MemberInfoBll().GetModel(item.GuaranteeID);
                var loanContractTemp = HtmlHelper.ReadHtmlFile(Server.MapPath(@"/ContractTemp/LoanContract.html"));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanNumber_Start-->", "<!--LoanNumber_End-->", item.LoanNumber);
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--Borrower_Start-->", "<!--Borrower_End-->", memberInfo.RealName.Replace(memberInfo.RealName, "***"));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanUserName_Start-->", "<!--LoanUserName_End-->", item.MemberName.Substring(0, 1) + "*****" + item.MemberName.Substring(item.MemberName.Length - 1, 1));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanCode_Start-->", "<!--LoanCode_End-->", memberInfo.IdentityCard.Replace(memberInfo.IdentityCard.Substring(0, memberInfo.IdentityCard.Length - 3), "************"));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingOrganizationCode_Start-->", "<!--BondingOrganizationCode_End-->", guaranteeMemberInfo.IdentityCard);
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Start-->", "<!--BondingCompany_End-->", guaranteeMemberInfo.RealName);
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Chapter_Start-->", "<!--BondingCompany_Chapter_End-->","<div class=\"seal-"+item.GuaranteeID+"\">&nbsp;</div>");
                
                var bidBll = new BidBLL();
                var bidList = bidBll.GetBidListByLoanId(item.ID);
                var sb = new StringBuilder();
                if (bidList != null)
                {
                    foreach (var bidModel in bidList)
                    {
                        sb.Append("<tr><td>" + bidModel.MemberName.Substring(0, 1) + "*****" + bidModel.MemberName.Substring(bidModel.MemberName.Length - 1, 1) + "</td><td>" + bidModel.IdentityCard.Replace(bidModel.IdentityCard.Substring(0, bidModel.IdentityCard.Length - 3), "************") + "</td><td style=\"text-align: right; padding-right: 30px;\">" + bidModel.BidAmount.ToString("N2") + "</td></tr>");
                    }
                }
                var repaymentDate = item.ReviewTime;
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LenderList_Start-->", "<!--LenderList_End-->", sb.ToString());
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanAmountUpp_Start-->", "<!--LoanAmountUpp_End-->", ConvertHelper.LowAmountConvertChCap(item.LoanAmount));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanAmountLow_Start-->", "<!--LoanAmountLow_End-->", item.LoanAmount.ToString("N2"));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanTerm_Start-->", "<!--LoanTerm_End-->", item.LoanTerm + item.BorrowModeStr);
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanStartDate_Start-->", "<!--LoanStartDate_End-->", repaymentDate.ToString("yyyy年MM月dd日"));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanEndDate_Start-->", "<!--LoanEndDate_End-->", (item.BorrowMode == 0) ? repaymentDate.AddDays(item.LoanTerm).ToString("yyyy年MM月dd日") : repaymentDate.AddMonths(item.LoanTerm).ToString("yyyy年MM月dd日"));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanRate_Start-->", "<!--LoanRate_Start-->", item.LoanRate.ToString(CultureInfo.InvariantCulture));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanPurpose_Start-->", "<!--LoanPurpose_End-->", item.LoanUseName);
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentMethod_Start-->", "<!--RepaymentMethod_End-->", item.RepaymentMethod.ToString(CultureInfo.InvariantCulture));
                loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentDate_Start-->", "<!--RepaymentDate_End-->", (item.BorrowMode == 0) ? "—" : repaymentDate.Day.ToString(CultureInfo.InvariantCulture));
                return HtmlHelper.WriteHtmlFile(loanContractTemp, ConfigHelper.ContractPhysicallPath + item.ReviewTime.ToString("yyyy-MM-dd"), item.LoanNumber + ".html");
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return false;
            }
        }
    }
}