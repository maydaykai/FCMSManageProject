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
    /// LoadDraw 的摘要说明
    /// </summary>
    public class LoadDraw : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(context.Request["id"]);
            BizSweepstake bs = new BizSweepstake();
            SweepstakeModel model = bs.GetSweepstake(id);
            foreach (var item in model.Prizes)
            {
                item.SweepstakeRecord = null;
                item.Sweepstake = null;
            }
            context.Response.Write(JsonHelper.ObjectToJson(model));
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