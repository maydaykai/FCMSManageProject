using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SignCert.BusinessModel;
using SignCert.DataAccess.Generic;
using SignCert.Model;

namespace SignCert.DataAccess
{
    internal class PdfBuildTaskController : GenericController<PdfBuildTask, CertDataBaseRepository>
    {
        public static List<PdfBuildTask> GetUnfinishTask()
        {
            Monitor.Enter(MObjLock);
            try
            {
                IQueryable<PdfBuildTask> result = from o in DataContext.PdfBuildTask
                                                  where o.HasFinishGeneralPdf == false
                                                  select o;
                return result.ToList();
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }

        public static bool AddNewTask(TaskModel task)
        {
            bool needAdd = false;
            Monitor.Enter(MObjLock);
            try
            {
                int result = (from o in DataContext.PdfBuildTask
                              where o.ContractId == task.ContractId
                              where o.ContractType == (int) task.ContractType
                              select o).Count();

                if (result == 0)
                {
                    var newTask = new PdfBuildTask
                        {
                            ContractId = task.ContractId,
                            ContractType = (int) task.ContractType
                        };
                    Insert(newTask);
                    needAdd = true;
                }
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }

            return needAdd;
        }

        public static void MarkTaskFinished(TaskModel task)
        {
            Monitor.Enter(MObjLock);
            try
            {
                PdfBuildTask result = (from o in DataContext.PdfBuildTask
                                       where o.ContractId == task.ContractId
                                       where o.ContractType == (int) task.ContractType
                                       select o).FirstOrDefault();
                if (result != null)
                {
                    result.HasFinishGeneralPdf = true;
                    SubmitChanges();
                }
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }
    }
}