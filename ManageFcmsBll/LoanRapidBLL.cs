using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class LoanRapidBLL
    {
        LoanRapidDAL dal = new LoanRapidDAL();

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(LoanRapidModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(LoanRapidModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            return dal.Delete(ID);
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LoanRapidModel GetLoanRapidModel(int id)
        {
            return dal.GetLoanRapidModel(id);
        }

        /// <summary>
        /// 获取快速借贷列表分页
        /// </summary>
        public IList<LoanRapidModel> GetPagedLoanRapidList(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            return dal.GetPagedLoanRapidList(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(LoanRapidModel model)
        {
            return dal.UpdateStatus(model);
        }

        /// <summary>
        /// 根据条件查询指定列定
        /// </summary>
        public IList<LoanRapidModel> GetLoanRapidList(string Where, string OrderBy, ref int TotalRows)
        {
            return dal.GetLoanRapidList(Where, OrderBy, ref TotalRows);
        }
    }
}
