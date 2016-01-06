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
    public partial class LoanPublish : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
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
                loanModel.ID = int.Parse(Request.Form["sel_LoanNumber"]);
                loanModel.BidStratTime = DateTime.Now;
                var greenChannelRecordModel = new GreenChannelRecordModel
                    {
                        BranchCompanyID = int.Parse(string.IsNullOrEmpty(txtMemberID2.Value) ? "0" : txtMemberID2.Value),
                        Scale = decimal.Parse(string.IsNullOrEmpty(txtScale.Value) ? "0" : txtScale.Value)
                    };
                int type = int.Parse(txtType.Value);

                //update by 2015-10-30 lxy 融金客
                AppointmentLoan appointmentLoan = null;
                if (loanModel.LoanTypeID == 25)
                {
                    appointmentLoan = new AppointmentLoan() { createTime = DateTime.Now };
                    appointmentLoan.isAppointment = 0;
                    appointmentLoan.endTime = DateTime.Now;                    
                    if (int.Parse(Request.Form["rad_appointment"]) == 1)
                    {
                        appointmentLoan.isAppointment = 1;
                        appointmentLoan.endTime = DateTime.Parse(Request.Form["txtAppointmentEndTime"]);
                        //appointmentLoan.biddingTime = DateTime.Parse(Request.Form["txtBiddingTime"]);
                        loanModel.BidStratTime = appointmentLoan.endTime.AddMinutes(15);
                    }
                    appointmentLoan.biddingTime = appointmentLoan.endTime;
                }
                loanModel.BidEndTime = loanModel.BidStratTime.AddDays(1);

                if (type == 0)
                {
                    var loanMemberInfoModel = new LoanMemberInfoModel
                        {
                            Age = int.Parse(txt_memberAge.Value),
                            MaritalStatus = int.Parse(Request.Form["rad_maritalStatus"]),
                            Sex = Request.Form["rad_sexStatus"],
                            DomicilePlace = int.Parse(Request.Form["sel_nativeCity"]),
                            FamilyNum = int.Parse(txt_familyNum.Value),
                            MonthlyPay = txt_monthlyIncome.Value,
                            HaveHouse = Convert.ToBoolean(int.Parse(Request.Form["rad_houseStatus"])),
                            HaveCar = Convert.ToBoolean(int.Parse(Request.Form["rad_carStatus"])),
                            WorkingLife = text_workDuration.Value,
                            JobStatus = Request.Form["rad_jobStatus"]
                        };
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                       loanBll.AddLoan(loanModel, loanMemberInfoModel, greenChannelRecordModel, appointmentLoan) > 0
                                                                           ? "MessageAlert('借款申请成功!','success', '');SelectFun();"
                                                                           : "MessageAlert('借款申请失败!','error', '');SelectFun();",
                                                                       true);
                }
                else
                {
                    var LoanEnterpriseMemberInfoModel = new LoanEnterpriseMemberInfoModel
                        {
                            Nature = Request.Form["sel_companyNature"],
                            IndustryCategory = Request.Form["sel_industry"],
                            CityId =  int.Parse(Request.Form["sel_liveCity"]),
                            Size = text_employeeNum.Value,
                            MainProducts = Request.Form["text_mainBusiness"],
                            RegisteredCapital = text_regCapital.Value,
                            BusinessScope = text_businessScope.Value,
                            SetUpyear = text_companyAge.Value
                        };
                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                       loanBll.AddLoanEnterprise(loanModel, LoanEnterpriseMemberInfoModel, greenChannelRecordModel, appointmentLoan) > 0
                                                                           ? "MessageAlert('借款申请成功!','success', '');SelectFun();"
                                                                           : "MessageAlert('借款申请失败!','error', '');SelectFun();",
                                                                       true);
                }
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
            var ds = fcmsUserbll.GetDimProductTypeList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_LoanScaleType.DataSource = ds;
            sel_LoanScaleType.DataValueField = "ID";
            sel_LoanScaleType.DataTextField = "Name";
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
            sel_liveProvince.DataSource = sel_nativeProvince.DataSource = sel_province.DataSource = ls;
            sel_liveProvince.DataValueField = sel_nativeProvince.DataValueField = sel_province.DataValueField = "ID";
            sel_liveProvince.DataTextField = sel_nativeProvince.DataTextField = sel_province.DataTextField = "Name";
            sel_province.DataBind(); sel_nativeProvince.DataBind(); sel_liveProvince.DataBind();
            sel_province.Items.Insert(0, new ListItem("--请选择--", "0"));
            sel_nativeProvince.Items.Insert(0, new ListItem("--请选择--", "0"));
            sel_liveProvince.Items.Insert(0, new ListItem("--请选择--", "0"));
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
            if (Request.Form["sel_LoanNumber"] == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择申请号！','warning', '');", true);
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
            if (!string.IsNullOrEmpty(txtMemberID2.Value))
            {
                int memberId = int.Parse(txtMemberID2.Value);
                if (memberId > 0)
                {
                    if (!CheckRate("绿色通道比例", txtScale.Value.Trim()))
                    {
                        return false;
                    }
                    var bll = new BranchCompanyBll();
                    var companyLockAmount = bll.GetBranchCompanyLockAmount(memberId);//绿色通道锁定金额
                    var companyBalance = new MemberBll().GetModel(memberId).Balance;//账户余额
                    var duein = new RepaymentPlanBll().GetDueInMoneyByMemberId(memberId);//待收总额
                    if ((companyBalance + duein - companyLockAmount -
                         decimal.Parse(txt_LoanAmount.Value)*decimal.Parse(txtScale.Value)/100) < 0)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('保证金余额不足！','warning', '');", true);
                        return false;
                    }
                }
            }
            
            //update by lxy 2015-10-30 融金客
            if (sel_LoanScaleType.Value == "25" && int.Parse(Request.Form["rad_appointment"]) == 1)
            {
                //if (Request.Form["txtBiddingTime"] == "")
                //{
                //    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入预约开始时间！','warning', '');", true);
                //    return false;
                //}
                if (Request.Form["txtAppointmentEndTime"] == "")
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入预约截至时间！','warning', '');", true);
                    return false;
                }
            }

            int type = int.Parse(txtType.Value);
            if (type == 0)
                CheckMemberInfo();
            else
                CheckEnterpriseInfo();
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

        private void CheckMemberInfo()
        {
            if (string.IsNullOrEmpty(txt_memberAge.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入用户年龄！','warning', '');", true);
                return;
            }
            if (sel_nativeProvince.Value == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择籍贯省份！','warning', '');", true);
                return;
            }
            if (Request.Form["sel_city"] == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择籍贯城市！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txt_familyNum.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入家庭人数！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txt_monthlyIncome.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入月收入水平！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(text_workDuration.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入工作年限！','warning', '');", true);
                return;
            }
        }
        private void CheckEnterpriseInfo()
         {
            if (Request.Form["sel_companyNature"] == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择公司性质！','warning', '');", true);
                return;
            }
            if (Request.Form["sel_liveCity"] == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择所在城市！','warning', '');", true);
                return;
            }
            if (Request.Form["sel_industry"] == "0")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择行业类别！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(text_regCapital.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入注册资本！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(text_mainBusiness.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入主营产品！','warning', '');", true);
                return;
            }
        
            if (string.IsNullOrEmpty(text_companyAge.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入公司年限！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(text_businessScope.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入经营范围！','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(text_employeeNum.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入员工人数！','warning', '');", true);
                return;
            }
        }
    }
}