using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// QueryAdvance 的摘要说明
    /// </summary>
    public class QueryAdvance : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string _ID = ConvertHelper.QueryString(context.Request, "ID", "");
            int _operator = ConvertHelper.QueryString(context.Request, "_operator", 0);  //操作人
            int currStep = ConvertHelper.QueryString(context.Request, "currStep", 0);   //操作步骤
            string msg = "";

            int loanID = 0;     //贷款ID
            int peNumber = 0;   //期数
            
            
            if (_ID != "")
            {
                loanID = Convert.ToInt32(_ID.Split('|')[0]);
                peNumber = Convert.ToInt32(_ID.Split('|')[1]);
            }

            if (new LoanBll().GetLoanModel(loanID).LoanTypeID == 8)
            {
                msg = "净值标不能申请垫付！";
            }
            else
            {
                //该借款如之前有逾期借款未归还全部，则不能进行申请
                int _peNumber = new RepaymentPlanBll().GetLatelyNotPeNumber(loanID);
                if (_peNumber != peNumber)
                {
                    msg = "请优先处理完第" + _peNumber + "期的贷款！";
                }
                else
                {
                    if (loanID != 0 && peNumber != 0)
                    {
                        RepaymentPlanBll bll = new RepaymentPlanBll();
                        //将还款记录写入到表
                        bool result = bll.InsertApproveAppayAdvanceInfo(loanID, peNumber, _operator, ref msg);
                    }
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            context.Response.Write(msg);
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