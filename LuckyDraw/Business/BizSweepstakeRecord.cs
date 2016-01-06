using System.Collections.Generic;
using LuckyDraw.BusinessModel;
using LuckyDraw.DataAccess;
using LuckyDraw.Model;
using System;

namespace LuckyDraw.Business
{
    public class BizSweepstakeRecord
    {
        public static List<Report> GetReport(int sId, DateTime sd, DateTime ed)
        {
            return SweepstakeRecordController.GetReport(sId, sd, ed);
        }

        public List<AwardWinnerModel> GetAllBySId(int sid)
        {
            return SweepstakeRecordController.GetAllBySId(sid);
        }
    }
}