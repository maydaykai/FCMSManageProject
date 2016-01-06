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
    public class IdentityAuthentBll
    {
        private readonly IdentityAuthentDal _identityAuthentDal = new IdentityAuthentDal();

        /// <summary>
        /// 根据会员ID判断其是否通过实名认证
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool AuthentExists(int memberId)
        {
            return _identityAuthentDal.AuthentExists(memberId);
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        {
            return _identityAuthentDal.GetPageList(fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public IdentityAuthentModel GetModel(int id)
        {
            return _identityAuthentDal.GetModel(id);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(IdentityAuthentModel model)
        {
            return _identityAuthentDal.Update(model);
        }

        /// <summary>
        /// 审核不通过
        /// </summary>
        public bool AuditFail(IdentityAuthentModel model)
        {
            return _identityAuthentDal.AuditFail(model);
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        public bool AuditSuccess(IdentityAuthentModel model)
        {
            return _identityAuthentDal.AuditSuccess(model);
        }
    }
}
