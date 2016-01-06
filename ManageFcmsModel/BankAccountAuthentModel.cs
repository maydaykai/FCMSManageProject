using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BankAccountAuthentModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get;
            set;
        }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 银行编号
        /// </summary>
        public string BankCode
        {
            get;
            set;
        }
        /// <summary>
        /// 省份ID
        /// </summary>
        public string ProvinceID
        {
            get;
            set;
        }
        /// <summary>
        /// 城市ID
        /// </summary>
        public string CityID
        {
            get;
            set;
        }
        /// <summary>
        /// 开户银行支行名称
        /// </summary>
        public string BranchBankName
        {
            get;
            set;
        }
        /// <summary>
        /// 大额行号
        /// </summary>
        public string Prcptcd
        {
            get;
            set;
        }
        /// <summary>
        /// 认证状态 认证状态 0:已添加 1:充值成功2:提现成功3:禁用
        /// </summary>
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName
        {
            get;
            set;
        }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName
        {
            get;
            set;
        }
    }
}
