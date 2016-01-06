using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsCommon;

namespace WebUI.MemberManage
{
    public partial class RechargeView : System.Web.UI.Page
    {
        public String srcMsg = null;
        public String signMsg = null;
        public String verifySrc = null;
        public bool verifyResult = false;

        public String serverUrl;

        public String key;
        public String version;
        public String language;
        public String inputCharset;
        public String merchantId;
        public String pickupUrl;
        public String receiveUrl;
        public String issuerId;
        public String payType;
        public String signType;
        public String orderNo;
        public String orderAmount;
        public String orderDatetime;
        public String queryDatetime;
        public String paymentOrderId;
        public String payDatetime;
        public String payAmount;
        public String ext1;
        public String ext2;
        public String payResult;
        public String errorCode;
        public String returnDatetime;

        public String querySignSrcMsg;
        public String querySignMsg;

        public String responseString;

        public String postData;

        public int iWebReadWriteTimeout = 20000;

        public int iWebTimeout = 20000;

        /// <summary> Byte value that maps to 'a' in Base64 encoding
        /// </summary>
        private const int LOWER_CASE_A_VALUE = 26;

        /// <summary> Byte value that maps to '0' in Base64 encoding
        /// </summary>
        private const int ZERO_VALUE = 52;

        /// <summary> Byte value that maps to '+' in Base64 encoding
        /// </summary>
        private const int PLUS_VALUE = 62;

        /// <summary> Byte value that maps to '/' in Base64 encoding
        /// </summary>
        private const int SLASH_VALUE = 63;

        /// <summary> Bit mask for one character worth of bits in Base64 encoding.
        /// Equivalent to binary value 111111b.
        /// </summary>
        private const int SIX_BIT_MASK = 63;

        /// <summary> Bit mask for one byte worth of bits in Base64 encoding.
        /// Equivalent to binary value 11111111b.
        /// </summary>
        private const int EIGHT_BIT_MASK = 0xFF;

        /// <summary> The input String to be decoded
        /// </summary>
        private System.String mString;

        /// <summary> Current position in the String(to be decoded)
        /// </summary>
        private int mIndex = 0;

        //region events



