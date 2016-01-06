using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.UserManage
{
    /// <summary>
    /// AreaHandler 的摘要说明
    /// </summary>
    public class AreaHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            int parentId = ConvertHelper.QueryString(context.Request, "parentid", 1);
            if (parentId <= 1)
            {
                context.Response.Write(GetProvinceList());
            }
            else
            {
                context.Response.Write(GetCityListByParentID(parentId));
            }
        }

        /// <summary>
        /// 获取省份列表
        /// </summary>
        private string GetProvinceList()
        {
            List<AreaModel> list = new AreaBLL().getProvinceList();
            return JsonHelper.ObjectToJson(list);
        }

        /// <summary>
        /// 获取市
        /// </summary>
        private string GetCityListByParentID(int id)
        {
            List<AreaModel> list = new AreaBLL().getCityListByParentID(id);
            return JsonHelper.ObjectToJson(list);
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