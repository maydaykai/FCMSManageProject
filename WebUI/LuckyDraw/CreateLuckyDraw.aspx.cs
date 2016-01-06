using System;
using System.Collections.Generic;
using System.Web.UI;
using LuckyDraw.Business;
using LuckyDraw.BusinessModel;
using LuckyDraw.Common;
using LuckyDraw.Model;

namespace WebUI.LuckyDraw
{
    /// <summary>
    /// 添加
    /// </summary>
    public partial class CreateLuckyDraw : Page
    {
        protected string _error = string.Empty;

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
                    Prizes = new List<Prize>(),
                    Unit = Convert.ToDecimal(Request.Form["Unit"] ?? "0"),
                    OutUnit = Convert.ToDecimal(Request.Form["OutUnit"] ?? "0")
                };

            for (int i = 1; i <= CommonData.PrizeKindCount; i++)
            {
                var p = new Prize
                    {
                        PrizeNumber = ConvertToInt(Request.Form[string.Format("txtPrize{0}Number", i)] ?? "0"),//奖品数量
                        PrizeName = Request.Form[string.Format("txtPrize{0}", i)] ?? string.Empty,//奖品名称
                        IntervalTime = Convert.ToDecimal(Request.Form[string.Format("txtPrize{0}TimeSpan", i)] ?? "0") / 60,//中奖时间间隔
                        WinningRate = Convert.ToDecimal(Request.Form[string.Format("SweepstakeOdds{0}", i)] ?? "0"),//中奖率
                        PrizeDefault = false
                    };
                if (!string.IsNullOrEmpty(p.PrizeName))
                {
                    sweepstake.Prizes.Add(p);
                }
                else
                {
                    break;
                }
            }

            int prizeDefault = Convert.ToInt32(Request.Form["rdPrizeDefault"] ?? "0") - 1;
            sweepstake.Prizes[prizeDefault].PrizeDefault = true;

            ExecuteResult result = BizSweepstake.Instance.CreateSweepstake(sweepstake);

            if (!result.Success)
            {
                _error = result.Error[0];
            }
        }

        private int ConvertToInt(string value)
        {
            int data;
            int.TryParse(value, out data);
            return data;
        }
    }
}