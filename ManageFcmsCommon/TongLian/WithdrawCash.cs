using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using com.aipg;
using com.aipg.package;

namespace ManageFcmsCommon.TongLian
{
    public class WithdrawCash
    {
        private static object lockobj = new object();

        /// <summary>
        /// 批量代付提现方法(同步)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="REQ_SN"></param>
        /// <param name="reqXml"></param>
        /// <returns></returns>
        public static Response WithdrawCashSync(DetailRequest d, string REQ_SN, ref string reqXml)
        {
            lock (lockobj)
            {
                try
                {
                    AIPG aipg = new AIPG
                        {
                            url = ParasHelper.url,
                            SigneCert = ParasHelper.SignCert,
                            VerifyCert = ParasHelper.VerifyCert
                        };
                    TradeRequest req = new BatchPayRequest(ParasHelper.userName, ParasHelper.password, REQ_SN, 5, ParasHelper.businessCode, ParasHelper.merchantId);
                    req.AddDetail(d);
                    reqXml = req.ToXML();
                    //同步请求
                    Response resp = aipg.Send(req);
                    if (resp != null)
                    {
                        return resp;
                    }
                    Log4NetHelper.WriteLog(string.Format("请求失败！ 错误类型：{0}， 错误描述：{1}", aipg.LastErr, aipg.LastErrMsg));
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
                return null;
            }
        }

        /// <summary>
        /// 单笔实时代付提现方法(同步)
        /// </summary>
        /// <param name="single"></param>
        /// <param name="REQ_SN"></param>
        /// <param name="reqXml"></param>
        /// <returns></returns>
        public static Response WithdrawCashSync(SingleTradePackage single, string REQ_SN, ref string reqXml)
        {
            lock (lockobj)
            {
                try
                {
                    AIPG aipg = new AIPG
                        {
                            url = ParasHelper.url,
                            SigneCert = ParasHelper.SignCert,
                            VerifyCert = ParasHelper.VerifyCert
                        };
                    SingleTradeRequest req = new SingleTradeRequest(single, "100014", ParasHelper.userName, ParasHelper.password, REQ_SN, 5, ParasHelper.businessCode, ParasHelper.merchantId);
                    reqXml = req.ToXML();
                    //同步请求
                    Response resp = aipg.Send(req);
                    if (resp != null)
                    {
                        return resp;
                    }
                    Log4NetHelper.WriteLog(string.Format("请求失败！ 错误类型：{0}， 错误描述：{1}", aipg.LastErr, aipg.LastErrMsg));
                }
                catch (Exception exx)
                {
                    Log4NetHelper.WriteError(exx);
                }
                return null;
            }
        }
    }
}
