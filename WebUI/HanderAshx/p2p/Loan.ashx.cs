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
    /// Loan 的摘要说明
    /// </summary>
    public class Loan : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "desc";
        private string _sortField = "LoanNumber";
        private string _loanNumber = "";
        private int _loanUser;
        private string _loanMemberName = "";
        private int _examStatus;
        private string _createTime = "";
        private int _guaranteeId;
        private int _tradeType;
        private int _branchCompanyId;

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
            _loanUser = ConvertHelper.QueryString(context.Request, "loanUser", 0);
            _loanMemberName = ConvertHelper.QueryString(context.Request, "loanMemberName", "");
            _examStatus = ConvertHelper.QueryString(context.Request, "examStatus", 0);
            _createTime = ConvertHelper.QueryString(context.Request, "createTime", "");
            _guaranteeId = ConvertHelper.QueryString(context.Request, "guaranteeId", 0);
            _tradeType = ConvertHelper.QueryString(context.Request, "tradeType", -1);
            _branchCompanyId = ConvertHelper.QueryString(context.Request, "branchCompanyId", -1);
            var filter = " 1=1";
            
            if (!string.IsNullOrEmpty(_loanNumber) && _loanNumber != "undefined")
            {
                filter += " and LoanNumber like '%" + _loanNumber + "%'";
            }
            if (_loanUser == 1)
            {
                if (!string.IsNullOrEmpty(_loanMemberName) && _loanMemberName != "undefined")
                {
                    filter += " and MemberName='" + _loanMemberName + "' ";
                }
            }
            if (_loanUser == 2)
            {
                if (!string.IsNullOrEmpty(_loanMemberName) && _loanMemberName != "undefined")
                {
                    filter += " and RealName='" + _loanMemberName + "' ";
                }
            }
            if (_examStatus != 0)
            {
                filter += " and ExamStatus = " + _examStatus;
            }
            else
            {
                filter += " and ExamStatus not in (2,4,6,8,9,10) ";
            }
            if (_tradeType != -1)
            {
                filter += " and TradeType = " + _tradeType;
            }
            if (!string.IsNullOrEmpty(_createTime) && _createTime != "undefined")
            {
                filter += " and convert(char(10),CreateTime,120)='" + _createTime + "' ";
            }
            if (_guaranteeId != 0)
            {
                filter += " and GuaranteeID = " + _guaranteeId;
            }
            if (_branchCompanyId > -1)
            {
                filter += " and ID IN (SELECT LoanID FROM dbo.GreenChannelRecord)";
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
                              loanModel.FullScaleTime,
                              loanModel.LoanUseName,
                              loanModel.ExamStatus,
                              loanModel.ExamStatusName,
                              loanModel.GuaranteeName,
                              loanModel.ReviewTime,
                              loanModel.SoonStatus,
                              loanModel.Mobile
                              //loanModel.RepaymentLastTime
 
                          });
            /*2015-07-10 屏蔽学生贷的初审和复审*/
          
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