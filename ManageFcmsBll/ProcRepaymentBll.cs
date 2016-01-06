using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class ProcRepaymentBll
    {
        ProcRepaymentDal _dal = new ProcRepaymentDal();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(int loanId, int repaymentType, ref string message)
        {
            return _dal.Add(loanId, repaymentType, ref message);
        }
    }
}
