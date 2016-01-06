using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// WithdrawAlertDetailHandler 的摘要说明
    /// </summary>
    public class WithdrawAlertDetailHandler : IHttpHandler
    {
        private int _limitDays = 7;
        private string _percent = "0.8";
        private int _memberId = 0;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _memberId = ConvertHelper.QueryString(context.Request, "memberId", 0);
            _limitDays = ConvertHelper.QueryString(context.Request, "limitDays", 3);
            _percent = ConvertHelper.QueryString(context.Request, "percent", "0.8");

            context.Response.Write(GetWithdrawAlertDetailList(_limitDays, Convert.ToDecimal(_percent), _memberId));
        }

        public string GetWithdrawAlertDetailList(int limitDays, decimal percent, int memberId)
        {
            var dataset = new MemberBll().GetWithdrawAlertDetailList(limitDays, percent, memberId);
            var dt = dataset.Tables[0];
            dt.Columns.Add(new DataColumn { DataType = typeof(string), AllowDBNull = true, ColumnName = "FeeTypeString" });
            foreach (DataRow dr in dt.Rows)
            {
                int feeType = dr["FeeType"] != null ? Convert.ToInt32(dr["FeeType"]) : -1;
                if (feeType == -1)
                {
                    dr["FeeTypeString"] = "";
                }
                else
                {
                    dr["FeeTypeString"] = FeeType.GetNameByType((FeeType.FeeTypeEnum)feeType);
                }
            }

            return JsonHelper.DataTableToJson(dt);
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