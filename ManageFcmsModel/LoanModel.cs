using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanModel
    {
        /// <summary>
        /// 借款标ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 借款人会员ID
        /// </summary>
        public int MemberID
        {
            get;
            set;
        }
        /// <summary>
        /// 借款标编号
        /// </summary>
        public string LoanNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 借款用途ID，关联DimLoanUse表
        /// </summary>
        public int DimLoanUseID
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
        /// 借款年利率
        /// </summary>
        public decimal LoanRate
        {
            get;
            set;
        }
        /// <summary>
        /// 即将发布利率
        /// </summary>
        public decimal ReleasedRate
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
        /// 还款方式
        /// </summary>
        public int RepaymentMethod
        {
            get;
            set;
        }
        /// <summary>
        /// 借款期限方式：0：按天借款 1：按月借款
        /// </summary>
        public int BorrowMode
        {
            get;
            set;
        }
        /// <summary>
        /// 0：线上 1：线下
        /// </summary>
        public int TradeType
        {
            get;
            set;
        }
        /// <summary>
        /// 最小投标金额
        /// </summary>
        public decimal BidAmountMin
        {
            get;
            set;
        }
        /// <summary>
        /// 最大投标金额
        /// </summary>
        public decimal BidAmountMax
        {
            get;
            set;
        }
        /// <summary>
        /// 竞标开始时间
        /// </summary>
        public DateTime BidStratTime
        {
            get;
            set;
        }
        /// <summary>
        /// 竞标结束时间
        /// </summary>
        public DateTime BidEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 发标保证金
        /// </summary>
        public decimal Bond
        {
            get;
            set;
        }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID
        {
            get;
            set;
        }
        /// <summary>
        /// 借款标审核状态
        /// </summary>
        public int ExamStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 借款标审核状态
        /// </summary>
        public string ExamStatusName
        {
            get;
            set;
        }
        /// <summary>
        /// 竞标进度（%）
        /// </summary>
        public decimal BiddingProcess
        {
            get;
            set;
        }
        /// <summary>
        /// 投标笔数
        /// </summary>
        public int BidCount
        {
            get;
            set;
        }
        /// <summary>
        /// 担保公司ID
        /// </summary>
        public int GuaranteeID
        {
            get;
            set;
        }
        /// <summary>
        /// 平台复审时间(放款日)
        /// </summary>
        public DateTime ReviewTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最近一次还款时间
        /// </summary>
        public DateTime RepaymentLastTime
        {
            get;
            set;
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 满标时间
        /// </summary>
        public DateTime FullScaleTime
        {
            get;
            set;
        }
        /// <summary>
        /// 核保审批时间
        /// </summary>
        public DateTime UnderTime
        {
            get;
            set;
        }
        /// <summary>
        /// 合同生成时间
        /// </summary>
        public DateTime ContractGenerTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否收取担保费
        /// </summary>
        public bool NeedGuarantee
        {
            get;
            set;
        }
        /// <summary>
        /// 担保费费率
        /// </summary>
        public decimal GuaranteeFee
        {
            get;
            set;
        }
        /// <summary>
        /// 是否收取借款人居间服务费
        /// </summary>
        public bool NeedLoanCharges
        {
            get;
            set;
        }
        /// <summary>
        /// 借款人居间服务费费率
        /// </summary>
        public decimal LoanServiceCharges
        {
            get;
            set;
        }
        /// <summary>
        /// 是否收取投资人居间服务费
        /// </summary>
        public bool NeedBidCharges
        {
            get;
            set;
        }
        /// <summary>
        /// 投资人居间服务费费率
        /// </summary>
        public decimal BidServiceCharges
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
        /// 借款人
        /// </summary>
        public String MemberName
        {
            get;
            set;
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public String RealName
        {
            get;
            set;
        }

        /// <summary>
        /// 贷款用途
        /// </summary>
        public String LoanUseName
        {
            get;
            set;
        }

        /// <summary>
        /// 省ID
        /// </summary>
        public int ProvinceID
        {
            get;
            set;
        }

        /// <summary>
        /// 会员注册时间
        /// </summary>
        public DateTime RegTime
        {
            get;
            set;
        }

        /// <summary>
        /// 居住城市
        /// </summary>
        public String City
        {
            get;
            set;
        }

        /// <summary>
        /// 籍贯
        /// </summary>
        public String Province
        {
            get;
            set;
        }

        /// <summary>
        /// 借款期限名称
        /// </summary>
        public String BorrowModeName
        {
            get;
            set;
        }
        public String BorrowModeStr
        {
            get
            {
                switch (BorrowMode)
                {
                    case 0:
                        return "天";
                    case 1:
                        return "个月";
                }
                return "天";
            }
        }
        /// <summary>
        /// 交易类型
        /// </summary>
        public String TradeTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 还款方式名称
        /// </summary>
        public String RepaymentMethodName
        {
            get;
            set;
        }

        /// <summary>
        /// 即将发布标志 0:即将发布 1：正在招标
        /// </summary>
        public int SoonStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 担保公司名称
        /// </summary>
        public string GuaranteeName
        {
            get;
            set;
        }

        /// <summary>
        /// LoanDescribe
        /// </summary>		
        public string LoanDescribe
        {
            get;
            set;
        }
        /// <summary>
        /// 借款期限
        /// </summary>
        public string LoanTermInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 时间戳：用于更新冲突
        /// </summary>		
        public long TimeStamp
        {
            get;
            set;
        }        
        /// <summary>
        /// 系统默认最低投资金额
        /// </summary>
        public decimal MinInvestment
        {
            get;
            set;
        }
        /// <summary>
        /// 系统默认最高投资金额
        /// </summary>
        public decimal MaxInvestment
        {
            get;
            set;
        }
        /// <summary>
        /// 自动投标比例%
        /// </summary>
        public decimal AutoBidScale
        {
            get;
            set;
        }
        /// <summary>
        /// 借款标题
        /// </summary>
        public string LoanTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 借款标类型
        /// </summary>
        public int LoanTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 借款标类型名称
        /// </summary>
        public string LoanScaleType
        {
            get;
            set;
        }
        /// <summary>
        /// 认证状态
        /// </summary>
        public string AuthStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 自动还款开关
        /// </summary>
        public bool SwitchAutoRepayment
        {
            get;
            set;
        }
        /// <summary>
        /// 生成逾期管理费开关
        /// </summary>
        public bool SwitchBuildOverdueFee
        {
            get;
            set;
        }
        /// <summary>
        /// 自动流标开关
        /// </summary>
        public bool SwitchAutoPass
        {
            get;
            set;
        }
        /// <summary>
        /// 最后还款时间
        /// </summary>
        public DateTime EndRePayTime { get; set; }

        /// <summary>
        /// 借款人电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 信用评分
        /// </summary>
        public int SumScore { get; set; }

        /// <summary>
        /// 信用等级
        /// </summary>
        public string ScoreLevel { get; set; }

        /// <summary>
        /// 分支机构
        /// </summary>
        public string Agency { get; set; }

        /// <summary>
        /// 项目负责人一
        /// </summary>
        public string LinkmanOne { get; set; }

        /// <summary>
        /// 项目负责人二
        /// </summary>
        public string LinkmanTwo { get; set; }

        /// <summary>
        /// 电话一
        /// </summary>
        public string TelOne { get; set; }

        /// <summary>
        /// 电话二
        /// </summary>
        public string TelTwo { get; set; }


        #region 调查信息

        /// <summary>
        /// 月收入
        /// </summary>
        public string MonthlyProfit { get; set; }

        /// <summary>
        /// 负债总额
        /// </summary>
        public string LiabilitiesAmount { get; set; }

        /// <summary>
        /// 负债比
        /// </summary>
        public string LiabilitiesRatio { get; set; }

        /// <summary>
        /// 月可支配收入
        /// </summary>
        public string MonthlyControlIncome { get; set; }

        /// <summary>
        /// 是否优质职业
        /// </summary>
        public int QualityProfessional { get; set; }

        /// <summary>
        /// 是否有房产
        /// </summary>
        public int HouseProperty { get; set; }

        /// <summary>
        /// 诉讼查询
        /// </summary>
        public string LitigationSeach { get; set; }

        /// <summary>
        /// 社保查询
        /// </summary>
        public string SocialSecuritySeach { get; set; }

        /// <summary>
        /// 信用记录查询
        /// </summary>
        public string CreditRecordSeach { get; set; }

        /// <summary>
        /// 联系人情况说明
        /// </summary>
        public string ContactSituation { get; set; }

        /// <summary>
        /// 其他情况说明
        /// </summary>
        public string OtherSituation { get; set; }

        #endregion
    }
}
