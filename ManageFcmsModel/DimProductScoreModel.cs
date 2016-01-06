using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class DimProductScoreModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 产品类型ID
        /// </summary>		
        public int ProductTypeId { get; set; }

        /// <summary>
        /// 评分项目
        /// </summary>		
        public string ScoreItems { get; set; }

        /// <summary>
        /// 满分分值
        /// </summary>		
        public int FullMarks { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark { get; set; }  
    }
}
