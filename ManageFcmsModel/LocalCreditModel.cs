using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LocalCreditModel
    {
        #region 深户贷基础资料

        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 贷款申请ID
        /// </summary>		
        public int LoanApplyId { get; set; }

        /// <summary>
        /// 贷款编号
        /// </summary>		
        public string LoanNumber { get; set; }

        /// <summary>
        /// 会员账户
        /// </summary>	
        public string MemberName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>	
        public string Sex { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>	
        public string RealName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>	
        public string IdentityCard { get; set; }

        /// <summary>
        /// 学历
        /// </summary>		
        public int Education { get; set; }

        /// <summary>
        /// 社保卡号
        /// </summary>		
        public string SocialCard { get; set; }

        /// <summary>
        /// 住宅地址
        /// </summary>		
        public string Residence { get; set; }

        /// <summary>
        /// 住宅电话
        /// </summary>		
        public string ResidenceTelephone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>		
        public string Mobile { get; set; }

        /// <summary>
        /// 单位性质
        /// </summary>		
        public int CompanyNature { get; set; }

        /// <summary>
        /// 工作单位名称
        /// </summary>		
        public string CompanyName { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>		
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 单位电话
        /// </summary>		
        public string CompanyTelephone { get; set; }

        /// <summary>
        /// 工作职务
        /// </summary>		
        public string Duties { get; set; }

        /// <summary>
        /// 应急联系人1
        /// </summary>		
        public string EmergencyName1 { get; set; }

        /// <summary>
        /// 应急联系人关系1
        /// </summary>		
        public string Relationship1 { get; set; }

        /// <summary>
        /// 应急联系人电话1
        /// </summary>		
        public string ContactNum1 { get; set; }

        /// <summary>
        /// 应急联系人2
        /// </summary>		
        public string EmergencyName2 { get; set; }

        /// <summary>
        /// 应急联系人关系2
        /// </summary>		
        public string Relationship2 { get; set; }

        /// <summary>
        /// 应急联系人电话2
        /// </summary>		
        public string ContactNum2 { get; set; }

        /// <summary>
        /// 应急联系人3
        /// </summary>		
        public string EmergencyName3 { get; set; }

        /// <summary>
        /// 应急联系人关系3
        /// </summary>		
        public string Relationship3 { get; set; }

        /// <summary>
        /// 应急联系人电话3
        /// </summary>		
        public string ContactNum3 { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>		
        public string WifeName { get; set; }

        /// <summary>
        /// 配偶身份证号码
        /// </summary>		
        public string IdCard { get; set; }

        /// <summary>
        /// 配偶移动电话
        /// </summary>		
        public string WifeMobile { get; set; }

        /// <summary>
        /// 配偶月均收入
        /// </summary>		
        public string WifeIncome { get; set; }

        /// <summary>
        /// 配偶单位名称
        /// </summary>		
        public string CompanyName2 { get; set; }

        /// <summary>
        /// 配偶单位地址
        /// </summary>		
        public string CompanyAddress2 { get; set; }

        /// <summary>
        /// 配偶单位电话
        /// </summary>		
        public string CompanyTelephone2 { get; set; }

        /// <summary>
        /// 配偶单位性质
        /// </summary>		
        public int CompanyNature2 { get; set; }

        /// <summary>
        /// 配偶工作职务
        /// </summary>		
        public string WifeDuties { get; set; }

        /// <summary>
        /// 房产权利人
        /// </summary>		
        public string HouseOwner { get; set; }

        /// <summary>
        /// 产权编号
        /// </summary>		
        public string HouseNumber { get; set; }

        /// <summary>
        /// 房产地址
        /// </summary>		
        public string HouseAddress { get; set; }

        /// <summary>
        /// 车辆权利人
        /// </summary>		
        public string CarOwner { get; set; }

        /// <summary>
        /// 车辆号码
        /// </summary>		
        public string CarNumber { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>		
        public string CarBrand { get; set; }


        #endregion

        #region 上传资料

        /// <summary>
        /// 身份证正面图
        /// </summary>		
        public string IDCardAuthen1 { get; set; }

        /// <summary>
        /// 身份证反面图
        /// </summary>		
        public string IDCardAuthen2 { get; set; }

        /// <summary>
        /// 身份证手持图
        /// </summary>		
        public string IDCardAuthen3 { get; set; }

        /// <summary>
        /// 户口本首页
        /// </summary>		
        public string BookAuthen1 { get; set; }

        /// <summary>
        /// 户口本本人页
        /// </summary>		
        public string BookAuthen2 { get; set; }

        /// <summary>
        /// 社保证明
        /// </summary>		
        public string SocialAuthen { get; set; }

        /// <summary>
        /// 工作证明
        /// </summary>		
        public string WorkAuthen { get; set; }

        /// <summary>
        /// 银行卡正面
        /// </summary>		
        public string BankCard1 { get; set; }

        /// <summary>
        /// 银行卡背面
        /// </summary>		
        public string BankCard2 { get; set; }

        /// <summary>
        /// 银行卡流水
        /// </summary>		
        public string BankStreamLine { get; set; }

        /// <summary>
        /// 信用报告,pdf
        /// </summary>		
        public string CreditReport { get; set; }

        /// <summary>
        /// 住址证明
        /// </summary>		
        public string ResidenceAuthen { get; set; }

        /// <summary>
        /// 信用卡
        /// </summary>		
        public string CreditCard { get; set; }
        
        /// <summary>
        /// 信用卡账单
        /// </summary>		
        public string CreditCardBill { get; set; }

        /// <summary>
        /// 学历证明
        /// </summary>		
        public string EducationAuthen { get; set; }

        /// <summary>
        /// 结婚证
        /// </summary>		
        public string MarryAuthen { get; set; }

        /// <summary>
        /// 房产证
        /// </summary>		
        public string HouseAuthen { get; set; }

        /// <summary>
        /// 房产证本人页
        /// </summary>		
        public string HouseAuthenPage { get; set; }

        #endregion

        #region 贷款申请信息

        public int MemberId { get; set; }

        public int LoanUseId { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal ExamLoanAmount { get; set; }

        /// <summary>
        /// 利率
        /// </summary>
        public decimal LoanRate { get; set; }

        /// <summary>
        /// 借款期限
        /// </summary>
        public int LoanTerm { get; set; }

        public int RepaymentMethod { get; set; }

        public string LoanUseName { get; set; }

        public int BorrowMode { get; set; }

        public int ExamStatus { get; set; }

        public string UseDescription { get; set; }

        public string AuditRecords { get; set; }

        public int LoanId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        #endregion

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
