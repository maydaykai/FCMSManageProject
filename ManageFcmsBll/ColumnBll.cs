using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class ColumnBll
    {
        private readonly ColumnDal _columnDal = new ColumnDal();

        #region Method
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(ColumnModel model, string rightGroup)
        {
            return _columnDal.Add(model, rightGroup);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int Update(ColumnModel model, string rightGroup)
        {
            return _columnDal.Update(model, rightGroup);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ColumnModel GetModel(int id)
        {
            return _columnDal.GetModel(id);
        }

        /// <summary>
        /// 获取栏目列表 相关列数据
        /// </summary>
        /// <param name="fields"> ID, UpdateTime, Description, Name, LinkUrl, ICon, ParentID, Sort, Visible, CreateTime</param>
        /// <param name="visible">0:获取所有栏目 1：获取启用的栏目</param>
        /// <returns></returns>
        public DataSet GetColumnlList(string fields, int visible, string where="")
        {
            return _columnDal.GetColumnlList(fields, visible ,where);
        }


        /// <summary>
        /// 根据栏目ID获取该栏目所拥有的操作权限
        /// </summary>
        /// <param name="columnId">栏目ID</param>
        /// <returns></returns>
        public DataSet GetColRightList(int columnId)
        {
            return _columnDal.GetColRightList(columnId);
        }

        /// <summary>
        /// 获取指定节点下的所有子栏目
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="visible"></param>
        /// <param name="rootID"></param>
        /// <returns></returns>
        public DataSet GetChildColumnList(string fields, int visible, int rootID)
        {
            return _columnDal.GetChildColumnList(fields, visible, rootID);
        }

        #endregion
    }
}
