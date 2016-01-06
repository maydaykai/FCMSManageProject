using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class AboutUsModel
    {
        /// <summary>
        /// 关于我们文档ID
        /// </summary>		
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 栏目ID
        /// </summary>		
        public int ColumnID
        {
            get;
            set;
        }
        /// <summary>
        /// 关于我们信息标题
        /// </summary>		
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 关于我们信息正文
        /// </summary>		
        public string Content
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
