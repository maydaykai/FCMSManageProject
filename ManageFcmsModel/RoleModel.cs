using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //Role
    public class RoleModel
    {

        /// <summary>
        /// 用户组ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>		
        public string Description { get; set; }
    }
}
