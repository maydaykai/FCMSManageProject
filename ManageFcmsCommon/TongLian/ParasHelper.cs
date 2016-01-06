using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsCommon.TongLian
{
    public class ParasHelper
    {
        public static string userName
        {
            get
            {
                return ConfigurationManager.AppSettings["userName"];
            }
        }
        public static string password
        {
            get
            {
                return ConfigurationManager.AppSettings["password"];
            }
        }
        public static string merchantId
        {
            get
            {
                return ConfigurationManager.AppSettings["merchantId"];
            }
        }
        public static string businessCode
        {
            get
            {
                return ConfigurationManager.AppSettings["businessCode"];
            }
        }
        public static string url
        {
            get
            {
                return ConfigurationManager.AppSettings["url"];
            }
        }
        private static string SignCertUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["signCertUrl"];//"D:\allinpay-pds\20058400000923704.p12";
            }
        }
        private static string VerifyCertUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["verifyCertUrl"]; //return new X509Certificate2(@"D:\allinpay-pds\allinpay-pds.cer");
            }
        }
        public static X509Certificate2 SignCert
        {
            get
            {
                return new X509Certificate2(@SignCertUrl, "111111", X509KeyStorageFlags.MachineKeySet);
            }
        }
        public static X509Certificate2 VerifyCert
        {
            get
            {
                return new X509Certificate2(@VerifyCertUrl);
            }
        }
    }
}
