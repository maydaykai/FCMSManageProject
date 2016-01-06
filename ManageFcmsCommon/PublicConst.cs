using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsCommon
{
    public class PublicConst
    {
        public const string Success = "Success"; 
        public const string Error = "Error";
        public const string Warning = "Warning";
        public const string ServerError = "服务器内部错误";

        public const string RegAddSuccess = "恭喜您，注册成功";
        public const string RegAddFail = "对不起，注册失败，如有疑问请联系客服";
        public const string RegUserNameExist = "该用户名已被注册，请更换用户名";
        public const string RegMobileExist = "该手机号码已被注册，请更换手机号码";
        public const string RegEmailExist = "该邮箱已被注册，请更换邮箱";

        public const string PersonalAuthentAddSuccess = "恭喜您，个人认证成功";
        public const string PersonalAuthentAddFail = "对不起，认证失败，如有疑问请联系客服";
        public const string PersonalAuthentIDCardExist = "身份证号码已存在，请重新输入";
        public const string PersonalAuthentIDCardNotMatching = "身份证号码与您输入的姓名不匹配，请重新输入";

        public const string LoanScoreUpdateSuccess = "恭喜您，信用评分成功";
        public const string LoanScoreUpdateFail = "对不起，信用评分失败";

        public const string PersonalAuthentUrl = "/Member/PersonalAuthentication.aspx";
        public const string EnterpriseAuthentUrl = "/Member/EnterpriseAuthentication.aspx";
        public const string BankCardAuthentUrl = "/Member/BankCardAuthentication.aspx";
        public const string MemberDefaultUrl = "/Member/MemberDefault.aspx";

        public const int MinYear = 1970;
        public const int MinMonth = 01;
        public const int MinDay = 01;
        public static DateTime MinDate = new DateTime(MinYear, MinMonth, MinDay);
    }
}
