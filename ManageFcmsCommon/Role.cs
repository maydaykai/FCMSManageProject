using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsCommon
{
    public class Role
    {
        /// <summary>
        /// 判断是否包含权限
        /// </summary>
        /// <param name="roleId"></param>用户权限
        /// <param name="role"></param>要判断的权限
        /// <returns></returns>
        public static bool IsRole(string roleId, string role)
        {
            return ((',' + roleId + ',').IndexOf(',' + role + ',') >= 0);
        }

        /// <summary>
        /// 判断是否包含权限
        /// </summary>
        /// <param name="role"></param>要判断的权限
        /// <returns></returns>
        public static bool IsRoleNew(string rightarray, string role)
        {
            return (rightarray.IndexOf("|" + role + "|", StringComparison.Ordinal) >= 0);
        }

        public static string ToQuery(string roleId)
        {
            string query = "";
            query = "(";

            while ((roleId).IndexOf(',') > 0)
            {
                query += (roleId).Substring(0, (roleId ).IndexOf(',')) + ",";
                roleId = roleId.Substring(roleId.IndexOf(',') + 1,roleId.Length - roleId.IndexOf(',') - 1);
            }
            query += roleId;
            query += ")";
            return query;
        }

    }
}
