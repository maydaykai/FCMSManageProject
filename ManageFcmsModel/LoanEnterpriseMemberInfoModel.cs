using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanEnterpriseMemberInfoModel
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
        /// MemberId
        /// </summary>
        public int MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// 公司性质：国有企业|集体企业|有限责任公司|股份有限公司|私营企业|中外合资企业|外商投资企业
        /// </summary>
        public string Nature
        {
            get;
            set;
        }
        /// <summary>
        /// 员工人数
        /// </summary>
        public string Size
        {
            get;
            set;
        }
        /// <summary>
        /// 行业类别
        /// </summary>
        public string IndustryCategory
        {
            get;
            set;
        }
        /// <summary>
        /// 所在城市
        /// </summary>
        public int CityId
        {
            get;
            set;
        }
        /// <summary>
        /// 主营产品
        /// </summary>
        public string MainProducts
        {
            get;
            set;
        }
        /// <summary>
        /// 注册资本
        /// </summary>
        public string RegisteredCapital
        {
            get;
            set;
        }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string BusinessScope
        {
            get;
            set;
        }
        /// <summary>
        /// 成立年数
        /// </summary>
        public string SetUpyear
        {
            get;
            set;
        }
    }
}
