using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using System.Data;

namespace ManageFcmsBll
{
    public class DimChannelBll
    {
        private readonly DimChannelDal dal = new DimChannelDal();

        public DataSet GetDimChannel()
        {
            return dal.GetDimChannel();
        }
    }
}
