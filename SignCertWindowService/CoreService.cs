using System;
using System.Collections.Concurrent;
using System.IO;
using System.ServiceModel;
using System.ServiceProcess;
using System.Threading;

namespace SignCertWindowService
{
    public partial class CoreService : ServiceBase
    {
        private const int DueTime = 1000; //单位：毫秒，异步生成时候使用
        private static readonly ConcurrentQueue<string> WorkItems = new ConcurrentQueue<string>();
        private ServiceHost _host;
        private Timer _worker;

        public CoreService()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     开启服务，端口号：6000
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\config\log4net.config");
            Log4NetHelper.ConfigureAndWatch(fileInfo);

            Thread.Sleep(1000 * 1);
            _worker = new Timer(Work, null, DueTime, Timeout.Infinite);

            Log4NetHelper.WriteLog("签章服务启动成功！");

            try
            {
                _host = new ServiceHost(typeof(CommandService));
                _host.Open();
                Log4NetHelper.WriteLog("WCF远程服务启动成功！");
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteLog(string.Format("WCF远程服务启动失败！原因：{0}", err));
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (_host != null)
                {
                    _host.Close(new TimeSpan(0, 0, 30));
                }
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteLog(string.Format("未能在30秒内关闭WCF服务，原因：{0}", err));
            }
        }

        public static void AddCommand(string command)
        {
            WorkItems.Enqueue(command);
        }

        private void Work(object state)
        {
            //停止调度
            _worker.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                if (WorkItems.Count >= 0)
                {
                    string command;
                    if (WorkItems.TryDequeue(out command))
                    {
                        ExecuteTask(command);
                    }
                }
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteLog(err.ToString());
            }
            finally
            {
                //恢复调度
                _worker.Change(DueTime, Timeout.Infinite);
            }
        }

        private void ExecuteTask(string command)
        {
            ExecuteProxy.RunCmd(command);
        }
    }
}