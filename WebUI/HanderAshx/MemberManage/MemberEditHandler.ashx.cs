using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.HanderAshx.MemberManage
{
    /// <summary>
    /// MemberEditHandler 的摘要说明
    /// </summary>
    public class MemberEditHandler : IHttpHandler
    {
        private string _sign = "";
        private string _memberId = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            _sign = ConvertHelper.QueryString(context.Request, "sign", "");
            _memberId = ConvertHelper.QueryString(context.Request, "MemberID", "").ToLower();
            if (!string.IsNullOrEmpty(_sign))
            {
                switch (_sign)
                { 
                    case "1":
                        context.Response.Write(SaveRegInfo(context));
                        break;
                    case "2":
                        context.Response.Write(SaveMemberInfo(context));
                        break;
                    case "3":
                        context.Response.Write(SaveDetailInfo(context));
                        break;
                    case "4":
                        context.Response.Write(SaveCashSetting(context));
                        break;
                    case "5":
                        context.Response.Write(SaveUserProfile(context));
                        break;
                    case "6":
                        context.Response.Write(SaveCorporate(context));
                        break;
                }
            }
        }

        private string SaveRegInfo(HttpContext context)
        {
            if (string.IsNullOrEmpty(_memberId))
            {
                return "error";
            }
            bool isLock = ConvertHelper.QueryString(context.Request, "IsLocked", "false").ToLower() == "true";
            bool isAllowWithdraw = ConvertHelper.QueryString(context.Request, "IsAllowWithdraw", "false").ToLower() == "true";
            MemberBll memberBll = new MemberBll();
            MemberModel model = memberBll.GetModel(int.Parse(_memberId));
            model.IsLocked = isLock;
            model.UpdateTime = DateTime.Now;
            model.AllowWithdraw = isAllowWithdraw;
            model.IsMarket = ConvertHelper.QueryString(context.Request, "Marketing", "0")=="1";
            return memberBll.Update(model) ? "success" : "error";
        }

        private string SaveMemberInfo(HttpContext context)
        {
            if (string.IsNullOrEmpty(_memberId))
            {
                return "error";
            }
            else
            {
                int province = ConvertHelper.QueryString(context.Request, "Province", 0);
                int city = ConvertHelper.QueryString(context.Request, "City", 0);
                string address = ConvertHelper.QueryString(context.Request, "Address", "");
                string telephone = ConvertHelper.QueryString(context.Request, "Telephone", "");
                string fax = ConvertHelper.QueryString(context.Request, "Fax", "");
               // string Marketing = ConvertHelper.QueryString(context.Request, "Marketing", "0");
                bool flag = false;
                MemberInfoBll memberInfoBll = new MemberInfoBll();
                MemberInfoModel model = memberInfoBll.GetModel(int.Parse(_memberId));
                model.Province = province;
                model.City = city;
                model.Address = address;
                model.Telephone = telephone;
                model.Fax = fax;
                model.UpdateTime = DateTime.Now;
               // model.IsMarket = Marketing == "1";
                flag = memberInfoBll.Update(model);
                return flag ? "success" : "error";
            }   
        }

        private string SaveDetailInfo(HttpContext context)
        {
            MemberDetailInfoBll memberDetailInfoBll = new MemberDetailInfoBll();
            MemberDetailInfoModel model = memberDetailInfoBll.GetModel(int.Parse(_memberId));
            model.MaritalStatus = ConvertHelper.QueryString(context.Request, "MaritalStatus", 2);
            model.Children = ConvertHelper.QueryString(context.Request, "Children", "false") == "true";
            model.House = ConvertHelper.QueryString(context.Request, "House", "false") == "true";
            model.HouseLoan = ConvertHelper.QueryString(context.Request, "HouseLoan", "false") == "true";
            model.Car = ConvertHelper.QueryString(context.Request, "Car", "false") == "true";
            model.CarLoan = ConvertHelper.QueryString(context.Request, "CarLoan", "false") == "true";
            model.NativePlace = ConvertHelper.QueryString(context.Request, "NativePlace", 0);
            model.DomicilePlace = ConvertHelper.QueryString(context.Request, "DomicilePlace", 0);
            model.HighestDegree = ConvertHelper.QueryString(context.Request, "HighestDegree", 0);
            model.JobStatus = ConvertHelper.QueryString(context.Request, "JobStatus", 0);
            model.MonthlyIncome = ConvertHelper.QueryString(context.Request, "MonthlyIncome", 0);
            model.JobType = ConvertHelper.QueryString(context.Request, "JobType", 0);
            model.CompanyName = ConvertHelper.QueryString(context.Request, "CompanyName", "");
            model.WorkCity = ConvertHelper.QueryString(context.Request, "WorkCity", 0);
            model.CompanyCategory = ConvertHelper.QueryString(context.Request, "CompanyCategory", 0);
            model.CompanySize = ConvertHelper.QueryString(context.Request, "CompanySize", 0);  
            model.WorkTerm = ConvertHelper.QueryString(context.Request, "WorkTerm", 0);
            model.CompanyPhone = ConvertHelper.QueryString(context.Request, "CompanyPhone", "");
            model.CompanyAddress = ConvertHelper.QueryString(context.Request, "CompanyAddress", "");
            model.ContactName = ConvertHelper.QueryString(context.Request, "ContactName", ""); 
            model.ContactRelation = ConvertHelper.QueryString(context.Request, "ContactRelation", "");
            model.ContactPhone = ConvertHelper.QueryString(context.Request, "ContactPhone", "");
            model.UpdateTime = DateTime.Now;
            bool flag = memberDetailInfoBll.Update(model);
            return flag ? "success" : "error";
        }


        private string SaveUserProfile(HttpContext context) 
        {
            MemberDetailInfoBll memberDetailInfoBll = new MemberDetailInfoBll();
          //  LoanMemberInfoModel model = menberDetailInfoBll.UserUpdate(int.Parse(_memberID));
            LoanMemberInfoModel model = new LoanMemberInfoModel();
            model.MemberId = ConvertHelper.QueryString(context.Request, "MemberID", 0);
            model.Age = ConvertHelper.QueryString(context.Request, "Age", 0); 
            model.MaritalStatus = ConvertHelper.QueryString(context.Request,"MaritalStatus",2);
            model.Sex = ConvertHelper.QueryString(context.Request, "Sex", "");
            model.DomicilePlace = ConvertHelper.QueryString(context.Request, "DomicilePlace", 0);
            model.FamilyNum = ConvertHelper.QueryString(context.Request, "FamilyNum", 0);
            model.MonthlyPay = ConvertHelper.QueryString(context.Request, "MonthlyPay", "");
            model.HaveHouse = ConvertHelper.QueryString(context.Request, "HaveHouse", 0) ==1;
            model.HaveCar = ConvertHelper.QueryString(context.Request,"HaveCar", 0) == 1;
            model.WorkingLife = ConvertHelper.QueryString(context.Request,"WorkingLife","");
            model.JobStatus = ConvertHelper.QueryString(context.Request, "JobStatus", "在职员工");
            
            bool flag = memberDetailInfoBll.LoanPubilshUser(model);
            return flag ? "success" : "error";
        }

        private string SaveCorporate(HttpContext context)
        {
            MemberDetailInfoBll memberDetailInfoBll = new MemberDetailInfoBll();
            LoanEnterpriseMemberInfoModel model = new  LoanEnterpriseMemberInfoModel();
            model.MemberId = ConvertHelper.QueryString(context.Request, "MemberId", 0);
            model.Nature = ConvertHelper.QueryString(context.Request, "Nature","");
            model.Size = ConvertHelper.QueryString(context.Request, "Size","");
            model.IndustryCategory = ConvertHelper.QueryString(context.Request, "IndustryCategory","");
            model.CityId = ConvertHelper.QueryString(context.Request, "CityId", 0);
            model.MainProducts = ConvertHelper.QueryString(context.Request, "MainProducts", "");
            model.RegisteredCapital = ConvertHelper.QueryString(context.Request, "RegisteredCapital", "");
            model.BusinessScope = ConvertHelper.QueryString(context.Request, "BusinessScope", "");
            model.SetUpyear = ConvertHelper.QueryString(context.Request, "SetUpyear", "");
            bool flag = memberDetailInfoBll.LoanEnterprise(model);
            return flag ? "success" : "error";
        }

        private string SaveCashSetting(HttpContext context)
        {
            bool flag = ConvertHelper.QueryString(context.Request, "CashSetting", "false").ToLower().Equals("true");
            MemberDetailInfoBll memberDetailInfoBll = new MemberDetailInfoBll();
            bool member = memberDetailInfoBll.SetMemberUnderCash(int.Parse(_memberId), flag);
           return member ? "success" : "error";
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