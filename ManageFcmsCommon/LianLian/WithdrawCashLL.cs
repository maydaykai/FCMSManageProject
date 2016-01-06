using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsCommon.LianLian
{
    public class WithdrawCashLL
    {
        /// <summary>
        /// pos方法
        /// </summary>
        /// 
        public static string PostJson(string serverUrl, string reqJson)
        {
            var http = (HttpWebRequest)WebRequest.Create(new Uri(serverUrl));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            
            byte[] bytes = Encoding.UTF8.GetBytes(reqJson);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return content;
        }
    }
}
