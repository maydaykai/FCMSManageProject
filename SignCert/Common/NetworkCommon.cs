using System.IO;
using System.Net;
using System.Text;

namespace SignCert.Common
{
    internal class NetworkCommon
    {
        public static string RequestUrl(string url, string data = null, string method = "GET",
                                        string contentType = "text", string charset = "utf-8")
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            request.ContentType = contentType;
            request.Headers.Add("charset:" + charset);
            Encoding encoding = Encoding.GetEncoding(charset);

            if (data != null)
            {
                byte[] buffer = encoding.GetBytes(data);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
            else
            {
                request.ContentLength = 0;
            }

            using (var wr = request.GetResponse() as HttpWebResponse)
            {
                if (wr != null)
                    // ReSharper disable AssignNullToNotNullAttribute
                    using (var reader = new StreamReader(wr.GetResponseStream(), encoding))
                    // ReSharper restore AssignNullToNotNullAttribute
                    {
                        return reader.ReadToEnd();
                    }
            }

            return string.Empty;
        }
    }
}