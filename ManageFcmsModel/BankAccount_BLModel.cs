using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BankAccount_BLModel
    {
        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>		
        public int BankID { get; set; }

        /// <summary>
        /// 开户人
        /// </summary>		
        public string AccountHolder { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>		
        public string BankCardNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>		
        public string BankName { get; set; }

        /// <summary>
        /// 银行代码
        /// </summary>		
        public string BankCode { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>		
        public string EnglishName { get; set; }
        /// <summary>
        /// 会员账户类型 -0：个人账户；1：企业账户
        /// </summary>
        public int MemberType { get; set; }
    }
}
