using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsCommon;
using ManageFcmsBll;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// AdvanceAuditing 的摘要说明
    /// </summary>
    public class AdvanceAuditing : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string _ID = ConvertHelper.QueryString(context.Request, "ID", "");
            int _operator = ConvertHelper.QueryString(context.Request, "_operator", 0);  //操作人
            string msg = "";

            int loanID = 0;     //贷款ID
            int peNumber = 0;   //期数

            if (_ID != "")
            {
                loanID = Convert.ToInt32(_ID.Split('|')[0]);
                peNumber = Convert.ToInt32(_ID.Split('|')[1]);
            }

            if (loanID != 0 && peNumber != 0)
            {
                RepaymentPlanBll bll = new RepaymentPlanBll();
                //将还款记录写入到表
                bool result = bll.InsertApproveAppayAdvanceInfo(loanID, peNumber, _operator,ref msg);
            }

            context.Response.ContentType = "text/plain";
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