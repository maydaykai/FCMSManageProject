using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class MobilePushBll
    {
        private readonly MobilePushDal _dal = new MobilePushDal();

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(MobilePushModel model)
        {
            return _dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MobilePushModel model)
        {
            return _dal.Update(model);
        }
    }
}
