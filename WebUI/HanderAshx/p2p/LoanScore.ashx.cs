using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.HanderAshx.p2p
{
    /// <summary>
    /// LoanScore 的摘要说明
    /// </summary>
    public class LoanScore : IHttpHandler
    {
        private int _loanId;
        private int _sign;
        private int _score;
        private int _id;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            _loanId = ConvertHelper.QueryString(context.Request, "loanId", 0);
            _sign = ConvertHelper.QueryString(context.Request, "sign", 0);
            _score = ConvertHelper.QueryString(context.Request, "score", 0);
            _id = ConvertHelper.QueryString(context.Request, "Id", 0);

            var filter = " l.LoanID = " + _loanId;

            if (_sign == 0) //获取评分列表
            {
               context.Response.Write(GetList(filter));
            }
            if (_sign == 1) //评分
            {
                context.Response.Write(Update(_id, _score,_loanId));
            }
            if (_sign == 2) //评分汇总
            {
                context.Response.Write(SumScore(_loanId));
            }
        }
        /// <summary>
        /// 获取评分列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetList(string filter)
        {
            var dataset = new LoanScoreBll().GetList(filter);
            return JsonHelper.DataTableToJson(dataset.Tables[0]);
        }
        /// <summary>
        /// 评分
        /// </summary>
        /// <param name="id"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public string Update(int id,int score,int loanId)
        {
            var returnval = new LoanScoreBll().Update(id,score);
            var dataset = new LoanScoreBll().GetSumScoreList(loanId);

            var result = "信用评分：" + dataset.Tables[0].Rows[0]["SumScore"].ToString() + " 等级：" + dataset.Tables[0].Rows[0]["ScoreLevel"].ToString();

            return returnval ? result : PublicConst.Error;
        }

        public string SumScore(int loanId)
        {
            var dataset = new LoanScoreBll().GetSumScoreList(loanId);
            return JsonHelper.DataTableToJson(dataset.Tables[0]);
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