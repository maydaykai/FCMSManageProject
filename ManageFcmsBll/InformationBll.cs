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
    public class InformationBll
    {
        private readonly InformationDal _dal = new InformationDal();

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(InformationModel model)
        {
            return _dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(InformationModel model)
        {
            return _dal.Update(model);
        }
        public bool UpdateAllStatus(int SectionID)
        {
            return _dal.UpdateAllStatus(SectionID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            return _dal.Delete(ID);
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return _dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public InformationModel GetModel(int ID)
        {
            return _dal.GetModel(ID);
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
    }
}
