using System;
using System.IO;
using System.Text;
using System.Web;

namespace ManageFcmsCommon
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

        public static void WriteError(Exception ex)
        {
            var st = new System.Diagnostics.StackTrace();
            if (HttpContext.Current == null)
            {
                WriteErrorLog(string.Format("错误：\r\n{0}\r\n堆栈信息：\r\n{1}\r\n", ex.Message, ex.StackTrace));
                return;
            }
            var req = HttpContext.Current.Request;
            var sbReqParams = new StringBuilder();
            sbReqParams.Append("\r\nUser-Agent:" + HttpUtility.UrlDecode(req.UserAgent));
            sbReqParams.Append("\r\nForm:" + HttpUtility.UrlDecode(req.Form.ToString()));
            sbReqParams.Append("\r\nCookies:" + HttpUtility.UrlDecode(req.Form.ToString()));
            WriteErrorLog(string.Format("数据层出错:{3}\r\n错误页面：{0}\r\n请求参数：{1}\r\n堆栈信息：\r\n{2}\r\n", req.Url, sbReqParams, st, ex.Message));
        }
    }
}
