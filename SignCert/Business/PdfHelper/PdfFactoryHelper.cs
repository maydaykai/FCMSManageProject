using System;
using System.Collections.Generic;
using System.Data;
using FCMSModel;
using ManageFcmsBll;
using ManageFcmsModel;
using SignCert.BusinessContract;
using SignCert.BusinessModel;
using SignCert.Model;

namespace SignCert.Business.PdfHelper
{
    public class PdfFactoryHelper
    {
        public static readonly PdfFactoryHelper Instance = new PdfFactoryHelper();

        private readonly Dictionary<ContractTypeEnum, Func<TaskModel, IPdfHelper>> _mappings =
            new Dictionary<ContractTypeEnum, Func<TaskModel, IPdfHelper>>();

        private PdfFactoryHelper()
        {
            _mappings.Add(ContractTypeEnum.LoanGuaranteeContract, GetLoanContractPdfHelper);
            _mappings.Add(ContractTypeEnum.BuyBackContract, GetBuyBackContractPdfHelper);
            _mappings.Add(ContractTypeEnum.CreditAssignment, GetCreditAssignmentContractPdfHelper);
        }

        /// <summary>
        /// 债权转让合同
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private IPdfHelper GetCreditAssignmentContractPdfHelper(TaskModel task)
        {
            var loanBll = new CreditAssignmentBll();
            CreditAssignmentDetailModel caDetailModel = loanBll.GetCreditAssignmentDetail(task.ContractId);
            MemberInfoModel memberInfo = new MemberInfoBll().GetModel(caDetailModel.MemberID);

            var capm = new CreditAssignmentPdfModel { CreditAssignmentInfo = caDetailModel, MemberInfo = memberInfo };

            DateTime transferSuccessDate = caDetailModel.FullScaleTime;
            capm.FileName =
                FileGenerateBusiness.Instance.GetAllInOneContractPdfPath(capm.CreditAssignmentInfo.LoanNumber,
                                                                         transferSuccessDate);

            DataSet bidList = new BidBLL().GetCaBidListByCaID(task.ContractId);
            if (bidList != null && bidList.Tables.Count > 0 && bidList.Tables[0].Rows.Count > 0)
            {
                capm.BidList = bidList.Tables[0].Rows;
            }

            return new CreditAssignmentContractPdfHelper(capm);
        }

        /// <summary>
        /// 借款及担保合同
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private IPdfHelper GetLoanContractPdfHelper(TaskModel task)
        {
            var loanBll = new LoanBll();
            LoanModel item = loanBll.GetLoanInfoModel("ID=" + task.ContractId);
            MemberInfoModel memberInfo = new MemberInfoBll().GetModel(item.MemberID); //借款人
            MemberInfoModel guaranteeMemberInfo = new MemberInfoBll().GetModel(item.GuaranteeID); //担保人信息
            List<BidModel> bidList = new BidBLL().GetBidListByLoanId(item.ID); //获取所有投资人列表生成PDF

            var lpm = new LoanPdfModel
                {
                    LoanInfo = item, //借款或者转让信息
                    MemberInfo = memberInfo, //借款人或者转让人
                    GuaranteeMemberInfo = guaranteeMemberInfo, //担保人信息
                    BidList = bidList
                };

            DateTime repaymentDate = item.ReviewTime;
            lpm.FileName = FileGenerateBusiness.Instance.GetAllInOneContractPdfPath(lpm.LoanInfo.LoanNumber,
                                                                                    repaymentDate);

            IPdfHelper pdfBuilder = new LocalCreditContractPdfHelper(lpm);//新的合同 //new LoanGuaranteeContractPdfHelper(lpm); //普通借款标
            return pdfBuilder;
        }

        /// <summary>
        /// 债权收益权转让及回购合同（典当）
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private IPdfHelper GetBuyBackContractPdfHelper(TaskModel task)
        {
            var loanBll = new LoanBll();
            LoanModel item = loanBll.GetLoanInfoModel("ID=" + task.ContractId);
            MemberInfoModel memberInfo = new MemberInfoBll().GetModel(item.MemberID); //转让人
            MemberInfoModel guaranteeMemberInfo = new MemberInfoBll().GetModel(item.GuaranteeID); //担保人信息
            List<BidModel> bidList = new BidBLL().GetBidListByLoanId(item.ID); //获取所有投资人列表生成PDF

            var lpm = new LoanPdfModel
                {
                    LoanInfo = item, //借款或者转让信息
                    MemberInfo = memberInfo, //借款人或者转让人
                    GuaranteeMemberInfo = guaranteeMemberInfo, //担保人信息
                    BidList = bidList
                };

            DateTime repaymentDate = item.ReviewTime;
            lpm.FileName = FileGenerateBusiness.Instance.GetAllInOneContractPdfPath(lpm.LoanInfo.LoanNumber,
                                                                                    repaymentDate);

            IPdfHelper pdfBuilder = new BuyBackContractPdfHelper(lpm); //典当小贷合同
            return pdfBuilder;
        }

        public IPdfHelper Create(TaskModel task)
        {
            if (_mappings.ContainsKey(task.ContractType))
                return _mappings[task.ContractType](task);

            return null;
        }
    }
}