using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class LoanScoreModel
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
        /// 借款标ID
        /// </summary>		
        public int LoanId
        {
            get;
            set;
        }
        /// <summary>
        /// 评分项目ID
        /// </summary>		
        public int ScoreItemsId
        {
            get;
            set;
        }
        /// <summary>
        /// 评分分值
        /// </summary>		
        public int Score
        {
            get;
            set;
        }
        /// <summary>
        /// 更新时间
        /// </summary>		
        public string UpdateTime
        {
            get;
            set;
        }

    }
}
