using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class CashRecoModel
    {
        /// <summary>
        /// 会员名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public string TransactionAmount { get; set; }

        /// <summary>
        /// 通联订单号
        /// </summary>
        public string AllinpaySerialNumber { get; set; }

        /// <summary>
        /// 通联交易状态码
        /// </summary>
        public string TransactionStatusCode { get; set; }

        /// <summary>
        /// 通联支付状态
        /// </summary>
        public string AllinPayStatus { get; set; }

        /// <summary>
        /// 融金宝支付状态
        /// </summary>
        public string RjbStatus { get; set; }
    }
}
