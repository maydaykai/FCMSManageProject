using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsCommon;
using Newtonsoft.Json;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// DimFeeType 的摘要说明
    /// </summary>
    public class DimFeeType : IHttpHandler
    {
        private int _sign = 1;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _sign = ConvertHelper.QueryString(context.Request, "sign", 1);
            Dictionary<int, string> dic = new Dictionary<int, string>();
            if (_sign == 1)//获取所有费用类型
                dic = FeeType.GetFeetypeList();
            else if (_sign == 2)//获取所有属于（会员）收入的费用类型
                dic = FeeType.GetRevenueFeeList();
            else if (_sign == 3)//获取所有属于（会员）支出的费用类型
                dic = FeeType.GetDefrayFeeList();
            else if (_sign == 4)//获取所有属于（会员）冻结的费用类型
                dic = FeeType.GetFreezeFeeList();
            else if (_sign == 5)//获取所有平台相关的费用类型
                dic = FeeType.GetP2PFeeList();
            else if (_sign == 6)//获取所有担保公司相关的费用类型
                dic = FeeType.GetBondingCompanyFeeList();
            else if (_sign == 5)//获取用于费用设置的费用类型
                dic = FeeType.GetSetFeeList();
            var list = new List<object>
                {
                    new
                        {
                            FeeTypeName = "全部类型", //费用名称
                            FeeTypeVal = -1 //费用值
                        }
                };
            list.AddRange(dic.Select(pair => new
                {
                    FeeTypeName = pair.Value, //费用名称
                    FeeTypeVal = pair.Key  //费用值
                }));
            context.Response.Write("{\"TotalRows\":" + dic.Count + ",\"Rows\":" + JsonConvert.SerializeObject(list) + "}");
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