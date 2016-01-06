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
    public class MemberPointsBLL
    {
        private readonly MemberPointsDAL _dal = new MemberPointsDAL();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddMemberPoints(DimMemberPointsModel model)
        {
            return _dal.AddMemberPoints(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateMemberPoints(DimMemberPointsModel model)
        {
            return _dal.UpdateMemberPoints(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimMemberPointsModel GetMemberPointsModel(int id)
        {
            return _dal.GetMemberPointsModel("ID=" + id);
        }

        /// <summary>
        /// 返回所有会员等级
        /// </summary>
        /// <returns></returns>
        public DataSet GetMemberPointsList()
        {
            return _dal.GetMemberPointsList();
        }
    }
}
