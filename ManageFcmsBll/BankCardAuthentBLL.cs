using ManageFcmsDal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class BankCardAuthentBLL
    {
        private readonly BankCardAuthentDAL _bankCardAuthentDal = new BankCardAuthentDAL();

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _bankCardAuthentDal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
        /// <summary>
        /// 认证满三次解锁
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool unlock(int ID)
        {
            return _bankCardAuthentDal.unlock(ID);
        }

        /// <summary>
        /// 重置认证状态
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Reset(int ID)
        {
            return _bankCardAuthentDal.Reset(ID);
        }

        /// <summary>
        /// BankID=更新的列
        /// ID=判断条件
        /// 更新BankCardAccount表中的BankID时根据BankAccountAuthent表中的ID判断
        /// ll</summary>
        public void UpdaBankCardAccountByIDExits(int BankID, int ID)
        {
            _bankCardAuthentDal.UpdaBankCardAccountByIDExits(BankID, ID);
        }
    }
}
