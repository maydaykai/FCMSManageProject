using ManageFcmsModel;
using ManageFcmsDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace ManageFcmsBll
{
    public class LocalCreditBll
    {
        LocalCreditDal _dal = new LocalCreditDal();
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LocalCreditModel GetLocailCreditModel(int id)
        {
            return _dal.GetLocailCreditModel(id);
        }

        /// <summary>
        /// 获取借款申请列表分页
        /// </summary>
        public IList<LocalCreditModel> GetPagedLoanApplyList(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            return _dal.GetPagedLoanApplyList(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }

        public DataTable GetPagedLoanApplyList(string where)
        {
            return _dal.GetPagedLoanApplyList(where);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(LoanApplyModel model)
        {
            return _dal.UpdateStatus(model);
        }

        /// <summary>
        /// 更新调查信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateInvestigationInfo(LocalCreditModel model)
        {
            return _dal.UpdateInvestigationInfo(model);
        }

         /// <summary>
        /// 得到一个申请贷款对象
        /// </summary>
        public LocalCreditApplyModel GetApplyByID(int applyID)
        {
            return _dal.GetApplyByID(applyID);
        }

         /// <summary>
        /// 编辑借款信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModLocalCreditApplyInfo(LocalCreditApplyModel model)
        {
            return _dal.ModLocalCreditApplyInfo(model);
        }

        /// <summary>
        /// 通过贷款ID找到申请ID
        /// </summary>
        /// <returns></returns>
        public int GetApplyidByLoanId(int LoanID)
        {
            return _dal.GetApplyidByLoanId(LoanID);
        }
    }
}
