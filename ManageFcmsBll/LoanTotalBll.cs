using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class LoanTotalBll
    {
        private readonly ManageFcmsDal.LoanTotalDal dal = new ManageFcmsDal.LoanTotalDal();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime dateStart, DateTime endDate)
        {
            return dal.GetList(dateStart, endDate);
        }
    }
}
