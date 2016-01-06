using System.Configuration;
using ManageFcmsCommon;
using SignCert.Common;

namespace SignCert.Model
{
    public class CfcaDataBaseRepository : CfcaDataBaseDataContext
    {
        private static readonly string ConnString = CommonData.EnableEncodeConnString ?
            StringHelper.DesDecrypt(ConfigurationManager.AppSettings["qianzhangConnectionString"], "87654321") : ConfigurationManager.AppSettings["qianzhangConnectionString"];

        public CfcaDataBaseRepository()
            : base(ConnString)
        {
        }
    }
}