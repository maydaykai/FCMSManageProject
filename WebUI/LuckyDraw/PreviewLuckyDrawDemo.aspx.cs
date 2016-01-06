using System;
using System.Collections.Generic;
using System.Web.UI;
using LuckyDraw.BusinessModel;
using LuckyDraw.Common;

namespace WebUI.LuckyDraw
{
    public partial class Preview : Page
    {
        public readonly List<String> Prizes = new List<string>(CommonData.PrizeKindCount);

        protected void Page_Load(object sender, EventArgs e)
        {
            var previewData = Session["PreviewData"] as SweepstakeModel;

            if (previewData != null)
            {
                for (int i = 0; i < previewData.Prizes.Count && i < CommonData.PrizeKindCount; i++)
                {
                    Prizes.Add(previewData.Prizes[i].PrizeName);
                }
            }
        }
    }
}