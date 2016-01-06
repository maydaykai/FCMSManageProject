using System;
using System.Security.Cryptography;
using System.Text;

namespace SignCert.Common
{
    public class CertCommon
    {
        /// <summary>
        ///     证书解密
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="mStrDecryptString"></param>
        /// <returns></returns>
        public static string RsaDecrypt(string xmlPrivateKey, string mStrDecryptString)
        {
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPrivateKey);
            byte[] rgb = Convert.FromBase64String(mStrDecryptString);
            byte[] bytes = provider.Decrypt(rgb, false);
            return new UnicodeEncoding().GetString(bytes);
        }

        /// <summary>
        ///     证书加密
        /// </summary>
        /// <param name="xmlPublicKey"></param>
        /// <param name="mStrEncryptString"></param>
        /// <returns></returns>
        public static string RsaEncrypt(string xmlPublicKey, string mStrEncryptString)
        {
            var provider = new RSACryptoServiceProvider();

            provider.FromXmlString(xmlPublicKey);
            byte[] bytes = new UnicodeEncoding().GetBytes(mStrEncryptString);
            return Convert.ToBase64String(provider.Encrypt(bytes, false));
        }
    }
}