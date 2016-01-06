using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class BranchCompanyBll
    {
        private readonly BranchCompanyDal _dal = new BranchCompanyDal();
        #region 分公司管理
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddBranchCompany(BranchCompanyModel model)
        {
            return _dal.AddBranchCompany(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBranchCompany(BranchCompanyModel model)
        {
            return _dal.UpdateBranchCompany(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BranchCompanyModel GetBranchCompanyModel(int id)
        {
            return _dal.GetBranchCompanyModel("ID=" + id);
        }

        /// <summary>
        /// 获取所有分公司
        /// </summary>
        /// <returns></returns>
        public DataSet GetBranchCompanyList()
        {
            return _dal.GetBranchCompanyList();
        }
        #endregion
        #region 分公司账号

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BranchCompanyMemberModel GetBranchCompanyMemberModel(int memberId)
        {
            return _dal.GetBranchCompanyMemberModel("MemberID=" + memberId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddBranchCompanyMember(BranchCompanyMemberModel model)
        {
            return _dal.AddBranchCompanyMember(model);
        }

        /// <summary>
        /// 获取锁定金额(属于分公司的账号)
        /// </summary>
        /// <returns></returns>
        public decimal GetBranchCompanyLockAmount(int MemberID)
        {
            return _dal.GetBranchCompanyLockAmount(MemberID);
        }

        #endregion
    }
}
