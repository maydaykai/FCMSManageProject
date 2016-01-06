using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class BranchCompanyModel
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
        /// 分公司名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 开业时间
        /// </summary>
        public DateTime SetUpDate
        {
            get;
            set;
        }
        /// <summary>
        /// 资料备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}
