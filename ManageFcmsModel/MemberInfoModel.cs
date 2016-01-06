using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    /// <summary>
    /// 会员基本信息
    /// </summary>
    public class MemberInfoModel
    {

        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>		
        public int MemberID { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string RealName { get; set; }

        /// <summary>
        /// 证件号码(身份证、组织机构代码等)
        /// </summary>		
        public string IdentityCard { get; set; }

        /// <summary>
        /// 性别：男/女
        /// </summary>		
        public string Sex { get; set; }

        /// <summary>
        /// 现居住省份
        /// </summary>		
        public int Province { get; set; }

        /// <summary>
        /// 现居住城市
        /// </summary>		
        public int City { get; set; }

        /// <summary>
        /// 现居住详细地址
        /// </summary>		
        public string Address { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>		
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>		
        public string Telephone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>		
        public string Fax { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }
      
    }
}
