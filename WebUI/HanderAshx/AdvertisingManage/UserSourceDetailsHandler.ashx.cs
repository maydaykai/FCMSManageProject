/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 用户来源明细AJAX请求处理
Update: 
**************************************************************/
using System;
using System.Web;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.HanderAshx.AdvertisingManage
{
    /// <summary>
    /// UserSourceDetailsHandler 的摘要说明
    /// </summary>
    public class UserSourceDetailsHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _secondary = "";
        private string _startDate = "";
        private string _endDate = "";
        private string _memberName = "";
        private string _visitPreUrl = "";
        private string _visitUrl = "";
        private string _isRecharge = "";
        private string _isBinBank = "";
        private string _isRealName = "";
        private string _regSource = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _secondary = ConvertHelper.QueryString(context.Request, "Secondary", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", DateTime.Now.ToString("yyyy-MM-dd"));
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            _memberName = ConvertHelper.QueryString(context.Request, "name", "");
            _visitPreUrl = ConvertHelper.QueryString(context.Request, "visitPreUrl", "");
            _visitUrl = ConvertHelper.QueryString(context.Request, "visitUrl", "");
            _isRecharge = ConvertHelper.QueryString(context.Request, "isRecharge", "false");
            _isBinBank = ConvertHelper.QueryString(context.Request, "isBinBank", "false");
            _isRealName = ConvertHelper.QueryString(context.Request, "isRealName", "false");
            _regSource = ConvertHelper.QueryString(context.Request, "regSource", "");

            var filter = "AND M.RegTime>='" + _startDate + "' AND M.RegTime<='" + DateTime.Parse(_endDate).AddDays(1).ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(_secondary))
            {
                filter += " AND M.ChannelRemark LIKE '%" + _secondary + "%'";
            }
            if (!string.IsNullOrEmpty(_memberName))
            {
                filter += " AND M.MemberName LIKE '%" + _memberName + "%'";
            }
            if (!string.IsNullOrEmpty(_visitPreUrl))
            {
                filter += " AND US.VisitPreUrl LIKE '%" + _visitPreUrl + "%'";
            }
            if (!string.IsNullOrEmpty(_visitUrl))
            {
                filter += " AND US.VisitUrl LIKE '%" + _visitUrl + "%'";
            }
            if (!string.IsNullOrEmpty(_regSource))
            {
                filter += " AND M.RegSource=" + _regSource;
            }
            if (bool.Parse(_isRecharge))
            {
                filter += " AND ISNULL(RR.Amount,0)>0";
            }
            if (bool.Parse(_isBinBank))
            {
                filter += " AND EXISTS (SELECT 1 FROM BankAccount BA WHERE BA.Status=1 AND BA.MemberID=M.ID)";
            }
            if (bool.Parse(_isRealName))
            {
                filter += " AND M.CompleStatus>=2";
            }

            context.Response.Write(GetPageList(_currentPage, _pageSize, filter));
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var dt = AdvertisementBLL.Instance.GetUserSourceDetails(filter, pagenum, pagesize, out total);
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