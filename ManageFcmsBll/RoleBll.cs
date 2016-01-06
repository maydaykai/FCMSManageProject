using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;
using System.Data;

namespace ManageFcmsBll
{
    public class RoleBll
    {
        private readonly RoleDal _roleDal = new RoleDal();

        /// <summary>
        /// 获取所有已启用的角色ID，角色名称
        /// </summary>
        public DataSet GetRoleList()
        {
            return _roleDal.GetRoleList();
        }

        /// <summary>
        /// 添加角色 角色权限
        /// </summary>
        public bool Add(RoleModel model, string roleRight)
        {
            return _roleDal.Add(model, roleRight);
        }

        /// <summary>
        /// 更新角色信息，同时更新角色权限
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleRight">角色权限字符串</param>
        /// <returns></returns>
        public bool Update(RoleModel model, string roleRight)
        {
            return _roleDal.Update(model, roleRight);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoleModel GetModel(int id)
        {
            return _roleDal.GetModel(id);
        }

        /// <summary>
        /// 获角色分页列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<RoleModel> GetRoleList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return _roleDal.GetRoleList(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }
    }
}
