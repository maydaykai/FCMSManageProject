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
    public class EnterpriseAuthentBll
    {
        private readonly EnterpriseAuthentDal _enterpriseAuthentDal = new EnterpriseAuthentDal();

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(EnterpriseAuthentModel model)
        {
            return _enterpriseAuthentDal.Update(model);
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _enterpriseAuthentDal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out  total);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EnterpriseAuthentModel GetModel(int id)
        {
            return _enterpriseAuthentDal.GetModel(id);
        }
    }
}
