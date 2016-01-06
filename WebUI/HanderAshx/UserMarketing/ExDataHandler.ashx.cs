using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.UserMarketing
{
    /// <summary>
    /// ExDataHandler 的摘要说明 数据报表
    /// </summary>
    public class ExDataHandler : IHttpHandler
    {

        //  public List<Marketing_EX_Data> GetEx_DataPageList(string starttime, string endtime, int roleId, int PageSize, int CurrentPage, ref int PageCount, ref int RecordCount, ref int SumRegcount, ref decimal SumRegmoney,
        // ref decimal SumBidNumContinued, ref decimal SumSuccessTransferMoney, ref decimal SumRealMoney, ref decimal SumInterest)

        private string _starttime = "";//开始时间
        private string _endtime = "";
        private int _userId = -1;
        private int _roleId = -1;
        private int _PageSize = 0;
        private int _CurrentPage = 0;
        private int _PageCount = 0;
        private int _RecordCount = 0;
        private int _SumRegcount = 0;
        private decimal _SumRegmoney=0;
        private int _SumBidNumContinued = 0;
        private decimal _SumSuccessTransferMoney = 0;
        private decimal _SumBidAmount = 0;
        private decimal _SumRealMoney = 0;
        private decimal _SumInterest = 0;
        private decimal _SumCurr_MouthMoney = 0;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _starttime = ConvertHelper.QueryString(context.Request, "starttime", "");
            _endtime = ConvertHelper.QueryString(context.Request, "endtime", "");
            _userId = ConvertHelper.QueryString(context.Request, "userId", 0);
            _roleId = ConvertHelper.QueryString(context.Request, "roleId", -1);
            _PageSize = ConvertHelper.QueryString(context.Request, "pagesize", 10);
            _CurrentPage = ConvertHelper.QueryString(context.Request, "pagenum", 1);


            context.Response.Write(GetFcmsUserList(_starttime, _endtime, _userId, _roleId, _PageSize, _CurrentPage, ref _PageCount, ref _RecordCount, ref _SumRegcount, ref _SumRegmoney, ref _SumBidNumContinued,
             ref _SumBidAmount, ref _SumSuccessTransferMoney, ref _SumRealMoney, ref _SumInterest, ref _SumCurr_MouthMoney));
        }

        public Object GetFcmsUserList(string starttime, string endtime,int userId, int roleId, int PageSize, int CurrentPage, ref int PageCount, ref int RecordCount, ref int SumRegcount, ref decimal SumRegmoney,
          ref int SumBidNumContinued, ref decimal SumBidAmount, ref decimal SumSuccessTransferMoney, ref decimal SumRealMoney, ref decimal SumInterest, ref decimal SumCurr_MouthMoney)
        {
            CurrentPage = CurrentPage + 1;

            int pageCount = 0;
            var fcmsUserbll = new Marketing_Ex_PersonBLL();
            // DataSet dsRoles = new RoleBll().GetRoleList();
            IList<Marketing_EX_Data> fcmsUserModelList = fcmsUserbll.GetEx_DataPageList(starttime, endtime, userId, roleId, PageSize, CurrentPage, ref PageCount, ref RecordCount, ref SumRegcount, ref SumRegmoney,
                ref SumBidNumContinued, ref SumBidAmount, ref SumSuccessTransferMoney, ref SumRealMoney, ref SumInterest, ref SumCurr_MouthMoney);
            var orders = (from model in fcmsUserModelList
                          select new
                          {
                              Id = model.Id,
                              Department = model.Department,
                              GroupName = model.Group,
                              RoleName = model.RoleName,
                              Name=model.Name,
                              Regcount = model.Regcount,
                              Regmoney=model.Regmoney,
                              BidNumContinued=model.BidNumContinued,
                              SumBidAmount=model.SumBidAmount,
                              SuccessTransferMoney=model.SuccessTransferMoney,
                              RealMoney=model.Regmoney,
                              Interest=model.Interest,
                              Curr_MouthMoney=model.Curr_MouthMoney
                          });
            var jsonData = new
            {
                TotalRows = PageCount,//记录数
                SumRegcount =SumRegcount,
                SumRegmoney=SumRegmoney,
                SumBidNumContinued=SumBidNumContinued,
                SumBidAmount=SumBidAmount,
                SumSuccessTransferMoney=SumSuccessTransferMoney,
                SumRealMoney=SumRealMoney,
                SumInterest=SumInterest,
                SumCurr_MouthMoney=SumCurr_MouthMoney,
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