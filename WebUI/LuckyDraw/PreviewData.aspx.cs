using System;
using System.Collections.Generic;
using System.Web.UI;
using LuckyDraw.BusinessModel;

namespace WebUI.LuckyDraw
{
    public partial class PreviewData : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var previewData = Session["PreviewData"] as SweepstakeModel;

            string data = GetData(previewData);
            Response.Write(data);
            Response.End();
        }

        private string GetData(SweepstakeModel sweepstakeModel)
        {
            int sweepstakeResult = GetSweepstakeResult(sweepstakeModel); //从1开始

            int roate = (sweepstakeResult - 1)*60;
            string prizeName = sweepstakeModel.Prizes[sweepstakeResult - 1].PrizeName;

            string data = string.Format("{{\"isHasChance\":\"true\",\"rotate\":{0},\"results\":\"{1}\"}}",
                                        roate, prizeName);
            return data;
        }


        private int GetSweepstakeResult(SweepstakeModel sweepstakeModel)
        {
            int sf = sweepstakeModel.SweepstakeFactor; //中奖系数
            double realSf = Math.Pow(Math.PI/2, Convert.ToDouble(sf)); //真实的中奖因子

            var points = new List<double>();
            for (int i = 0; i < sweepstakeModel.Prizes.Count; i++)
            {
                int v = sweepstakeModel.Prizes[i].PrizeNumber;

                points.Add(0); //默认值
                points[i] = v*Math.Pow(realSf, i + 1);
            }

            double sumPoints = 0;
            points.ForEach(p => sumPoints += p);

            //获取默认的奖品选项
            int defaultIx = 0;
            for (int m = 0; m < sweepstakeModel.Prizes.Count; m++)
            {
                if (sweepstakeModel.Prizes[m].PrizeDefault)
                {
                    defaultIx = m;
                    break;
                }
            }

            //开始随机抽奖
            var rd = new Random();
            double rdNumber = rd.NextDouble()*sumPoints;
            double rdRealNumber = 0;
            int resultIx = defaultIx; //初始化即为默认的中奖项
            for (int j = 0; j < points.Count; j++) //找出随机数对应的数据阶梯分布
            {
                rdRealNumber += points[j];
                if (rdRealNumber >= rdNumber)
                {
                    resultIx = j;
                    break;
                }
            }


            //特殊判断

            //处理已经抽奖完毕的选项
            int prizeNumber = sweepstakeModel.Prizes[resultIx].PrizeNumber;
            if (prizeNumber <= 0) //如果该奖品剩余数目不大于0
            {
                resultIx = defaultIx;
            }
            else
            {
                //处理两次时间间隔选项
                double splitTime = (sweepstakeModel.EndDate.AddDays(1) - sweepstakeModel.StartDate).TotalHours/
                                   sweepstakeModel.Prizes[resultIx].PrizeNumber;

                //从数据库获取上次中奖的时间
                DateTime lastTime = DateTime.MinValue; //***因为是演示，这里暂时硬编码

                double realSplit = (DateTime.Now - lastTime).TotalHours;
                if (realSplit <= splitTime) //如果小于时间间隔
                {
                    resultIx = defaultIx;
                }
            }

            //***因为是演示，此处省略将中奖结果放入数据库


            return resultIx + 1;
        }
    }
}