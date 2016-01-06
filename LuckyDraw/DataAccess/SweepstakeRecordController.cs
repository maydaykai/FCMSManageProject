using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LuckyDraw.BusinessModel;
using LuckyDraw.DataAccess.Generic;
using LuckyDraw.Model;
using ManageFcmsCommon;

namespace LuckyDraw.DataAccess
{
    public class SweepstakeRecordController : GenericController<SweepstakeRecord, RjbDbLuckyDrawDataBaseRepository>
    {
        public static RecordPaged GetAllAwardWinner(int sweepstakeId, int pageSize, int pageIndex, string title = "")
        {
            Monitor.Enter(MObjLock);
            try
            {
                RecordPaged p = new RecordPaged();
                var Records = from o in DataContext.SweepstakeRecord
                              where o.SweepstakeId == sweepstakeId
                              select o;
                if (!string.IsNullOrEmpty(title))
                {
                    Records = from r in Records
                              join m in DataContext.Member on r.MemberID equals m.ID
                              where m.MemberName == title
                              select r;
                }
                p.TotalRows = Records.Count();
                var q = Records.Skip(pageSize * pageIndex).Take(pageSize);
                IQueryable<AwardWinnerModel> data = from r in q
                                                    join m in DataContext.Member on r.MemberID equals m.ID
                                                    join info in DataContext.MemberInfo on m.ID equals info.MemberID
                                                    select new AwardWinnerModel
                                                        {
                                                            SweepstakeName = r.Sweepstake == null ? string.Empty : r.Sweepstake.SweepstakeName,
                                                            PrizeName = r.Prize == null ? string.Empty : r.Prize.PrizeName,
                                                            MemberName = m.MemberName,
                                                            CreateDate = r.CreateDate,
                                                            Phone = m.Mobile,
                                                            RealName = info.RealName
                                                        };

                p.Rows = data.ToList();
                return p;

                //var query = from r in DataContext.SweepstakeRecord
                //            join m in DataContext.Member on r.MemberID equals m.ID
                //            join s in DataContext.Sweepstake on r.SweepstakeId equals s.SweepstakeId
                //            join p in DataContext.Prize on s.SweepstakeId equals p.SweepstakeId
                //            where s.SweepstakeId == sweepstakeId
                //            select new AwardWinnerModel
                //            {
                //                SweepstakeName = s.SweepstakeName,
                //                PrizeName = p.PrizeName,
                //                MemberName = m.MemberName
                //            };

                //return query.ToList();
            }
            catch (Exception err)
            {
                return null;
            }
            finally
            {
                Monitor.Exit(MObjLock);
            }
        }

        internal static List<Report> GetReport(int sId, DateTime sd, DateTime ed)
        {

            var query = DataContext.SweepstakeRecord.Where(m => m.SweepstakeId == sId && m.CreateDate >= sd && m.CreateDate <= ed).GroupBy(g => g.PrizeId).Select(d => new { Count = d.Count(), PrizeId = d.Key }).ToList();

            List<Report> list = new List<Report>();
            foreach (var item in query)
            {
                Report r = new Report();
                r.Count = item.Count;
                Prize p = PrizeController.GetPrize((int)item.PrizeId);
                r.PrizeName = p.PrizeName;
                r.TotaL = 0;
                if (p.PrizeName.Contains("元现金红包"))
                {
                    decimal temp = Convert.ToDecimal(p.PrizeName.Substring(0, p.PrizeName.IndexOf("元现金红包")));
                    r.TotaL = temp * item.Count;
                }
                list.Add(r);
            }
            return list;
        }

        internal static List<AwardWinnerModel> GetAllBySId(int sid)
        {
            var Records = from o in DataContext.SweepstakeRecord
                          where o.SweepstakeId == sid
                          select o;
            IQueryable<AwardWinnerModel> data = from r in Records
                                                join m in DataContext.Member on r.MemberID equals m.ID
                                                join info in DataContext.MemberInfo on m.ID equals info.MemberID
                                                select new AwardWinnerModel
                                                {
                                                    SweepstakeName = r.Sweepstake == null ? string.Empty : r.Sweepstake.SweepstakeName,
                                                    PrizeName = r.Prize == null ? string.Empty : r.Prize.PrizeName,
                                                    MemberName = m.MemberName,
                                                    CreateDate = r.CreateDate,
                                                    Phone = m.Mobile,
                                                    RealName = info.RealName
                                                };

            return data.ToList();
        }
    }
}