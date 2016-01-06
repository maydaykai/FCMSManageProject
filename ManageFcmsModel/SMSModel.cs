using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class SMSModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>		
        public string Mobile { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>		
        public string SmsContent { get; set; }

        /// <summary>
        /// 需要发送的时间
        /// </summary>		
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 发送的次数（短信发送失败后，允许5次发送）
        /// </summary>		
        public int SendTimes { get; set; }

        /// <summary>
        /// 短信发送级别（级别为1-5、数据越大，级别越高，需要优先发送）
        /// </summary>		
        public int SendLevel { get; set; }

        /// <summary>
        /// 发送状态（0=待发送、1=发送成功、-1103=发送失败）
        /// </summary>		
        public int SendStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>		
        public DateTime UpdateTime { get; set; }
    }
}
