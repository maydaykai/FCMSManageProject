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
    public class MobileUpdateRecordBll
    {
        private MobileUpdateRecordDal _dal = new MobileUpdateRecordDal();
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize,
                                     out int total)
        {
            return _dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MobileUpdateRecordModel getMobileUpdateRecordModel(string strWhere)
        {
            return _dal.getMobileUpdateRecordModel(strWhere);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool updateMobileUpdateRecord(MobileUpdateRecordModel model)
        {
            return _dal.updateMobileUpdateRecord(model);
        }

        /// <summary>
        /// 修改手机号码成功处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool mobileUpdateSuccess(MobileUpdateRecordModel model)
        {
            return _dal.mobileUpdateSuccess(model);
        }
    }
}
