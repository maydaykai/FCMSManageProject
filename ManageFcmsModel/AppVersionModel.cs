using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class AppVersionModel
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
        /// 操作系统
        /// </summary>
        public string OS
        {
            get;
            set;
        }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName
        {
            get;
            set;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get;
            set;
        }
        /// <summary>
        /// 版本编号
        /// </summary>
        public string VersionNum
        {
            get;
            set;
        }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownURL
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
