using System;
using ManageFcmsCommon;

namespace SignCert.Business
{
    public class FileGenerateBusiness
    {
        public static readonly FileGenerateBusiness Instance = new FileGenerateBusiness();

        /// <summary>
        ///     格式为：合同序列号/会员序列号_投标ID.PDF
        /// </summary>
        /// <param name="loanNumber"></param>
        /// <param name="memberId"></param>
        /// <param name="bidId"></param>
        /// <returns></returns>
        public string GetContractPdfPath(string loanNumber, int memberId, int bidId)
        {
            string result = string.Format("{0}{1}/{2}_{3}.pdf", ConfigHelper.ContractPhysicallPath, loanNumber,
                                          memberId, bidId);
            return result;
        }


        /// <summary>
        ///     格式为：时间/合同序列号.PDF
        /// </summary>
        /// <param name="loanNumber"></param>
        /// <param name="transferSuccessDate"></param>
        /// <returns></returns>
        public string GetAllInOneContractPdfPath(string loanNumber, DateTime transferSuccessDate)
        {
            string result = string.Format("{0}/{1}/{2}.pdf", ConfigHelper.ContractPhysicallPath, transferSuccessDate.ToString("yyyy-MM-dd"), loanNumber);
            return result;
        }
    }
}