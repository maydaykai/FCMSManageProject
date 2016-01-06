using System;

namespace LuckyDraw.BusinessModel
{
    public class AwardWinnerModel
    {
        public string SweepstakeName { get; set; }

        public string PrizeName { get; set; }

        public string MemberName { get; set; }
        public string RealName { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
    }
}