using System.Linq;
using System.Threading;
using SignCert.DataAccess.Generic;
using SignCert.Model;

namespace SignCert.DataAccess
{
    public class UserCertController : GenericController<UserCert, CertDataBaseRepository>
    {
        /// <summary>
        ///     获取用户可用的证书信息
        /// </summary>
        /// <param name="userId"></param>
        public static UserCert GetUserAvaibleCert(int userId)
        {
            Monitor.Enter(MObjLock);
            try
            {
                UserCert result = (from o in DataContext.UserCert
                                   where o.Avaible
                                   where o.UserId == userId
                                   select o).AsQueryable().FirstOrDefault();

                return result;
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }

        /// <summary>
        /// 在系统里增加用户可用证书序列
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="certSerialNumber"></param>
        public static void AddUserAvaibleCert(int userId, string certSerialNumber)
        {
            Monitor.Enter(MObjLock);
            try
            {
                var userCert = new UserCert { UserId = userId, CertSerialNumber = certSerialNumber, Avaible = true };
                Insert(userCert);
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }

        /// <summary>
        /// 使用户的证书不可用
        /// </summary>
        /// <param name="userId"></param>
        public static void SetUserCertUnavaible(int userId)
        {
            Monitor.Enter(MObjLock);
            try
            {
                var result = (from o in DataContext.UserCert
                              where o.Avaible
                              where o.UserId == userId
                              select o).AsQueryable();

                foreach (var userCert in result)
                {
                    userCert.Avaible = false;
                }

                SubmitChanges();
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }
    }
}