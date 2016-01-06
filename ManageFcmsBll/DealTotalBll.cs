using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class DealTotalBll
    {
        private readonly ManageFcmsDal.DealTotalDal dal = new ManageFcmsDal.DealTotalDal();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime totalDate)
        {
            return dal.GetList(totalDate);
        }
    }
}
