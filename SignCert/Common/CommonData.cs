using System;
using System.Configuration;

namespace SignCert.Common
{
    public class CommonData
    {
        /// <summary>
        ///     电子签章的目录位置
        /// </summary>
        public static string SealFolder = ConfigurationManager.AppSettings["SealFolder"] ?? @"C:\Seals#\";

        /// <summary>
        ///     电子签章的目录位置
        /// </summary>
        public static string CfcaSealUrl = ConfigurationManager.AppSettings["CfcaSealUrl"] ??
                                           @"http://192.168.1.46:8000/APWebPF/CfcaCertServlet";

        /// <summary>
        ///     默认不工作在单元测试
        /// </summary>
        public static bool WorkInUnitTestModel { get; set; }

        /// <summary>
        /// 数据库连接字符串是否加密
        /// </summary>
        public static bool EnableEncodeConnString = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableEncodeConnString"]);
    }
}