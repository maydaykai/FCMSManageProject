using System;
using System.Text;

namespace SignCert.Common
{
    public class StringCommon
    {
        public static string GetBase64(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            string result = Convert.ToBase64String(bytes);
            return result;
        }
    }
}