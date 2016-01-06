using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class FocusFigureModel
    {
        /// <summary>
        /// 首页焦点图ID
        /// </summary>		
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 焦点图标题
        /// </summary>		
        public string Title
        {
            get;
            set;
        }  
        /// <summary>
        /// 首页焦点图链接URL
        /// </summary>		
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 首页焦点图大图
        /// </summary>		
        public string LargePicture
        {
            get;
            set;
        }
        /// <summary>
        /// 首页焦点图小图
        /// </summary>		
        public string SmallPicture
        {
            get;
            set;
        }
        /// <summary>
        /// 状态：0-不启用 1-启用
        /// </summary>		
        public int Status
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

        /// <summary>
        /// 排序的值
        /// </summary>
        public int ShowDesc
        {
            get;
            set;
        }
    }
}
