using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManageFcmsCommon
{
    public class RegexHelper
    {

        /// <summary>
        /// 用户名验证（只能是26个英文字母与数字组成）
        /// </summary>
        public static bool IsUserName(string itemValue)
        {
            return (IsRegEx("^[A-Za-z0-9]+$", itemValue));
        }

        /// <summary>
        /// 用户名验证（只能是26个英文字母与数字组成）
        /// </summary>
        public static bool IsRate(string itemValue)
        {
            //return (IsRegEx("^(?:[1-9][0-9]?|100?|0)(?:\\.[0-9][0-9])?$", itemValue));
            return (IsRegEx("^[1-9]\\d?|100|0(\\.\\d{1,2})?$", itemValue));
        }

        /// <summary>
        /// Decimal类型验证(可验证 大于等于零，小于等于99999999.99 的数字)
        /// </summary>
        public static bool IsDecimal(string itemValue)
        {
            return (IsRegEx("^([1-9][\\d]{0,7}|0)(\\.[\\d]{1,2})?$", itemValue));
        }
        /// <summary>
        /// 借款金额验证
        /// </summary>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static bool IsAmount(string itemValue)
        {
            return (IsRegEx("^[1-9]\\d{0,3}0000(\\.00)?$", itemValue));
        }


        private static bool IsRegEx(string regEx, string itemValue)
        {
            try
            {
                var regex = new Regex(regEx);
                return regex.IsMatch(itemValue);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
