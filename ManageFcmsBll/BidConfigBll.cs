using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class BidConfigBll
    {
        private readonly BidConfigDal _bidConfigDal = new BidConfigDal();

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(BidConfigModel model)
        {
            return _bidConfigDal.Add(model);
        }


        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(BidConfigModel model)
        {
            return _bidConfigDal.Update(model);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BidConfigModel GetModel(int id)
        {
            return _bidConfigDal.GetModel(id);
        }

        /// <summary>
        /// 返回所有投标额度设置
        /// </summary>
        /// <returns></returns>
        public DataSet GetBidConfigList()
        {
            return _bidConfigDal.GetBidConfigList();
        }
    }
}
