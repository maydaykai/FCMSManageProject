using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanRapidModel
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
        /// 姓名
        /// </summary>		
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 手机号码
        /// </summary>		
        public string Phone
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
        public int LoanUseID
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
        public int LoanMode
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
        public int City
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
        /// Status
        /// </summary>		
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// Memo
        /// </summary>		
        public string Describe
        {
            get;
            set;
        }
        /// <summary>
        /// 借款用途ID
        /// </summary>		
        public string RealName
        {
            get;
            set;
        }
    }
}
