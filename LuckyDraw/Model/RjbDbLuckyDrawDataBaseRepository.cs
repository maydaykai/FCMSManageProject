using System.Configuration;
using ManageFcmsCommon;

namespace LuckyDraw.Model
{
    public class RjbDbLuckyDrawDataBaseRepository : RjbDbLuckyDrawDataContext
    {
        private static readonly string ConnString = StringHelper.DesDecrypt(
            ConfigurationManager.AppSettings["FcmsConn"], "87654321");

        public RjbDbLuckyDrawDataBaseRepository()
            : base(ConnString)
        {
        }
    }
}