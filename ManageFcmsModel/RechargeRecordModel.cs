using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class RechargeRecordModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>		
        public int MemberID { get; set; }

        /// <summary>
        /// 充值类型：0-线上 1-线下
        /// </summary>		
        public int Type { get; set; }

        /// <summary>
        /// 充值渠道：比如 0-其他、 1-通联、 2-通联移动支付（IOS）、3-通联移动支付（Android）
        /// </summary>		
        public int RechargeChannel { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>		
        public string OrderNumber { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>		
        public string MerchantOrderNo { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>		
        public decimal Amount { get; set; }

        /// <summary>
        /// 充值手续费
        /// </summary>		
        public decimal RechargeFee { get; set; }

        /// <summary>
        /// 充值状态：0-付款中 1-付款成功 2-付款失败
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 请求报文
        /// </summary>		
        public string RequestXml { get; set; }

        /// <summary>
        /// 响应报文
        /// </summary>		
        public string ResponseXml { get; set; }

        /// <summary>
        /// 充值申请时间
        /// </summary>		
        public DateTime ApplicationTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 审核状态：0-初审中 1-复审中 2-初审不通过 3-复审不通过 4-复审通过
        /// </summary>		
        public int AuditStatus { get; set; }

        /// <summary>
        /// 审核记录 例如：初审通过—Norton—2014-06-17 09:36:46—（审核意见：同意）|复审通过—Norton—2014-06-17 09:36:55—（审核意见：同意）
        /// </summary>		
        public string AuditRecords { get; set; }
    }
}
