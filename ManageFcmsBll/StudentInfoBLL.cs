using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class StudentInfoBLL
    {
        readonly StudentInfoDAL _dal = new StudentInfoDAL();
        public bool UpdateByMemberId(StudentInfo model)
        {
            return _dal.UpdateByMemberId(model);
        }
    }
}
