using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;

namespace ManageFcmsBll
{
    public class RegistrationBll
    {
        public static DataSet GetList(DateTime dateStart, DateTime endDate, int typeId, string channelType = "", int dateType = 1)
        {
            return RegistrationDal.GetList(dateStart, endDate, typeId, channelType, dateType);
        }
    }
}
