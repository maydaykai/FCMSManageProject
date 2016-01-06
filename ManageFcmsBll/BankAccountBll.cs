using ManageFcmsConn;
using ManageFcmsDal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class BankAccountBll
    {
        private readonly BankAccountDal _bankAccountDal = new BankAccountDal();
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _bankAccountDal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetAuthentPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " BankAccount_Authent P left join Bank B on P.BankCode=B.BankCode left join Member M on P.MemberID=M.ID left join MemberInfo MI on P.MemberID=MI.MemberID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BankAccountModel getBankAccountModel(string strWhere)
        {
            return _bankAccountDal.getBankAccountModel(strWhere);
        }
         
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BankAccountModel getBankAccountModel(int id)
        {
            return _bankAccountDal.getBankAccountModel("BA.ID="+id);
        }


        /// <summary>
        /// 根据MemberID获取未认证/已认证 认证支付银行卡数据
        /// </summary>
        public BankAccountAuthentModel GetBankAccountAuthentModel(int memberId)
        {
            return _bankAccountDal.GetBankAccountAuthentModel("[Status] < 3 AND MemberID=" + memberId);
        }
        /// <summary>
        /// 根据MemberID获取未认证/已认证 认证支付银行卡数据
        /// </summary>
        public BankAccountAuthentModel GetBankAccountAuthentModel(string where)
        {
            return _bankAccountDal.GetBankAccountAuthentModel(where);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBankAccountAuthent(int ID)
        {
            return _bankAccountDal.UpdateBankAccountAuthent(ID);
        }

        /// <summary>
        /// id=会员ID
        /// BankCode=银行名称编号
        /// 根据会员Id修改会员信息 
        /// </summary>
        /// <returns></returns>
        public bool UpdBankAccountAuthentById(int id, int BankCode)
        {
            return _bankAccountDal.UpdBankAccountAuthentById(id, BankCode);
        }
    }
}
