using System;
using System.IO;
using System.Text;

namespace SignCertWindowService
{
    public class Log4NetHelper
    {
        public static readonly log4net.ILog Loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 

        public static readonly log4net.ILog Logerror = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 


        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void ConfigureAndWatch(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
        }

        public static void WriteLog(string info)
        {
            if (Loginfo.IsInfoEnabled)
            {
                Loginfo.Info(info);
            }
        }

        public static void WriteLog(string info, Exception se)
        {
            if (Logerror.IsErrorEnabled)
            {
                Logerror.Error(info, se);
            }
        }
        public static void WriteErrorLog(string info)
        {
            if (Logerror.IsErrorEnabled)
            {
                Logerror.Error(info);
            }
        }

    }
}
