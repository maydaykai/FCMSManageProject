using System;
using System.Configuration;

namespace SignCertWindowService
{
    public class CommonData
    {
        /// <summary>
        ///     生成签章
        /// </summary>
        public static bool SyncGenerate = Convert.ToBoolean(ConfigurationManager.AppSettings["SyncGenerate"]);
    }
}