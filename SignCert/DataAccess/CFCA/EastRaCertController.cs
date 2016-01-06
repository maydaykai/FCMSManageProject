using System.Linq;
using System.Threading;
using SignCert.DataAccess.Generic;
using SignCert.Model;

namespace SignCert.DataAccess.CFCA
{
    public class EastRaCertController : GenericController<TBL_EASTRACERT, CfcaDataBaseRepository>
    {
        /// <summary>
        ///     从签章系统获取真实的证书信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TBL_EASTRACERT GetCertBySn(string id)
        {
            Monitor.Enter(MObjLock);
            try
            {
                TBL_EASTRACERT result = (from o in DataContext.TBL_EASTRACERT
                                         where o.ID.Equals(id)
                                         select o).AsQueryable().FirstOrDefault();

                return result;
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }
    }
}