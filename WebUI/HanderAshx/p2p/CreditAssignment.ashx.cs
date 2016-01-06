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
    /// CreditAssignment 的摘要说明
    /// </summary>
    public class CreditAssignment : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "LoanNumber";
        private string _loanNumber = "", _oldLoanNumber = "", _startDate = "", _endDate = "";
        private int _examStatus;

        //private int _userId;


        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";

            //_userId = 2;
            //FcmsUserModel _fcmsUserModel = new FcmsUserBll().GetModel(_userId);

            _currentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);
            _pageSize = ConvertHelper.QueryString(context.Request, "pagesize", 0);
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "desc");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "id");
            _loanNumber = ConvertHelper.QueryString(context.Request, "loanNumber", "");
            _oldLoanNumber = ConvertHelper.QueryString(context.Request, "oldLoanNumber", "");
            _startDate = ConvertHelper.QueryString(context.Request, "startDate", "");
            _endDate = ConvertHelper.QueryString(context.Request, "endDate", "");
            _examStatus = ConvertHelper.QueryString(context.Request, "examStatus", 0);

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_loanNumber) && _loanNumber != "undefined")
            {
                filter += " and C.LoanNumber like '%" + _loanNumber + "%'";
            }
            if (!string.IsNullOrEmpty(_oldLoanNumber) && _oldLoanNumber != "undefined")
            {
                filter += " and L.LoanNumber like '%" + _oldLoanNumber + "%'";
            }
            if (_examStatus != 0)
            {
                filter += " and C.ExamStatus = " + _examStatus;
            }
            if (!string.IsNullOrEmpty(_startDate) && _startDate != "undefined")
            {
                filter += " and C.CreateTime>='" + _startDate + "' ";
            }
            if (!string.IsNullOrEmpty(_endDate) && _endDate != "undefined")
            {
                filter += " and C.CreateTime<='" + _endDate + "' ";
            }

            var sortColumn = "C.ExamStatus ," + _sortField + " " + _sort;
            context.Response.Write(GetPageLoanManage(_currentPage, _pageSize, sortColumn, filter));

        }

        //获取数据
        public Object GetPageLoanManage(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;

            int pageCount = 0;
            var bll = new CreditAssignmentBll();
            IList<CreditAssignmentModel> list = bll.GetPageCreditAssignmentModel(filter, sortField, pagenum, pagesize, ref pageCount);
            var orders = (from loanModel in list
                          select new
                          {
                              loanModel.ID,
                              loanModel.MemberName,
                              loanModel.RealName,
                              loanModel.OldLoanNumber,
                              loanModel.LoanTitle,
                              loanModel.LoanNumber,
                              loanModel.LoanAmount,
                              loanModel.DiscountRate,
                              loanModel.RealLoanAmount,
                              loanModel.LoanRate,
                              loanModel.LoanTermInfo,
                              loanModel.RemainedDays,
                              loanModel.BiddingProcess,
                              loanModel.CreateTime,
                              loanModel.FullScaleTime,
                              loanModel.ExamStatus,
                              loanModel.ExamStatusName,
                              loanModel.FreezeAmount
                          });
            var jsonData = new
            {
                TotalRows = pageCount,//记录数
                Rows = orders//实体列表
            };
            var s = JsonHelper.ObjectToJson(jsonData);
            return s;
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