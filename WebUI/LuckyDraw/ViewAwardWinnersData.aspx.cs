using System;
using System.Collections.Generic;
using System.Web.UI;
using LuckyDraw.Business;
using LuckyDraw.BusinessModel;
using Newtonsoft.Json;
using ManageFcmsCommon;
namespace WebUI.LuckyDraw
{
    public partial class ViewAwardWinnersData : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = Request.QueryString["Title"];
            string sweepstakeId = Request.QueryString["SweepstakeId"];
            int pageIndex = Convert.ToInt32(Request.QueryString["pagenum"]);
            int pageSize = Convert.ToInt32(Request.QueryString["pagesize"]);

            if (sweepstakeId != null)
            {
                RecordPaged data = BizSweepstake.GetAllAwardWinner(Convert.ToInt32(sweepstakeId), pageSize, pageIndex, title);
                Response.Write(JsonHelper.ObjectToJson(data));
            }

            Response.End();
        }
    }
}