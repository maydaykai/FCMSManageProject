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
    public class ApplyDeferLoanBLL
    {
        ApplyDeferLoanDAL dal = new ApplyDeferLoanDAL();
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
        /// <summary>
        /// 审核展期申请
        /// </summary>
        /// <param name="model"></param>
        /// <param name="operateID"></param>
        /// <returns></returns>
        public bool auditDeferLoan(ApplyDeferLoanModel model)
        {
            return dal.auditDeferLoan(model);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ApplyDeferLoanModel getApplyDeferLoanModel(string strWhere)
        {
            return dal.getApplyDeferLoanModel(strWhere);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ApplyDeferLoanModel getApplyDeferLoanModel(int ID)
        {
            return dal.getApplyDeferLoanModel("ID=" + ID);
        }
    }
}
