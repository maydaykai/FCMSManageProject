using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ManageFcmsCommon;
using ManageFcmsBll;

namespace WebUI.HanderAshx.Basic
{
    /// <summary>
    /// ProductHandler 的摘要说明
    /// </summary>
    public class ProductHandler : IHttpHandler
    {
        private int _sign;
        private int _productId;
        private string _field;
        private int _productscoreId;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _sign = ConvertHelper.QueryString(context.Request, "sign", 0);
            _productId = ConvertHelper.QueryString(context.Request, "productId", 0);
            _field = ConvertHelper.QueryString(context.Request, "field", "");
            _field = context.Server.HtmlDecode(_field);
            _productscoreId = ConvertHelper.QueryString(context.Request, "productscoreId", 0);
            context.Response.Write(GetJsonData(_sign));
        }

        private Object GetJsonData(int caseSign)
        {
            switch (caseSign)
            {
                case 0:
                    return ProductListByJson();
                case 1:
                    return ProjectListByJson();
                case 2:
                    return Update();
                default:
                    return null;
            }

        }

        #region 获取产品列表
        private object ProductListByJson()
        {
            var productBll = new DimProductTypeBLL();
            var dataSet = productBll.GetProductList();
            var dt = dataSet.Tables[0];
            string s = JsonHelper.DataTableToJson(dt);
            return s;
        }
        #endregion

        #region 获取某个产品下面的所有项目

        private object ProjectListByJson()
        {
            var ptPrBll = new DimProductTypeBLL();
            var dataSet = ptPrBll.GePtProjectList(_productId);
            var dt = dataSet.Tables[0];
            string s = JsonHelper.DataTableToJson(dt);
            return s;
        }
        #endregion

        #region 更新

        private string Update()
        {
            var productScoreBll = new DimProductScoreBLL();
            var result = productScoreBll.UpdateByIdToField("dbo.DimProductScore", _field, _productscoreId);
            return result ? "{\"result\":1,\"message\":\"更新成功\"}" : "{\"result\":0,\"message\":\"更新失败\"}";
        }
        #endregion



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}