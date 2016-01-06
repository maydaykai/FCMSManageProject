using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class MobilePushModel
    {
        /// <summary>
        /// 推送消息ID
        /// </summary>		
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 推送消息类型：1-公告新闻 2-最新动态 3-行业资讯  4-推荐标
        /// </summary>		
        public int MessageType
        {
            get;
            set;
        }
        /// <summary>
        /// 推送标题
        /// </summary>		
        public string PushTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 推送内容
        /// </summary>		
        public string PushContent
        {
            get;
            set;
        }
        /// <summary>
        /// 推送的额外信息(json格式数据)
        /// </summary>		
        public int EventID
        {
            get;
            set;
        }
        /// <summary>
        /// 推送状态：0-未推送 1-已推送
        /// </summary>		
        public bool PushStatus
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
    }
}
