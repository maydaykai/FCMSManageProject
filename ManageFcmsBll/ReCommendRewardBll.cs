using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class ReCommendRewardBll
    {
        private readonly ReCommendRewardDal _dal = new ReCommendRewardDal();
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize,
                                     out int total)
        {
            return _dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }

        public bool GetData()
        {
            return _dal.GetData();
        }

        public string GetLowerLevel(int memberId, int year, int month)
        {
            return _dal.GetLowerLevel(memberId, year, month);
        }

        /// <summary>
        /// 推荐统计列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-10-08</remarks>
        public DataTable GetPageList1(string filter, int pageIndex, int pageSize, out int total)
        {
            return _dal.GetPageList1(filter, pageIndex, pageSize, out total);
        }

        /// <summary>
        /// 获取下级推荐数据
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="filter2">查询条件2</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-10-08</remarks>
        public DataTable GetRecommendDetalis(string filter, string filter2, int pageIndex, int pageSize, out int total)
        {
            return _dal.GetRecommendDetalis(filter, filter2, pageIndex, pageSize, out total);
        }
    }
}
