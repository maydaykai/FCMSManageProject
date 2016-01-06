using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class BankAccount_BLBLL
    {
        private readonly BankAccount_BLDAL _dal = new BankAccount_BLDAL();

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BankAccount_BLModel GetModel(int id)
        {
            return _dal.GetModel(id);
        }
    }
}
