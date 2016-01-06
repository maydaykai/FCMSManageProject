using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class IdentityAuthentModel
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
        /// 身份证号码
        /// </summary>		
        public string IdentityCard { get; set; }

        /// <summary>
        /// 认证结果：-1-认证不通过 0-未认证 1-认证通过
        /// </summary>		
        public int AuthentResult { get; set; }

        /// <summary>
        /// AuthentNumber
        /// </summary>		
        public int AuthentNumber { get; set; }

        /// <summary>
        /// 身份证相片(第3方认证接口返回的信息)
        /// </summary>		
        public string IdPhoto { get; set; }

        /// <summary>
        /// 审核状态: -1-审核不通过 0-未审核 1-审核通过
        /// </summary>		
        public int ExamStatus { get; set; }

        /// <summary>
        /// 请求报文
        /// </summary>		
        public string RequestXml { get; set; }

        /// <summary>
        /// 响应报文
        /// </summary>		
        public string ResponseXml { get; set; }

        /// <summary>
        /// 到期时间（认证有效期）
        /// </summary>		
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>		
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 更细时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }
    }
}
