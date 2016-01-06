using System.Collections.Generic;
using ManageFcmsModel;

namespace SignCert.Model
{
    public class LoanPdfModel
    {
        public LoanModel LoanInfo { get; set; }

        /// <summary>
        /// 借款人或者转让人
        /// </summary>
        public MemberInfoModel MemberInfo { get; set; }

        /// <summary>
        /// 担保人信息
        /// </summary>
        public MemberInfoModel GuaranteeMemberInfo { get; set; }

        /// <summary>
        /// 投资人列表
        /// </summary>
        public List<BidModel> BidList { get; set; }

        /// <summary>
        /// 投资人, 目前使用在 借款及担保合同
        /// </summary>
        public BidModel Investor { get; set; }

        /// <summary>
        /// 生成的文件名
        /// </summary>
        public string FileName { get; set; }
    }
}