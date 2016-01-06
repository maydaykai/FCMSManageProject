using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class RechargeRecoDifferenceModel
    {
        /// <summary>
        /// 会员名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 通联订单号
        /// </summary>
        public string AllinpaySerialNumber { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string MerchantOrderNumber { get; set; }

        /// <summary>
        /// 通联标识
        /// </summary>
        public string PaymentStatus { get; set; }


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
