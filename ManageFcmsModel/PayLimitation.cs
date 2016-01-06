using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class PayLimitation
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 单笔限额
        /// </summary>
        public string SinglePay { get; set; }

        /// <summary>
        /// 单日限额
        /// </summary>
        public string SingleDay { get; set; }

        /// <summary>
        /// 单月限额
        /// </summary>
        public string SingleMonth { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
