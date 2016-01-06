/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-18
Description: 渠道效果分析AJAX请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.AdvertisingManage
{
    /// <summary>
    /// ChannelEffectAnalyzeHandler 的摘要说明
    /// </summary>
    public class ChannelEffectAnalyzeHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private int _channelId = -1;
        private string _startDate = "";
        private string _endDate = "";
        private int _statType = 0;
        private int _statMode = 0;
        private string[] _orders = { "CONVERT(CHAR(10),A.RegTime,120)", "CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(week,A.RegTime))))+CONVERT(VARCHAR,datepart(week,A.RegTime))+'周'", "CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(MM,A.RegTime))))+CONVERT(VARCHAR,datepart(MM,A.RegTime))+'月'", "CONVERT(CHAR(4),A.RegTime,120)+'年'" };
        private string[] _statTypes = { 
                                          "(SELECT COUNT(distinct BI.MemberID) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND CONVERT(CHAR(10),A.RegTime,120)=CONVERT(CHAR(10),BI.CreateTime,120)) bidCount,ISNULL((SELECT SUM(BI.BidAmount) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND CONVERT(CHAR(10),A.RegTime,120)=CONVERT(CHAR(10),BI.CreateTime,120)),0) bidAmount",
                                          "(SELECT COUNT(distinct BI.MemberID) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND (CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(week,A.RegTime))))+CONVERT(VARCHAR,datepart(week,A.RegTime))+'周')=(CONVERT(CHAR(4),BI.CreateTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(week,BI.CreateTime))))+CONVERT(VARCHAR,datepart(week,BI.CreateTime))+'周')) bidCount,ISNULL((SELECT SUM(BI.BidAmount) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND (CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(week,A.RegTime))))+CONVERT(VARCHAR,datepart(week,A.RegTime))+'周')=(CONVERT(CHAR(4),BI.CreateTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(week,BI.CreateTime))))+CONVERT(VARCHAR,datepart(week,BI.CreateTime))+'周')),0) bidAmount",
                                          "(SELECT COUNT(distinct BI.MemberID) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND (CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(MM,A.RegTime))))+CONVERT(VARCHAR,datepart(MM,A.RegTime))+'月')=(CONVERT(CHAR(4),BI.CreateTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(MM,BI.CreateTime))))+CONVERT(VARCHAR,datepart(MM,BI.CreateTime))+'月')) bidCount,ISNULL((SELECT SUM(BI.BidAmount) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND (CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(MM,A.RegTime))))+CONVERT(VARCHAR,datepart(MM,A.RegTime))+'月')=(CONVERT(CHAR(4),BI.CreateTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(MM,BI.CreateTime))))+CONVERT(VARCHAR,datepart(MM,BI.CreateTime))+'月')),0) bidAmount",
                                          "(SELECT COUNT(distinct BI.MemberID) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND CONVERT(CHAR(4),A.RegTime,120)+'年'=CONVERT(CHAR(4),BI.CreateTime,120)+'年') bidCount,ISNULL((SELECT SUM(BI.BidAmount) FROM Bidding BI,Member ME,Loan LA WHERE BI.MemberID=ME.ID AND BI.LoanID=LA.ID AND LA.LoanTypeID<>6 AND ME.Channel=DC.Id AND BI.CreateTime>='{0}' AND BI.CreateTime<'{1}' AND CONVERT(CHAR(4),A.RegTime,120)+'年'=CONVERT(CHAR(4),BI.CreateTime,120)+'年'),0) bidAmount"
                                      };
        private string[] _channelFees = { 
                                            ",ISNULL((SELECT SUM(CF.dayFee) FROM ChannelFee CF WHERE CF.channelID=DC.Id AND CONVERT(CHAR(10),A.RegTime,120)=CONVERT(CHAR(10),CF.CreateTime,120)),0) /(1+DC.Rebate/100) channelFee",
                                            ",ISNULL((SELECT SUM(CF.dayFee) FROM ChannelFee CF WHERE CF.channelID=DC.Id AND (CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(week,A.RegTime))))+CONVERT(VARCHAR,datepart(week,A.RegTime))+'周')=(CONVERT(CHAR(4),CF.CreateTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(week,CF.CreateTime))))+CONVERT(VARCHAR,datepart(week,CF.CreateTime))+'周')),0) /(1+DC.Rebate/100) channelFee",
                                            ",ISNULL((SELECT SUM(CF.dayFee) FROM ChannelFee CF WHERE CF.channelID=DC.Id AND (CONVERT(CHAR(4),A.RegTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(MM,A.RegTime))))+CONVERT(VARCHAR,datepart(MM,A.RegTime))+'月')=(CONVERT(CHAR(4),CF.CreateTime,120)+'年第'+REPLICATE('0',2-DATALENGTH(CONVERT(VARCHAR,datepart(MM,CF.CreateTime))))+CONVERT(VARCHAR,datepart(MM,CF.CreateTime))+'月')),0) /(1+DC.Rebate/100) channelFee",
                                            ",ISNULL((SELECT SUM(CF.dayFee) FROM ChannelFee CF WHERE CF.channelID=DC.Id AND CONVERT(CHAR(4),A.RegTime,120)+'年'=CONVERT(CHAR(4),CF.CreateTime,120)+'年'),0)/(1+DC.Rebate/100) channelFee"
                                        };
        public void ProcessRequest(HttpContext context)
        {
            //"COUNT(distinct B.MemberID) bidCount,ISNULL(SUM(B.BidAmount),0) bidAmount"
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", DateTime.Now.ToString("yyyy-MM-dd"));
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            _channelId = ConvertHelper.QueryString(context.Request, "channelId", -1);
            _statType = ConvertHelper.QueryString(context.Request, "statType", 0);
            _statMode = ConvertHelper.QueryString(context.Request, "statMode", 0);
                         
            if (_statType == 1)
                _statTypes[_statMode] = string.Format(_statTypes[_statMode], _startDate, _endDate);

            var fields = _orders[_statMode] + " RegTime,DC.Channel,DC.Rebate,COUNT(A.ID) RegCount";
            fields += ",COUNT(distinct CASE WHEN CONVERT(CHAR(10),B.CreateTime,120)=CONVERT(CHAR(10),A.RegTime,120) THEN B.MemberID ELSE NULL END) dayBidCount";
            fields += ",COUNT(distinct R.MemberID) payCount,ISNULL(SUM(R.Amount),0) payAmount,ISNULL(SUM(RE.SurPrincipal+RE.SurReInterest+RE.SurOverInterest),0) daishou,";
            fields += _statType == 1 ? string.Format(_statTypes[_statMode], _startDate, _endDate) : "COUNT(distinct B.MemberID) bidCount,ISNULL(SUM(B.BidAmount),0) bidAmount";
            fields += _channelFees[_statMode];
            var filter = " AND A.RegTime>='" + _startDate + "' AND A.RegTime<'" + DateTime.Parse(_endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";
            if (_channelId > -1)
                filter += " AND DC.Id=" + _channelId;

            filter += " GROUP BY DC.Channel,DC.Id,DC.Rebate," + _orders[_statMode];

            context.Response.Write(GetPageList(_currentPage, _pageSize, fields, filter, _orders[_statMode]));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string fields, string filter, string order)
        {
            pagenum = pagenum + 1;
            int total;
            var dt = AdvertisementBLL.Instance.GetChannelAnalyzeList(fields, filter, order, pagenum, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
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