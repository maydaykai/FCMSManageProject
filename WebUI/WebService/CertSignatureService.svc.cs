using System;
using ManageFcmsCommon;
using SignCert;
using SignCert.DataAccess;
using SignCert.Model;

namespace WebUI.WebService
{
    /// <summary>
    ///     电子签章对外第三方接口服务
    /// </summary>
    public class CertSignatureService : ICertSignatureService
    {
        public bool AddCreditAssignmentTask(string contractId)
        {
            try
            {
                Log4NetHelper.WriteLog(string.Format("即将通过WCF添加新任务，新任务参数contractId：{0}", contractId));
                int cid = Convert.ToInt32(DESStringHelper.DecryptString(contractId));
                SignCertApp.Instance.AddCreditAssignmentTask(cid);
                Log4NetHelper.WriteLog(string.Format("通过WCF添加新任务成功，新任务ID：{0}", cid));
                return true;
            }
            catch (Exception error)
            {
                Log4NetHelper.WriteErrorLog(string.Format("通过WCF添加新任务失败，新任务ID：{0}， 失败原因：{1}", contractId, error));
                return false;
            }
        }

        public bool SetAutoSignStatus(string memberId, bool enable)
        {
            try
            {
                int mid = Convert.ToInt32(DESStringHelper.DecryptString(memberId));
                UserCertOverviewController.SetAutoSignStatus(mid, enable);
                return true;
            }
            catch (Exception error)
            {
                Log4NetHelper.WriteErrorLog(string.Format("通过WCF设置用户证书托管状态失败，会员ID：{0}， 失败原因：{1}", memberId, error));
                return false;
            }
        }

        public bool GetAgreeAutoSignStatus(string memberId)
        {
            try
            {
                int mid = Convert.ToInt32(DESStringHelper.DecryptString(memberId));
                UserCertOverview userCertOverview = UserCertOverviewController.GetOrAddUserCertOverviewByUserId(mid);
                return userCertOverview.AgreeAutoSign;
            }
            catch (Exception error)
            {
                Log4NetHelper.WriteErrorLog(string.Format("通过WCF查询用户证书托管状态失败，会员ID：{0}， 失败原因：{1}", memberId, error));
                return false;
            }
        }

        //public Task AddTaskEntity(Task task)
        //{
        //    return new Task();//task == null ? 100 : task.LoanId;
        //}
    }
}