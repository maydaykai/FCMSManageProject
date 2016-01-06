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
    public class CashRecordBLL
    {
        private readonly CashRecordDAL _cashRecordDal = new CashRecordDAL();

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            return _cashRecordDal.GetPageList(fields, filters, sortStr, currentPage, pageSize, out total);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CashRecordModel getCashRecordModel(string strWhere)
        {
            return _cashRecordDal.getCashRecordModel(strWhere);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CashRecordModel getCashRecordModel(int id)
        {
            return _cashRecordDal.getCashRecordModel("ID=" + id);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool updateCashRecord(CashRecordModel model)
        {
            return _cashRecordDal.updateCashRecord(model);
        }
        /// <summary>
        /// 复审不通过
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool withdrawCashAuditFail(CashRecordModel model)
        {
            return _cashRecordDal.withdrawCashAuditFail(model);
        }
        /// <summary>
        /// 复审通过
        /// </summary>
        /// <param name="single"></param>
        /// <returns></returns>
        public bool withdrawCashAuditSuccess(CashRecordModel model, ref string message)
        {
            CashRecordModel cashRecordModel = getCashRecordModel(model.ID);
            cashRecordModel.Status = model.Status;
            cashRecordModel.Remark = model.Remark;
            cashRecordModel.AuditRecords = model.AuditRecords;
            cashRecordModel.CashPayType = model.CashPayType;
            return model.CashPayType == 1
                       ? _cashRecordDal.withdrawCashAuditSuccess(cashRecordModel, ref message)
                       : (model.CashPayType == 2
                              ? _cashRecordDal.withdrawCashAuditSuccessByLL(cashRecordModel, ref message)
                              : _cashRecordDal.withdrawCashAuditSuccessByQuick(cashRecordModel, ref message));
        }

        /// <summary>
        /// 查询余额
        /// </summary>
        /// <returns></returns>
        public ResponseLLModel GetLLBalance()
        {
            return _cashRecordDal.GetLLBalance();
        }

        /// <summary>
        /// 线下提现申请
        /// </summary>
        public object BelowCashApply(int memberId, string accountHolder, int bankId, string bankAccount, decimal cashAmount)
        {
            return _cashRecordDal.BelowCashApply(memberId, accountHolder, bankId, bankAccount, cashAmount);
        }

        /// <summary>
        /// 更新数据(线下提现[初审通过])
        /// </summary>
        public bool BelowCashCheckPass(CashRecordModel model)
        {
            return _cashRecordDal.BelowCashCheckPass(model);
        }

        /// <summary>
        /// 更新数据(线下提现[复审通过])
        /// </summary>
        public object BelowCashReCheckPass(CashRecordModel model)
        {
            return _cashRecordDal.BelowCashReCheckPass(model);
        }

        /// <summary>
        /// 更新数据(线下提现[初审不通过/复审不通过])
        /// </summary>
        public object BelowCashNotPass(CashRecordModel model)
        {
            return _cashRecordDal.BelowCashNotPass(model);
        }

        /// <summary>
        /// 总计
        /// </summary>
        public object Aggregate(string filters)
        {
            return _cashRecordDal.Aggregate(filters);
        }

        /// <summary>
        /// 提现成功处理(通联轮询/连连异步通知)
        /// </summary>
        /// <param name="reqSn"></param>
        /// <returns></returns>
        public bool WithdrawCheckSuccess(string reqSn)
        {
            return _cashRecordDal.WithdrawCheckSuccess(reqSn);
        }

        /// <summary>
        /// 连连支付提现成功处理+1
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public bool WithdrawSuccessByLL(int MemberID)
        {
            return _cashRecordDal.WithdrawSuccessByLL(MemberID);
        }

        /// <summary>
        /// 提现失败处理(通联轮询/连连异步通知) 表示最终失败
        /// </summary>
        /// <param name="reqSn"></param>
        /// <returns></returns>
        public bool WithdrawCheckFail(string reqSn)
        {
            return _cashRecordDal.WithdrawCheckFail(reqSn);
        }

        #region 处理提现预警状态
        /// <summary>
        /// 处理提现预警状态
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <param name="status">状态：2存疑 3通过</param>
        /// <param name="note">备注</param>
        /// <param name="memberId">操作用户ID</param>
        /// <returns>true false</returns>
        /// <remarks>add 卢侠勇,2015-09-22</remarks>
        public bool ModifyWithdrawWarning(int id, int status, string note, int memberId)
        {
            return _cashRecordDal.ModifyWithdrawWarning(id, status, note, memberId);
        }
        #endregion
    }
}
