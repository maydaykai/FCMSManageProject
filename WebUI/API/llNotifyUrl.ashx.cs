using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using LLYTPay;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using Newtonsoft.Json;

namespace WebUI.API
{
    /// <summary>
    /// llNotifyUrl 的摘要说明
    /// </summary>
    public class llNotifyUrl : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Response = context.Response;
            Request = context.Request;
            SortedDictionary<string, string> sPara = GetRequestPost();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                String orderNo = sPara["no_order"];
                String resultPay = sPara["result_pay"];
                if (!YinTongUtil.checkSign(sPara, ConfigHelper.YT_PUB_KEY, //验证失败
                    ConfigHelper.MD5_KEY))
                {
                    Log4NetHelper.WriteLog("代付异步通知验签失败。订单号：" + orderNo);
                    Response.Write(@"{""ret_code"":""9999"",""ret_msg"":""验签失败""}");
                    Response.End();
                }
                else
                {
                    var bll = new CashRecordBLL();
                    if ("SUCCESS".Equals(resultPay))
                    {
                        bll.WithdrawCheckSuccess(orderNo);
                        try
                        {
                            bll.WithdrawSuccessByLL(bll.getCashRecordModel("REQ_SN='" + orderNo + "'").MemberID);
                        }
                        catch (Exception ex)
                        {
                            Log4NetHelper.WriteError(ex);
                        }
                    }
                    else if ("FAILURE".Equals(resultPay))
                    {
                        bll.WithdrawCheckFail(orderNo);
                    }
                    Response.Write(@"{""ret_code"":""0000"",""ret_msg"":""交易成功""}");
                    Response.End();
                }
            }
            else
            {
                Response.Write(@"{""ret_code"":""9999"",""ret_msg"":""交易失败""}");
                Response.End();
            }
        }
        private HttpResponse Response { get; set; }
        private HttpRequest Request { get; set; }
        /// <summary>
        /// 获取POST过来通知消息，并以“参数名=参数值”的形式组成字典
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            string reqStr = readReqStr();
            SortedDictionary<string, string> sArray = JsonConvert.DeserializeObject<SortedDictionary<string, string>>(reqStr);
            return sArray;
        }

        //从request中读取流，组成字符串返回
        public String readReqStr()
        {
            StringBuilder sb = new StringBuilder();
            Stream inputStream = Request.GetBufferlessInputStream();
            StreamReader reader = new StreamReader(inputStream, System.Text.Encoding.UTF8);

            String line = null;
            while ((line = reader.ReadLine()) != null)
            {
                sb.Append(line);
            }
            reader.Close();
            return sb.ToString();
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