        protected void Page_Load(object sender, EventArgs e)
        {
            //填充交易数据
            serverUrl = "http://service.allinpay.com/gateway/index.do?";  //submit server url 
            key = "1234567890";   //md5 key
            version = "v1.5"; //version
            merchantId = "109127551407001";//merchantId
            signType = "0";
            orderNo = ConvertHelper.QueryString(Request, "orderNo", ""); //Request["orderNo"];
            orderDatetime = ConvertHelper.QueryString(Request, "orderDatetime", ""); //Request["orderDatetime"];
            queryDatetime = DateTime.Now.ToString("yyyyMMddHHmmss"); ;


            //生成signMsg
            //1、首先组签名原串 querySignSrcMsg
            StringBuilder bufSignSrc = new StringBuilder();
            appendSignPara(bufSignSrc, "merchantId", merchantId);
            appendSignPara(bufSignSrc, "version", version);
            appendSignPara(bufSignSrc, "signType", signType);
            appendSignPara(bufSignSrc, "orderNo", orderNo);
            appendSignPara(bufSignSrc, "orderDatetime", orderDatetime);
            appendSignPara(bufSignSrc, "queryDatetime", queryDatetime);
            appendLastSignPara(bufSignSrc, "key", key);

            querySignSrcMsg = bufSignSrc.ToString();
            //2、再对签名原串进行MD5摘要
            querySignMsg = FormsAuthentication.HashPasswordForStoringInConfigFile(querySignSrcMsg, "MD5");

            //发起POST请求到服务端

            postData = HttpUtility.UrlEncode("merchantId") + "=" + HttpUtility.UrlEncode(merchantId)
                + "&" + HttpUtility.UrlEncode("version") + "=" + HttpUtility.UrlEncode(version)
                + "&" + HttpUtility.UrlEncode("signType") + "=" + HttpUtility.UrlEncode(signType)
                + "&" + HttpUtility.UrlEncode("orderNo") + "=" + HttpUtility.UrlEncode(orderNo)
                + "&" + HttpUtility.UrlEncode("orderDatetime") + "=" + HttpUtility.UrlEncode(orderDatetime)
                + "&" + HttpUtility.UrlEncode("queryDatetime") + "=" + HttpUtility.UrlEncode(queryDatetime)
                + "&" + HttpUtility.UrlEncode("signMsg") + "=" + HttpUtility.UrlEncode(querySignMsg);


            byte[] tRequestMessage = System.Text.ASCIIEncoding.UTF8.GetBytes(postData);
            HttpWebRequest tWebRequest = null;
            BufferedStream tRequestStream = null;
            HttpWebResponse tWebResponse = null;
            Byte[] tResponseByteArray = null;
            try
            {

                #region
                //1、准备连接
                System.Net.ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();

                tWebRequest = (HttpWebRequest)WebRequest.Create(serverUrl);
                tWebRequest.Method = "POST";
                tWebRequest.ProtocolVersion = HttpVersion.Version11;
                tWebRequest.ContentType = "application/x-www-form-urlencoded";

                tWebRequest.KeepAlive = false;
                tWebRequest.ServicePoint.Expect100Continue = false;
                tWebRequest.ReadWriteTimeout = iWebReadWriteTimeout;
                tWebRequest.Timeout = iWebTimeout;

                //2、提交信息
                tWebRequest.ContentLength = tRequestMessage.Length;
                tRequestStream = new BufferedStream(tWebRequest.GetRequestStream());
                if (!tRequestStream.CanWrite)
                {
                    //不能连接serverUrl
                    throw new Exception("不能建立连接");
                }

                tRequestStream.Write(tRequestMessage, 0, tRequestMessage.Length);
                tRequestStream.Flush();

                //3、接收响应
                tWebResponse = (HttpWebResponse)tWebRequest.GetResponse();
                if (tWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    //交易平台未成功处理请求
                    throw new Exception("交易平台未成功处理请求");
                }

                Stream tReceiveStream = tWebResponse.GetResponseStream();
                StreamReader tStreamReader = new StreamReader(tReceiveStream);
                String tLine = null;
                String tResponseMessage = "";
                while ((tLine = tStreamReader.ReadLine()) != null)
                {
                    tResponseMessage += tLine;
                }
                tResponseByteArray = System.Text.ASCIIEncoding.UTF8.GetBytes(tResponseMessage);


                responseString = System.Text.Encoding.UTF8.GetString(tResponseByteArray);

                //解析返回字符串
                string[] parameters = responseString.Split('&');

                foreach (string param in parameters)
                {
                    string[] var = param.Split('=');
                    if (var.Length == 2)
                    {
                        string name = var[0];
                        string value = var[1];
                        value = HttpUtility.UrlDecode(value, Encoding.UTF8);
                        if (string.Compare(name, "merchantId") == 0)
                        {
                            merchantId = value;
                        }
                        if (string.Compare(name, "version") == 0)
                        {
                            version = value;
                        }
                        if (string.Compare(name, "language") == 0)
                        {
                            language = value;
                        }
                        if (string.Compare(name, "signType") == 0)
                        {
                            signType = value;
                        }
                        if (string.Compare(name, "payType") == 0)
                        {
                            payType = value;
                        }
                        if (string.Compare(name, "issuerId") == 0)
                        {
                            issuerId = value;
                        }
                        if (string.Compare(name, "paymentOrderId") == 0)
                        {
                            paymentOrderId = value;
                        }
                        if (string.Compare(name, "orderNo") == 0)
                        {
                            orderNo = value;
                        }
                        if (string.Compare(name, "orderDatetime") == 0)
                        {
                            orderDatetime = value;
                        }
                        if (string.Compare(name, "orderAmount") == 0)
                        {
                            orderAmount = value;
                        }
                        if (string.Compare(name, "payDatetime") == 0)
                        {
                            payDatetime = value;
                        }
                        if (string.Compare(name, "payAmount") == 0)
                        {
                            payAmount = value;
                        }
                        if (string.Compare(name, "ext1") == 0)
                        {
                            ext1 = value;
                        }
                        if (string.Compare(name, "ext2") == 0)
                        {
                            ext2 = value;
                        }
                        if (string.Compare(name, "payResult") == 0)
                        {
                            payResult = value;
                        }
                        if (string.Compare(name, "errorCode") == 0)
                        {
                            errorCode = value;
                        }
                        if (string.Compare(name, "returnDatetime") == 0)
                        {
                            returnDatetime = value;
                        }
                        if (string.Compare(name, "signMsg") == 0)
                        {
                            signMsg = value;
                        }

                    }
                    else if (var.Length > 2)
                    {
                        string name = var[0];
                        string value = param.Substring(name.Length + 1);
                        value = HttpUtility.UrlDecode(value, Encoding.UTF8);
                        if (string.Compare(name, "paymentOrderId") == 0)
                        {
                            paymentOrderId = value;
                        }
                        if (string.Compare(name, "orderNo") == 0)
                        {
                            orderNo = value;
                        }
                        if (string.Compare(name, "ext1") == 0)
                        {
                            ext1 = value;
                        }
                        if (string.Compare(name, "ext2") == 0)
                        {
                            ext2 = value;
                        }
                        if (string.Compare(name, "signMsg") == 0)
                        {
                            signMsg = value;
                        }
                    }
                }
                #endregion

                StringBuilder bufSignSrcVerify = new StringBuilder();

                appendSignPara(bufSignSrcVerify, "merchantId", merchantId);
                appendSignPara(bufSignSrcVerify, "version", version);
                appendSignPara(bufSignSrcVerify, "language", language);
                appendSignPara(bufSignSrcVerify, "signType", signType);
                appendSignPara(bufSignSrcVerify, "payType", payType);
                appendSignPara(bufSignSrcVerify, "issuerId", issuerId);
                appendSignPara(bufSignSrcVerify, "paymentOrderId", paymentOrderId);
                appendSignPara(bufSignSrcVerify, "orderNo", orderNo);
                appendSignPara(bufSignSrcVerify, "orderDatetime", orderDatetime);
                appendSignPara(bufSignSrcVerify, "orderAmount", orderAmount);
                appendSignPara(bufSignSrcVerify, "payDatetime", payDatetime);
                appendSignPara(bufSignSrcVerify, "payAmount", payAmount);
                appendSignPara(bufSignSrcVerify, "ext1", ext1);
                appendSignPara(bufSignSrcVerify, "ext2", ext2);
                appendSignPara(bufSignSrcVerify, "payResult", payResult);
                appendSignPara(bufSignSrcVerify, "errorCode", errorCode);
                appendLastSignPara(bufSignSrcVerify, "returnDatetime", returnDatetime);

                //查询结果返回报文，组织的签名原串
                srcMsg = bufSignSrcVerify.ToString();

                verifyResult = verify(srcMsg, signMsg, "cert/TLCert.cer", false); //证书路径请参考verify方法的注释设置 



            }
            catch (Exception exc)
            {
                //异常捕获处理
                Log4NetHelper.WriteError(exc);
            }
            finally
            {
                //4、关闭连接

                if (tRequestStream != null)
                {
                    try { tRequestStream.Close(); }
                    catch (Exception) { }
                }
                if (tWebResponse != null)
                {
                    try { tWebResponse.Close(); }
                    catch (Exception) { }
                }
            }

            return;

        }


