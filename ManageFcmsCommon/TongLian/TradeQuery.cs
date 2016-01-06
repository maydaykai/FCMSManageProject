using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.aipg;
using com.aipg.package;

namespace ManageFcmsCommon.TongLian
{
    public class TradeQuery
    {
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="ReqSn">流水号</param>
        /// <returns></returns>
        public static string query(string ReqSn)
        {
            try
            {
                AIPG aipg = new AIPG();
                aipg.url = ParasHelper.url;
                aipg.SigneCert = ParasHelper.SignCert;
                aipg.VerifyCert = ParasHelper.VerifyCert;
                Query req = new Query("200004", ParasHelper.userName, ParasHelper.password, ReqSn, 5, ParasHelper.businessCode, ParasHelper.merchantId);

                //同步请求
                Response resp = aipg.Send(req);
                if (resp != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Format("交易代码：{0} ， 信息：{1} ；返回码：{2} ， 返回文本：{3}    ", resp.RetCode, resp.ErrMsg, resp.Trans_ret_code ?? "无", resp.Trans_ret_msg ?? "无"));
                    return sb.ToString();
                }
                else
                {
                    string str = string.Format("请求失败！ 错误类型：{0}， 错误描述：{1}", aipg.LastErr, aipg.LastErrMsg);
                    Log4NetHelper.WriteLog(str);
                    return str;
                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
            }
            return "";
        }
    }
}
