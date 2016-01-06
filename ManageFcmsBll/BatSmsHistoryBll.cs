using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class BatSmsHistoryBll
    {
        private readonly ManageFcmsDal.BatSmsHistoryDal dal = new ManageFcmsDal.BatSmsHistoryDal();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int pagenum, int pagesize, string sortField, string totalDate, ref int total)
        {
            return dal.GetList(pagenum, pagesize, sortField, totalDate, ref total);
        }


    }
}
