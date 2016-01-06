using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ManageFcmsCommon;

namespace WebUI.js.Uploadify
{
    /// <summary>
    /// UploadifyHandler 的摘要说明
    /// </summary>
    public class UploadifyHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            try
            {
                var file = context.Request.Files["Filedata"];
                string uploadPath = context.Request.MapPath("/SmsManage/ExcelUploder/");
                if (file != null)
                {
                    string fileName = file.FileName;
                    //string imgUrl = "http://" + context.Request.Url.Authority + "/SmsManage/ExcelUploder/" + fileName;
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    file.SaveAs(uploadPath + fileName);
                    context.Response.Write(uploadPath + fileName);
                }
                else
                {
                    context.Response.Write("");
                }

            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
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