using System;
using System.Security.Cryptography.X509Certificates;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using SignCert.BusinessContract;
using SignCert.Common;
using SignCert.DataAccess;
using SignCert.DataAccess.CFCA;
using SignCert.Model;
using iTextSharp.text;

namespace SignCert.Business.CertSignHelper
{
    /// <summary>
    ///     借款人和投资人签名需要私钥加密信息后写入PDF
    /// </summary>
    public class WordsCertSign : ICerSigntHelper
    {
        private Document _document;
        private string _keyword;
        private int _userId;
        private string _userName;
        private string _userType;

        //这种方式是把用户的签名附加在后面
        public bool Sign()
        {
            //0.确保用户有证书
            var cb = new CertBusiness();
            MemberInfoModel memberInfo = CommonData.WorkInUnitTestModel ? new MemberInfoModel { MemberID = 1 } : new MemberInfoBll().GetModel(_userId);

            string message = string.Empty;
            bool success = CommonData.WorkInUnitTestModel || cb.AutoVerifyApplyCert(memberInfo, ref message);
            if (!success)
            {
                string contentErrorTitle = string.Format("{0}({1})签名信息：", _userType, _userName);
                string contentErrorCertSn = string.Format("{0}", message);
                var contentError = new Paragraph {Alignment = Element.ALIGN_LEFT};
                contentError.Add(new Chunk(Environment.NewLine));
                contentError.Add(new Chunk(contentErrorTitle, CustomFont.Msyh10Bold));
                contentError.Add(new Chunk(Environment.NewLine));
                contentError.Add(new Chunk(contentErrorCertSn, CustomFont.Msyh10Bold));
                _document.Add(contentError);

                Log4NetHelper.WriteLog(string.Format("用户：{0}({1})申请证书失败,原因：{2}。", memberInfo.RealName,
                                                     memberInfo.MemberID, message));
                return false;
            }

            Log4NetHelper.WriteLog(string.Format("用户：{0}({1})证书校验通过。", memberInfo.RealName, memberInfo.MemberID));

            //1.需要拿到用户的证书
            string certSn = UserCertController.GetUserAvaibleCert(_userId).CertSerialNumber;
            TBL_EASTRACERT cert = EastRaCertController.GetCertBySn(certSn); //得到证书信息

            //2.需要签名
            var certInfo = new X509Certificate2(cert.PFXCPNTENT.ToArray(), cert.PASSWORD, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            string certPrivateKey = certInfo.PrivateKey.ToXmlString(true);
            
            //3.需要写入PDF
            string contentTitle = string.Format("{0}({1})签名信息：", _userType, _userName);
            string contentCertSn = string.Format("证书序号：{0}-{1}", certSn, certInfo.SerialNumber);
            string contentSign = CertCommon.RsaEncrypt(certPrivateKey, _keyword); //此处为签名后的信息

            var content = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content.Add(new Chunk(Environment.NewLine));
            content.Add(new Chunk(contentTitle, CustomFont.Msyh10Bold));
            content.Add(new Chunk(Environment.NewLine));
            content.Add(new Chunk(contentCertSn, CustomFont.Msyh10Bold));
            content.Add(new Chunk(Environment.NewLine));
            content.Add(new Phrase(contentSign, CustomFont.Msyh8));
            _document.Add(content);

            UserCertOverviewController.AddSignNumber(_userId); //增加签名次数

            return true;
        }


        public bool Verify(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return false;

            string certSn = UserCertController.GetUserAvaibleCert(_userId).CertSerialNumber;
            TBL_EASTRACERT cert = EastRaCertController.GetCertBySn(certSn);

            var certInfo = new X509Certificate2(cert.PFXCPNTENT.ToArray(), cert.PASSWORD, X509KeyStorageFlags.Exportable);

            //var certPublicKey = certInfo.PublicKey.ToString();//此处一般使用公钥
            string certPrivateKey = certInfo.PrivateKey.ToXmlString(true);

            string contentDecrypt = CertCommon.RsaDecrypt(certPrivateKey, _keyword);
            return content.Equals(contentDecrypt);
        }

        public WordsCertSign Init(Document document, int userId, string userName, string keyword, string userType)
        {
            _document = document;
            _userId = userId;
            _userName = userName;
            _userType = userType;
            _keyword = keyword;

            return this;
        }
    }
}