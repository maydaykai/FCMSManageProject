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
    public class LoanScoreBll
    {
        private readonly LoanScoreDal _dal = new LoanScoreDal();

        /// <summary>
        /// 增加
        /// </summary>
        public bool Add(int loanId, int productTypeId)
        {
            return _dal.Add(loanId , productTypeId);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(int id, int score)
        {
            return _dal.Update(id,score);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int loanId)
        {
            return _dal.Delete(loanId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return _dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得评分汇总及等级
        /// </summary>
        public DataSet GetSumScoreList(int loanId)
        {
            return _dal.GetSumScoreList(loanId);
        }
    }
}
