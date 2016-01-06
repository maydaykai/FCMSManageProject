using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using LitJson;

namespace WebUI.HanderAshx.Information
{
    /// <summary>
    /// AppUploadHandler 的摘要说明
    /// </summary>
    public class AppUploadHandler : IHttpHandler
    {
        private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            //文件保存目录路径
            String savePath = ManageFcmsCommon.ConfigHelper.AppFilePhysicallPath;

            //文件保存目录URL
            String saveUrl = ManageFcmsCommon.ConfigHelper.AppFileVirtualPath;

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("apk", "apk");

            //最大文件大小
            int maxSize = 10000000;
            this.context = context;

            //HttpPostedFile imgFile = context.Request.Files["imgFile"];
            HttpPostedFile apkFile = context.Request.Files[0];
            if (apkFile == null)
            {
                showError("请选择文件。");
            }

            //String dirPath = context.Server.MapPath(savePath);
            String dirPath = savePath;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String dirName = context.Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "apk";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = apkFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (apkFile.InputStream == null || apkFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            //dirPath += dirName + "\\";
            //saveUrl += dirName + "/";

            //if (!Directory.Exists(dirPath)) {
            //    Directory.CreateDirectory(dirPath);
            //}
            //String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            //dirPath += ymd + "/";
            //saveUrl += ymd + "/";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String filePath = dirPath + fileName;

            apkFile.SaveAs(filePath);

            String fileUrl = saveUrl + fileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;
            hash["fileName"] = fileName;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
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