using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class Marketing_RoleNameModel
    {
        //营销报表角色
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        public string RoleCode
        {
            get;
            set;
        }

        public int PartentID { get; set; }
        public int Leave { get; set; }

        public string PName
        {
            get;
            set;
        }
    }
}
