using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class LoanBll
    {
        readonly LoanDal _dal = new LoanDal();
        /// <summary>
        /// 增加一条借贷数据
        /// </summary>
        public int AddLoan(LoanModel model, LoanMemberInfoModel loanMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel)
        {
            return _dal.AddLoan(model, loanMemberInfoModel, greenChannelRecordModel);
        }

        public int AddLoan(LoanModel model, LoanMemberInfoModel loanMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel, AppointmentLoan appointmentLoan)
        {
            return _dal.AddLoan(model, loanMemberInfoModel, greenChannelRecordModel, appointmentLoan);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LoanModel model)
        {
            return _dal.Update(model);
        }

        public bool Update_Student(LoanModel model)
        {
            return _dal.Update_Student(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="loandId"></param>
        /// <param name="memberId"></param>
        /// <param name="LoanAmount"></param>
        /// <param name="LoanTerm"></param>
        /// <param name="LoanDescribe"></param>
        /// <returns></returns>
        public bool Review_student(int loandId, int memberId, decimal LoanAmount, int LoanTerm)
        {
            string LoanDescribe = "";
            string ReviewComments = "";
            var model = _dal.GetLoanModel(loandId);
            //历史审核记录
            var histotymodel = new AuditHistoryDal().GetAuditHistoryModelByLoanId(loandId);
            if (model != null)
            {
                //综上所诉，平台同意其8000.00元融资申请
                int a = model.LoanDescribe.IndexOf("其");
                int b = model.LoanDescribe.IndexOf("元");
                LoanDescribe = model.LoanDescribe.Replace(model.LoanDescribe.Substring(a, b - a), LoanAmount.ToString());
                //历史审核
            }

            //ReviewComments = histotymodel.ReviewComments.Replace(histotymodel.ReviewComments.Substring(hist_one, hist_two - hist_one), LoanAmount.ToString());
            return _dal.Review_student(loandId, memberId, LoanAmount, LoanTerm, LoanDescribe);
        }

        /// <summary>
        /// 更新一条数据(事物方式，带审核历史数据)
        /// </summary>
        public bool UpdateTran(LoanModel model, AuditHistoryModel auditModel)
        {
            return _dal.UpdateTran(model, auditModel);
        }

        /// <summary>
        /// 生成借款标还款计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BuildPlan(int id)
        {
            return _dal.BuildPlan(id);
        }

        /// <summary>
        /// 生成借款标还款计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BuildPlanTran(LoanModel model, AuditHistoryModel auditModel)
        {
            return _dal.BuildPlanTran(model, auditModel);
        }

        /// <summary>
        /// 得到一个借贷对象实体
        /// </summary>
        public LoanModel GetLoanModel(int id)
        {
            return _dal.GetLoanModel(id);
        }
        /// <summary>
        /// 获取借贷列表分页
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public List<LoanModel> GetPageLoanModel(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            return _dal.GetPageLoanModel(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }


        public List<LoanModel> GetPageLoanMore(string OrderBy, int PageIndex, int PageSize, ref int TotalRows, int loanUseID, int repayment, string keyword, decimal amountmin, decimal amountmax, int examstatus, int cityid, int loanTerm)
        {
            string Where = "";
            Where = " LoanAmount >= " + amountmin * 10000 + " and LoanAmount <= " + amountmax * 10000 + " and ExamStatus = " + examstatus;
            if (loanUseID != 0)
            {
                Where += " and DimLoanUseID = " + loanUseID;
            }
            if (repayment != 0)
            {
                Where += " and RepaymentMethod = " + repayment;
            }
            if (keyword != "")
            {
                Where += " and LoanNumber like '%" + keyword + "%'";
            }
            if (cityid != 0)
            {
                Where += " and cityid = " + cityid;
            }
            if (loanTerm != 0)
            {
                Where += " and loanTerm = " + loanTerm;
            }
            return _dal.GetPageLoanManageModel(Where, OrderBy, PageIndex, PageSize, ref TotalRows);
        }

        public List<LoanModel> GetPageLoanManage(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            return _dal.GetPageLoanManageModel(whereStr, orderBy, currentPage, pageSize, ref rowsCount);
        }
        /// <summary>
        /// 未发布的借款
        /// </summary>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<LoanModel> GetPageUnpublishedLoan(string OrderBy, int PageIndex, int PageSize, ref int TotalRows, string startDate, string endDate, string title)
        {
            StringBuilder Where = new StringBuilder(" ExamStatus in (1,3,5,7) ");
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime sDate = Convert.ToDateTime(startDate);
                Where.Append(" and CreateTime >= '" + sDate + "' ");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime eDate = Convert.ToDateTime(endDate);
                Where.Append(" and CreateTime <= '" + eDate + "' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                Where.Append(" and LoanNumber like '%" + title + "%' ");
            }
            return _dal.GetPageLoanManageModel(Where.ToString(), OrderBy, PageIndex, PageSize, ref TotalRows);
        }
        /// <summary>
        /// 正在招标的借款
        /// </summary>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<LoanModel> GetPageBiddingLoan(string OrderBy, int PageIndex, int PageSize, ref int TotalRows, string startDate, string endDate, string title)
        {
            StringBuilder Where = new StringBuilder(" ExamStatus = 9 ");
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime sDate = Convert.ToDateTime(startDate);
                Where.Append(" and BidStratTime >= '" + sDate + "' ");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime eDate = Convert.ToDateTime(endDate);
                Where.Append(" and BidEndTime >= '" + eDate + "' ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                Where.Append(" and LoanNumber like '%" + title + "%' ");
            }
            return _dal.GetPageLoanManageModel(Where.ToString(), OrderBy, PageIndex, PageSize, ref TotalRows);
        }
        public List<LoanModel> GetLoanInfoList(int loanid)
        {
            string Where = "";
            if (loanid > 0)
            {
                Where += " id = " + loanid;
            }
            return _dal.GetLoanInfoListModel(Where);
        }

        public LoanModel GetLoanInfoModel(string strWhere)
        {
            return _dal.GetLoanInfoModel(strWhere);
        }

        /// <summary>
        /// 获取会员当前未还贷款金额
        /// </summary>
        /// <param name="loanid"></param>
        /// <returns></returns>
        public decimal GetMemberSurAmount(int memberID)
        {
            return _dal.GetMemberSurAmount(memberID);
        }
        /// <summary>
        /// 获取会员审批中的申请金额
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public decimal GetMemberAuditAmount(int memberID)
        {
            return _dal.GetMemberAuditAmount(memberID);
        }

        /// <summary>
        /// 平台项目汇总
        /// </summary>
        public DataTable SysProjectSummary(int currentPage, int pageSize, string filter, out int total)
        {
            return _dal.SysProjectSummary(currentPage, pageSize, filter, out total);
        }

        /// <summary>
        /// 开启/关闭 开关
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool OnOrOffSwitch(int type, int id, int status)
        {
            return _dal.OnOrOffSwitch(type, id, status);
        }

        /// <summary>
        /// 获取借款标会员/企业信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetUserInfo(int memberId, string sql)
        {
            return _dal.GetUserInfo(memberId, sql);
        }

        public int AddLoanEnterprise(LoanModel loanModel, LoanEnterpriseMemberInfoModel loanEnterpriseMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel)
        {
            return _dal.AddLoanEnterprise(loanModel, loanEnterpriseMemberInfoModel, greenChannelRecordModel);
        }

        public int AddLoanEnterprise(LoanModel loanModel, LoanEnterpriseMemberInfoModel loanEnterpriseMemberInfoModel, GreenChannelRecordModel greenChannelRecordModel, AppointmentLoan appointmentLoan)
        {
            return _dal.AddLoanEnterprise(loanModel, loanEnterpriseMemberInfoModel, greenChannelRecordModel, appointmentLoan);
        }

        /// <summary>
        /// 临时保存借款标描述
        /// </summary>
        public bool UpdateLoanDescribe(LoanModel model)
        {
            return _dal.UpdateLoanDescribe(model);
        }

        #region 获取预约用户列表
        /// <summary>
        /// 获取预约用户列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRows">总记录数</param>
        /// <returns>列表数据</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUserList(string filter, int pageIndex, int pageSize, ref int totalRows)
        {
            return _dal.GetAppointmentBiddingUserList(filter, pageIndex, pageSize, ref totalRows);
        }
        #endregion

        #region 获取预约信息
        /// <summary>
        /// 获取预约信息
        /// </summary>
        /// <param name="id">预约ID</param>
        /// <returns>预约信息</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUser(int id)
        {
            return _dal.GetAppointmentBiddingUser(id);
        }
        #endregion

        #region 修改预约用户数据
        /// <summary>
        /// 修改预约用户数据
        /// </summary>
        /// <param name="id">预约id</param>
        /// <param name="memberId">用户id</param>
        /// <param name="status">状态</param>
        /// <param name="operationId">操作员id</param>
        /// <param name="operationNote">操作备注</param>
        /// <returns>true 成功 false 失败</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public bool AddAppointmentBiddingUser(int id, int? memberId, int status, int operationId, string operationNote)
        {
            return _dal.AddAppointmentBiddingUser(id, memberId, status, operationId, operationNote);
        }
        #endregion

        #region 获取已确定预约用户信息列表
        /// <summary>
        /// 获取已确定预约用户信息列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRows">总记录数</param>
        /// <param name="filter">查询条件</param>
        /// <returns>列表数据</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUserList(int pageIndex, int pageSize, ref int totalRows, string filter)
        {
            return _dal.GetAppointmentBiddingUserList(pageIndex, pageSize, ref totalRows, filter);
        }
        #endregion

        #region 根据借款ID获取预约用户信息列表
        /// <summary>
        /// 根据借款ID获取预约用户信息列表
        /// </summary>
        /// <param name="loanId">借款ID</param>
        /// <returns>列表数据</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable GetAppointmentBiddingUserList(int loanId)
        {
            return _dal.GetAppointmentBiddingUserList(loanId);
        }
        #endregion
        /// <summary>
        /// 添加标信息到微信发标提醒
        /// </summary>
        /// <param name="lm"></param>
        public void AddWeixinNoticeMessage(LoanModel lm)
        {
            _dal.AddWeixinNoticeMessage(lm);
        }
    }
}
