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
    public class MemberInfoBll
    {
        private readonly MemberInfoDal _memberInfoDal = new MemberInfoDal();

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total)
        { 
            int totalPage;
            return _memberInfoDal.GetPageList(fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberInfoModel GetModel(int memberID)
        {
            return _memberInfoDal.GetModel(memberID);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MemberInfoModel model)
        {
            return _memberInfoDal.Update(model);
        }
    }
}
