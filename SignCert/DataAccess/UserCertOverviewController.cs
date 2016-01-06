using System;
using System.Linq;
using System.Threading;
using ManageFcmsCommon;
using SignCert.DataAccess.Generic;
using SignCert.Model;

namespace SignCert.DataAccess
{
    public class UserCertOverviewController : GenericController<UserCertOverview, CertDataBaseRepository>
    {
        /// <summary>
        ///     获取用户证书状态总览，如是否同意自动签章以及签章次数等
        ///     如果没有用户的记录，则自动添加
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserCertOverview GetOrAddUserCertOverviewByUserId(int userId)
        {
            Monitor.Enter(MObjLock);
            try
            {
                UserCertOverview result = (from o in DataContext.UserCertOverview
                                           where o.UserId == userId
                                           select o).FirstOrDefault();
                if (result == null)
                {
                    var uco = new UserCertOverview { UserId = userId, SignedNumber = 0 };
                    Insert(uco);
                    return uco;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteError(err);
                return new UserCertOverview();
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }

        /// <summary>
        /// 设置用户是否启用自动托管
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="enable"></param>
        public static void SetAutoSignStatus(int userId, bool enable)
        {
            Monitor.Enter(MObjLock);
            try
            {
                UserCertOverview result = GetOrAddUserCertOverviewByUserId(userId);
                result.AgreeAutoSignTime = DateTime.Now;
                result.AgreeAutoSign = enable;
                Update(result);
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteError(err);
                return;
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }

        /// <summary>
        /// 此处容易并发
        /// </summary>
        /// <param name="userId"></param>
        public static void AddSignNumber(int userId)
        {
            Monitor.Enter(MObjLock);
            try
            {
                UserCertOverview result = GetOrAddUserCertOverviewByUserId(userId);
                if (result != null)
                {
                    result.SignedNumber++;
                    Update(result);
                }
            }
            catch (Exception err)
            {
                Log4NetHelper.WriteError(err);
                return;
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }
    }
}