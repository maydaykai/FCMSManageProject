using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using System.Web.Script.Serialization;
using ManageFcmsModel;

namespace WebUI.HanderAshx.Basic
{
    /// <summary>
    /// PayLimitationHandler 的摘要说明
    /// </summary>
    public class PayLimitationHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            var method = this.GetType().GetMethod(context.Request["Method"]);
            method.Invoke(this, new object[] { context });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hc"></param>
        public void GetData(HttpContext hc)
        {
            var dt = new PayLimitationBll().GetData().Tables[0];
            hc.Response.Write(JsonHelper.DataTableToJson(dt));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hc"></param>
        public void Update(HttpContext hc)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var model = js.Deserialize<PayLimitation>(hc.Request["Para"]);
            int result = new PayLimitationBll().Update(model);
            hc.Response.Write("{\"success\":\"" + (result > 0 ? "success" : "failed") + "\"}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hc"></param>
        public void Add(HttpContext hc)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var model = js.Deserialize<PayLimitation>(hc.Request["Para"]);
            int result = new PayLimitationBll().Add(model);
            hc.Response.Write("{\"success\":\"" + (result > 0 ? "success" : "failed") + "\"}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hc"></param>
        public void Delete(HttpContext hc)
        {
            var id = Convert.ToInt32(hc.Request["Id"]);
            new PayLimitationBll().Delete(id);
            hc.Response.Write("{\"success\":\"success\"}");
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