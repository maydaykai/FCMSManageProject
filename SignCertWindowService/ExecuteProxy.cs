using System.ComponentModel;
using System.Diagnostics;

namespace SignCertWindowService
{
    public class ExecuteProxy
    {
        internal static void RunCmd(string command)
        {
            try
            {
                var process = new Process
                    {
                        StartInfo =
                            {
                                FileName = "cmd",
                                Arguments = "/c " + command,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                UseShellExecute = false,
                                RedirectStandardInput = true,
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                CreateNoWindow = true
                            }
                    };

                process.Start();
                process.StandardInput.WriteLine("exit");
                string result = process.StandardOutput.ReadToEnd();
                Log4NetHelper.WriteLog(string.Format("CMD输出数据为：{0}", result));
            }
            catch (Win32Exception ex)
            {
                throw ex;
            }
        }
    }
}