using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// BidPieTotalHandler 的摘要说明
    /// </summary>
    public class BidPieTotalHandler : IHttpHandler
    {
        private int _typeId;
        private int _EnterType;
        private int _DaysValues;
        private DateTime _BeginTime;
        private DateTime _EndTime;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _typeId = int.Parse(ConvertHelper.QueryString(context.Request, "typeId", "1"));
            _EnterType = int.Parse(ConvertHelper.QueryString(context.Request, "EnterType", "0"));
            _DaysValues = int.Parse(ConvertHelper.QueryString(context.Request, "DaysValues", "0"));
            // context.Response.Write(GetBidPieTotalList(_typeId));
            _BeginTime = DateTime.Parse(ConvertHelper.QueryString(context.Request, "BeginTime", "1990-01-01"));
            _EndTime = DateTime.Parse(ConvertHelper.QueryString(context.Request, "EndTime", "1990-01-01"));

            context.Response.Write(GetBidPieTotalList(_typeId, _EnterType,  _BeginTime, _EndTime));
        }

        public string GetBidPieTotalList(int typeId, int EnterType,  DateTime BeginTime, DateTime EndTime)
        {

            // var dataset = new BidPieTotalBll().GetList(typeId);
            var dataset = new BidPieTotalBll().GetList_version_one(typeId, EnterType, BeginTime, EndTime);
            return JsonHelper.DataTableToJson(dataset.Tables[0]);
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