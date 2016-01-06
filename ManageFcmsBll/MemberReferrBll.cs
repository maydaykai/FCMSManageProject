using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsModel;
using ManageFcmsDal;

namespace ManageFcmsBll
{
   public class MemberReferrBll
    {
       public static DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
       {
           return MemberRefrrDal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
       }
    }
}
