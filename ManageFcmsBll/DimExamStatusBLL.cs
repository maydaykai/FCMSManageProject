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
    public class DimExamStatusBll
    {
        readonly DimExamStatusDal _dal = new DimExamStatusDal();
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DimExamStatusModel> GetDimExamStatusModelList()
        {
            return _dal.GetDimExamStatusModelList();
        }

        public DataSet GetDimExamStatusList()
        {
            return _dal.GetDimExamStatusList();
        }
    }
}
