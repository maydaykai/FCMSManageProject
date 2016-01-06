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
    /// LoanSwitch 的摘要说明
    /// </summary>
    public class LoanSwitch : IHttpHandler
    {

        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "LoanNumber";
        private string _loanNumber = "";

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

            var filter = " ExamStatus=9";

            if (!string.IsNullOrEmpty(_loanNumber) && _loanNumber != "undefined")
            {
                filter += " and LoanNumber like '%" + _loanNumber + "%'";
            }

            var sortColumn = "ExamStatus ," + _sortField + " " + _sort;
            context.Response.Write(GetPageLoanManage(_currentPage, _pageSize, sortColumn, filter));

        }

        //获取数据
        public Object GetPageLoanManage(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;

            int pageCount = 0;
            var loanBll = new LoanBll();
            IList<LoanModel> list = new LoanBll().GetPageLoanManage(filter, sortField, pagenum, pagesize, ref pageCount);
            var orders = (from loanModel in list
                          select new
                          {
                              loanModel.ID,
                              loanModel.MemberName,
                              loanModel.RealName,
                              loanModel.LoanNumber,
                              loanModel.LoanAmount,
                              loanModel.LoanTermInfo,
                              loanModel.CreateTime,
                              loanModel.EndRePayTime,
                              loanModel.SwitchAutoRepayment,
                              loanModel.SwitchBuildOverdueFee,
                              loanModel.SwitchAutoPass
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