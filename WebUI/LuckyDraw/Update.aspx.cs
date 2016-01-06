using System;
using System.Collections.Generic;
using System.Web.UI;
using LuckyDraw.Business;
using LuckyDraw.BusinessModel;
using Newtonsoft.Json;

namespace WebUI.LuckyDraw
{
    public partial class Update : Page //BasePage
    {
        private readonly Dictionary<string, Func<bool>> _methods; //前台可以直接调用的方法

        public Update()
        {
            _methods = new Dictionary<string, Func<bool>> { { "GetSweepstakeHistory", GetSweepstakeHistory } };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var op = Request.PathInfo.TrimStart('/');
            if (_methods.ContainsKey(op) && _methods[op]()) ;
        }

        private bool GetSweepstakeHistory()
        {
            List<SweepstakeHistoryMode> data = BizSweepstake.GetAllSweepstake();
            var result = new {data};

            Response.Write(JsonConvert.SerializeObject(result));
            Response.End();

            return true;
        }
    }
}