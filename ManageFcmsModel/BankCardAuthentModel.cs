using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BankCardAuthentModel
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
        /// MemberID
        /// </summary>		
        public int MemberID
        {
            get;
            set;
        }
        /// <summary>
        /// 银行类型
        /// </summary>		
        public int BankType
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
        /// 开户人
        /// </summary>		
        public string Account
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
        /// 认证金额（系统随机生成）
        /// </summary>		
        public decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 标记：0-未汇款 1-已汇款
        /// </summary>		
        public bool sign
        {
            get;
            set;
        }
        /// <summary>
        /// 汇款方式：0-线下 1-线上
        /// </summary>		
        public int PayType
        {
            get;
            set;
        }
        /// <summary>
        /// 汇款状态：-1-汇款失败  0-汇款中 1-汇款成功
        /// </summary>		
        public int PayStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 验证次数
        /// </summary>
        public int VerTimes
        {
            get;
            set;
        }
        /// <summary>
        /// BankID
        /// </summary>
        public int BankID
        {
            get;
            set;
        }
        /// <summary>
        /// BankCode
        /// </summary>
        public string BankCode
        {
            get;
            set;
        }
        /// <summary>
        /// RealName
        /// </summary>
        public string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// AuthentResult
        /// </summary>
        public int AuthentResult
        {
            get;
            set;
        }
        /// <summary>
        /// EnglishName
        /// </summary>
        public string EnglishName
        {
            get;
            set;
        }
    }
}
