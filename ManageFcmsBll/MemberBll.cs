using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using System.Data;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    public class MemberBll
    {
        private readonly MemberDal _dal = new MemberDal();
        //获取账户余额
        public decimal GetBalance(int Id)
        {
            return _dal.GetBalance(Id);
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize,
                                     out int total)
        {
            return _dal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }

        public DataTable GetPageList1(string fields, string filters, string sortStr, int currentPage, int pageSize,
                             out int total)
        {
            return _dal.GetPageList1(fields, filters, sortStr, currentPage, pageSize, out total);
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage,
                                     int pageSize, out int total)
        {
            return _dal.GetPageList(fields, tables, filters, sortStr, currentPage, pageSize, out total);
        }

        /// <summary>
        /// 按条件获得会员列表
        /// </summary>
        public DataTable GetMemberList(string fields, string filters, string sortStr, out int total)
        {
            return _dal.GetMemberList(fields, filters, sortStr, out total);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberModel GetModel(int ID)
        {
            return _dal.GetModel(ID);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MemberModel model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 获得会员账户信息数据列表
        /// </summary>
        public DataSet GetList(string filter, string orderBy, int currentPage, int pageSize, DateTime dateEnd, ref int rowsCount)
        {
            return _dal.GetList(filter, orderBy, currentPage, pageSize, dateEnd, ref rowsCount);
        }

        /// <summary>
        /// 会员账户数据汇总
        /// </summary>
        public DataTable MemberDataCollection(int currentPage, int pageSize, string filter, out int total)
        {
            return _dal.SummaryStatistics("Proc_MemberDataCollection", currentPage, pageSize, filter, out total);
        }

        /// <summary>
        /// 投资人项目汇总
        /// </summary>
        public DataTable InvestorProjectSummary(int currentPage, int pageSize, string filter, out int total)
        {
            return _dal.SummaryStatistics("Proc_InvestorProjectSummary", currentPage, pageSize, filter, out total);
        }

        /// <summary>
        /// 获得异常提现预警列表
        /// </summary>
        public DataSet GetWithdrawAlertList(int limitDays, decimal percent)
        {
            return _dal.GetWithdrawAlertList(limitDays, percent);
        }

        /// <summary>
        /// 获得异常提现预警交易明细列表
        /// </summary>
        public DataSet GetWithdrawAlertDetailList(int limitDays, decimal percent, int memberId)
        {
            return _dal.GetWithdrawAlertDetailList(limitDays, percent, memberId);
        }

        /// <summary>
        /// 会员推荐关系
        /// </summary>
        public DataTable GetMemberRecommend(int pageSize, int currentPage, string filter, string strOrderBy, out int recordCount)
        {
            return _dal.GetMemberRecommend(pageSize, currentPage, filter, strOrderBy, out recordCount);
        }

        /// <summary>
        /// 根据会员名取会员ID
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public int GetMemberId(string memberName)
        {
            return _dal.GetMemberId(memberName);
        }

        /// <summary>
        /// 获取可收回体验券收益数据列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetWithdrawInterestNhList()
        {
            return _dal.GetWithdrawInterestNhList();
        }

        public DataSet MemberData(string Where)
        {
            return _dal.Summary(Where);
        }

        /// <summary>
        /// 融金宝日报表统计
        /// </summary>
        public DataTable DailyReport(DateTime nowDate)
        {
            return _dal.DailyReport(nowDate);
        }

        /// <summary>
        /// 会员资金汇总
        /// </summary>
        public DataTable MemberFundSummary(DateTime nowDate)
        {
            return _dal.MemberFundSummary(nowDate);
        }

        /// <summary>
        /// 运营日报表平台资金存量
        /// </summary>
        public DataTable PlatformCapitalStock(DateTime nowDate)
        {
            return _dal.PlatformCapitalStock(nowDate);
        }

        /// <summary>
        /// 会员账户可用余额统计
        /// </summary>
        public object MemberBalanceTatal(string filters)
        {
            return _dal.MemberBalanceTatal(filters);
        }

        /// <summary>
        /// 短信数据分页
        /// </summary>
        public DataTable GetPagePrivateSmsList(string fields, string filters, string sortStr, int currentPage,
                                               int pageSize, out int total)
        {
            return _dal.GetPagePrivateSmsList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
        #region
        //检测用户名是否存在 true为不存在
        public bool UserNameExists(string userName)
        {
            return _dal.UserNameExists(userName);
        }

        /// <summary>
        /// 会员注册（个人）
        /// </summary>
        public int Add(MemberModel model, string reCode)
        {
            return _dal.Add(model, reCode);
        }

        /// <summary>
        /// 增加一条数据  认证通过
        /// </summary>
        public bool AddIdentityAuthent(IdentityAuthentModel model)
        {
            return _dal.AddIdentityAuthent(model);
        }

        /// <summary>
        /// VIP申请
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool ApplyVip(int memberId)
        {
            return _dal.ApplyVip(memberId);
        }

        /// <summary>
        /// 投标
        /// </summary>
        public bool Add(ProcInvestModel model, ref string message)
        {
            return _dal.Add(model, ref message);
        }

        /// <summary>
        /// 根据memberId获取优惠码
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public string GetCouponCodeByMemberId(int memberId)
        {
            return _dal.GetCouponCodeByMemberId(memberId);
        }

        /// <summary>
        /// 查询IdentityNum表可用身份证条数
        /// </summary>
        /// <returns></returns>
        public int GetIdentityNumCount()
        {
            return _dal.GetIdentityNumCount();
        }

        /// <summary>
        /// 获取IdentityNum表可用身份证datatable
        /// </summary>
        /// <returns></returns>
        public DataTable GetIdentityNumDt(int count)
        {
            return _dal.GetIdentityNumDt(count);
        }

        /// <summary>
        /// 修改为已使用
        /// </summary>
        /// <returns></returns>
        public bool UpdateIdentityNumByID(int ID)
        {
            return _dal.UpdateIdentityNumByID(ID);
        }

        /// <summary>
        /// 检测身份证是否存在
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns></returns>
        public bool IdCardExists(string idCard)
        {
            return _dal.IdCardExists(idCard);
        }

        #endregion

        #region 获取广告渠道列表
        /// <summary>
        /// 获取广告渠道列表
        /// </summary>
        /// <returns>渠道列表</returns>
        /// <remarks>add 卢侠勇,2015-08-27</remarks>
        public DataTable GetChannel()
        {
            return _dal.GetChannel();
        }
        #endregion

        #region 根据用户名获取用户信息
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="memberName">用户名</param>
        /// <returns>用户信息</returns>
        /// <remarks>add 卢侠勇,2015-10-13</remarks>
        public DataTable Getmember(string memberName)
        {
            return _dal.Getmember(memberName);
        }
        #endregion
    }
}
