using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //RoleRights
    public class RoleRightModel
    {

        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>		
        public int RoleID { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>		
        public int ColumnID { get; set; }

        /// <summary>
        /// RightID
        /// </summary>		
        public int RightID { get; set; }

    }
}
