using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class Marketing_UserJoinRoleModel
    {

        public int Id { get; set; }
        public int UserInfoId { get; set; }
        public int RoleNameId { get; set; }
        /// <summary>
        /// type=0 用户角色  type=1 指定系统角色
        /// </summary>
        public int Type { get; set; }
    }
}
