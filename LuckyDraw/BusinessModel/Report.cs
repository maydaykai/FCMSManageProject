using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.BusinessModel
{
    public class Report
    {
        /// <summary>
        /// 奖项名称
        /// </summary>
        public string PrizeName { get; set; }
        public int PrizeId { get; set; }
        /// <summary>
        /// 中奖个数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 中奖金额
        /// </summary>
        public decimal TotaL { get; set; }
    }

    public class RecordPaged
    {
        public int TotalRows { get; set; }
        public List<AwardWinnerModel> Rows { get; set; }
    }
}
