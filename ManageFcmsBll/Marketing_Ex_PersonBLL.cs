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
    public class Marketing_Ex_PersonBLL
    {
        //
        private readonly Marketing_Ex_PersonDAL dal = new Marketing_Ex_PersonDAL();

        /// <summary>
        /// 营销报表扩展功能-业务人员列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<Ex_PersonModel> GetCompetencePageList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return dal.GetCompetencePageList(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }

        /// <summary>
        /// 查询所在的
        /// </summary>
        /// <param name="PartentID"></param>
        /// <returns></returns>
        public DataSet GetPartentTable(int PartentID, int leave)
        {
            return dal.GetPartentTable(PartentID, leave);
        }

        public DataSet GetPartentTable_Second(int PartentID, int leave)
        {
            return dal.GetPartentTable_Second(PartentID, leave);
        }

        /// <summary>
        /// 查询数据报表
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="roleId"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageCount"></param>
        /// <param name="RecordCount"></param>
        /// <param name="SumRegcount"></param>
        /// <param name="SumRegmoney"></param>
        /// <param name="SumBidNumContinued"></param>
        /// <param name="SumSuccessTransferMoney"></param>
        /// <param name="SumRealMoney"></param>
        /// <param name="SumInterest"></param>
        /// <returns></returns>
        public List<Marketing_EX_Data> GetEx_DataPageList(string starttime, string endtime,int userId, int roleId, int PageSize, int CurrentPage, ref int PageCount, ref int RecordCount, ref int SumRegcount, ref decimal SumRegmoney,
     ref int SumBidNumContinued, ref decimal SumBidAmount, ref decimal SumSuccessTransferMoney, ref decimal SumRealMoney, ref decimal SumInterest, ref decimal SumCurr_MouthMoney)
        {
            return dal.GetEx_DataPageList(starttime, endtime, userId, roleId, PageSize, CurrentPage, ref PageCount, ref RecordCount, ref SumRegcount, ref SumRegmoney,
               ref SumBidNumContinued,ref SumBidAmount, ref SumSuccessTransferMoney, ref SumRealMoney, ref SumInterest,ref  SumCurr_MouthMoney);
        }

        /// <summary>
        /// 删除营销人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelMarketing_Ex_Person(int id)
        {
            return dal.DelMarketing_Ex_Person(id);
        }

        ///// <summary>
        ///// 添加异动信息
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public int AddMoveModel(Marketing_MoveModel model)
        //{
        //    return dal.AddMoveModel(model);
        //}

        public List<Ex_PersonModel> GetPersonList(int roleId)
        {
            return dal.GetPersonList(roleId);
        }
    }
}