        /**
         * 根据传入的参数做验签 
         * @param srcMsg 签名用源串
         * @param signMsg 通联响应中给出的签名串
         * @param certPath 证书路径 
         * @param isAbsolatePath 是否绝对路径，如certpath参数值为证书绝对路径，则填true，否则填false
         */
        private bool verify(String srcMsg, String signMsg, String certPath, Boolean isAbsolatePath)
        {
            //base64解码签名串
            Byte[] signMsgBytes = decode(signMsg);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //读取x509证书
            X509Certificate2 x509 = new X509Certificate2();
            if (isAbsolatePath)
            {
                //设置证书的绝对路径
                //x509.Import("cert/TLCert.cer");
                x509.Import(certPath);
            }
            else
            {
                //或者设置证书的相对路径
                //x509.Import(HttpContext.Current.Server.MapPath("../cert/TLCert.cer"));
                x509.Import(HttpContext.Current.Server.MapPath(certPath));
            }

            //x509.PublicKey.Key.ToXmlString();
            //灌注到rsa
            rsa.FromXmlString(x509.PublicKey.Key.ToXmlString(false));
            bool verifyResult = rsa.VerifyData(System.Text.Encoding.UTF8.GetBytes(srcMsg), "SHA1", signMsgBytes);

            return verifyResult;
        }

        public byte[] decode(System.String data)
        {
            mString = data;
            mIndex = 0;

            /// <summary> Total number of Base64 characters in the input
            /// </summary>
            int mUsefulLength = 0;
            int length = mString.Length;
            for (int i = 0; i < length; i++)
            {
                if (isUsefulChar(mString[i]))
                {
                    mUsefulLength++;
                }
            }

            //mString = data;


            // A Base64 byte array is 75% the size of its String representation
            int byteArrayLength = mUsefulLength * 3 / 4;

            byte[] result = new byte[byteArrayLength];

            int byteTriplet = 0;
            int byteIndex = 0;

            // Continue until we have less than 4 full characters left to
            // decode in the input.
            while (byteIndex + 2 < byteArrayLength)
            {

                // Package a set of four characters into a byte triplet
                // Each character contributes 6 bits of useful information
                byteTriplet = mapCharToInt(NextUsefulChar);
                byteTriplet <<= 6;
                byteTriplet |= mapCharToInt(NextUsefulChar);
                byteTriplet <<= 6;
                byteTriplet |= mapCharToInt(NextUsefulChar);
                byteTriplet <<= 6;
                byteTriplet |= mapCharToInt(NextUsefulChar);

                // Grab a normal byte (eight bits) out of the byte triplet
                // and put it in the byte array
                result[byteIndex + 2] = (byte)(byteTriplet & EIGHT_BIT_MASK);
                byteTriplet >>= 8;
                result[byteIndex + 1] = (byte)(byteTriplet & EIGHT_BIT_MASK);
                byteTriplet >>= 8;
                result[byteIndex] = (byte)(byteTriplet & EIGHT_BIT_MASK);
                byteIndex += 3;
            }

            // Check if we have one byte left to decode
            if (byteIndex == byteArrayLength - 1)
            {
                // Take out the last two characters from the String
                byteTriplet = mapCharToInt(NextUsefulChar);
                byteTriplet <<= 6;
                byteTriplet |= mapCharToInt(NextUsefulChar);

                // Remove the padded zeros
                byteTriplet >>= 4;
                result[byteIndex] = (byte)(byteTriplet & EIGHT_BIT_MASK);
            }

            // Check if we have two bytes left to decode
            if (byteIndex == byteArrayLength - 2)
            {
                // Take out the last three characters from the String
                byteTriplet = mapCharToInt(NextUsefulChar);
                byteTriplet <<= 6;
                byteTriplet |= mapCharToInt(NextUsefulChar);
                byteTriplet <<= 6;
                byteTriplet |= mapCharToInt(NextUsefulChar);

                // Remove the padded zeros
                byteTriplet >>= 2;
                result[byteIndex + 1] = (byte)(byteTriplet & EIGHT_BIT_MASK);
                byteTriplet >>= 8;
                result[byteIndex] = (byte)(byteTriplet & EIGHT_BIT_MASK);
            }

            return result;
        }

