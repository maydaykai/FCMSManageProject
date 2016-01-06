using ManageFcmsConn;
using ManageFcmsDal;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsBll
{
    public class MemberDetailInfoBll
    {
        private readonly MemberDetailInfoDal _memberDetailInfoDal = new MemberDetailInfoDal();

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            return _memberDetailInfoDal.GetPageList(fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MemberDetailInfoModel model)
        {
            return _memberDetailInfoDal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberDetailInfoModel GetModel(int memberID)
        {
            return _memberDetailInfoDal.GetModel(memberID);
        }

        /// <summary>
        /// 获取线下提现设置
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool GetUnderCashSetting(int memberId)
        {
            return _memberDetailInfoDal.GetUnderCashSetting(memberId);
        }

        /// <summary>
        /// 会员线下提现设置
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="allowUnderCash"></param>
        /// <returns></returns>
        public bool SetMemberUnderCash(int memberId, bool allowUnderCash)
        {
            return _memberDetailInfoDal.SetMemberUnderCash(memberId, allowUnderCash);
        }

        public bool SetMember(int memberId)
        {
            return _memberDetailInfoDal.SetMember(memberId);
        }

        public bool LoanPubilshUser(LoanMemberInfoModel loanMemberInfoModel)
        {
            return _memberDetailInfoDal.LoanPubilshUser(loanMemberInfoModel);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LoanMemberInfoModel getLoanMemberInfoModel(int memberId)
        {
            return _memberDetailInfoDal.getLoanMemberInfoModel(memberId);
        }

        public bool LoanEnterprise(LoanEnterpriseMemberInfoModel loanEnterpriseMemberInfoModel)
        {
            return _memberDetailInfoDal.LoanEnterprise(loanEnterpriseMemberInfoModel);
        }
    }
}
