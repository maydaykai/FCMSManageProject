using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class DimProductScoreBLL
    {
        private readonly DimProductScoreDAL _dal = new DimProductScoreDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="field"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateByIdToField(string tableName, string field, int id)
        {
            return _dal.UpdateByIdToField(tableName, field, id);
        }
    }
}
