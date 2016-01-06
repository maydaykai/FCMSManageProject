using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class CashRecordModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 会员ID/用户ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 代付类型：0-会员提现、1-银行卡认证汇款
        /// </summary>
        public int Type { get; set; }

        public string TypeStr
        {
            get { return Type == 0 ? "会员提现" : "银行卡认证汇款"; }
        }

        /// <summary>
        /// 提现方式：0-线上 1-线下
        /// </summary>
        public int CashMode { get; set; }

        /// <summary>
        /// 关联银行卡表ID
        /// </summary>
        public int BankAccountID { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal CashAmount { get; set; }

        /// <summary>
        /// 提现手续费
        /// </summary>
        public decimal CashFee { get; set; }

        /// <summary>
        /// 审核状态：0-初审中 1-复审中 2-初审不通过 3-复审不通过 4-复审通过
        /// </summary>
        public int Status { get; set; }

        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "初审中";
                    case 1:
                        return "复审中";
                    case 2:
                        return "初审不通过";
                    case 3:
                        return "复审不通过";
                    case 4:
                        return "复审通过";
                }
                return "初审中";
            }
        }

        /// <summary>
        /// 提现汇款状态：0-汇款中 1-汇款成功 2-汇款失败
        /// </summary>
        public int CashStatus { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string REQ_SN { get; set; }

        /// <summary>
        /// 请求报文
        /// </summary>
        public string RequestXml { get; set; }

        /// <summary>
        /// 响应报文
        /// </summary>
        public string ResponseXml { get; set; }

        /// <summary>
        /// 审核记录 例如：初审通过—Norton—2014-06-17 09:36:46—（审核意见：同意）|复审通过—Norton—2014-06-17 09:36:55—（审核意见：同意）
        /// </summary>	
        public string AuditRecords { get; set; }

        /// <summary>
        /// 审核备注说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplicationTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 申请理由
        /// </summary>
        public string  ApplyReason { get; set; }

        /// <summary>
        /// 关联银行卡表Type 1:通联；2：连连
        /// </summary>
        public int BankAccountType
        {
            get;
            set;
        }
        /// <summary>
        /// 提现通道 1:通联；2：连连
        /// </summary>
        public int CashPayType
        {
            get;
            set;
        }

        /// <summary>
        /// 预警回访记录
        /// </summary>
        public string WarningRecord { get; set; }

        /// <summary>
        /// 预警状态：0正常 1风险 2存疑 3通过 4拒绝
        /// </summary>
        public int WarningStatus { get; set; }
    }
}
