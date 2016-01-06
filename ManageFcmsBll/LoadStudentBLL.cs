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
    public class LoadStudentBLL
    {
        readonly LoadStudentDAL _dal = new LoadStudentDAL();
        /// <summary>
        /// 查询学生信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetLoan_StudentInfo(int Id)
        {
            return _dal.GetLoan_StudentInfo(Id);
        }

        /// <summary>
        /// 查询借款信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetLoan_StudentLoanApplyInfo(int Id)
        {
            return _dal.GetLoan_StudentLoanApplyInfo(Id);
        }
        
        /// <summary>
        /// 修改学生贷款信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateStudentLoanApplyInfo(StudentLoanApply model)
        {
            return _dal.UpdateStudentLoanApplyInfo(model); 
        }
    }
}
