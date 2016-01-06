using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class DimLoanUseBll
    {
        readonly DimLoanUseDal _dal = new DimLoanUseDal();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddDimLoanUse(DimLoanUseModel model)
        {
            return _dal.AddDimLoanUse(model);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DimLoanUseModel> GetDimLoanUseModelList(string strWhere)
        {
            return _dal.GetDimLoanUseModelList(strWhere);
        }

        /// <summary>
        /// 获得借款类型名称
        /// </summary>
        public List<DimLoanUseModel> GetDimLoanUseName(string strWhere)
        {
            var where = new StringBuilder();
            where.Append(" ID = " + strWhere);
            return _dal.GetDimLoanUseModelList(where.ToString());
        }
    }
}
