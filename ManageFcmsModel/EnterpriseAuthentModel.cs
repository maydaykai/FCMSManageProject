using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class EnterpriseAuthentModel
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
        /// 企业名称
        /// </summary>		
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 营业执照注册号
        /// </summary>		
        public string RegistrationNo { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>		
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 法人代表
        /// </summary>		
        public string LegalName { get; set; }

        /// <summary>
        /// 经办人姓名
        /// </summary>		
        public string OperatorRealName { get; set; }

        /// <summary>
        /// 经办人身份证号码
        /// </summary>		
        public string OperatorIdentityCard { get; set; }

        /// <summary>
        /// 经办人手机号码
        /// </summary>		
        public string OperatorPhone { get; set; }

        /// <summary>
        /// 经营场所_____城市ID
        /// </summary>		
        public int CityID { get; set; }

        /// <summary>
        /// 经营场所详细地址
        /// </summary>		
        public string Address { get; set; }

        /// <summary>
        /// 认证结果：-1-认证不通过 0-未认证 1-认证通过
        /// </summary>		
        public int AuthentResult { get; set; }

        /// <summary>
        /// 认证次数
        /// </summary>		
        public int AuthentNumber { get; set; }

        /// <summary>
        /// 请求报文
        /// </summary>		
        public string RequestXml { get; set; }

        /// <summary>
        /// 响应报文
        /// </summary>		
        public string ResponseXml { get; set; }

        /// <summary>
        /// 审核状态：-1-审核不通过 0-未审核 1-审核通过
        /// </summary>		
        public int ExamStatus { get; set; }

        /// <summary>
        /// 到期时间(认证有效期)
        /// </summary>		
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>		
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }
    }
}
