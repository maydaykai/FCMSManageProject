using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class FcmsUserBll
    {
        private readonly FcmsUserDal _fcmsUserDal = new FcmsUserDal();
        /// <summary>
        /// 判断用户名是否已存在
        /// </summary>
        public bool Exists(string userName)
        {
            return _fcmsUserDal.Exists(userName);
        }

        //登陆验证
        public bool LoginValidate(ref FcmsUserModel fcmsUserModel)
        {
            return _fcmsUserDal.LoginValidate(ref fcmsUserModel);
        }

        /// <summary>
        /// 获取所有角色是担保公司的数据(担保公司角色ID目前是8，如有变动请修改)
        /// </summary>
        public DataSet GetGcList()
        {
            return _fcmsUserDal.GetGcList();
        }
        /// <summary>
        /// 获取借款用途数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimLoanUseList()
        {
            return _fcmsUserDal.GetDimLoanUseList();
        }

        /// <summary>
        /// 学生贷借款用途
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimLoanUseList_StudenInfo()
        {
            return _fcmsUserDal.GetDimLoanUseList_StudenInfo();
        }

        /// <summary>
        /// 获取借款标类型数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimLoanScaleTypeList()
        {
            return _fcmsUserDal.GetDimLoanScaleTypeList();
        }

        /// <summary>
        /// 获取还款方式
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimRepaymentMethodList()
        {
            return _fcmsUserDal.GetDimRepaymentMethodList();
        }

        /// <summary>
        /// 增加
        /// </summary>
        public bool Add(FcmsUserModel model)
        {
            return _fcmsUserDal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(FcmsUserModel model)
        {
            return _fcmsUserDal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FcmsUserModel GetModel(int id)
        {
            return _fcmsUserDal.GetModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        //public DataTable GetUserListForPager(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        //{
        //    return _fcmsUserDal.GetUserListForPager(fields, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        //}

        /// <returns></returns>
        public List<FcmsUserModel> GetFcmsUserList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return _fcmsUserDal.GetFcmsUserList(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }

        /// <summary>
        /// 获取产品类型数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetDimProductTypeList()
        {
            return _fcmsUserDal.GetDimProductTypeList();
        }
    }
}
