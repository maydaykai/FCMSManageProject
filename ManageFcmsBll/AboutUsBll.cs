using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class AboutUsBll
    {
        private readonly AboutUsDal _dal = new AboutUsDal();

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(AboutUsModel model)
        {
            return _dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(AboutUsModel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AboutUsModel GetModel(int columnID)
        {
            return _dal.GetModel(columnID);
        }
    }
}
