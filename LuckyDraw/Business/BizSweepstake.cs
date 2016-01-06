using System.Collections.Generic;
using LuckyDraw.BusinessModel;
using LuckyDraw.DataAccess;
using LuckyDraw.Model;

namespace LuckyDraw.Business
{
    public class BizSweepstake
    {
        public static readonly BizSweepstake Instance = new BizSweepstake();

        public List<Sweepstake> GetALLSweepstake()
        {
            return SweepstakeController.GetALLSweepstake();
        }

        public SweepstakeModel GetSweepstake(int id)
        {
            return SweepstakeController.GetSweepstake(id);
        }

        public ExecuteResult CreateSweepstake(SweepstakeModel sweepstakeModel)
        {
            var sweepstake = new Sweepstake
                {
                    SweepstakeName = sweepstakeModel.SweepstakeName,
                    StartDate = sweepstakeModel.StartDate,
                    EndDate = sweepstakeModel.EndDate,
                    SweepstakeFactor = sweepstakeModel.SweepstakeFactor,
                    Unit = sweepstakeModel.Unit,
                    OutUnit = sweepstakeModel.OutUnit
                };

            //DTO
            sweepstakeModel.Prizes.ForEach(p => sweepstake.Prize.Add(p));

            return SweepstakeController.CreateSweepstake(sweepstake);
        }


        public static List<SweepstakeHistoryMode> GetAllSweepstake()
        {
            List<Sweepstake> data = SweepstakeController.GetAllSweepstake();
            var result = new List<SweepstakeHistoryMode>();
            data.ForEach(p => result.Add(new SweepstakeHistoryMode
                {
                    SweepstakeId = p.SweepstakeId,
                    SweepstakeName = p.SweepstakeName,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                }));

            return result;
        }

        public static RecordPaged GetAllAwardWinner(int sweepstakeId, int pageSize, int pageIndex, string title = "")
        {
            return SweepstakeRecordController.GetAllAwardWinner(sweepstakeId, pageSize, pageIndex, title);
        }

        public ExecuteResult UpdateSweepstake(SweepstakeModel sweepstakeModel)
        {
            var sweepstake = new Sweepstake
            {
                SweepstakeId = sweepstakeModel.SweepstakeId,
                SweepstakeName = sweepstakeModel.SweepstakeName,
                StartDate = sweepstakeModel.StartDate,
                EndDate = sweepstakeModel.EndDate,
                SweepstakeFactor = sweepstakeModel.SweepstakeFactor,
                Unit = sweepstakeModel.Unit,
                OutUnit = sweepstakeModel.OutUnit
            };

            //DTO
            //PrizeController.DelAllPrizeBySID(sweepstake.SweepstakeId);
            foreach (var item in sweepstakeModel.Prizes)
            {
                if (item.PrizeId == 0)
                    PrizeController.Insert(item);
                else if (!string.IsNullOrEmpty(item.PrizeName))
                {
                    PrizeController.Update(item);
                }
                else
                {
                    PrizeController.Delete(item);
                }
            }
            return SweepstakeController.UpdateSweepstake(sweepstake);
        }
    }
}