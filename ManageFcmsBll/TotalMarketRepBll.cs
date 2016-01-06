using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class TotalMarketRepBll
    {
        private readonly ManageFcmsDal.TotalMarketRepDal dal = new ManageFcmsDal.TotalMarketRepDal();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime date1, DateTime date2)
        {
            return dal.GetList(date1, date2);
        }
        public DataSet GetListNew(DateTime date1, DateTime date2)
        {
            return dal.GetListNew(date1, date2);
        }

        public DataSet GetExcelTable(string date1, string date2, string nexttime)
        {
            return dal.GetExcelTable(date1, date2, nexttime);
        }

        /// <summary>
        /// 查询明细
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="nexttime"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public DataSet GetListNewByMemberId(DateTime date1, DateTime date2, string nexttime, string memberName)
        {
            return dal.GetListNewByMemberId(date1, date2, nexttime, memberName);
        }
    }
}
