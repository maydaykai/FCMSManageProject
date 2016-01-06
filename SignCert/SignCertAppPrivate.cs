using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ManageFcmsCommon;
using SignCert.Business.PdfHelper;
using SignCert.BusinessContract;
using SignCert.BusinessModel;
using SignCert.Common;
using SignCert.DataAccess;
using SignCert.Model;

namespace SignCert
{
    public partial class SignCertApp
    {
        #region 基础数据
        private const int DueTime = 1000; //单位：毫秒 
        private readonly ConcurrentQueue<TaskModel> _workItems = new ConcurrentQueue<TaskModel>();
        private Timer _worker;

        #endregion

        #region 内部处理逻辑

        /// <summary>
        ///     检查有无需要处理的历史合同
        /// </summary>
        private void CollectUnfinishTask()
        {
            if (CommonData.WorkInUnitTestModel) return;

            List<PdfBuildTask> unfinishTaskList = PdfBuildTaskController.GetUnfinishTask();
            Log4NetHelper.WriteLog("需要还原的合同任务数目为：" + unfinishTaskList.Count);

            if (unfinishTaskList.Count == 0) return;

            unfinishTaskList.ForEach(
                p => _workItems.Enqueue(new TaskModel
                    {
                        ContractId = p.ContractId,
                        ContractType = (ContractTypeEnum) p.ContractType
                    }));
        }

        private void AddTask(int contractId, ContractTypeEnum contractType)
        {
            var task = new TaskModel
                {
                    ContractId = contractId,
                    ContractType = contractType
                };

            bool needAdd = false;
            if (!CommonData.WorkInUnitTestModel)
                needAdd = PdfBuildTaskController.AddNewTask(task);

            if (needAdd || CommonData.WorkInUnitTestModel)
                _workItems.Enqueue(task);
        }

        private void Work(object state)
       {
            //停止调度
            _worker.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                if (_workItems.Count >= 0)
                {
                    TaskModel task;
                    if (_workItems.TryDequeue(out task))
                    {
                        if (CommonData.WorkInUnitTestModel) return;

                        Log4NetHelper.WriteLog(string.Format("电子签章合同生成开始，合同序号：{0}", task.ContractId));
                        //执行生成合同与签章任务
                        ExecuteTask(task);
                        Log4NetHelper.WriteLog(string.Format("电子签章合同生成成功，合同序号：{0}。", task.ContractId));

                        //设置为任务已经完成
                        PdfBuildTaskController.MarkTaskFinished(task);
                        Log4NetHelper.WriteLog(string.Format("电子签章合同成功状态已更新到数据库，合同序号：{0}。", task.ContractId));
                    }
                }
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteError(err);
            }
            finally
            {
                //恢复调度
                _worker.Change(DueTime, Timeout.Infinite);             
                
            }
        }

        /// <summary>
        ///     按标执行任务：生成合同并且签名
        /// </summary>
        /// <param name="task"></param>
        private void ExecuteTask(TaskModel task)
        {
            IPdfHelper pdfBuilder = PdfFactoryHelper.Instance.Create(task);
            if (pdfBuilder == null)
            {
                Log4NetHelper.WriteErrorLog(string.Format("无法识别的合同类型,合同编号:{0},合同类型：{1}", task.ContractId,
                                                          task.ContractType));
                return;
            }

            try
            {
                if (pdfBuilder.CanExecute()) //不能覆盖已经生成的合同
                    pdfBuilder.Execute();
            }
            catch (Exception err)
            {
                if (File.Exists(pdfBuilder.FileName))
                {
                    try
                    {
                        File.Delete(pdfBuilder.FileName);
                    }
                    catch (Exception delError)
                    {
                        Log4NetHelper.WriteErrorLog(string.Format("删除文件：{0} 发生错误， 原因：{1}", pdfBuilder.FileName, delError));
                    }
                }

                Log4NetHelper.WriteErrorLog(string.Format("生成合同错误，合同名：{0}, 错误原因：{1}", pdfBuilder.FileName, err));
                throw; //必须抛出错误
            }
        }

        #endregion
    }
}