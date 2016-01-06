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
    //AuditHistory
    public partial class AuditHistoryBll
    {

        private readonly AuditHistoryDal _dal = new AuditHistoryDal();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AuditHistoryModel model)
        {
            return _dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AuditHistoryModel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AuditHistoryModel GetAuditHistoryModel(int ID)
        {

            return _dal.GetAuditHistoryModel(ID);
        }


        public AuditHistoryModel GetAuditHistoryModelByLoanId(int loadnId)
        {
            return _dal.GetAuditHistoryModelByLoanId(loadnId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return _dal.GetList(strWhere);
        }

    }
}
