using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class ColumnRightBll
    {
        private readonly ColumnRightDal _columnRightDal = new ColumnRightDal();

        /// <summary>
        /// 根据栏目ID获取栏目所拥有的操作权限
        /// </summary>
        /// <param name="columnId"></param>
        /// <returns></returns>
        public string GetColumnRightStr(int columnId)
        {
            return _columnRightDal.GetColumnRightStr(columnId);
        }
    }
}
