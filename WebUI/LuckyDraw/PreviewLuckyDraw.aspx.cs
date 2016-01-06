using System;
using System.Collections.Generic;
using System.Web.UI;
using LuckyDraw.BusinessModel;
using LuckyDraw.Common;
using LuckyDraw.Model;

namespace WebUI.LuckyDraw
{
    public partial class PreviewLuckyDraw : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateNewSweepstake();
        }

        private void CreateNewSweepstake()
        {
            var sweepstake = new SweepstakeModel
                {
                    SweepstakeName = Request.Form["txtSweepstakeName"] ?? string.Empty,
                    StartDate = Convert.ToDateTime(Request.Form["StartDate"] ?? string.Empty),
                    EndDate = Convert.ToDateTime(Request.Form["EndDate"] ?? string.Empty),
                    SweepstakeFactor = Convert.ToInt32(Request.Form["SweepstakeFactor"] ?? "0"),
                    Prizes = new List<Prize>()
                };

            for (int i = 1; i <= CommonData.PrizeKindCount; i++)
            {
                var p = new Prize
                    {
                        PrizeNumber = ConvertToInt(Request.Form[string.Format("txtPrize{0}Number", i)] ?? "0"),
                        PrizeName = Request.Form[string.Format("txtPrize{0}", i)] ?? string.Empty,
                        IntervalTime = Convert.ToDecimal(Request.Form[string.Format("txtPrize{0}TimeSpan", i)] ?? "0"),
                        WinningRate = Convert.ToDecimal(Request.Form[string.Format("SweepstakeOdds{0}", i)] ?? "0"),
                        PrizeDefault = false
                    };
                sweepstake.Prizes.Add(p);
            }

            int prizeDefault = Convert.ToInt32(Request.Form["rdPrizeDefault"] ?? "0") - 1;
            sweepstake.Prizes[prizeDefault].PrizeDefault = true;

            Session["PreviewData"] = sweepstake;
        }

        private int ConvertToInt(string value)
        {
            int data;
            int.TryParse(value, out data);
            return data;
        }
    }
}