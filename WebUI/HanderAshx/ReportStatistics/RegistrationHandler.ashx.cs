using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// RegistrationHandler 的摘要说明
    /// </summary>
    public class RegistrationHandler : IHttpHandler
    {

        private string _dateStart;
        private string _dateEnd;
        private int _typeId;
        //用户来源
        private string _channelType = string.Empty;
        private int _dateType;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _dateStart = ConvertHelper.QueryString(context.Request, "dateStart", DateTime.Now.ToString("yyyy-MM-dd"));
            _dateEnd = ConvertHelper.QueryString(context.Request, "dateEnd", DateTime.Now.ToString("yyyy-MM-dd"));
            _typeId = int.Parse(ConvertHelper.QueryString(context.Request, "typeId", "1"));
            _channelType = ConvertHelper.QueryString(context.Request, "ChannelType", "");
            _channelType = string.IsNullOrEmpty(_channelType) ? "" : _channelType.Substring(0, _channelType.Length - 1);
            _dateType = int.Parse(ConvertHelper.QueryString(context.Request, "dateType", "1"));

            context.Response.Write(GetLoanTotalList(Convert.ToDateTime(_dateStart), Convert.ToDateTime(_dateEnd), _typeId, _dateType, _channelType));
        }

        public string GetLoanTotalList(DateTime dateStart, DateTime endDate, int typeId, int dateType, string channelType = "")
        {

            var dataset = RegistrationBll.GetList(dateStart, endDate, typeId, channelType, dateType);
            var jsonStr = JsonHelper.DataTableToJson(dataset.Tables[0]);
            return jsonStr;
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