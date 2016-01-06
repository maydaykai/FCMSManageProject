using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SignCert.SignCertWindowServiceProxy;

namespace SignCert.Common
{
    public class SignCertServiceProxy
    {
        private static CommandServiceClient Scwsp = null;

        public static CommandServiceClient GetProxy()
        {
            if (Scwsp == null)
            {
                Scwsp = new CommandServiceClient();
            }

            if (Scwsp.State != CommunicationState.Opened)
            {
                Scwsp.Open();
            }

            return Scwsp;
        }
    }
}
