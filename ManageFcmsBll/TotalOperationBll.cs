using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class TotalOperationBll
    {
        private readonly ManageFcmsDal.TotalOperationDal dal = new ManageFcmsDal.TotalOperationDal();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime dateStart, DateTime endDate)
        {
            return dal.GetList(dateStart, endDate);
        }

        /// <summary>
        /// 获得月统计数据列表
        /// </summary>
        public DataSet GetMonthList(string yearstr)
        {
            return dal.GetMonthList(yearstr);
        }
   }
}
