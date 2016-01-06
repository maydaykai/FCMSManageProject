using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// RechargeHandler 的摘要说明
    /// </summary>
    public class RechargeHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _sn = "";
        private string _memberName = "";
        private string _type = "";
        private string _status = "";
        private string _startDate;
        private string _endDate;
        private int _rechargeChannel;
        private decimal _rechargeAmountMin;
        private decimal _rechargeAmountMax;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "DESC");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "P.ID");
            _sn = ConvertHelper.QueryString(context.Request, "SN", "");

            _type = ConvertHelper.QueryString(context.Request, "type", "");
            _rechargeChannel = ConvertHelper.QueryString(context.Request, "rechargeChannel", -1);
            _memberName = ConvertHelper.QueryString(context.Request, "memberName", "");
            _status = ConvertHelper.QueryString(context.Request, "status", "");
            _rechargeAmountMin = ConvertHelper.QueryString(context.Request, "rechargeAmountMin", 0);
            _rechargeAmountMax = ConvertHelper.QueryString(context.Request, "rechargeAmountMax", 0);

            switch (_sortField)
            {
                case "OrderNumberString":
                    _sortField = "P.OrderNumber"; break;
                case "RechargeChannelString": _sortField = "P.RechargeChannel"; break;
                case "StatusString": _sortField = "P.Status"; break;
                case "TypeString": _sortField = "P.Type"; break;
                default: _sortField = "P.ApplicationTime"; break;

            }

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_sn))
            {
                filter += " and P.OrderNumber like '%" + _sn + "%' OR P.MerchantOrderNo like '%" + _sn + "%'";
            }
            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " and M.MemberName like '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_type))
            {
                filter += " and P.Type=" + _type;
            }
            if (_rechargeChannel >= 0)
            {
                filter += " and P.RechargeChannel=" + _rechargeChannel;
            }
            if (!string.IsNullOrEmpty(_status))
            {
                filter += " and P.Status=" + _status;
            }
            if (!string.IsNullOrEmpty(_startDate))
            {
                filter += " and DATEDIFF(s,ApplicationTime,'" + _startDate + "')<=0";
            }
            if (!string.IsNullOrEmpty(_endDate))
            {
                filter += " and DATEDIFF(s,ApplicationTime,'" + _endDate + "')>=0";
            }
            if (_rechargeAmountMin > 0)
            {
                filter += " and P.Amount>=" + _rechargeAmountMin;
            }
            if (_rechargeAmountMax > 0)
            {
                filter += " and P.Amount<=" + _rechargeAmountMax;
            }

            var sortColumn = _sortField + " " + _sort;
            context.Response.Write(GetPageList(_currentPage, _pageSize, sortColumn, filter));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var fields = new StringBuilder();
            fields.Append(" P.ID, P.Amount,P.Type,P.AuditStatus,P.ApplicationTime, P.MemberID, P.MerchantOrderNo, P.OrderNumber, ");
            fields.Append("OrderNumberString=(case when ISNULL(P.OrderNumber,'')='' then '<span style=\"color:#ff0000\">一</span>' else P.OrderNumber end),");
            fields.Append("RechargeChannelString=(case when P.RechargeChannel=1 then '通联支付' when P.RechargeChannel=2 then '通联移动支付（IOS）' when P.RechargeChannel=3 then '通联移动支付（Android）' when P.RechargeChannel=4 then '通联WAP支付' when P.RechargeChannel=5 then '连连支付'when P.RechargeChannel=6 then '连连移动支付（IOS）'when P.RechargeChannel=7 then '连连移动支付（Android）'when P.RechargeChannel=8 then '连连WAP支付' else '线下充值' end),P.RechargeFee,");
            fields.Append("StatusString=(case when P.Status=0 then '付款中' when P.Status=1 then '<span style=\"color:#436EEE\">付款成功</span>' when P.Status=2 then '<span style=\"color:#FF0000;\">付款失败</span>' end),");
            fields.Append("TypeString=(case when P.Type=0 then '线上' when P.Type=1 then '<span style=\"color:#EE7942\">线下</span>' end), P.UpdateTime, M.MemberName,");
            fields.Append("AuditStatusStr=( case when P.AuditStatus=0 then '初审中' when P.AuditStatus=1 then '复审中' when P.AuditStatus=2 then '初审不通过' when P.AuditStatus=3 then '复审不通过' when P.AuditStatus=4 then '复审通过' END) ");
            var rechargeRecordBll = new RechargeRecordBll();
            var dt = rechargeRecordBll.GetPageList(fields.ToString(), filter, sortField, pagenum, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);

            var aggregate = rechargeRecordBll.Aggregate(filter);
            if (aggregate == null)
            {
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":0,\"Rows\":" + jsonStr + "}";
            }
            else
            {
                aggregate = ConvertHelper.ToDecimal(aggregate.ToString());
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":" + aggregate + ",\"Rows\":" + jsonStr + "}";
            }
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