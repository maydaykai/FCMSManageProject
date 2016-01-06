using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.UserMarketing
{
    /// <summary>
    /// DelPersonHandler 的摘要说明 删除用户信息
    /// </summary>
    public class DelPersonHandler : IHttpHandler
    {

        private int _id;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _id = ConvertHelper.QueryString(context.Request, "id", 0);
            var productBll = new Marketing_Ex_PersonBLL();
            var relust = productBll.DelMarketing_Ex_Person(_id);
            if (relust > 0)
            {
                //如果删除成功写入到异动表
                Marketing_MoveModel mode = new Marketing_MoveModel();
                mode.Type = 1;//删除
                mode.FirstMemberId = _id;
                mode.SecondMemberId = -1;
                mode.Leave = -1;
                mode.ParentId = -1;
                mode.StartTime =DateTime.Now;
                productBll.AddMoveModel(mode);

            }
            // var relust=new {}
            context.Response.Write(relust);
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