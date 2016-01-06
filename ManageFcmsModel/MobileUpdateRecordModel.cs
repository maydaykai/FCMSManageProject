using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class MobileUpdateRecordModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 0 能接收手机短信；1 不能接收手机短信
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 修改前手机号码
        /// </summary>
        public string OldMobile { get; set; }

        /// <summary>
        /// 修改后新手机号码
        /// </summary>
        public string NewMobile { get; set; }

        /// <summary>
        /// 申诉状态        -1  审核/验证失败；0 验证身份成功；1 验证旧手机成功/提交申诉成功；2 验证新手机成功/审核成功；
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 申诉说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 审核记录
        /// </summary>
        public string AuditRecords { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
