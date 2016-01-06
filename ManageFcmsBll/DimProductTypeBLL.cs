using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class DimProductTypeBLL
    {

        private readonly DimProductTypeDAL _dal = new DimProductTypeDAL();

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetProductList()
        {
            return _dal.GetProductList();
        }

        /// <summary>
        /// 获取某个产品下面的所有项目
        /// </summary>
        /// <returns></returns>
        public DataSet GePtProjectList(int productId)
        {
            return _dal.GePtProjectList(productId);
        }
    }
}
