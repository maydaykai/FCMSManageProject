using LuckyDraw.Business;
using LuckyDraw.BusinessModel;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.LuckyDraw
{
    /// <summary>
    /// GetTotalCount 的摘要说明
    /// </summary>
    public class GetTotalCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(context.Request["id"]);
            BizSweepstake bs = new BizSweepstake();
            SweepstakeModel s = bs.GetSweepstake(id);
            BizBidding bb = new BizBidding();
            int count = bb.GetAllCount(s.StartDate, s.EndDate, 5000, s.OutUnit);
            context.Response.Write(JsonHelper.ObjectToJson(new { count = count }));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}