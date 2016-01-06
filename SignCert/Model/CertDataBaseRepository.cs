using System.Configuration;
using ManageFcmsCommon;
using SignCert.Common;

namespace SignCert.Model
{
    public class CertDataBaseRepository : CertDataBaseDataContext
    {
        private static readonly string ConnString = CommonData.EnableEncodeConnString ?
            StringHelper.DesDecrypt(ConfigurationManager.AppSettings["CertConnectionString"], "87654321") : ConfigurationManager.AppSettings["CertConnectionString"];

        public CertDataBaseRepository()
            : base(ConnString)
        {
        }
    }
}