using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class ProjectTemplateBll
    {
        readonly ProjectTemplateDal _dal = new ProjectTemplateDal();

        /// <summary>
        /// 获取用户对应未发标借款标号数据列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetProjectTemplate(int productTypeId)
        {
            return _dal.GetProductTemplate(productTypeId);
        }
    }
}
