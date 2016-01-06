using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class Marketing_MoveModel
    {

        public int Id { get; set; }
        /// <summary>
        /// 异动类型 1 删除 2.离职 3.重新分配部门
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 当前操作的用户Id
        /// </summary>
        public int FirstMemberId { get; set; }

        /// <summary>
        /// 变更后的Id 
        /// </summary>
        public int SecondMemberId { get; set; }

        /// <summary>
        /// 上一级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Leave { get; set; }
    }
}
