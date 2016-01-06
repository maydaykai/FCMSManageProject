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
    public class ProcFundReportBll
    {
        private readonly ProcFundReportDal _dal = new ProcFundReportDal();
        public DataSet GetList(int memberID, string filter, int currentPage, int pageSize, string strOrderBy, out int recordCount)
        {
            return _dal.GetList(memberID, filter, currentPage, pageSize, strOrderBy, out recordCount);
        }
    }
}
