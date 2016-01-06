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
    public class LoanApplyBll
    {
        readonly LoanApplyDal _dal = new LoanApplyDal();
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LoanApplyModel GetLoanApplyModel(int id)
        {
            return _dal.GetLoanApplyModel(id);
        }

        /// <summary>
        /// 获取快速借贷列表分页
        /// </summary>
        public IList<LoanApplyModel> GetPagedLoanApplyList(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            return _dal.GetPagedLoanApplyList(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(LoanApplyModel model)
        {
            return _dal.UpdateStatus(model);
        }

        /// <summary>
        /// 获取用户对应未发标借款标号数据列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetLoanNumberList(int memberId)
        {
            return _dal.GetLoanNumberList(memberId);
        }

        /// <summary>
        /// 获取用户对应未发标借款标号数据列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetLoanApplyProductInfoList(string where)
        {
            return _dal.GetLoanApplyProductInfoList(where);
        }
    }
}
