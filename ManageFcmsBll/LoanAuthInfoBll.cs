using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class LoanAuthInfoBll
    {
        private readonly LoanAuthInfoDal _dal = new LoanAuthInfoDal();

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(int id, string authDate,string isAuth)
        {
            return _dal.Update(id, authDate, isAuth);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return _dal.GetList(strWhere);
        }
    }
}