        /// <param name="c">Character to be examined
        /// </param>
        /// <returns> Whether or not the character is a Base64 character
        /// 
        /// </returns>
        private bool isUsefulChar(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || (c == '+') || (c == '/');
        }

        /// <summary> Convert a Base64 character to its 6 bit value as defined by the mapping.
        /// </summary>
        /// <param name="c">Base64 character to decode
        /// </param>
        /// <returns> int representation of 6 bit value
        /// 
        /// </returns>
        private int mapCharToInt(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return c - 'A';
            }

            if (c >= 'a' && c <= 'z')
            {
                return (c - 'a') + LOWER_CASE_A_VALUE;
            }

            if (c >= '0' && c <= '9')
            {
                return (c - '0') + ZERO_VALUE;
            }

            if (c == '+')
            {
                return PLUS_VALUE;
            }

            if (c == '/')
            {
                return SLASH_VALUE;
            }

            throw new System.ArgumentException(c + " is not a valid Base64 character.");
        }


        /// <summary> Convert a byte between 0 and 63 to its Base64 character equivalent
        /// </summary>
        /// <param name="b">Byte value to be converted
        /// </param>
        /// <returns> Base64 char value
        /// 
        /// </returns>
        private char mapByteToChar(byte b)
        {
            if (b < LOWER_CASE_A_VALUE)
            {
                return (char)('A' + b);
            }

            if (b < ZERO_VALUE)
            {
                return (char)('a' + (b - LOWER_CASE_A_VALUE));
            }

            if (b < PLUS_VALUE)
            {
                return (char)('0' + (b - ZERO_VALUE));
            }

            if (b == PLUS_VALUE)
            {
                return '+';
            }

            if (b == SLASH_VALUE)
            {
                return '/';
            }

            throw new System.ArgumentException("Byte " + b + " is not a valid Base64 value");
        }

        /// <summary> Traverse the String until hitting the next Base64 character.
        /// Assumes that there is still another valid Base64 character
        /// left in the String.
        /// </summary>
        private char NextUsefulChar
        {
            get
            {
                char result = '_'; // Start with a non-Base64 character
                while (!isUsefulChar(result))
                {
                    result = mString[mIndex++];
                }

                return result;
            }

        }



        //---------------------------------------以下代码请勿更动------------------------------------------------------------

        private bool isEmpty(String src)
        {
            if (null == src || "".Equals(src) || "-1".Equals(src))
            {
                return true;
            }
            return false;
        }

        private void appendSignPara(System.Text.StringBuilder buf, String key, String value)
        {
            if (!isEmpty(value))
            {
                buf.Append(key).Append('=').Append(value).Append('&');
            }
        }

        private void appendLastSignPara(System.Text.StringBuilder buf, String key,
                String value)
        {
            if (!isEmpty(value))
            {
                buf.Append(key).Append('=').Append(value);
            }
        }


        //for 2.0
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   //   Always   accept  
            return true;
        }
        //for 1.1
        internal class AcceptAllCertificatePolicy : ICertificatePolicy
        {
            public AcceptAllCertificatePolicy()
            {
            }

            public bool CheckValidationResult(ServicePoint sPoint, System.Security.Cryptography.X509Certificates.X509Certificate cert, WebRequest wRequest, int certProb)
            {
                //   Always   accept  
                return true;
            }
        }
    }
}