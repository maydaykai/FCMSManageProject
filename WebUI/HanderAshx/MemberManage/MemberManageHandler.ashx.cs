using ManageFcmsBll;
using ManageFcmsCommon;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// MemberManageHandler 的摘要说明
    /// </summary>
    public class MemberManageHandler : IHttpHandler
    {
        private int _currentPage = 1;
        private int _pageSize;
        private string _sort = "asc";
        private string _sortField = "ID";
        private string _sUserType = "0";
        private string _isLock = "";
        private string _type = "";
        private string _compleStatus = "";
        private string _isVip = "";
        private string _uName = "";
        private string _output = "";
        private int _flag;
        private string _totalDate;
        private int _branchCompanyId;
       

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
            _sort = ConvertHelper.QueryString(context.Request, "sortOrder", "DESC");
            _sortField = ConvertHelper.QueryString(context.Request, "sortdatafield", "P.ID");

            _sUserType = ConvertHelper.QueryString(context.Request, "sUserType", "0");
            _isLock = ConvertHelper.QueryString(context.Request, "isLock", "");
            _type = ConvertHelper.QueryString(context.Request, "type", "");
            _compleStatus = ConvertHelper.QueryString(context.Request, "compleStatus", "");
            _isVip = ConvertHelper.QueryString(context.Request, "isVip", "");
            _uName = ConvertHelper.QueryString(context.Request, "uName", "");
            _output = ConvertHelper.QueryString(context.Request, "output", "");
            _flag = ConvertHelper.QueryString(context.Request, "flag", 0);
            _totalDate = ConvertHelper.QueryString(context.Request, "totalDate", DateTime.Now.ToString("yyyy-MM-dd"));
            _branchCompanyId = ConvertHelper.QueryString(context.Request, "branchCompanyId", -1);

            var filter = " 1=1";

            if (!string.IsNullOrEmpty(_uName))
            {
                if (_sUserType == "0")
                {
                    filter += " and P.MemberName like '%" + _uName + "%'";
                }
                else if(_sUserType =="1")
                {
                    filter += " and M.RealName like '%" + _uName + "%'";
                }
                else
                    filter += " and P.Mobile = '" + _uName + "'";
            }
            if (!string.IsNullOrEmpty(_isLock))
            {
                filter += " and P.IsLocked=" + _isLock;
            }
            if (!string.IsNullOrEmpty(_type))
            {
                filter += " and P.Type=" + _type;
            }
            if (!string.IsNullOrEmpty(_compleStatus))
            {
                filter += " and P.CompleStatus=" + _compleStatus;
            }
            if (!string.IsNullOrEmpty(_isVip))
            {
                if (_isVip == "1")
                {
                    filter += " and P.VIPStartTime is not null";
                }
                else
                {
                    filter += " and P.VIPStartTime is null";
                }
            }
            if (_branchCompanyId > -1)
            {
                filter += " and P.ID IN (SELECT MemberID FROM dbo.BranchCompanyMember";
                if(_branchCompanyId > 0)
                    filter += " WHERE BranchCompanyID=" + _branchCompanyId;
                filter += ")";
            }
            var sortColumn = _sortField + " " + _sort;

            if (!string.IsNullOrEmpty(_output))
            {
                filter += " and convert(char(10),P.RegTime,120) = '" + _totalDate + "'";
                context.Response.ContentType = "application/x-xls";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=MemberList.xls");
                HSSFWorkbook hssfWorkbook = OutputSearchResult(sortColumn, filter);
                hssfWorkbook.Write(context.Response.OutputStream);
            }
            else
            {
                
                context.Response.Write(_flag == 1 ? GetPageList1(_currentPage, _pageSize, sortColumn, filter): GetPageList(_currentPage, _pageSize, sortColumn, filter));
            }
        }

        //获得分页数据
        public object GetPageList(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var memberBll = new MemberBll();
            var dt = memberBll.GetPageList(" P.ID, P.MemberName, M.RealName, P.Mobile, P.Email, IsLocked=(case when P.IsLocked=1 then '是' else '否' end), P.RegTime, P.LastLoginTime, P.Balance, P.Type, TypeStr=(case when P.Type=-1 then '平台账户' when P.Type=0 then '个人会员' when P.Type=1 then '企业会员' when P.Type=2 then '担保公司' when P.Type=3 then '回购公司' end), CompleStatus=(case when P.CompleStatus=1 then '注册完成'  when P.CompleStatus=2 and P.Type=0 then '个人认证完成' when P.CompleStatus=2 and P.Type=1 then '企业认证完成' when P.CompleStatus=3 then 'VIP已申请' when P.CompleStatus=4 then '完善个人信息' end), MemberLevel=(case when P.MemberLevel=1 then 'VIP会员' else '普通会员' end), P.VIPStartTime, P.VIPEndTime, BC.Name,AllowWithdraw=(case when P.AllowWithdraw=1 then '是' else '否' end),IsMarket=(case when P.IsMarket=1 then '是' else '否' end),IsDueIn=(case when dbo.GetMemberAccountPayable(P.ID)>=100000 then '是' else '否' end)", filter, sortField, pagenum, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            var balanceTatal = memberBll.MemberBalanceTatal(filter);
            if (balanceTatal == null)
            {
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":0,\"Rows\":" + jsonStr + "}";
            }
            else
            {
                balanceTatal = ConvertHelper.ToDecimal(balanceTatal.ToString());
                jsonStr = "{\"TotalRows\":" + total + ",\"AmountAggregate\":" + balanceTatal + ",\"Rows\":" + jsonStr + "}";
            }
            return jsonStr;
            //jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            //return jsonStr;
        }

        //获得分页数据1 
        public object GetPageList1(int pagenum, int pagesize, string sortField, string filter)
        {
            pagenum = pagenum + 1;
            int total;
            var memberBll = new MemberBll();
            var dt = memberBll.GetPageList1(" P.ID, P.MemberName, M.RealName,P.Mobile, P.Email, P.RecoCode , IsLocked=(case when P.IsLocked=1 then '是' else '否' end), P.RegTime, P.LastLoginTime, P.Balance, P.Type, TypeStr=(case when P.Type=-1 then '平台账户' when P.Type=0 then '个人会员' when P.Type=1 then '企业会员' when P.Type=2 then '担保公司' when P.Type=3 then '回购公司' end), CompleStatus=(case when P.CompleStatus=1 then '注册完成'  when P.CompleStatus=2 and P.Type=0 then '个人认证完成' when P.CompleStatus=2 and P.Type=1 then '企业认证完成' when P.CompleStatus=3 then 'VIP已申请' when P.CompleStatus=4 then '完善个人信息'  end), MemberLevel=(case when P.MemberLevel=1 then 'VIP会员' else '普通会员' end), P.VIPStartTime, P.VIPEndTime,C.CouponCode, (select case when COUNT(*)=0 then 0 else 1 end from dbo.Bidding where MemberID = P.ID) BiddingCount  ,AllowWithdraw=(case when P.AllowWithdraw=1 then '是' else '否' end),IsMarket=(case when P.IsMarket=1 then '是' else '否' end),IsDueIn=(case when dbo.GetMemberAccountPayable(P.ID)>=100000 then '是' else '否' end)", filter, sortField, pagenum, pagesize, out total);
            var jsonStr = JsonHelper.DataTableToJson(dt);
            jsonStr = "{\"TotalRows\":" + total + ",\"Rows\":" + jsonStr + "}";
            return jsonStr;
        }

        //数据导出
        public HSSFWorkbook OutputSearchResult(string sortField, string filter)
        {
            int total;
            var memberBll = new MemberBll();
            var dt = memberBll.GetMemberList(" P.ID, P.MemberName, M.RealName, P.Mobile, P.Email, IsLocked=(case when P.IsLocked=1 then '是' else '否' end), P.RegTime, P.LastLoginTime, P.Balance, Type=(case when P.Type=0 then '个人会员' when P.Type=1 then '企业会员' end), CompleStatus=(case when P.CompleStatus=1 then '注册完成' when P.CompleStatus=2 and P.Type=0 then '个人认证完成' when P.CompleStatus=2 and P.Type=1 then '企业认证完成' when P.CompleStatus=3 then '银行卡认证完成' when P.CompleStatus=4 then 'VIP已申请' when P.CompleStatus=5 then '填写个人详细信息' end), MemberLevel=(case when P.MemberLevel=1 then 'VIP会员' else '普通会员' end), P.VIPStartTime, P.VIPEndTime,AllowWithdraw=(case when P.AllowWithdraw=1 then '是' else '否' end),IsMarket=(case when P.IsMarket=1 then '是' else '否' end),IsDueIn=(case when dbo.GetMemberAccountPayable(P.ID)>=100000 then '是' else '否' end)", filter, sortField, out total);
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("会员列表");
            IRow rowHeader = sheet.CreateRow(0);
            rowHeader.CreateCell(0, CellType.STRING).SetCellValue("会员名");
            rowHeader.CreateCell(1, CellType.STRING).SetCellValue("真实姓名");
            rowHeader.CreateCell(2, CellType.STRING).SetCellValue("手机号码");
            rowHeader.CreateCell(3, CellType.STRING).SetCellValue("电子邮箱");
            rowHeader.CreateCell(4, CellType.STRING).SetCellValue("是否锁定");
            rowHeader.CreateCell(5, CellType.STRING).SetCellValue("可用余额");
            rowHeader.CreateCell(6, CellType.STRING).SetCellValue("会员类型");
            rowHeader.CreateCell(7, CellType.STRING).SetCellValue("注册完成状态");
            rowHeader.CreateCell(8, CellType.STRING).SetCellValue("注册时间");
            rowHeader.CreateCell(9, CellType.STRING).SetCellValue("最后一次登陆时间");
            rowHeader.CreateCell(10, CellType.STRING).SetCellValue("会员等级");
            rowHeader.CreateCell(11, CellType.STRING).SetCellValue("VIP开始时间");
            rowHeader.CreateCell(12, CellType.STRING).SetCellValue("VIP结束时间");
            rowHeader.CreateCell(13, CellType.STRING).SetCellValue("是否允许提现");
            rowHeader.CreateCell(14, CellType.STRING).SetCellValue("是否营销人员");
            rowHeader.CreateCell(15, CellType.STRING).SetCellValue("待收是否大于10W");
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                IRow dataRow = sheet.CreateRow(rowIndex + 1);
                dataRow.CreateCell(0, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["MemberName"]));
                dataRow.CreateCell(1, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["RealName"]));
                dataRow.CreateCell(2, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Mobile"]));
                dataRow.CreateCell(3, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Email"]));
                dataRow.CreateCell(4, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["IsLocked"]));
                dataRow.CreateCell(5, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Balance"]));
                dataRow.CreateCell(6, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["Type"]));
                dataRow.CreateCell(7, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["CompleStatus"]));
                dataRow.CreateCell(8, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["RegTime"]));
                dataRow.CreateCell(9, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["LastLoginTime"]));
                dataRow.CreateCell(10, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["MemberLevel"]));
                dataRow.CreateCell(11, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["VIPStartTime"]));
                dataRow.CreateCell(12, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["VIPEndTime"]));
                dataRow.CreateCell(13, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["AllowWithdraw"]));
                dataRow.CreateCell(14, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["IsMarket"]));
                dataRow.CreateCell(15, CellType.STRING).SetCellValue(ConvertToString(dt.Rows[rowIndex]["IsDueIn"]));
            }
            return hssfWorkbook;
        }

        private string ConvertToString(object obj)
        {
            return obj == null ? "" : obj.ToString();
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