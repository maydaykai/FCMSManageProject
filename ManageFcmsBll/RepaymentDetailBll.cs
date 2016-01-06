using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class RepaymentDetailBll
    {
        RepaymentDetailDal _dal = new RepaymentDetailDal();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int loanId, int repaymentType)
        {
            return _dal.GetRepaymentDetailList(loanId, repaymentType);
        }
    }
}
