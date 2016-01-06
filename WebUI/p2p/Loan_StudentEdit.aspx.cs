using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Text;

namespace WebUI.p2p
{
    public partial class Loan_StudentEdit : BasePage
    {
        //调用业务
        LoadStudentBLL _student = new LoadStudentBLL();
        AreaBLL _arebll = new AreaBLL();
        LoanBll _loan = new LoanBll();
        FcmsUserBll fcmsUserbll = new FcmsUserBll();

        #region 声明信息
        #endregion
        private int _loanId = 0;
        int memberId = 0;
        int PlaceResidenceCity = 0;//所在城市ID
        int NativePlaceCity = 0;//籍贯
        int StudentLoanApplyId = 0;

        private static LoanModel _loanInfoModel = new LoanModel();
        // private static FcmsUserModel _fcmsUserModel = new FcmsUserModel();
        private static LoanModel _loanModel = new LoanModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            memberId = Convert.ToInt32(Request.QueryString["ID"].ToString());
            StudentLoanApplyId = Convert.ToInt32(Request.QueryString["StudentLoanApplyId"].ToString());
            string curr_days = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            hidd_start.Value = curr_days;
            hidd_end.Value = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm");

            if (!IsPostBack)
            {
                LoadMemberInfo();
                //LoadSetValue();
                FileInit();
            }
            else
            {

                if (!string.IsNullOrEmpty(hidd_loanId.Value))
                {
                    FileInit();

                    var sumScore = new LoanScoreBll().GetSumScoreList(Convert.ToInt32(hidd_loanId.Value));

                    lab_SumScore.InnerText = sumScore.Tables[0].Rows[0]["SumScore"].ToString();
                    lab_ScoreLevel.InnerText = sumScore.Tables[0].Rows[0]["ScoreLevel"].ToString();
                }


            }

        }

        /// <summary>
        /// 加载默认信息
        /// </summary>
        public void LoadSetValue()
        {
            txtBidAmountMin.Text = "100";
            txtAutoBidScale.Value = "0";
            txtGuaranteeFee.Value = "4.0";
            txt_server1.Value = "1.00";
            txt_server2.Value = "10.00";
        }

        /// <summary>
        /// 加载下拉列表
        /// </summary>
        public void LoadList()
        {
            //  var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetDimLoanUseList_StudenInfo();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_loanUseName.DataSource = ds;
            sel_loanUseName.DataValueField = "ID";
            sel_loanUseName.DataTextField = "LoanUseName";
            sel_loanUseName.DataBind();
            sel_loanUseName.Items.Insert(0, new ListItem("--请选择--", "0"));

        }

        /// <summary>
        /// 加载信息
        /// </summary>
        public void LoadMemberInfo()
        {
            StudentRlust info = new StudentRlust();
            var memberbll = new MemberBll();
            var memberinfobll = new MemberInfoBll();
            var LoanModelbll = new LoanBll();

            //用户信息
            info.MemberModel = memberbll.GetModel(memberId);
            info.MemberInfoModel = memberinfobll.GetModel(memberId);
            //学生信息
            string temp_StudentInfo = JsonConvert.SerializeObject(_student.GetLoan_StudentInfo(memberId));
            info.studentInfo = JsonConvert.DeserializeObject<List<StudentInfo>>(temp_StudentInfo)[0];
            //查询学生借款信息
            string tem_StudentLoanApply = JsonConvert.SerializeObject(_student.GetLoan_StudentLoanApplyInfo(StudentLoanApplyId));
            info.studentLoanApply = JsonConvert.DeserializeObject<List<StudentLoanApply>>(tem_StudentLoanApply)[0];

            //更新学生的基本信息
            string year = info.MemberInfoModel.IdentityCard.Substring(6, 4); //1989
            string currentYear = DateTime.Now.Year.ToString(); //2011

            StudentInfoBLL _stubll = new StudentInfoBLL();
            int age = Convert.ToInt32(currentYear) - Convert.ToInt32(year);
            info.studentInfo.Age = age;
            _stubll.UpdateByMemberId(info.studentInfo);


            //如果存在借款标Id则查询借款信息
            if (info.studentLoanApply.LoanId > 0)
            {
                info.loanModel = LoanModelbll.GetLoanModel(info.studentLoanApply.LoanId);
            }
            Info(info);

        }


        //绑定担保公司下拉列表
        private void BindGcList()
        {
            // var fcmsUserbll = new FcmsUserBll();
            var ds = fcmsUserbll.GetGcList();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            sel_Guarantee.DataSource = ds;
            sel_Guarantee.DataValueField = "RelationID";
            sel_Guarantee.DataTextField = "RealName";
            sel_Guarantee.DataBind();
            sel_Guarantee.Items.Insert(0, new ListItem("--请选择--", "0"));
            //保存到当前字典
            sel_Guarantee.Value = "243";

            //加载

            for (int i = 1; i < 37; i++)
            {
                lab_LoanTerm.Items.Insert(i - 1, new ListItem(i.ToString() + "个月", i.ToString()));
            }

        }

