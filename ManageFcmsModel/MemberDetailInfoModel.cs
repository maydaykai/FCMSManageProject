using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class MemberDetailInfoModel
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
        /// 最高学历
        /// </summary>		
        public int HighestDegree
        {
            get;
            set;
        }
        /// <summary>
        /// 户口所在地
        /// </summary>		
        public int DomicilePlace
        {
            get;
            set;
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>		
        public int MaritalStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 子女
        /// </summary>		
        public bool Children
        {
            get;
            set;
        }
        /// <summary>
        /// 房子
        /// </summary>		
        public bool House
        {
            get;
            set;
        }
        /// <summary>
        /// 房贷
        /// </summary>		
        public bool HouseLoan
        {
            get;
            set;
        }
        /// <summary>
        /// 车
        /// </summary>		
        public bool Car
        {
            get;
            set;
        }
        /// <summary>
        /// 车贷
        /// </summary>		
        public bool CarLoan
        {
            get;
            set;
        }
        /// <summary>
        /// 籍贯
        /// </summary>		
        public int NativePlace
        {
            get;
            set;
        }
        /// <summary>
        /// 职业
        /// </summary>		
        public int JobType
        {
            get;
            set;
        }
        /// <summary>
        /// 工作状况
        /// </summary>		
        public int JobStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 月收入
        /// </summary>		
        public int MonthlyIncome
        {
            get;
            set;
        }
        /// <summary>
        /// 公司名称
        /// </summary>		
        public string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 工作城市
        /// </summary>		
        public int WorkCity
        {
            get;
            set;
        }
        /// <summary>
        /// 公司类别
        /// </summary>		
        public int CompanyCategory
        {
            get;
            set;
        }
        /// <summary>
        /// 公司规模
        /// </summary>		
        public int CompanySize
        {
            get;
            set;
        }
        /// <summary>
        /// 现公司工作年限
        /// </summary>		
        public int WorkTerm
        {
            get;
            set;
        }
        /// <summary>
        /// 公司电话
        /// </summary>		
        public string CompanyPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 公司地址
        /// </summary>		
        public string CompanyAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 直系家属联系人姓名
        /// </summary>		
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 与直系家属联系人关系
        /// </summary>		
        public string ContactRelation
        {
            get;
            set;
        }
        /// <summary>
        /// 直系家属联系人电话
        /// </summary>		
        public string ContactPhone
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
    }
}
