using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// DataStatistics 的摘要说明
    /// </summary>
    public class DataStatistics : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "Application/json";
            MethodInfo method = this.GetType().GetMethod(context.Request["Method"]);
            method.Invoke(this, new object[] { context });
        }

        /// <summary>
        /// 担保公司剩余应还本金
        /// </summary>
        /// <param name="hc"></param>
        public void GuaranteeRePrincipalTotal(HttpContext hc)
        {
            var dataset = new GuaranteeBll().GuaranteeRePrincipalTotal();
            hc.Response.Write(JsonHelper.DataTableToJson(dataset.Tables[0]));
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