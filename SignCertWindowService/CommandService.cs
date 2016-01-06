using System;

namespace SignCertWindowService
{
    public class CommandService : ICommandService
    {
        private readonly string _modelName = CommonData.SyncGenerate ? "同步生成" : "异步生成";

        public string ExecuteCommand(string command)
        {
            try
            {
                Log4NetHelper.WriteLog(string.Format(">>以模式{0}开始执行命令：{1}", _modelName, command));
                if (CommonData.SyncGenerate)
                {
                    ExecuteProxy.RunCmd(command);
                }
                else
                {
                    CoreService.AddCommand(command);
                }

                Log4NetHelper.WriteLog(string.Format(">>以模式{0}执行命令成功，命令为：{1}", _modelName, command));

                return string.Empty;
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteLog(string.Format(">>以模式{0}执行命令失败，命令为：{1}，原因为:{2}", _modelName, command, err));
                return err.ToString();
            }
        }
    }
}