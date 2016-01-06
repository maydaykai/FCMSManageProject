using System;
using System.Configuration;

namespace LuckyDraw.Common
{
    public class CommonData
    {
        /// <summary>
        /// 数据库连接字符串是否加密
        /// </summary>
        public static bool EnableEncodeConnString = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableEncodeConnString"]);
        /// <summary>
        /// 奖项
        /// </summary>
        public static int PrizeKindCount = 10;
    }
}