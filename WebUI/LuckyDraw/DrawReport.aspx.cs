using LuckyDraw.Business;
using LuckyDraw.BusinessModel;
using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WebUI.LuckyDraw
{
    public partial class DrawReport : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BizSweepstake bs = new BizSweepstake();
            List<Sweepstake> list = bs.GetALLSweepstake();
            selType.DataSource = list;
            selType.DataTextField = "SweepstakeName";
            selType.DataValueField = "SweepstakeId";
            selType.DataBind();
            
            BizBidding bb = new BizBidding();
            int count = bb.GetAllCount(list[0].StartDate, list[0].EndDate, list[0].Unit, list[0].OutUnit);
            Label1.Text = count.ToString();
        }

        protected void selType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selType.SelectedValue);
            BizSweepstake bs = new BizSweepstake();
            SweepstakeModel s = bs.GetSweepstake(id);
            BizBidding bb = new BizBidding();
            int count = bb.GetAllCount(s.StartDate, s.EndDate, 5000, s.OutUnit);
            Label1.Text = count.ToString();
        }
    }
}