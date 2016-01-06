using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class CostVersionBll
    {
        private  readonly CostVersionDal _costVersionDal=new CostVersionDal();
        /// <summary>
        /// 返回所有费用版本
        /// </summary>
        /// <returns></returns>
        public DataSet GetCostVersionList()
        {
            return _costVersionDal.GetCostVersionList();
        }
    }
}