        //根据Id查询信息
        public void Info(StudentRlust model)
        {
            LoadList();
            BindGcList();
            #region 用户信息
            lab_MemberName.InnerText = model.MemberModel.MemberName;//用户名
            lab_RealName.InnerText = model.MemberInfoModel.RealName;
            lab_IdentityCard.InnerText = model.MemberInfoModel.IdentityCard;
            lab_Sex.InnerText = model.studentInfo.Sex;
            // lab_Age.InnerText = model.studentInfo.Age.ToString();//WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' })
            #endregion
            WdataBidStartTime.Attributes.Add("onclick", "return WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' });");
            WdataBidEndTime.Attributes.Add("onclick", "return WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' });");
            #region 学生的借款信息
            lab_LoanAmount.Text = model.studentLoanApply.LoanAmount.ToString("F2");
            lab_LoanRate.Value = model.studentLoanApply.LoanRate.ToString();//利率
            lab_LoanTerm.Value = model.studentLoanApply.LoanTerm.ToString();
            lab_UseDescription.InnerText = model.studentLoanApply.UseDescription;
            lab_AuditRecords.InnerText = model.studentLoanApply.AuditRecords;
            txt_server1.Value = "1.00";//区间服务费
            txt_server2.Value = "10.00";
            //借款期限
            switch (model.studentLoanApply.LoanTerm)
            {
                case 0:
                case 1:
                case 2:
                    lab_LoanRate.Value = "10.00";
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    lab_LoanRate.Value = "10.00";
                    break;
                default:
                    lab_LoanRate.Value = "10.00";
                    break;

            }

            int curr_University = (DateTime.Now.Year - model.studentInfo.EnrollmentYear.Year) <= 0 ? 1 : (DateTime.Now.Year - model.studentInfo.EnrollmentYear.Year);
            string[] array = new string[] { "零", "一", "二", "三", "四" };

            string curr_year = curr_University >= 4 ? array[4] : array[curr_University];
            //借款描叙
            //本人为深圳大学2013级电子信息专业大二学生，借款是用于旅游的费用。望大家支持，谢谢！ 
            string UseDescription = string.Format("本人为{0}{1}级{2}{3}学生，借款是用于{4}。望大家支持，谢谢！",
                model.studentInfo.UniversityName, model.studentInfo.EnrollmentYear.ToString("yyyy"), "大" + curr_year + "", model.studentInfo.Professional,
                fcmsUserbll.GetDimLoanUseList_StudenInfo().Tables[0].Select("ID=" + model.studentLoanApply.LoanUseId)[0]["LoanUseName"]);


            //（1）经调查，借款人为深圳大学2013级电子信息专业大二学生，借款是用于旅游的费用。
            //（2）深圳市金信联融资担保有限公司经调查同意为该笔借款提供担保。
            //综上所诉，平台同意其2000元融资申请。 担保公司 默认为第一个
            //  string company = fcmsUserbll.GetGcList().Tables[0].Rows[0]["AnotherName"].ToString();
            string remake = string.Format("(1) 经调查，借款人为{0}{1}级{2}{3}学生，借款是用于{4} </br>(2){5}经调查同意为该笔借款提供担保。</br> (3)综上所诉，平台同意其{6}元融资申请。",
                model.studentInfo.UniversityName, model.studentInfo.EnrollmentYear.ToString("yyyy"), "大" + curr_year + "", model.studentInfo.Professional,
               fcmsUserbll.GetDimLoanUseList_StudenInfo().Tables[0].Select("ID=" + model.studentLoanApply.LoanUseId)[0]["LoanUseName"], "深圳市金信联融资担保有限公司", model.studentLoanApply.LoanAmount);
            textReviewComments.InnerText = remake;
            lab_createTime.InnerText = model.studentLoanApply.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            sel_repaymentMethod.Value = model.studentLoanApply.AuditRecords != "" ? "1" : "0";//是否审核
            sel_loanUseName.Value = model.studentLoanApply.LoanUseId.ToString();//借款用途
            //审核结果
            StringBuilder str = new StringBuilder();
            str.Append("<h4>★项目描述★</h4><div><p style = \"padding-left:20px;\"></p>" + UseDescription + "</div>");
            str.Append("<h4>★平台审批意见★</h4><div><p style = \"padding-left:20px;\"></p>" + remake + "</div>");

            //HtmlHelper.
            content1.InnerText = str.ToString();



            switch (model.studentLoanApply.ExamStatus)
            {
                case 2: radio_Audit_loading.Checked = true; break;
                case 1: radio_audit.Checked = true; break;

            }
            // radio_audit
            //图片信息
            lab_PositiveIdentityCard.Text = "<a href=\"" + model.studentInfo.PositiveIdentityCard + "\" target=\"_blank\">点击查看</a>";
            lab_NegativeIdentityCard.Text = "<a href=\"" + model.studentInfo.NegativeIdentityCard + "\" target=\"_blank\">点击查看</a>";
            lab_StudentIDCard.Text = "<a href=\"" + model.studentInfo.StudentIDCard + "\" target=\"_blank\">点击查看</a>";
            //lba_Bankwater.Text = "<a href=\"" + model.studentInfo.StudentInformationScreenshot + "\" target=\"_blank\">点击查看</a>";//银行流水
            lab_StudentInformationScreenshot.Text = "<a href=\"" + model.studentInfo.StudentInformationScreenshot + "\" target=\"_blank\">点击查看</a>";
            lab_Alipay.Text = "<a href=\"" + model.studentInfo.StudentInformationalipay + "\" target=\"_blank\">点击查看</a>";
            lab_StudentInformationlastYearAlipay.Text = "<a href=\"" + model.studentInfo.StudentInformationlastYearAlipay + "\" target=\"_blank\">点击查看</a>";
            lab_HeadsetIdentityCard.Text = "<a href=\"" + model.studentInfo.HeadsetIdentityCard + "\" target=\"_blank\">点击查看</a>";
            #endregion

            #region 学生的个人信息
            lab_UniversityName.InnerText = model.studentInfo.UniversityName;
            lab_Education.InnerText = model.studentInfo.Education;
            //lab_FamilySize.InnerText = model.studentInfo.FamilySize.ToString();//家庭人数
            PlaceResidenceCity = model.studentInfo.PlaceResidenceCity;
            lab_StudentID.InnerText = model.studentInfo.StudentID;
            lab_EnrollmentYear.InnerText = model.studentInfo.EnrollmentYear.ToString("yyyy-MM-dd");

            //lab_GraduationDate.InnerText = model.studentInfo.GraduationDate.ToString("yyyy-MM-dd");
            lab_Mobile.InnerText = model.MemberModel.Mobile;
            lab_Professional.InnerText = model.studentInfo.Professional;
            //地址信息_arebll.GetAreList("id=" + model.studentInfo.PlaceResidenceCity)
            string temp_are = JsonConvert.SerializeObject(_arebll.GetAreList("a.id=" + model.studentInfo.PlaceResidenceCity));
            View_AreaModel aremodel = JsonConvert.DeserializeObject<List<View_AreaModel>>(temp_are).FirstOrDefault();
            lab_Mailingaddress.InnerText = aremodel.ProvinceName + aremodel.CityName + model.studentInfo.PlaceResidenceDetailed;
            //lab_NativePlaceCity
            //  string temp_NativePlaceCity = JsonConvert.SerializeObject(_arebll.GetAreList("a.id=" + model.studentInfo.NativePlaceCity));
            // View_AreaModel are_native = JsonConvert.DeserializeObject<List<View_AreaModel>>(temp_NativePlaceCity).FirstOrDefault();
            // lab_NativePlaceCity.InnerText = are_native.ProvinceName + are_native.CityName;
            //家庭住址
            string temp_account = JsonConvert.SerializeObject(_arebll.GetAreList("a.id=" + model.studentInfo.AccountLocationCity));
            View_AreaModel are_account = JsonConvert.DeserializeObject<List<View_AreaModel>>(temp_account).FirstOrDefault();
            lab_AccountLocationCity.InnerText = are_account.ProvinceName + are_account.CityName + model.studentInfo.AccountLocationDetailed;


            List<RelationshipModel> list = JsonConvert.DeserializeObject<List<RelationshipModel>>(model.studentInfo.RelativeContactMethod);
            List<RelationshipModel> FriendList = JsonConvert.DeserializeObject<List<RelationshipModel>>(model.studentInfo.FriendContactMethod);




            lab_Lineal_one.InnerText = list[0].Name;

            lab_Lineal_one_mobile.InnerText = list[0].Phone;

            // lab_Lineal_one
            lab_one_job.InnerText = list[0].Employer;
            lab_one_tel.InnerText = list[0].Phone;
            lab_one_adress.InnerText = list[0].Address;




            lab_Lineal_two.InnerText = list[1].Name;
            lab_Lineal_one_mobile.InnerText = list[1].Phone;
            lab_Lineal_two.InnerText = list[1].Name;

            lab_two_job.InnerText = list[1].Employer;
            lab_two_tel.InnerText = list[1].Phone;
            lab_two_adress.InnerText = list[1].Address;





            //直系亲属

            lab_Lineal_two.InnerText = list[1].Name;



            //朋友辅导员
            switch (FriendList.Count)
            {
                case 1:
                    /*辅导员为第一条数据*/

                    break;
                case 2:

                    lab_Friend_one.InnerText = FriendList[1].Name;
                    lab_Friend_one_mobile.InnerText = FriendList[1].Phone;

                    break;
                case 3:

                    lab_Friend_one.InnerText = FriendList[1].Name;
                    lab_Friend_one_mobile.InnerText = FriendList[1].Phone;
                    lab_Friend_two.InnerText = FriendList[2].Name;
                    lab_Friend_two_mobile.InnerText = FriendList[2].Phone;
                    break;


            }






            var sumScore = new LoanScoreBll().GetSumScoreList(_loanId);
            lab_SumScore.InnerText = sumScore.Tables[0].Rows[0]["SumScore"].ToString();
            lab_ScoreLevel.InnerText = sumScore.Tables[0].Rows[0]["ScoreLevel"].ToString();

            if (model.studentLoanApply.ExamStatus == 1)
            {
                // button2.Attributes.Add("disabled", "disabled");
                // button2.CssClass = "inputButtonDisabled";
            }
            else if (model.studentLoanApply.ExamStatus == 2)
            {
                // button2.Attributes.Add("disabled", "disabled");
                // button2.CssClass = "inputButtonDisabled";
                button1.Attributes.Add("disabled", "disabled");
                button1.CssClass = "inputButtonDisabled";

            }
            #endregion
            if (model.studentLoanApply.LoanId > 0)
            {
                _loanId = model.studentLoanApply.LoanId;
                hidd_loanId.Value = _loanId.ToString();
                #region 已经生成的借款信息
                txtAutoBidScale.Value = model.loanModel.AutoBidScale.ToString();//字段投标比例
                txtBidAmountMin.Text = model.loanModel.BidAmountMin.ToString();
                BidAmountMax.Text = model.loanModel.BidAmountMax.ToString();
                WdataBidStartTime.Text = model.loanModel.BidStratTime.ToString("yyyy-MM-dd HH:mm:ss");
                WdataBidEndTime.Text = model.loanModel.BidEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                sel_Guarantee.Value = model.loanModel.GuaranteeID.ToString();//担保公司
                txtGuaranteeFee.Value = model.loanModel.GuaranteeFee.ToString();//担保费用
                txt_server1.Value = model.loanModel.LoanServiceCharges.ToString();
                if (string.IsNullOrEmpty(model.loanModel.BidServiceCharges.ToString()))
                {
                    txt_server1.Value = "1.00";//区间服务费

                }
                else if (Convert.ToDecimal(model.loanModel.BidServiceCharges.ToString()) <= 0)
                {
                    txt_server1.Value = "1.00";//区间服务费
                }

                else
                {
                    txt_server1.Value = model.loanModel.LoanServiceCharges.ToString();//区间服务费
                }

                txt_server2.Value = model.loanModel.BidServiceCharges.ToString();

                //合同信息
                text_ContractNo.Value = model.loanModel.ContractNo;
                text_Agency.Value = model.loanModel.Agency;
                text_LinkmanOne.Value = model.loanModel.LinkmanOne;
                txt_TelOne.Value = model.loanModel.TelOne;
                txt_TelTwo.Value = model.loanModel.TelTwo;
               // model.loanModel.NeedBidCharges
                #endregion



                #region 禁用按钮属性

                if ((model.loanModel.ExamStatus == 1 && !IsRole("9")) || (model.loanModel.ExamStatus == 3 && !IsRole("10")) || (model.loanModel.ExamStatus == 7 && !IsRole("11")) || (model.loanModel.ExamStatus != 1 && model.loanModel.ExamStatus != 3 && model.loanModel.ExamStatus != 7))
                {
                    button1.Attributes.Add("disabled", "disabled");
                    button1.CssClass = "inputButtonDisabled";

                    // button2.Attributes.Add("disabled", "disabled");
                    // button2.CssClass = "inputButtonDisabled";

                    BtnScore.Attributes.Add("disabled", "disabled");
                    BtnScore.CssClass = "inputButtonDisabled";
                    Button4.Attributes.Add("disabled", "disabled");
                    Button4.CssClass = "inputButtonDisabled";
                    BtnDeleteFile.Attributes.Add("disabled", "disabled");
                    BtnDeleteFile.CssClass = "inputButtonDisabled";
                    BtnAuth.Attributes.Add("disabled", "disabled");
                    BtnAuth.CssClass = "inputButtonDisabled";
                }
                if (model.studentLoanApply.ExamStatus == 2)
                {
                    button2.Attributes.Add("disabled", "disabled");
                    button2.CssClass = "inputButtonDisabled";
                }
                //初审拒绝
                if (model.loanModel.ExamStatus == 2)
                {
                    button2.Attributes.Add("disabled", "disabled");
                    button2.CssClass = "inputButtonDisabled";
                }


                if (model.loanModel.ExamStatus == 5)
                {
                    BtnAuth.Attributes.Add("disabled", "disabled");
                    BtnAuth.CssClass = "inputButtonDisabled";

                    BtnScore.Attributes.Add("disabled", "disabled");
                    BtnScore.CssClass = "inputButtonDisabled";
                }

                //禁用编辑和生成借款信息按钮
                if (model.studentLoanApply.LoanId > 0 && model.loanModel.ExamStatus == 5)
                {
                    button2.Attributes.Add("disabled", "disabled");
                    button2.CssClass = "inputButtonDisabled";
                }
                //非审核角色不能使用审核按钮
                if (!IsRole("9") && !IsRole("10") && !IsRole("11"))
                {
                    button1.Attributes.Add("disabled", "disabled");
                    button1.CssClass = "inputButtonDisabled";

                    Button4.Attributes.Add("disabled", "disabled");
                    Button4.CssClass = "inputButtonDisabled";

                    BtnScore.Attributes.Add("disabled", "disabled");
                    BtnScore.CssClass = "inputButtonDisabled";
                    BtnDeleteFile.Attributes.Add("disabled", "disabled");
                    BtnDeleteFile.CssClass = "inputButtonDisabled";
                    BtnAuth.Attributes.Add("disabled", "disabled");
                    BtnAuth.CssClass = "inputButtonDisabled";

                }
                #endregion
            }
            else
            {
                if (model.studentLoanApply.ExamStatus == 2)
                {
                    button2.Attributes.Add("disabled", "disabled");
                    button2.CssClass = "inputButtonDisabled";
                }
                LoadSetValue();
                BidAmountMax.Text = model.studentLoanApply.LoanAmount.ToString("F2");

            }




        }

    
        /// <summary>
        /// 判断是否包含权限
        /// </summary>
        /// <param name="role"></param>要判断的权限
        /// <returns></returns>
        public static bool IsRole(string role)
        {
            return (RightArray.IndexOf("|" + role + "|", StringComparison.Ordinal) >= 0);
        }
        protected void BtnAuth_Click(object sender, EventArgs e)
        {
            //认证
            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageWindow(590, 360, '/p2p/LoanAuthInfo.aspx?loanId=" + hidd_loanId.Value + "');", true);
        }


