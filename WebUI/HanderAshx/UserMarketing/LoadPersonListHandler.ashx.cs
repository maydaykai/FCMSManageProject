using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.UserMarketing
{
    /// <summary>
    /// LoadPersonListHandler 的摘要说明 加载查询条件
    /// </summary>
    public class LoadPersonListHandler : IHttpHandler
    {
        private int _sign;
        private int _firstId;
        private int _SecondId;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _sign = ConvertHelper.QueryString(context.Request, "sign", 0);
            _firstId = ConvertHelper.QueryString(context.Request, "firstId", 0);
            _SecondId = ConvertHelper.QueryString(context.Request, "SecondId", 0);
            context.Response.Write(GetJsonData(_sign));
        }



        private Object GetJsonData(int caseSign)
        {
            //
            Marketing_Ex_PersonBLL _bll = new Marketing_Ex_PersonBLL();
            switch (caseSign)
            {
                    //加载默认选择
                case 0:
                  return   JsonHelper.DataTableToJson(_bll.GetPartentTable(_firstId,2).Tables[0]);   
                case 1:
                    //加载第二级菜单
                  return JsonHelper.DataTableToJson(_bll.GetPartentTable_Second(_SecondId, 3).Tables[0]);   
                default:
                    return null;
            }

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