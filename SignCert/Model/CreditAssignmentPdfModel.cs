using System.Data;
using FCMSModel;
using ManageFcmsModel;

namespace SignCert.Model
{
    public class CreditAssignmentPdfModel
    {
        public CreditAssignmentDetailModel CreditAssignmentInfo { get; set; }

        /// <summary>
        ///     转让人
        /// </summary>
        public MemberInfoModel MemberInfo { get; set; }

        /// <summary>
        ///     投资受让人列表
        /// </summary>
        public DataRowCollection BidList { get; set; }

        /// <summary>
        ///     投资受让人, 目前暂不使用
        /// </summary>
        public BidModel Investor { get; set; }

        /// <summary>
        ///     生成的文件名
        /// </summary>
        public string FileName { get; set; }
    }
}