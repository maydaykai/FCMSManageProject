using LuckyDraw.Business;
using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WebUI.LuckyDraw
{
    public partial class ViewAwardWinners : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BizSweepstake bs = new BizSweepstake();
            List<Sweepstake> list = bs.GetALLSweepstake();
            selType.DataSource = list;
            selType.DataTextField = "SweepstakeName";
            selType.DataValueField = "SweepstakeId";
            selType.DataBind();
        }
    }
}