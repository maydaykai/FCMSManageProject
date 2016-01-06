using System.Collections.Generic;
using LuckyDraw.BusinessModel;
using LuckyDraw.DataAccess;
using LuckyDraw.Model;
using System;

namespace LuckyDraw.Business
{
    public class BizBidding
    {
        public int GetAllCount(DateTime sd, DateTime ed,decimal? money,decimal? outUnit=0)
        {
            return BiddingController.GetChance(sd, ed, money,outUnit);
        }
    }
}