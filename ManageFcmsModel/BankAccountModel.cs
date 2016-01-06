using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BankAccountModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// MemberID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 银行卡类型 1，借记卡；2，信用卡
        /// </summary>
        public int BankCardType { get; set; }

        /// <summary>
        /// 银行ID
        /// </summary>
        public string BankID { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo { get; set; }

        /// <summary>
        /// 状态：0 禁用 1 启用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 开户人
        /// </summary>
        public string AccountHolder { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行代码
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 银行英文名称
        /// </summary>
        public string EnglishName { get; set; }
    }
}
