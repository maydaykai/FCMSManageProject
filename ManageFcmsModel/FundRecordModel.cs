using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class FundRecordModel
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
		/// 收款方用户ID
        /// </summary>
        public int PayeeMemberID
        {
            get;
            set;
        }        
		/// <summary>
		/// 出款方用户ID
        /// </summary>
        public int PartyMemberID
        {
            get;
            set;
        }        
		/// <summary>
		/// 费用类型:
         //0=充值
         //1=借款
         //2=竞标
         //3=提现
         //4=本金
         //5=返回投标金额
         //6=担保费
         //7=借款利息
         //8=逾期利息
         //9=逾期滞纳金
         //10=提前还款违约金
         //11=投资人居间服务费
         //12=借款人居间服务费
         //13=会员充值手续费
         //14=会员提现手续费
         //15=用户充值手续费
         //16=用户提现手续费
         //17=银行卡认证汇款
         //18=银行结算利息
         //19=发标保证金
         //20=悔标违约金
         //21=退还担保费
         //22=退还借款人居间服务费
        //23=返回悔标违约金
        /// </summary>
        public int FeeType
        {
            get;
            set;
        }        
		/// <summary>
		/// 金额
        /// </summary>
        public decimal Amount
        {
            get;
            set;
        }        
		/// <summary>
		/// 收款方账户可用余额
        /// </summary>
        public decimal PayeeBalance
        {
            get;
            set;
        }        
		/// <summary>
		/// 出款方账户可用余额
        /// </summary>
        public decimal PartyBalance
        {
            get;
            set;
        }        
		/// <summary>
		/// 状态：1：正常  2： 冻结  3：作废
        /// </summary>
        public int Status
        {
            get;
            set;
        }        
		/// <summary>
		/// 借款ID,跟借款标无关联的默认为0
        /// </summary>
        public int LoanID
        {
            get;
            set;
        }        
		/// <summary>
		/// 关联ID，用于关联其他资金变动记录
        /// </summary>
        public int RelationID
        {
            get;
            set;
        }        
		/// <summary>
		/// 资金变动描述说明
        /// </summary>
        public string Description
        {
            get;
            set;
        }        
		/// <summary>
		/// 记录时间
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
