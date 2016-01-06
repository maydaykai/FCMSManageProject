using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanApplyModel
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
        public int MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// 会员名
        /// </summary>		
        public string MemberName
        {
            get;
            set;
        }
        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string RealName
        {
            get;
            set;
        }

        /// <summary>
        /// 借款金额
        /// </summary>		
        public decimal LoanAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 借款用途ID
        /// </summary>		
        public int LoanUseId
        {
            get;
            set;
        }

        /// <summary>
        /// 借款用途
        /// </summary>		
        public string LoanUseName
        {
            get;
            set;
        }

        /// <summary>
        /// 产品类型ID
        /// </summary>		
        public int ProductTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 产品类型名
        /// </summary>		
        public string ProductTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 借款利率
        /// </summary>
        public decimal LoanRate
        {
            get;
            set;
        }
        /// <summary>
        /// 借款期限
        /// </summary>		
        public int LoanTerm
        {
            get;
            set;
        }
        /// <summary>
        /// 0： 按天   1：按月 
        /// </summary>		
        public int BorrowMode
        {
            get;
            set;
        }
        /// <summary>
        /// 还款方式
        /// </summary>		
        public int RepaymentMethod
        {
            get;
            set;
        }
        /// <summary>
        /// 还款方式名
        /// </summary>		
        public string RepaymentMethodName
        {
            get;
            set;
        }
        /// <summary>
        /// 所在省份
        /// </summary>		
        public int Province
        {
            get;
            set;
        }
        /// <summary>
        /// 省份名称
        /// </summary>	
        public string ProvinceName
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
        /// 城市名称
        /// </summary>		
        public string CityName
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态
        /// </summary>		
        public int ExamStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 有无担保函
        /// </summary>
        public bool HaveGuaranteeLetter
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
        /// 还款来源
        /// </summary>		
        public string RepaymentSource
        {
            get;
            set;
        }

        /// <summary>
        /// 审核记录 例如：初审通过—Norton—2014-06-17 09:36:46—（审核意见：同意）|复审通过—Norton—2014-06-17 09:36:55—（审核意见：同意）
        /// </summary>		
        public string AuditRecords { get; set; }

        /// <summary>
        /// 借款申请编号
        /// </summary>
        public string LoanNumber { get; set; }


        /// <summary>
        /// 注册手机号码
        /// </summary>
        public string Mobile { get; set; }
        
    }
}
