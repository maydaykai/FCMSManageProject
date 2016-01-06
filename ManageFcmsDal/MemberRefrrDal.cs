using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace ManageFcmsDal
{
   public class MemberRefrrDal
    {

       public static DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
       {
           int totalPage;
           const string tables = "Member P left join MemberInfo M on  P.ID = M.MemberID  ";
           return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
       } 

    }
}
