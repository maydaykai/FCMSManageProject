using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BankModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int Id { get; set; }

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

    }
}
