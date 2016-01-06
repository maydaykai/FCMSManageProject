using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class BidPieTotalBll
    {
        private readonly ManageFcmsDal.BidPieTotalDal dal = new ManageFcmsDal.BidPieTotalDal();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int typeId)
        {
            return dal.GetList(typeId);
        }

        /// <summary>
        /// 获得数据列表（传入时间）
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="EnterType"></param>
        /// <param name="DaysValues"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public DataSet GetList_version_one(int typeId, int EnterType,  DateTime BeginTime, DateTime EndTime)
        {
            return dal.GetList_version_one(typeId, EnterType,  BeginTime, EndTime);
        }
    }
}
