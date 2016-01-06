using System;
using System.Collections.Generic;
using LuckyDraw.Model;

namespace LuckyDraw.BusinessModel
{
    public class SweepstakeModel
    {
        public int SweepstakeId { get; set; }
        public string SweepstakeName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int SweepstakeFactor { get; set; }

        public List<Prize> Prizes { get; set; }
        public decimal? Unit { get; set; }

        public decimal? OutUnit { get; set; }
    }
}