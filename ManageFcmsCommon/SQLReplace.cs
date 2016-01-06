using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManageFcmsCommon
{
    public class SQLReplace
    {
        /// <summary>
        /// SQL关键字过滤
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceSQLKey(string value)
        {
            if (value == "") return value;
            return Regex.Replace(value, @"(\s(and)\s)|(\s(or)\s)|(\s(insert)\s)|(\s(update)\s)|(\s(drop)\s)|(\s(delete)\s)|(\s(select)\s)|(\s(where)\s)|(\s(exec)\s)|(\s(backup)\s)|(\s(create)\s)|(\s(alter)\s)|", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// SQL关键字过滤
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceSQLKeyS(string value)
        {
            if (value == "") return value;
            return Regex.Replace(value, @"(\s(insert)\s)|(\s(update)\s)|(\s(drop)\s)|(\s(delete)\s)|(\s(select)\s)|(\s(where)\s)|(\s(exec)\s)|(\s(backup)\s)|(\s(create)\s)|(\s(alter)\s)|", "", RegexOptions.IgnoreCase);
        }
    }
}
