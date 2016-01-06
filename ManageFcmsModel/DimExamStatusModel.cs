using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //DimExamStatus
    public class DimExamStatusModel
    {

        /// <summary>
        /// 审核状态ID
        /// </summary>		
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态名称
        /// </summary>		
        public string ExamStatusName
        {
            get;
            set;
        }

    }
}
