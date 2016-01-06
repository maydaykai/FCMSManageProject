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
    public class AppVersionBll
    {
        private readonly AppVersionDal _dal = new AppVersionDal();

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(AppVersionModel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AppVersionModel GetModel(int ID)
        {
            return _dal.GetModel(ID);
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
    }
}
