using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{

    public class Marketing_RoleBLL
    {
        private readonly Marketing_RoleDAL dal = new Marketing_RoleDAL();

        public DataSet GetRoleList()
        {
            return dal.GetRoleList();
        }

        public int UserJoinRoleSave(string RoleXml, int UserInfoId, int type)
        {
            return dal.UserJoinRoleSave(RoleXml, UserInfoId, type);
        }

        public List<Marketing_RoleNameModel> GetRoleListToPage(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return dal.GetRoleListToPage(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }

        /// <summary>
        /// 基本定义功能分页
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<Marketing_FunctionOperationModel> GetFunctionOperationPageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return dal.GetFunctionOperationPageList(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }

        /// <summary>
        /// 权限分页
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<Marketing_CompetenceModel> GetCompetencePageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return dal.GetCompetencePageList(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddFunctionOperation(Marketing_FunctionOperationModel model)
        {
            return dal.AddFunctionOperation(model);
        }

        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFunctionOperation(Marketing_FunctionOperationModel model)
        {
            return dal.UpdateFunctionOperation(model);
        }

        public Marketing_FunctionOperationModel GetFunctionOperationById(int Id)
        {
            return dal.GetFunctionOperationById(Id);
        }

        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public DataSet GetColumnlList(int visible)
        {
            return dal.GetColumnlList(visible);
        }

        /// <summary>
        /// 组长分配角色列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<Marketing_RoleNameModel> GetRoleNamePageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            List<Marketing_RoleNameModel> list = new List<Marketing_RoleNameModel>();
            return dal.GetRoleNamePageList(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }

        /// <summary>
        /// 分配人员列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetDistributionTable(int Id)
        {
            return dal.GetDistributionTable(Id);
        }
        /// <summary>
        /// 查询当前人员关系
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet SetDistributionTable(int roleId)
        {
            return dal.SetDistributionTable(roleId);
        }

        /// <summary>
        /// 分配人员关系
        /// </summary>
        /// <param name="RoleXml"></param>
        /// <param name="UserInfoId"></param>
        /// <returns></returns>
        public int Ex_PersonToRoleSave(string RoleXml, int UserInfoId)
        {
            return dal.Ex_PersonToRoleSave(RoleXml, UserInfoId);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddRole(Marketing_RoleNameModel model)
        {
            return dal.AddRole(model);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateRole(Marketing_RoleNameModel model)
        {
            return dal.UpdateRole(model);
        }

        /// <summary>
        /// 根据Id查询角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Marketing_RoleNameModel GetRoleNameModelById(int Id)
        {
            return dal.GetRoleNameModelById(Id);
        }

        /// <summary>
        /// 同步营销人员
        /// </summary>
        /// <param name="mouth"></param>
        public void SynchronizeEx_Person(string mouth)
        {
             dal.SynchronizeEx_Person(mouth);
        }

        /// <summary>
        /// 系统角色用户
        /// </summary>
        /// <returns></returns>
        public DataSet GetSystemRoleList()
        {
            return dal.GetSystemRoleList();
        }

        /// <summary>
        /// 查询角色表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetRoleListByUserInfoId(int userId, int type)
        {
            return dal.GetRoleListByUserInfoId(userId, type);
        }

        public DataSet GetMarkingRoleListByUserInfoId(int userId, int type)
        {
            return dal.GetMarkingRoleListByUserInfoId(userId, type);
        }
    }
}
