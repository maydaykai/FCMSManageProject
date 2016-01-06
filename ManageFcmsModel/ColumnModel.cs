using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //Column
    public class ColumnModel
    {
        /// <summary>
        /// 栏目编号
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>		
        public string LinkUrl { get; set; }

        /// <summary>
        /// 栏目图标
        /// </summary>		
        public string ICon { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>		
        public int ParentID { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>		
        public int Sort { get; set; }

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

        /// <summary>
        /// 描述
        /// </summary>		
        public string Description { get; set; }
    }
}
