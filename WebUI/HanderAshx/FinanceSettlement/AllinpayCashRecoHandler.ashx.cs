using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.FinanceSettlement
{
    /// <summary>
    /// AllinpayCashRecoHandler 的摘要说明
    /// </summary>
    public class AllinpayCashRecoHandler : IHttpHandler
    {

        private string _settlementDate = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _settlementDate = ConvertHelper.QueryString(context.Request, "SettlementDate", DateTime.Now.ToShortDateString());
            _settlementDate = _settlementDate == "undefined" ? DateTime.Now.ToShortDateString() : _settlementDate;
            context.Response.Write(GetCashRecoDifferenceModelList(_settlementDate));
        }

        //获取数据
        public Object GetCashRecoDifferenceModelList(string settlementDate)
        {
            settlementDate = Convert.ToDateTime(settlementDate).ToString("yyyyMMdd");
            var rechargeReconciliationBll = new CashRecoBll();
            var rrdList = rechargeReconciliationBll.GetCashRecoDifferenceModelList(settlementDate);
            var jsonData = new
            {
                TotalRows = rrdList.Count,//记录数
                Rows = rrdList//实体列表
            };
            var s = JsonHelper.ObjectToJson(jsonData);
            return s;
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