using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //Right
    public class RightModel
    {
        /// <summary>
        /// 权限ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>		
        public string RightName { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>		
        public bool Visible { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }
    }
}
