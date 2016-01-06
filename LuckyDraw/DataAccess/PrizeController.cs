using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LuckyDraw.BusinessModel;
using LuckyDraw.DataAccess.Generic;
using LuckyDraw.Model;

namespace LuckyDraw.DataAccess
{
    public class PrizeController : GenericController<Prize, RjbDbLuckyDrawDataBaseRepository>
    {

        internal static ExecuteResult UpdatePrize(Prize sweepstake)
        {
            var result = new ExecuteResult();

            Monitor.Enter(MObjLock);
            try
            {
                if (sweepstake.PrizeId == 0)
                    Insert(sweepstake);
                else
                    Update(sweepstake);
            }
            catch (Exception err)
            {
                result.Error.Add("数据已过时，请刷新页面后重新尝试");
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }

            return result;
        }
        public static Prize GetPrize(int id)
        {
            return DataContext.Prize.FirstOrDefault(m => m.PrizeId == id);
        }
        public static void DelAllPrizeBySID(int sid)
        {
            if (sid != 0)
            {
                var query = DataContext.Prize.Where(m => m.SweepstakeId == sid);
                DataContext.Prize.DeleteAllOnSubmit(query);
            }
        }
    }
}