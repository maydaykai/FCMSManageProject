using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{

    public class BankBll
    {
        readonly BankDal _dal=new BankDal();

        //获取银行列表
        public DataTable GetBankTypeList()
        {
            return _dal.GetBankTypeList();
        }
    }
}
