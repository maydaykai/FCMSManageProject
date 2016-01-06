using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class AreaBLL
    {
        AreaDAL dal = new AreaDAL();
        /// <summary>
        /// 根据条件获取省
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<AreaModel> getProvinceList()
        {
            return dal.getAreaModelList("ParentID=1");
        }
        /// <summary>
        /// 根据条件获取市
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<AreaModel> getCityListByParentID(int id)
        {
            return dal.getAreaModelList("ParentID=" + id);
        }

        /// <summary>
        /// 根据ID获取地区
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AreaModel getAreaByID(int id)
        {
            return dal.getAreaModelList("ID=" + id).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有省市
        /// </summary>
        /// <returns></returns>
        public List<AreaModel> getALLArea()
        {
            return dal.getAreaModelList("");
        }

        /// <summary>
        /// 查询省
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetAreList(string strWhere)
        {
            return dal.GetAreList(strWhere);
        }
    }
}
