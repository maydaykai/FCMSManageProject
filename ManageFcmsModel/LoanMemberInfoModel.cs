using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanMemberInfoModel
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
        /// 借款人年龄
        /// </summary>
        public int Age
        {
            get;
            set;
        }
        /// <summary>
        /// 婚姻状况：已婚|未婚
        /// </summary>
        public int MaritalStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 性别：男|女
        /// </summary>
        public string Sex
        {
            get;
            set;
        }
        /// <summary>
        /// 籍贯
        /// </summary>
        public int DomicilePlace
        {
            get;
            set;
        }
        /// <summary>
        /// 家庭人数
        /// </summary>
        public int FamilyNum
        {
            get;
            set;
        }
        /// <summary>
        /// 月收入水平
        /// </summary>
        public string MonthlyPay
        {
            get;
            set;
        }
        /// <summary>
        /// 房产： 有|无
        /// </summary>
        public bool HaveHouse
        {
            get;
            set;
        }
        /// <summary>
        /// 车产： 有|无
        /// </summary>
        public bool HaveCar
        {
            get;
            set;
        }
        /// <summary>
        /// 工作年限
        /// </summary>
        public string WorkingLife
        {
            get;
            set;
        }
        /// <summary>
        /// 职业状态： 在职员工|私企老板
        /// </summary>
        public string JobStatus
        {
            get;
            set;
        }

       
    }
}
