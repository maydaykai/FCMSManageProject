using System;
using System.Threading;
using ManageFcmsCommon;
using SignCert.BusinessModel;

namespace SignCert
{
    /// <summary>
    ///     异步任务调度器
    /// </summary>
    public partial class SignCertApp
    {
        public static readonly SignCertApp Instance = new SignCertApp();

        public void Start()
        {
            Log4NetHelper.WriteLog("合同生成服务已经启动");
            
            var filepath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            filepath = filepath + "\\libpath.xml";
            Log4NetHelper.WriteLog(string.Format("环境变量信息为：{0}", filepath));

            CollectUnfinishTask(); 

            _worker = new Timer(Work, null, DueTime, Timeout.Infinite);
        }

        public void Stop()
        {
            _worker.Change(Timeout.Infinite, Timeout.Infinite);
            _worker.Dispose();
        }

        public int GetWorkingItemCount()
        {
            return _workItems.Count;
        }

        public void AddLoanGuaranteeContractTask(int contractId)
        {
            AddTask(contractId, ContractTypeEnum.LoanGuaranteeContract);
            Log4NetHelper.WriteLog(string.Format("动态添加新任务成功，新任务ID：{0},类型：{1}", contractId,ContractTypeEnum.LoanGuaranteeContract));
        }

        public void AddBuyBackContractTask(int contractId)
        {
            AddTask(contractId, ContractTypeEnum.BuyBackContract);
            Log4NetHelper.WriteLog(string.Format("动态添加新任务成功，新任务ID：{0},类型：{1}", contractId, ContractTypeEnum.BuyBackContract));
        }

        public void AddCreditAssignmentTask(int contractId)
        {
            AddTask(contractId, ContractTypeEnum.CreditAssignment);
            Log4NetHelper.WriteLog(string.Format("动态添加新任务成功，新任务ID：{0},类型：{1}", contractId, ContractTypeEnum.CreditAssignment));
        }
    }
}