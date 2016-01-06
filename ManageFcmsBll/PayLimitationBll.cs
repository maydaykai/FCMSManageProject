using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class PayLimitationBll
    {
        private PayLimitationDal dal = new PayLimitationDal();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public DataSet GetData(string bankName = null)
        {
            return dal.GetData(bankName);
        }

        public int Update(PayLimitation limiataion)
        {
            return dal.Update(limiataion);
        }

        public int Add(PayLimitation limiataion)
        {
            return dal.Add(limiataion);
        }

        public void Delete(int id)
        {
            dal.Delete(id);
        }
    }
}
