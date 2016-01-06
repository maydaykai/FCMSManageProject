using System;

namespace LuckyDraw.BusinessModel
{
    public class SweepstakeHistoryMode
    {
        public int SweepstakeId { get; set; }

        public string SweepstakeName { get; set; }

        public DateTime StartDate { get; set; }

        public string StartDateDesc
        {
            get { return StartDate.ToString("yyyy-MM-dd"); }//yyyy-MM-dd HH:mm:ss
        }

        public DateTime EndDate { get; set; }

        public string EndDateDesc
        {
            get { return EndDate.ToString("yyyy-MM-dd"); }
        }

        public string Status
        {
            get
            {
                string result;

                if (DateTime.Now <= StartDate)
                {
                    result = "未开始";
                }
                else if (DateTime.Now >= StartDate && DateTime.Now < EndDate.AddDays(1))
                {
                    result = "进行中";
                }
                else
                {
                    result = "已结束";
                }

                return result;
            }
        }
    }
}