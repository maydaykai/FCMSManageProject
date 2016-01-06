using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class GuaranteeNoModel
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
        /// 担保公司ID
        /// </summary>
        public int GuaranteeId
        {
            get;
            set;
        }
        /// <summary>
        /// 担保公司名称
        /// </summary>
        public string GuaranteeName
        {
            get;
            set;
        }
        /// <summary>
        /// 担保涵号
        /// </summary>
        public string GuaranteeNo
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
    }
}
