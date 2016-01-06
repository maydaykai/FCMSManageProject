using LuckyDraw.DataAccess.Generic;
using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuckyDraw.DataAccess
{
    public class BiddingController : GenericController<Bidding, RjbDbLuckyDrawDataBaseRepository>
    {

        /// <summary>
        /// 抽奖次数
        /// </summary>
        /// <returns></returns>
        public static int GetChance(DateTime sd, DateTime ed, decimal? money, decimal? outUnit = 0)
        {
            int count = 0;
            Monitor.Enter(MObjLock);
            try
            {
                ed = ed.AddDays(1);
                //var query = DataContext.Bidding.Where(m => m.BidAmount >= money && m.CreateTime >= sd && m.CreateTime <= ed && !m.IsTransfer);
                var query = from m in DataContext.Bidding
                            join l in DataContext.Loan on m.LoanID equals l.ID
                            where m.BidAmount >= money && m.CreateTime >= sd && m.CreateTime <= ed && m.CALoanID == 0 && l.LoanTypeID != 6 && !m.IsTransfer
                            select m;
                List<decimal> list = new List<decimal>();
                //if (outUnit != 0 && outUnit != null)
                //{
                //    foreach (var item in query)
                //    {
                //        list.Add((decimal)(item.BidAmount % outUnit));
                //    }
                //    count = (int)list.Sum(m => Math.Floor((double)m / (double)money));
                //}
                //else
                //{
                    count = (int)query.Sum(m => Math.Floor((double)m.BidAmount / (double)money));
                //}
            }
            catch (Exception err)
            {
                count = 0;
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
            return count;
        }
    }
}
