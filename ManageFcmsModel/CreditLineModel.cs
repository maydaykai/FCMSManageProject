using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
   public class CreditLineModel
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
        /// 授信额度
        /// </summary>
        public decimal CreditLine
        {
            get;
            set;
        }
        /// <summary>
        ///  卡号
        /// </summary>
        public string CardNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 授信编号
        /// </summary>
        public string CreditNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard
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
        /// 操作员ID
        /// </summary>
        public int OpUid
        {
            get;
            set;
        }
    }
}
