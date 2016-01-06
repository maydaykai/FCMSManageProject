using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class InformationModel
    {
        /// <summary>
        /// 资讯ID
        /// </summary>		
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯栏目ID
        /// </summary>		
        public int SectionID
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯标题
        /// </summary>		
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯内容
        /// </summary>		
        public string Content
        {
            get;
            set;
        }

        public string SummaryCount { get; set; }
        /// <summary>
        /// 审核状态：0-未审核 1-审核通过 2-审核不通过
        /// </summary>		
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// 是否推荐（在资讯管理首页显示）
        /// </summary>		
        public bool Recommend
        {
            get;
            set;
        }
        /// <summary>
        /// NewsImage
        /// </summary>		
        public string NewsImage
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间
        /// </summary>		
        public DateTime PubTime
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

        /// <summary>
        /// 扩展属性，值为xml格式
        /// </summary>
        public string ExtendedContent
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻小类的ID
        /// </summary>
        public int MediaTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻小类的值
        /// </summary>
        public int Image_value
        {
            get;
            set;
        }

        /// <summary>
        /// url
        /// </summary>
        public string url
        {
            get;
            set;
        }
    }
}
