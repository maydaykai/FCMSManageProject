using System;
using System.Collections.Generic;
using System.Data;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class RightBll
    {
        private readonly RightDal _rightDal = new RightDal();

        /// <summary>
        /// 返回权限组 sign：0-返回所有权限 1-只返回启用的权限
        /// </summary>
        /// <param name="sign">0-返回所有权限 1-只返回启用的权限</param>
        /// <returns></returns>
        public DataSet GetRightList(int sign)
        {
            return _rightDal.GetRightList(sign);
        }


        /// <summary>
        /// 增加
        /// </summary>
        public int Add(RightModel model)
        {
            return _rightDal.Add(model);
        }


        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(RightModel model)
        {
            return _rightDal.Update(model);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RightModel GetModel(int id)
        {
            return _rightDal.GetModel(id);
        }
    }
}