        protected void BtnScore_Click(object sender, EventArgs e)
        {
            var lsb = new LoanScoreBll();
            if (!string.IsNullOrEmpty(hidd_loanId.Value))
            {
                lsb.Add(Convert.ToInt32(hidd_loanId.Value), Convert.ToInt16(23));//添加学生贷款信息
            }

            //评分
            ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageWindow(590, 360, '/p2p/LoanScore.aspx?loanId=" + hidd_loanId.Value + "');", true);
        }

        /// <summary>
        /// 保存借款信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void button2_Click(object sender, EventArgs e)
        {
            //根据用户Id更改信息
            StudentLoanApply info = new StudentLoanApply();
            decimal LoanAmount = Convert.ToDecimal(Request["lab_LoanAmount"]);//借款金额
            info.MemberId = memberId;
            info.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);
            info.LoanAmount = LoanAmount;
            info.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);//利率
            // info.UseDescription = Request.Form["content1"];//借款描叙
            info.UseDescription = lab_UseDescription.InnerText.ToString();
            info.ID = StudentLoanApplyId;
            //sel_loanUseName
            info.LoanUseId = Convert.ToInt32(Request["sel_loanUseName"]);
            if (radio_audit.Checked)
            {
                info.ExamStatus = 1;
            }
            else if (radio_Audit_loading.Checked)
            {
                info.ExamStatus = 2;
            }

            info.UpdateTime = DateTime.Now;
            //借款信息审核结果
            string fcmsUserName = SessionHelper.Get("FcmsUserName").ToString();
            string curr_boor = string.Format("当前审核人Id为[{0},审核时间[{1}],审核结果[{2}]]", fcmsUserName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), radio_audit.Checked == true ? "审核通过" : "拒绝审核");
            info.AuditRecords = curr_boor;//审核人Id记录
            int relv = 0;
            //如果是复审 
            if (!string.IsNullOrEmpty(hidd_loanId.Value))
            {
                int loandId = Convert.ToInt32((hidd_loanId.Value.ToString()));

                //Convert.ToIn32(hidd_loanId.Value.ToString());
                _loan.Review_student(loandId, memberId, Convert.ToDecimal(Request["lab_LoanAmount"]), Convert.ToInt32(Request["lab_LoanTerm"]));
            }
            //更新数据hidd_loanId.Value

            //如果审核通过则插入借款信息表
            if (radio_audit.Checked && string.IsNullOrEmpty(hidd_loanId.Value))
            {
                if (string.IsNullOrEmpty(hidd_loanId.Value))
                {
                    //添加借款标信息
                    #region 借款标信息
                    LoanModel loanmodel = new LoanModel();
                    loanmodel.CityID = PlaceResidenceCity;//城市
                    loanmodel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                    loanmodel.MemberID = memberId;
                    loanmodel.LoanTitle = "学生贷";//借款标题
                    loanmodel.LoanAmount = LoanAmount;//借款金额
                    loanmodel.LoanTypeID = 23;//借款标类型=学生贷款
                    loanmodel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                    loanmodel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);//借款期限
                    loanmodel.RepaymentMethod = 4;//还款方式
                    loanmodel.BorrowMode = 1;//按月
                    loanmodel.TradeType = 0;//线上还是线下
                    loanmodel.ID = 0;//Id
                    loanmodel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    loanmodel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]) > Convert.ToDecimal(Request["lab_LoanAmount"]) ? Convert.ToDecimal(Request["lab_LoanAmount"]) : Convert.ToDecimal(Request["BidAmountMax"]);
                    loanmodel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);
                    loanmodel.LoanServiceCharges = Convert.ToDecimal(Request["txt_server1"]);
                    loanmodel.BidServiceCharges = Convert.ToDecimal(Request["txt_server2"]);
                    loanmodel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    loanmodel.BidStratTime = Convert.ToDateTime(WdataBidStartTime.Text);
                    loanmodel.BidEndTime = Convert.ToDateTime(WdataBidEndTime.Text);
                    loanmodel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);


                    #endregion
                    #region 家庭信息
                    LoanMemberInfoModel loanMemberInfoModel = new LoanMemberInfoModel();
                    loanMemberInfoModel.HaveCar = false;
                    loanMemberInfoModel.WorkingLife = "0";
                    loanMemberInfoModel.JobStatus = "在校学生";
                    loanMemberInfoModel.Age = 0;//借款人年龄
                    loanMemberInfoModel.MaritalStatus = 0;//是否已婚
                    loanMemberInfoModel.Sex = lab_Sex.InnerText;//性别
                    loanMemberInfoModel.DomicilePlace = 1;
                    loanMemberInfoModel.HaveHouse = false;
                    loanMemberInfoModel.FamilyNum = 1;//家庭人数
                    loanMemberInfoModel.MonthlyPay = "0";//月收入水平
                    #endregion
                    #region 企业信息
                    GreenChannelRecordModel greenChannelRecordModel = new GreenChannelRecordModel();
                    greenChannelRecordModel.BranchCompanyID = 0;
                    greenChannelRecordModel.Scale = 0;
                    #endregion

                    relv = _loan.AddLoan(loanmodel, loanMemberInfoModel, greenChannelRecordModel);//添加借款信息

                    if (relv > 0)
                    {
                        info.LoanId = relv;
                        hidd_loanId.Value = relv.ToString();

                        loanmodel.ID = relv;
                        /*新增数据有默认值，更新数据*/
                        _loan.Update_Student(loanmodel);
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('添加成功。','success', '');", true);
                    }

                }
                else
                {
                    //已经审核的信息不能修改
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('已经生成借款信息,不能修改学生借款信息。','error', '');", true);
                }
                //复审去列表审核 生成还款计划
                _student.UpdateStudentLoanApplyInfo(info);//更新当前信息
            }
            if (radio_Audit_loading.Checked && string.IsNullOrEmpty(hidd_loanId.Value))
            {
                if (_student.UpdateStudentLoanApplyInfo(info))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('修改成功。','success', '');", true);
                }//更新当前信息
            }


            LoadMemberInfo();
        }

        /// <summary>
        /// 生成还款信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void button1_Click(object sender, EventArgs e)
        {
            //根据relv查询信息
            string tem_StudentLoanApply = JsonConvert.SerializeObject(_student.GetLoan_StudentLoanApplyInfo(StudentLoanApplyId));
            StudentLoanApply model = JsonConvert.DeserializeObject<List<StudentLoanApply>>(tem_StudentLoanApply)[0];

            //参数验证 评分 担保公司
            if (Convert.ToInt32(sel_Guarantee.Value) <= 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请选择担保公司。','error', '');", true);
                return;
            }

            if (Convert.ToInt32(lab_SumScore.InnerText) <= 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('评分不能为0。','error', '');", true);
                return;
            }

            if (string.IsNullOrEmpty(Request["txt_server2"]))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('区间服务费不能为空。','error', '');", true);
                return;
            }

            //最大金额不能大于借款金额
            if (Convert.ToDecimal(BidAmountMax.Text.ToString()) > Convert.ToDecimal(lab_LoanAmount.Text.ToString()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('最大金额不能大于借款金额。','error', '');", true);
                return;
            }

            //判断是否生成还款信息
            if (model.LoanId > 0)
            {

                _loanInfoModel = new LoanBll().GetLoanInfoModel(" id =" + model.LoanId);
                _loanModel = new LoanBll().GetLoanModel(model.LoanId);
                //判断是否已经生成借款信息
                if (_loanModel.ExamStatus == 5)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('已经生成借款信息。','error', '');", true);
                    return;
                }



                var loanBll = new LoanBll();
                var auditHistoryModel = new AuditHistoryModel();

                if (!string.IsNullOrEmpty(hidd_loanId.Value))
                {
                    auditHistoryModel.LoanID = Convert.ToInt32(hidd_loanId.Value);
                }
                else
                {
                    auditHistoryModel.LoanID = -1;
                }

                auditHistoryModel.Result = radio_audit.Checked == true ? true : false;
                auditHistoryModel.Reason = "";//
                auditHistoryModel.UserID = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
                bool faulst = false;
                auditHistoryModel.ReviewComments = Request["textReviewComments"];
                #region 平台初审
                if (IsRole("9") && _loanModel.ExamStatus == 1) //平台初审
                {

                    _loanModel.AuthStatus = ControlHelper.GetCheckBoxList(ckbAuthList);
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    //默认初步审核
                    _loanModel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                    //_loanModel.ReleasedRate = Convert.ToDecimal(Request["txtReleasedRate"]);
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);

                    _loanModel.NeedGuarantee = true;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);

                    _loanModel.NeedLoanCharges = Convert.ToDecimal(Request["txt_server1"]) > 0;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txt_server1"]);

                    _loanModel.NeedBidCharges = Convert.ToDecimal(Request["txt_server2"]) > 0; ;
                    _loanModel.BidServiceCharges = Convert.ToDecimal(Request["txt_server2"]);

                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);



                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);

                    _loanModel.RepaymentMethod = 4;
                    _loanModel.ExamStatus = radio_audit.Checked ? 3 : 2; //已经审核
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.LoanTitle = "学生贷";//标题

                    // _loanModel.LoanTypeID = 23;//学生贷
                    _loanModel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                    _loanModel.LoanTypeID = 23;
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["lab_LoanAmount"]);


                    _loanModel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);

                    _loanModel.LoanDescribe = Request.Form["content1"];

                    _loanModel.BorrowMode = 1;
                    _loanModel.TradeType = 0;


                    auditHistoryModel.Process = 3;

                    _loanModel.SumScore = Convert.ToInt16(lab_SumScore.InnerText);
                    _loanModel.ScoreLevel = lab_ScoreLevel.InnerText;
                    //Reason

                    // _loanModel.Reason
                    _loanModel.ContractNo = Request["text_ContractNo"];
                    _loanModel.Agency = Request["text_Agency"];
                    _loanModel.LinkmanOne = Request["text_LinkmanOne"];
                    _loanModel.LinkmanTwo = Request["text_LinkmanTwo"];
                    _loanModel.TelOne = Request["txt_TelOne"];
                    _loanModel.TelTwo = Request["txt_TelTwo"];

                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);


                    faulst = true;
                }
                #endregion
                //平台二审
                #region 平台二审
                if (IsRole("10") && _loanModel.ExamStatus == 3 && faulst == false)
                {
                    _loanModel.AuthStatus = ControlHelper.GetCheckBoxList(ckbAuthList);
                    _loanModel.GuaranteeID = Convert.ToInt16(Request["sel_Guarantee"]);
                    //默认初步审核
                    _loanModel.LoanRate = Convert.ToDecimal(Request["lab_LoanRate"]);
                    //_loanModel.ReleasedRate = Convert.ToDecimal(Request["txtReleasedRate"]);
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.NeedGuarantee = true;
                    _loanModel.GuaranteeFee = Convert.ToDecimal(Request["txtGuaranteeFee"]);
                    _loanModel.NeedLoanCharges = Convert.ToDecimal(Request["txt_server1"]) > 0;
                    _loanModel.LoanServiceCharges = Convert.ToDecimal(Request["txt_server1"]);

                    _loanModel.NeedBidCharges = Convert.ToDecimal(Request["txt_server2"]) > 0; ;
                    _loanModel.BidServiceCharges = Convert.ToDecimal(Request["txt_server2"]);

                    _loanModel.BidAmountMin = Convert.ToDecimal(Request["txtBidAmountMin"]);
                    _loanModel.BidAmountMax = Convert.ToDecimal(Request["BidAmountMax"]);




                    _loanModel.BidStratTime = Convert.ToDateTime(Request["WdataBidStartTime"]);
                    _loanModel.BidEndTime = Convert.ToDateTime(Request["WdataBidEndTime"]);

                    _loanModel.RepaymentMethod = 4;
                    _loanModel.ExamStatus = radio_audit.Checked ? 5 : 4;  //已经审核
                    _loanModel.AutoBidScale = Convert.ToDecimal(Request["txtAutoBidScale"]);
                    _loanModel.LoanTitle = "学生贷";//标题

                    // _loanModel.LoanTypeID = 23;//学生贷
                    _loanModel.DimLoanUseID = Convert.ToInt32(Request["sel_loanUseName"]);
                    _loanModel.LoanTypeID = 23;
                    _loanModel.LoanAmount = Convert.ToDecimal(Request["lab_LoanAmount"]);

                    _loanModel.LoanTerm = Convert.ToInt32(Request["lab_LoanTerm"]);

                    _loanModel.LoanDescribe = Request.Form["content1"];

                    _loanModel.BorrowMode = 1;
                    _loanModel.TradeType = 0;
                   // auditHistoryModel.ReviewComments
                    auditHistoryModel.Process = 4;

                    int a = auditHistoryModel.ReviewComments.IndexOf("其");
                    int b = auditHistoryModel.ReviewComments.IndexOf("元");
                    string LoanDescribe = auditHistoryModel.ReviewComments.Replace(auditHistoryModel.ReviewComments.Substring(a, b - a), _loanModel.LoanAmount.ToString());
                    auditHistoryModel.ReviewComments = LoanDescribe;

                    _loanModel.SumScore = Convert.ToInt16(lab_SumScore.InnerText);
                    _loanModel.ScoreLevel = lab_ScoreLevel.InnerText;
                    //Reason

                    // _loanModel.Reason
                    _loanModel.ContractNo = Request["text_ContractNo"];
                    _loanModel.Agency = Request["text_Agency"];
                    _loanModel.LinkmanOne = Request["text_LinkmanOne"];
                    _loanModel.LinkmanTwo = Request["text_LinkmanTwo"];
                    _loanModel.TelOne = Request["txt_TelOne"];
                    _loanModel.TelTwo = Request["txt_TelTwo"];
                    //复审也能修改信息
                    int loandId = Convert.ToInt32((hidd_loanId.Value.ToString()));


                    ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                           loanBll.UpdateTran(_loanModel, auditHistoryModel)
                                                               ? "MessageAlert('审核成功。','success', '');"
                                                               : "MessageAlert('审核失败。','error', '');", true);
                    _loan.Review_student(loandId, memberId, Convert.ToDecimal(Request["lab_LoanAmount"]), Convert.ToInt32(Request["lab_LoanTerm"]));
                    LoadMemberInfo();
                }

                #endregion

                #region 财务初审
                #endregion

                #region 财务二审
                #endregion

                #region 审核信息



                #endregion
                //如果添加成功则更新信息

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('不能生成借款信息。','error', '');", true);
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string waterFile = FileUpload1.FileName;//水印文件
            string srcfile = "src/" + FileUpload1.FileName;//原始文件
            if (!string.IsNullOrEmpty(hidd_loanId.Value))
            {
                string uploadPath = DESStringHelper.EncryptString(hidd_loanId.Value);
                string filePath = ConfigHelper.LoanFilePhysicallPath;
                string fileEx = waterFile.Substring(waterFile.LastIndexOf(".", StringComparison.Ordinal) + 1);
                if (!string.IsNullOrEmpty(waterFile))
                {
                    if (!Directory.Exists(filePath + uploadPath))
                    {
                        Directory.CreateDirectory(filePath + uploadPath);
                    }
                    if (!Directory.Exists(filePath + uploadPath + "/src"))
                    {
                        Directory.CreateDirectory(filePath + uploadPath + "/src");
                    }
                    FileUpload1.SaveAs(filePath + uploadPath + "/" + srcfile);
                    System.Drawing.Image srcImg = System.Drawing.Image.FromFile(filePath + uploadPath + "/" + srcfile);
                    if (fileEx == "jpg" || fileEx == "bmp" || fileEx == "gif" || fileEx == "png")
                    {
                        System.Drawing.Image img = ImageWatermark.CreateWatermark(
                        srcImg,
                        System.Drawing.Image.FromFile(@"D:\files\logo.png"),
                        0.5F,
                        ContentAlignment.BottomRight);
                        img.Save(filePath + uploadPath + "/" + waterFile);//保存水印文件
                        img.Dispose();
                        srcImg.Dispose();
                    }
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件上传成功！','warning', '');", true);
                    FileInit();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件上传失败！','warning', '');", true);
                }
            }


        }
        protected void BtnDeleteFile_Click(object sender, EventArgs e)
        {
            string myfile = text_FileName.Value;
            string uploadPath = DESStringHelper.EncryptString(hidd_loanId.Value.ToString());
            string filePath = ConfigHelper.LoanFileVirtualPath;
            if (!string.IsNullOrEmpty(text_FileName.Value))
            {
                if (File.Exists(filePath + uploadPath + "/" + myfile))
                {
                    File.Delete(filePath + uploadPath + "/" + myfile);
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件删除成功！','warning', '');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('文件不存在！','warning', '');", true);
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入文件名！','warning', '');", true);
            }
            FileInit();
        }


        private void FileInit()
        {
            tbDirInfo.Rows.Clear();
            //获取虚拟路径
            string filePath = ConfigHelper.LoanFilePhysicallPath;
            string urlPath = ConfigHelper.LoanFileVirtualPath;
            string strCurrentDir;
            //strCurrentDir = filePath + DESStringHelper.EncryptString(_loanId.ToString());
            strCurrentDir = filePath + DESStringHelper.EncryptString(hidd_loanId.Value);
            FileInfo fi;
            DirectoryInfo di;
            TableCell td;
            TableRow tr;


            string FileName;	 //文件名称
            long FileSize;	 //文件大小
            DateTime FileModify;	 //文件更新时间

            DirectoryInfo dir = new DirectoryInfo(strCurrentDir);
            if (Directory.Exists(strCurrentDir))
            {
                foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
                {
                    FileSize = 0;

                    if (fsi is FileInfo)
                    {
                        //表示当前fsi是文件
                        fi = (FileInfo)fsi;
                        FileName = fi.Name;
                        FileSize = fi.Length;
                        FileModify = fi.LastWriteTime;
                        //通过扩展名来选择文件显示图标
                        //组建新的行
                        tr = new TableRow();

                        td = new TableCell();
                        td.Controls.Add(
                            new LiteralControl("<a href='" + urlPath + dir.Name + "/" + FileName.ToString() +
                                               "' target='_blank'>" + FileName.ToString() + "</a>"));
                        tr.Cells.Add(td);

                        td = new TableCell();
                        td.Controls.Add(new LiteralControl(FileSize.ToString()));
                        tr.Cells.Add(td);

                        td = new TableCell();
                        td.Controls.Add(new LiteralControl(FileModify.ToString()));
                        tr.Cells.Add(td);

                        //td.Controls.Add(
                        //    new LiteralControl("<a href='" + urlPath + dir.Name + "/" + FileName.ToString() +"' target='_blank'><img height=200px width=200px src='" + urlPath + dir.Name + "/" + FileName.ToString() + "' alt='" + FileName.ToString() + "' ></a>"));
                        //tr.Cells.Add(td);

                        //td = new TableCell();
                        //td.Controls.Add(new LiteralControl(FileName.ToString()));
                        //tr.Cells.Add(td);

                        tbDirInfo.Rows.Add(tr);
                    }
                }
            }
        }
    }
}