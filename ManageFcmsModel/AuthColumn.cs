using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class AuthColumn
    {
        /// <summary>
        /// 认证字段枚举
        /// </summary>
        public enum AuthColumnEnum
        {
            [AuthAttruate(Title = "身份证")]
            AIDCard = 0,

            [AuthAttruate(Title = "户口本")]
            ABooklet = 1,

            [AuthAttruate(Title = "结婚证/离婚证")]
            AMarrCert = 2,

            [AuthAttruate(Title = "水电物业费等清单")]
            APropertyFee = 3,

            [AuthAttruate(Title = "个人征信报告")]
            APersonCreditReport = 4,

            [AuthAttruate(Title = "房产证或按揭合同")]
            AHouseCert = 5,

            [AuthAttruate(Title = "营业执照")]
            AtradingCert = 6,

            [AuthAttruate(Title = "组织机构代码证")]
            AOrgCode = 7,

            [AuthAttruate(Title = "税务登记证")]
            ATaxCert = 8,

            [AuthAttruate(Title = "开户许可证")]
            AAccountLicence = 9,

            [AuthAttruate(Title = "企业章程")]
            AEnterpriseRule = 10,

            [AuthAttruate(Title = "验资报告")]
            AVerificationReport = 11,

            [AuthAttruate(Title = "企业征信报告")]
            AEnterpriseCreditReport = 12,

            [AuthAttruate(Title = "全体股东决议书")]
            AShareholdersResolution = 13,

            [AuthAttruate(Title = "销售合同、单据")]
            ASalesContract = 14,

            [AuthAttruate(Title = "完税证明")]
            ADutyPaidProof = 15,

            [AuthAttruate(Title = "员工工资单")]
            AEmployeePayroll = 16,

            [AuthAttruate(Title = "购货合同、单据")]
            APurchaseContract = 17,

            [AuthAttruate(Title = "公司银行流水记录")]
            ABankWater = 18,

            [AuthAttruate(Title = "财务审计报告")]
            AFinancialReport = 19,

            [AuthAttruate(Title = "公司资产证明")]
            AAssetsCert = 20,

            [AuthAttruate(Title = "关联企业资料")]
            AEnterpriseInfo = 21
        }
        /// <summary>
        /// 获取所有费用类型
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetAuthColumnList()
        {
            var feeTypeDic = new Dictionary<int, string>();

            AddItem(feeTypeDic, AuthColumnEnum.AIDCard); //身份证
            AddItem(feeTypeDic, AuthColumnEnum.ABooklet); //户口本
            AddItem(feeTypeDic, AuthColumnEnum.AMarrCert); //结婚证/离婚证
            AddItem(feeTypeDic, AuthColumnEnum.APropertyFee); //水电物业费等清单
            AddItem(feeTypeDic, AuthColumnEnum.APersonCreditReport); //个人征信报告
            AddItem(feeTypeDic, AuthColumnEnum.AHouseCert); //房产证或按揭合同
            AddItem(feeTypeDic, AuthColumnEnum.AtradingCert); //营业执照
            AddItem(feeTypeDic, AuthColumnEnum.AOrgCode); //组织机构代码证
            AddItem(feeTypeDic, AuthColumnEnum.ATaxCert); //税务登记证
            AddItem(feeTypeDic, AuthColumnEnum.AAccountLicence); //开户许可证
            AddItem(feeTypeDic, AuthColumnEnum.AEnterpriseRule); //企业章程
            AddItem(feeTypeDic, AuthColumnEnum.AVerificationReport); //验资报告
            AddItem(feeTypeDic, AuthColumnEnum.AEnterpriseCreditReport); //企业征信报告
            AddItem(feeTypeDic, AuthColumnEnum.AShareholdersResolution); //全体股东决议书
            AddItem(feeTypeDic, AuthColumnEnum.ASalesContract); //销售合同、单据
            AddItem(feeTypeDic, AuthColumnEnum.ADutyPaidProof); //完税证明
            AddItem(feeTypeDic, AuthColumnEnum.AEmployeePayroll); //员工工资单
            AddItem(feeTypeDic, AuthColumnEnum.APurchaseContract); //购货合同、单据
            AddItem(feeTypeDic, AuthColumnEnum.ABankWater); //公司银行流水记录
            AddItem(feeTypeDic, AuthColumnEnum.AFinancialReport); //财务审计报告
            AddItem(feeTypeDic, AuthColumnEnum.AAssetsCert); //公司资产证明
            AddItem(feeTypeDic, AuthColumnEnum.AEnterpriseInfo); //关联企业资料

            return feeTypeDic;
        }
        /// <summary>
        /// 添加Dictionary的项
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="column"></param>
        private static void AddItem(Dictionary<int, string> dic, AuthColumnEnum column)
        {
            dic.Add((int)column, GetAttrTitle(column));
        }
        /// <summary>
        /// 获取认证字段名称
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private static string GetAttrTitle(AuthColumnEnum column)
        {
            Type enumType = column.GetType();

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("不是枚举类型");
            }

            string enumItem = Enum.GetName(enumType, (int)column);
            if (enumItem == null)
            {
                return string.Empty;
            }

            object[] objs = enumType.GetField(enumItem).GetCustomAttributes(typeof(AuthAttruate), false);
            if (objs.Length > 0)
            {
                AuthAttruate attr = objs[0] as AuthAttruate;
                return attr.Title;
            }
            return "未知";
        }
        /// <summary>
        /// 认证类型属性
        /// </summary>
        public class AuthAttruate : Attribute
        {
            public string Title;
        }
    }
}
