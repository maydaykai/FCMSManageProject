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
    public class ApplyAbandonLoanBLL
    {
        ApplyAbandonLoanDAL dal = new ApplyAbandonLoanDAL();
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
        /// <summary>
        /// 审核流标申请
        /// </summary>
        /// <param name="model"></param>
        /// <param name="operateID"></param>
        /// <returns></returns>
        public bool auditAbandonLoan(ApplyAbandonLoanModel model)
        {
            return dal.auditAbandonLoan(model);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ApplyAbandonLoanModel getApplyAbandonLoanModel(string strWhere)
        {
            return dal.getApplyAbandonLoanModel(strWhere);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ApplyAbandonLoanModel getApplyAbandonLoanModel(int ID)
        {
            return dal.getApplyAbandonLoanModel("ID=" + ID);
        }
    }
}
