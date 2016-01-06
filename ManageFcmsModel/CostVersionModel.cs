using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class CostVersionModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>		
        public string VersionName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>		
        public bool EnableStatus { get; set; }
    }
}
