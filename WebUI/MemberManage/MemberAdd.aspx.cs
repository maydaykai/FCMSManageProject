using System;
using System.Data;
using System.Diagnostics;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.MemberManage
{
    public partial class MemberAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var bll = new MemberBll();
            currentNum.InnerText = bll.GetIdentityNumCount().ToString();
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            var realName = txtRealName.Value;
            var identity = txtIdentity.Value;
            var bll = new MemberBll();
            if (string.IsNullOrEmpty(realName))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('真实姓名不能为空。','warning','');", true);
                return;
            }
            if (string.IsNullOrEmpty(identity))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('身份证号码不能为空。','warning','');", true);
                return;
            }
            if (bll.IdCardExists(identity))//身份证已存在
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('该身份证号码已被注册','warning', '');", true);
                return;
            }
            var userName = StringHelper.GetFirstPYLetter(realName.Substring(0, 1)).ToLower() +
                            new Random().Next(10000, 100000) +
                            StringHelper.GetFirstPYLetter(realName.Substring(1, 1)).ToLower();//取真实姓名前两位拼音首字母，中间由5位随机数隔开
            var pwd = txtPwd.Value;
            var mobile = txtMobile.Value;
            const string reCode = "";
            var hanlderMessage = "";
            //if (string.IsNullOrEmpty(userName))
            //{
            //    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('用户名不能为空。','warning','');", true);
            //    return;
            //}
            //Regex nameRegex = new Regex("^[a-zA-Z0-9]{5,12}$");
            //if (!nameRegex.IsMatch(userName))
            //{
            //    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('用户名格式错误。','warning','');", true);
            //    return;
            //}
            if (!bll.UserNameExists(userName))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('用户名已存在，请换一个','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                pwd = txt_defaultPwd.Value;
            }
            pwd = DESStringHelper.GetMd5Hash(pwd);
            var id = bll.Add(new MemberModel
                    {
                        MemberName = userName, 
                        PassWord = pwd, 
                        TranPassWord = pwd, 
                        Mobile=mobile
                    }, reCode);
            bool flag = false;
            if (id > 0)
            {
                hanlderMessage += "注册成功。";
                flag = bll.AddIdentityAuthent(new IdentityAuthentModel
                    {
                        MemberID = id,
                        RealName = realName,
                        IdentityCard = identity,
                    });
                if (flag)
                {
                    hanlderMessage += "-----身份认证成功。";
                    flag = bll.ApplyVip(id);
                    if (flag)
                    {
                        var couponCode = bll.GetCouponCodeByMemberId(id);
                        hanlderMessage += "-----VIP申请成功。----优惠码为：" +
                                          couponCode;
                        LoanModel model = new LoanBll().GetLoanInfoModel("LoanTypeID=6 AND ExamStatus=5 AND TradeType=1");
                        if (model != null)
                        {
                            string errorMsg = "";
                            flag = bll.Add(new ProcInvestModel
                                {
                                    LoanID = model.ID,
                                    LoanMemberID = model.MemberID,
                                    BidMemberID = id,
                                    BidAmount = 10000,
                                    CouponCode = couponCode
                                }, ref errorMsg);
                            if (flag)
                                hanlderMessage += "-----投标成功。";
                            else
                                hanlderMessage += "-----" + errorMsg;
                        }else
                            hanlderMessage += "-----没有可投的新手标，请暂停操作，联系财务，待财务发标后，本次注册需手动投。";
                    }
                    else
                        hanlderMessage += "-----VIP申请失败。";
                }else
                    hanlderMessage += "-----身份认证失败。";
            }else
                hanlderMessage += "注册失败。";
            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                   "MessageAlert('" + hanlderMessage + "','" + (flag
                                                       ? "success"
                                                       : "error") + "', '');", true);
        }

        protected void Button_Loop(object sender, EventArgs e)
        {
            try
            {
                errorMsg.InnerText = "";
                var bll = new MemberBll();
                string num = txtNum.Value;
                if (string.IsNullOrEmpty(num))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('请输入要循环的条数。','warning', '');",
                                                           true);
                    return;
                }
                DataTable dt = bll.GetIdentityNumDt(Convert.ToInt32(num));
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] dataRows = null;
                    dataRows = dt.Select();
                    var mobile = txtMobile.Value;
                    var pwd = DESStringHelper.GetMd5Hash(txt_defaultPwd.Value);
                    const string reCode = "";
                    string hanlderMessage = "错误详情：";
                    int successNum = 0, failNum = 0;
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    foreach (DataRow dr in dataRows)
                    {
                        var realName = dr["Name"].ToString();
                        var identity = dr["IdentityNum"].ToString();
                        if (string.IsNullOrEmpty(realName) || string.IsNullOrEmpty(identity) ||
                            bll.IdCardExists(identity))
                        {
                            failNum += 1;
                            hanlderMessage += ("第" + failNum + "条：身份证号码已被注册。<br />");
                            bll.UpdateIdentityNumByID(Convert.ToInt32(dr["ID"]));
                            continue;
                        }
                        var userName = StringHelper.GetFirstPYLetter(realName.Substring(0, 1)).ToLower() +
                                        new Random().Next(10000, 100000) +
                                        StringHelper.GetFirstPYLetter(realName.Substring(1, 1)).ToLower();
                        //取真实姓名前两位拼音首字母，中间由5位随机数隔开
                        if (!bll.UserNameExists(userName))
                        {
                            failNum += 1;
                            hanlderMessage += ("第" + failNum + "条：用户名已被注册。<br />");
                            bll.UpdateIdentityNumByID(Convert.ToInt32(dr["ID"]));
                            continue;
                        }
                        var id = bll.Add(new MemberModel
                        {
                            MemberName = userName,
                            PassWord = pwd,
                            TranPassWord = pwd,
                            Mobile = mobile
                        }, reCode);
                        if (id > 0)
                        {
                            bool flag = bll.AddIdentityAuthent(new IdentityAuthentModel
                                {
                                    MemberID = id,
                                    RealName = realName,
                                    IdentityCard = identity,
                                });
                            if (flag)
                            {
                                flag = bll.ApplyVip(id);
                                if (flag)
                                {
                                    var couponCode = bll.GetCouponCodeByMemberId(id);
                                    LoanModel model =
                                        new LoanBll().GetLoanInfoModel("LoanTypeID=6 AND ExamStatus=5 AND TradeType=1");
                                    if (model != null)
                                    {
                                        string errorMsg2 = "";
                                        flag = bll.Add(new ProcInvestModel
                                            {
                                                LoanID = model.ID,
                                                LoanMemberID = model.MemberID,
                                                BidMemberID = id,
                                                BidAmount = 10000,
                                                CouponCode = couponCode
                                            }, ref errorMsg2);
                                        if (flag)
                                        {
                                            successNum += 1;
                                            bll.UpdateIdentityNumByID(Convert.ToInt32(dr["ID"]));
                                        }
                                        else
                                        {
                                            failNum += 1;
                                            hanlderMessage += ("第" + failNum + "条：" + errorMsg2 + "。<br />");
                                        }
                                    }
                                    else
                                    {
                                        failNum += 1;
                                        hanlderMessage += ("第" + failNum + "条：当前无可投新手标。<br />");
                                        break;
                                    }
                                }
                                else
                                {
                                    failNum += 1;
                                    hanlderMessage += ("第" + failNum + "条：VIP申请失败。<br />");
                                }
                            }
                            else
                            {
                                failNum += 1;
                                hanlderMessage += ("第" + failNum + "条：添加身份证信息失败。<br />");
                            }
                        }
                        else{
                            failNum += 1;
                            hanlderMessage += ("第" + failNum + "条：添加用户失败。<br />");
                        }
                        System.Threading.Thread.Sleep(new Random().Next(20, 40) * 1000);
                    }
                    stopwatch.Stop();
                    errorMsg.InnerHtml += "循环结果：成功条数：" + successNum + "，错误条数：" + failNum + "，共" + (successNum + failNum) +
                                         "条。" + "运行时长：" + stopwatch.Elapsed.TotalSeconds + "秒。<br />" + hanlderMessage;
                }
                else
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlert('可用身份证信息为空。','warning', '');",
                                                           true);
            }
            catch
            {
                errorMsg.InnerText = "错误信息：程序出错，请联系技术人员。";
            }
        }
    }
}