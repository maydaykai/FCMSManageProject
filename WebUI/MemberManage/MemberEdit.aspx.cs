using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsModel;

namespace WebUI.MemberManage
{
    public partial class MemberEdit : BasePage
    {
        private int _id;

        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "ID", 0);
            if (!IsPostBack)
            {
                LoadData(_id);
            }

        }
        protected MemberModel model {
            get { return new MemberBll().GetModel(_id); }
        }


        private void LoadData(int id)
        {
            // 注册信息加载
            MemberBll memberBll = new MemberBll();
            int total;
            DataTable dt =
                memberBll.GetPageList(
                    " P.ID,P.IsMarket, P.MemberName, P.Mobile, P.Email, P.LastIP, P.RegTime, P.IsLocked, P.LastLoginTime, P.CreditPoint, P.MemberPoint, P.Times, P.Balance, P.Type, P.CompleStatus, P.MemberLevel, P.VIPStartTime, P.VIPEndTime, P.AllowWithdraw",
                    " P.ID=" + id, "  P.ID DESC", 1, 1, out total);
            if (dt.Rows.Count > 0)
            {
                txtMemberName.Value = ObjectToString(dt.Rows[0]["MemberName"]);
                txtMobile.Value = ObjectToString(dt.Rows[0]["Mobile"]);
                txtEmail.Value = ObjectToString(dt.Rows[0]["Email"]);
                txtCreditPoint.Value = ObjectToString(dt.Rows[0]["CreditPoint"]);
                txtMemberPoint.Value = ObjectToString(dt.Rows[0]["MemberPoint"]);
                txtBalance.Value = ObjectToString(dt.Rows[0]["Balance"]);
                if (ObjectToString(dt.Rows[0]["IsMarket"]) == "True" || ObjectToString(dt.Rows[0]["IsMarket"]) == "true")
                {
                    Radio3.Checked = true;
                    Radio4.Checked = false;
                }
                else
                {
                    Radio4.Checked = true;
                    Radio3.Checked = false;
                }
                if (ObjectToString(dt.Rows[0]["Type"]) == "1")
                {
                    rdTypeEntprise.Checked = true;
                }
                else
                {
                    rdTypeIdentity.Checked = true;
                }
                txtMemberLevel.Value = ObjectToString(dt.Rows[0]["MemberLevel"]);
                txtLastIP.Value = ObjectToString(dt.Rows[0]["LastIP"]);
                txtLastLoginTime.Value = OjbectToDateTime(dt.Rows[0]["LastLoginTime"]);
                txtTimes.Value = ObjectToString(dt.Rows[0]["Times"]);
                txtRegTime.Value = OjbectToDateTime(dt.Rows[0]["RegTime"]);
                cbIsLocked.Checked = Convert.ToBoolean(dt.Rows[0]["IsLocked"]);
                txtVIPStartTime.Value = OjbectToDateTime(dt.Rows[0]["VIPStartTime"]);
                txtVIPEndTime.Value = OjbectToDateTime(dt.Rows[0]["VIPEndTime"]);
                cbIsAllowWithdraw.Checked = Convert.ToBoolean(dt.Rows[0]["AllowWithdraw"]);
            }

            // 会员基本信息加载
            MemberInfoBll memberInfoBll = new MemberInfoBll();
            dt =
                memberInfoBll.GetPageList(
                    " P.RealName, P.IdentityCard, P.Sex, P.Province, P.City, P.Address, P.Telephone, P.Fax",
                    " MemberInfo P", "P.MemberID=" + id, "  P.ID DESC", 1, 1, out total);
            if (dt.Rows.Count > 0)
            {
                txtRealName.Value = ObjectToString(dt.Rows[0]["RealName"]);
                txtIdentityCard.Value = ObjectToString(dt.Rows[0]["IdentityCard"]);

                hdProvince.Value = ObjectToString(dt.Rows[0]["Province"]);
                hdCity.Value = ObjectToString(dt.Rows[0]["City"]);
                txtAddress.Value = ObjectToString(dt.Rows[0]["Address"]);
                txtTelephone.Value = ObjectToString(dt.Rows[0]["Telephone"]);
                txtFax.Value = ObjectToString(dt.Rows[0]["Fax"]);
            }

            // 会员详细信息加载
            MemberDetailInfoBll memberDetailInfoBll = new MemberDetailInfoBll();
            dt =
                memberDetailInfoBll.GetPageList(
                    " P.HighestDegree, P.DomicilePlace, P.MaritalStatus, P.Children, P.House, P.HouseLoan, P.Car, P.CarLoan, P.NativePlace, P.JobType, P.JobStatus, P.MonthlyIncome, P.CompanyName, P.WorkCity, P.CompanyCategory, P.CompanySize, P.WorkTerm, P.CompanyPhone, P.CompanyAddress, P.ContactName, P.ContactRelation, P.ContactPhone",
                    " MemberDetailInfo P", "P.MemberID=" + id, "  P.ID DESC", 1, 1, out total);
            if (dt.Rows.Count > 0)
            {
                if (ObjectToString(dt.Rows[0]["MaritalStatus"]) == "1")
                {
                    rdMaritalStatusYes.Checked = true;
                }
                else
                {
                    rdMaritalStatusNo.Checked = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["Children"]))
                {
                    rdChildrenYes.Checked = true;
                }
                else
                {
                    rdChildrenNo.Checked = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["House"]))
                {
                    rdHouseYes.Checked = true;
                }
                else
                {
                    rdHouseNo.Checked = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["HouseLoan"]))
                {
                    rdHouseLoanYes.Checked = true;
                }
                else
                {
                    rdHouseLoanNo.Checked = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["Car"]))
                {
                    rdCarYes.Checked = true;
                }
                else
                {
                    rdCarNo.Checked = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["CarLoan"]))
                {
                    rdCarLoanYes.Checked = true;
                }
                else
                {
                    rdCarLoanNo.Checked = true;
                }
                var areaBll = new AreaBLL();
                var areaNativePlaceModel = areaBll.getAreaByID(ConvertHelper.ToInt(dt.Rows[0]["NativePlace"].ToString()));
                hdNativePlaceProvince.Value = areaNativePlaceModel == null
                                                  ? "0"
                                                  : areaNativePlaceModel.ParentID.ToString();
                hdNativePlaceCity.Value = ObjectToString(dt.Rows[0]["NativePlace"]);
                var areaDomicilePlaceModel =
                    areaBll.getAreaByID(ConvertHelper.ToInt(dt.Rows[0]["DomicilePlace"].ToString()));
                hdDomicilePlaceProvince.Value = areaDomicilePlaceModel == null
                                                    ? "0"
                                                    : areaDomicilePlaceModel.ParentID.ToString();
                hdDomicilePlaceCity.Value = ObjectToString(dt.Rows[0]["DomicilePlace"]);
                selHighestDegree.Value = ObjectToString(dt.Rows[0]["HighestDegree"]);
                selJobStatus.Value = ObjectToString(dt.Rows[0]["JobStatus"]);
                selMonthlyIncome.Value = ObjectToString(dt.Rows[0]["MonthlyIncome"]);
                selJobType.Value = ObjectToString(dt.Rows[0]["JobType"]);
                txtCompanyName.Value = ObjectToString(dt.Rows[0]["CompanyName"]);
                var areaWorkCityModel = areaBll.getAreaByID(ConvertHelper.ToInt(dt.Rows[0]["WorkCity"].ToString()));
                hdWorkCityProvince.Value = areaWorkCityModel == null ? "0" : areaWorkCityModel.ParentID.ToString();
                hdWorkCityCity.Value = ObjectToString(dt.Rows[0]["WorkCity"]);
                selCompanyCategory.Value = ObjectToString(dt.Rows[0]["CompanyCategory"]);
                selCompanySize.Value = ObjectToString(dt.Rows[0]["CompanySize"]);
                selWorkTerm.Value = ObjectToString(dt.Rows[0]["WorkTerm"]);
                txtCompanyPhone.Value = ObjectToString(dt.Rows[0]["CompanyPhone"]);
                txtCompanyAddress.Value = ObjectToString(dt.Rows[0]["CompanyAddress"]);
                txtContactName.Value = ObjectToString(dt.Rows[0]["ContactName"]);
                txtContactRelation.Value = ObjectToString(dt.Rows[0]["ContactRelation"]);
                txtContactPhone.Value = ObjectToString(dt.Rows[0]["ContactPhone"]);

                //会员企业信息加载
                //    string sql = model.Type == 0 ? "SELECT LI.*,A.ParentID FROM dbo.LoanMemberInfo LI inner join Area A ON LI.DomicilePlace=A.ID WHERE MemberId=@MemberID" : "SELECT * FROM dbo.LoanEnterpriseMemberInfo WHERE MemberId=@MemberID";
                //    var dtt = new LoanBll().GetUserInfo(model.ID, sql);

                //    if (dtt != null && dtt.Rows.Count > 0)
                //    {
                //        if (model.Type == 0)
                //        {
                //            txt_memberAge.Value = ObjectToString(dtt.Rows[0]["Age"]);
                //            if (ObjectToString(dtt.Rows[0]["MaritalStatus"]) == "1")
                //            {
                //                Marriage1.Checked = true;
                //            }
                //            else
                //            {
                //                Marriage.Checked = true;
                //            }

                //        if (ObjectToString(dtt.Rows[0]["Sex"]).Equals("男"))
                //        {
                //            RadioMan.Checked = true;
                //        }
                //        else
                //        {
                //            RadioWoman.Checked = true;
                //        }
                //        hdWorkCityProvince1.Value = areaNativePlaceModel == null ? "0" : areaNativePlaceModel.ParentID.ToString();
                //        hdWorkCityCity1.Value = ObjectToString(dtt.Rows[0]["DomicilePlace"]);
                //        txt_familyNum.Value = ObjectToString(dtt.Rows[0]["FamilyNum"]);
                //        txt_monthlyIncome.Value = ObjectToString(dtt.Rows[0]["MonthlyPay"]);

                //        if (ObjectToString(dtt.Rows[0]["HaveHouse"]) == "True")
                //        {
                //            txt_Radioyse.Checked = true;
                //        }
                //        else
                //        {
                //            txt_Radiono.Checked = true;
                //        }

                //            txt_familyNum.Value = ObjectToString(dtt.Rows[0]["FamilyNum"]);
                //            txt_monthlyIncome.Value = ObjectToString(dtt.Rows[0]["MonthlyPay"]);

                //            if (ObjectToString(dtt.Rows[0]["HaveHouse"]) == "True")
                //            {
                //                txt_Radioyse.Checked = true;
                //            }
                //            else
                //            {
                //                txt_Radiono.Checked = true;
                //            }


                //            if (ObjectToString(dtt.Rows[0]["HaveCar"]) == "True")
                //            {
                //                txt_Caryes.Checked = true;
                //            }
                //            else
                //            {
                //                txt_Carno.Checked = true;
                //            }

                //            text_workDuration.Value = ObjectToString(dtt.Rows[0]["WorkingLife"]);
                //            if (ObjectToString(dtt.Rows[0]["JobStatus"]).Equals("在职员工"))
                //            {
                //                txt_Employee.Checked = true;
                //            }
                //            else
                //            {
                //                txt_Haveboss.Checked = true;
                //            }

                //        }
                //        else if (model.Type == 1)
                //        {
                //            sel_companyNature.Value = ObjectToString(dtt.Rows[0]["Nature"]);
                //            var areaNativePlaceModel2 = areaBll.getAreaByID(ConvertHelper.ToInt(dt.Rows[0]["CityID"].ToString()));
                //            HiddenState.Value = areaNativePlaceModel2 == null ? "0" : areaNativePlaceModel.ParentID.ToString();
                //            Hiddencity.Value = ObjectToString(dt.Rows[0]["CityID"]);
                //            sel_industry.Value = ObjectToString(dtt.Rows[0]["IndustryCategory"]);
                //            txt_regCapital.Value = ObjectToString(dtt.Rows[0]["RegisteredCapital"]);
                //            text_mainBusiness.Value = ObjectToString(dtt.Rows[0]["MainProducts"]);
                //            text_companyAge.Value = ObjectToString(dtt.Rows[0]["SetUpyear"]);
                //            text_businessScope.Value = ObjectToString(dtt.Rows[0]["BusinessScope"]);
                //            text_employeeNum.Value = ObjectToString(dtt.Rows[0]["Size"]);
                //        }
                //    }

                }


                bool flag = memberDetailInfoBll.GetUnderCashSetting(id);
                cbCashSetting.Checked = flag;
                Repeater1Bind();
                MemberDetailInfoBll memberdetailInfobll = new MemberDetailInfoBll();
                bool member = memberdetailInfobll.SetMember(_id);
            setting.Attributes.Add("style", member ? "display:block;" : "display:none;");

            if (model.Type == 0)
                {
                    memberInfo.Attributes.Add("style", "display:block");
                    enterpriseInfo.Attributes.Add("style", "display:none");

                }
                else
                {
                    memberInfo.Attributes.Add("style", "display:none");
                    enterpriseInfo.Attributes.Add("style", "display:block");
                }

                if (!Role.IsRoleNew(RightArray, "3"))
                {
                    btnSaveRegInfo.Visible = false;
                    btnSaveMemberInfo.Visible = false;
                    btnSaveDetailInfo.Visible = false;
                    btnSaveCashSetting.Visible = false;
                    btnOK.Visible = false;
                    btnCorporate.Visible = false;
                }

            }


        private void Repeater1Bind()
        {
            //会员投资记录
            int PageSize = Pagination1.PageSize;
            int CurrentPage = Pagination1.CurrentPage;
            int TotalRows = 0;
            DataTable bidRecordDt = new BidBLL().GetBidRecordDt("B.MemberID=" + _id, "B.ID DESC", CurrentPage, PageSize,
                                                                ref TotalRows);
            Repeater1.DataSource = bidRecordDt;
            Repeater1.DataBind();
            Pagination1.TotalRecords = TotalRows;
        }

        protected void Pagination1_PageChanged(object sender, CommandEventArgs e)
        {
            Repeater1Bind();
        }
        private string ObjectToString(object obj)
        {
            return obj == null ? "" : obj.ToString();
        }

        private string OjbectToDateTime(object obj)
        {
            return obj == null ? "" : string.Format("{0:yyyy-MM-dd HH:mm:ss}", obj);
        }
    }

}