using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LuckyDraw.BusinessModel;
using LuckyDraw.DataAccess.Generic;
using LuckyDraw.Model;

namespace LuckyDraw.DataAccess
{
    public class SweepstakeController : GenericController<Sweepstake, RjbDbLuckyDrawDataBaseRepository>
    {
        public static SweepstakeModel GetSweepstake(int id)
        {
            var sweepstakeModel = from m in DataContext.Sweepstake
                                  join p in DataContext.Prize on m.SweepstakeId equals p.SweepstakeId into prizes
                                  where m.SweepstakeId == id
                                  select new SweepstakeModel
                                  {
                                      SweepstakeId = m.SweepstakeId,
                                      EndDate = m.EndDate,
                                      StartDate = m.StartDate,
                                      SweepstakeFactor = m.SweepstakeFactor,
                                      SweepstakeName = m.SweepstakeName,
                                      Prizes = prizes.ToList(),
                                      Unit = m.Unit,
                                      OutUnit=m.OutUnit
                                  };
            return sweepstakeModel.FirstOrDefault();
        }

        public static ExecuteResult CreateSweepstake(Sweepstake sweepstake)
        {
            var result = new ExecuteResult();

            Monitor.Enter(MObjLock);
            try
            {
                //IQueryable<Sweepstake> activeSweepstake = from o in DataContext.Sweepstake
                //                                          where DateTime.Now < o.EndDate.AddDays(1)
                //                                          select o;

                //if (activeSweepstake.Any()) //没有将要或者正在进行的抽奖
                //{
                //    result.Error.Add("还有尚未结束的抽奖活动");
                //}
                //else
                //{
                DataContext.Sweepstake.InsertOnSubmit(sweepstake);
                SubmitChanges();
                //}
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

        public static List<Sweepstake> GetAllSweepstake()
        {
            Monitor.Enter(MObjLock);
            try
            {
                return DataContext.Sweepstake.ToList();
            }
            catch (Exception err)
            {
                return new List<Sweepstake>();
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }

        internal static ExecuteResult UpdateSweepstake(Sweepstake sweepstake)
        {
            var result = new ExecuteResult();

            Monitor.Enter(MObjLock);
            try
            {
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

        internal static List<Sweepstake> GetALLSweepstake()
        {
            return SelectAll().ToList();
        }
    }
}