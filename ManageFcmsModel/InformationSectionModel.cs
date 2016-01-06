using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class InformationSectionModel
    {
        /// <summary>
        /// 资讯栏目ID
        /// </summary>		
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯栏目名称
        /// </summary>		
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯栏目父级ID
        /// </summary>		
        public int ParentID
        {
            get;
            set;
        }        
    }
}
