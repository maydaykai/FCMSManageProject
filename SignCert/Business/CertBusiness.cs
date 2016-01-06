using System;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using SignCert.BusinessModel;
using SignCert.Common;
using SignCert.DataAccess;
using SignCert.Model;

namespace SignCert.Business
{
    public class CertBusiness
    {
        /// <summary>
        ///     检查用户有无证书
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool HasAvaibleCert(int userId)
        {
            return UserCertController.GetUserAvaibleCert(userId) != null;
        }

        /// <summary>
        ///     帮助用户实现自动申请证书
        /// </summary>
        /// <param name="user"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool AutoVerifyApplyCert(MemberInfoModel user, ref string errorMessage)
        {
            bool success = false;

            try
            {
                ApplyCertModel data = BuildApplyCertConditionByMemberInfo(user);
                string url = CommonData.CfcaSealUrl + GetApplyString(data);

                Log4NetHelper.WriteLog("准备申请或校验证书，接口地址与参数为：" + url);

                string sn = NetworkCommon.RequestUrl(url);

                if (!sn.StartsWith("<msg>"))
                {
                    UserCert cert = UserCertController.GetUserAvaibleCert(user.MemberID);
                    if (cert == null)
                    {
                        UserCertController.AddUserAvaibleCert(user.MemberID, sn); //用户新申请
                    }
                    else
                    {
                        if (sn != cert.CertSerialNumber) //RA端更新或者吊销了旧证书
                        {
                            UserCertController.SetUserCertUnavaible(user.MemberID);
                            UserCertController.AddUserAvaibleCert(user.MemberID, sn);
                        }
                    }

                    success = true;
                }
                else
                {
                    errorMessage = sn;
                }
            }
            catch (Exception err)
            {
                errorMessage = err.Message;
            }


            return success;
        }


        private ApplyCertModel BuildApplyCertConditionByMemberInfo(MemberInfoModel user)
        {
            var result = new ApplyCertModel();
            var memberBll = new MemberBll();
            const string none = "-";
            MemberModel member = memberBll.GetModel(user.MemberID);

            result.CertType = member.Type == 1 ? 1 : 2; //CFCA:1-机构证书 2-个人证书
            result.CertModel = 1; //CFCA:1 按年收费
            result.OrganCode = 678; //CFCA: 此处统一填写为678. 机构编码

            result.UserName = StringCommon.GetBase64(user.RealName ?? none); //个人标识，必须使用Base64编码
            result.OrgName = StringCommon.GetBase64(user.RealName ?? none); //企业标识，必须使用Base64编码
            result.EngName = member.MemberName ?? none; //默认使用注册账号代替英文名
            result.IdTypeCode = member.Type == 1 ? 3 : 1; //证件类型: 3-工商代码号 1-身份证
            result.Email = "21011210@qq.com";// member.Email ?? none;
            result.Address = user.Address ?? none;
            result.TelNo = user.Telephone ?? member.Mobile ?? none;

            result.UserIdNo = user.IdentityCard; //营业执照号/个人身份证号
            result.UserIdNum = user.IdentityCard; //营业执照号/个人身份证号（申请者）

            return result;
        }

        private string GetApplyString(ApplyCertModel data)
        {
            const string resultFormat =
                @"?certType={0}&certModel={1}&userName={2}&EngName={3}&IdTypeCode={4}&userIdNo={5}&email={6}&organCode={7}&orgName={8}&address={9}&TelNo={10}&userIdNum={11}";

            string result = string.Format(resultFormat, data.CertType, data.CertModel, data.UserName, data.EngName,
                                          data.IdTypeCode, data.UserIdNo, "21011210@qq.com", data.OrganCode, data.OrgName,
                                          data.Address, data.TelNo, data.UserIdNum);
            return result;
        }
    }
}