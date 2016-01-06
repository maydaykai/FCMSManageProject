using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System.Text;

namespace WebUI.ContractTemp
{
    public partial class ContractTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Contract_Btn_Click(object sender, EventArgs e)
        {
            var loanId = ConvertHelper.ToInt(txtLoanID.Value.Trim());
            ClientScript.RegisterClientScriptBlock(GetType(), "", GenerationContract(loanId) ? "MessageAlert('合同生成成功。','success', '');" : "MessageAlert('合同生成失败。','error', '');", true);
        }

        //生成合同
        private bool GenerationContract(int loanId)
        {
            try
            {
                var loanBll = new LoanBll();
                var item = loanBll.GetLoanInfoModel("ID=" + loanId);
                var memberInfo = new MemberInfoBll().GetModel(item.MemberID);
                var loanContractTemp = "";

                if (18 <= item.LoanTypeID && item.LoanTypeID <= 21)
                {
                    #region  典当小贷合同

                    loanContractTemp = HtmlHelper.ReadHtmlFile(Server.MapPath(item.LoanTypeID == 20 ? @"/ContractTemp/jxxdContract.html" : @"/ContractTemp/jxddContract.html"));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanNumber_Start-->", "<!--LoanNumber_End-->", item.LoanNumber);
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--Borrower_Start-->", "<!--Borrower_End-->", memberInfo.RealName);
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanUserName_Start-->", "<!--LoanUserName_End-->", item.MemberName.Substring(0, 2) + "***" + item.MemberName.Substring(item.MemberName.Length - 2, 2));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingOrganizationCode_Start-->", "<!--BondingOrganizationCode_End-->", memberInfo.IdentityCard);

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
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LastRepayDate_Start-->", "<!--LastRepayDate_End-->", (item.BorrowMode == 0) ? repaymentDate.AddDays(item.LoanTerm).ToString("yyyy-MM-dd") : repaymentDate.AddMonths(item.LoanTerm).ToString("yyyy-MM-dd"));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanRate_Start-->", "<!--LoanRate_End-->", item.LoanRate.ToString(CultureInfo.InvariantCulture));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Chapter_Start-->", "<!--BondingCompany_Chapter_End-->", "<div class=\"seal-" + item.GuaranteeID + "\">&nbsp;</div>");
                    var repaymentPlan = new StringBuilder();
                    var repaymentPlanBll = new RepaymentPlanBll();
                    var dt = repaymentPlanBll.GetRepaymentPlanByLoanID(loanId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            repaymentPlan.Append("<tr><td>" + dr["PeNumber"] + "</td><td>" + dr["RePayDate"] + "</td><td>" + dr["RePrincipal"] + "</td><td>" + dr["ReInterest"] + "</td><td>" + dr["RemainAmount"] + "</td></tr>");
                        }
                    }
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentPlan_Start-->", "<!--RepaymentPlan_End-->", repaymentPlan.ToString());

                    #endregion
                }
                else
                {
                    #region 普通借款标
                    var guaranteeMemberInfo = new MemberInfoBll().GetModel(item.GuaranteeID);
                    loanContractTemp = HtmlHelper.ReadHtmlFile(Server.MapPath(@"/ContractTemp/LoanContract.html"));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanNumber_Start-->", "<!--LoanNumber_End-->", item.LoanNumber);
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--Borrower_Start-->", "<!--Borrower_End-->", memberInfo.RealName.Replace(memberInfo.RealName, "***"));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanUserName_Start-->", "<!--LoanUserName_End-->", item.MemberName.Substring(0, 1) + "*****" + item.MemberName.Substring(item.MemberName.Length - 1, 1));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanCode_Start-->", "<!--LoanCode_End-->", memberInfo.IdentityCard.Replace(memberInfo.IdentityCard.Substring(0, memberInfo.IdentityCard.Length - 3), "************"));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingOrganizationCode_Start-->", "<!--BondingOrganizationCode_End-->", guaranteeMemberInfo.IdentityCard);
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Start-->", "<!--BondingCompany_End-->", guaranteeMemberInfo.RealName);
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--BondingCompany_Chapter_Start-->", "<!--BondingCompany_Chapter_End-->", "<div class=\"seal-" + item.GuaranteeID + "\">&nbsp;</div>");

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
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanRate_Start-->", "<!--LoanRate_End-->", item.LoanRate.ToString(CultureInfo.InvariantCulture));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--LoanPurpose_Start-->", "<!--LoanPurpose_End-->", item.LoanUseName);
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentMethod_Start-->", "<!--RepaymentMethod_End-->", item.RepaymentMethod.ToString(CultureInfo.InvariantCulture));
                    loanContractTemp = HtmlHelper.HtmlReplace(loanContractTemp, "<!--RepaymentDate_Start-->", "<!--RepaymentDate_End-->", (item.BorrowMode == 0) ? "—" : repaymentDate.Day.ToString(CultureInfo.InvariantCulture));
                    #endregion
                }
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