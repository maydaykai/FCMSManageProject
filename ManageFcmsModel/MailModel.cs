using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class MailModel
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
        /// 发送人ID
        /// </summary>
        public int SendUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人ID
        /// </summary>
        public int ReceiveUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 阅读状态 0为未读 1为已读
        /// </summary>
        public bool ReadStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string MailContent
        {
            get;
            set;
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get;
            set;
        }
    }
}
