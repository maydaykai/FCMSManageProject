using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class GuaranteeNoBll
    {
        private GuaranteeNoDal _dal = new GuaranteeNoDal();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddGuaranteeNo(GuaranteeNoModel model)
        {
            return _dal.AddGuaranteeNo(model);
        }

        /// <summary>
        /// 获取保函列表
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public List<GuaranteeNoModel> GetPageGuaranteeNoModel(string Where, string OrderBy, int PageIndex, int PageSize,
                                                              ref int TotalRows)
        {
            return _dal.GetPageGuaranteeNoModel(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }
    }
}
