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
    public class FocusFigureBll
    {
        private readonly FocusFigureDal _dal = new FocusFigureDal();

        /// <summary>
        /// 增加
        /// </summary>
        public int Add(FocusFigureModel model)
        {
            return _dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(FocusFigureModel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FocusFigureModel GetModel(int ID)
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
