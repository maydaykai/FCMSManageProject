using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ManageFcmsCommon
{
    public static class ConfigHelper
    {
        /// <summary>
        /// //站点根目录物理路径
        /// </summary>
        public static string WebRoot = HttpRuntime.AppDomainAppPath;

        /// <summary>
        /// 征信认证系统webservice接口的用户名
        /// </summary>
        public static string ZxUserid = WebConfigurationManager.AppSettings["zx_UserID"];

        /// <summary>
        /// 征信认证系统webservice接口的密码
        /// </summary>
        public static string ZxPwd = WebConfigurationManager.AppSettings["zx_Pwd"];

        /// <summary>
        /// 合同文件虚拟路径
        /// </summary>
        public static string ContractVirtualPath = WebConfigurationManager.AppSettings["contractVirtualPath"];

        /// <summary>
        /// 合同文件物理路径
        /// </summary>
        public static string ContractPhysicallPath = WebConfigurationManager.AppSettings["contractPhysicallPath"];

        /// <summary>
        /// 上传图片虚拟路径
        /// </summary>
        public static string ImgVirtualPath = WebConfigurationManager.AppSettings["imgVirtualPath"];

        /// <summary>
        /// 上传图片物理路径
        /// </summary>
        public static string ImgPhysicallPath = WebConfigurationManager.AppSettings["imgPhysicallPath"];

        /// <summary>
        /// 身份证照片虚拟路径
        /// </summary>
        public static string IdPhotoVirtualPath = WebConfigurationManager.AppSettings["idPhotoVirtualPath"];

        /// <summary>
        /// 身份证照片物理路径
        /// </summary>
        public static string IdPhotoPhysicallPath = WebConfigurationManager.AppSettings["idPhotoPhysicallPath"];

        /// <summary>
        /// 借款标审核附件虚拟路径
        /// </summary>
        public static string LoanFileVirtualPath = WebConfigurationManager.AppSettings["loanFileVirtualPath"];

        /// <summary>
        /// 借款标审核附件物理路径
        /// </summary>
        public static string LoanFilePhysicallPath = WebConfigurationManager.AppSettings["loanFilePhysicallPath"];

        /// <summary>
        /// APP文件虚拟路径
        /// </summary>
        public static string AppFileVirtualPath = WebConfigurationManager.AppSettings["appFileVirtualPath"];

        /// <summary>
        /// APP文件物理路径
        /// </summary>
        public static string AppFilePhysicallPath = WebConfigurationManager.AppSettings["appFilePhysicallPath"];

        #region 连连支付配置文件
        // RSA银通公钥
        public static string YT_PUB_KEY = WebConfigurationManager.AppSettings["YT_PUB_KEY"];
        // RSA商户私钥
        public static string TRADER_PRI_KEY = WebConfigurationManager.AppSettings["TRADER_PRI_KEY"];
        // MD5 KEY
        public static string MD5_KEY = StringHelper.DesDecrypt(WebConfigurationManager.AppSettings["MD5_KEY"],"lianlian");
        // 接收异步通知地址
        public static string NOTIFY_URL = WebConfigurationManager.AppSettings["NOTIFY_URL"]; //请变更yourdomain为你的域名（及端口）
        // 商户编号
        public static string OID_PARTNER = WebConfigurationManager.AppSettings["OID_PARTNER"];   //请变更为您的商户号
        // 签名方式 RSA或MD5
        public static string SIGN_TYPE = "MD5";    					//请选择签名方式
        // 接口版本号，固定1.0
        public static string VERSION = "1.0";
        // 业务类型，连连支付根据商户业务为商户开设的业务类型； （101001：虚拟商品销售、109001：实物商品销售、108001：外部账户充值）
        public static string BUSI_PARTNER = WebConfigurationManager.AppSettings["BUSI_PARTNER"];  //请选择业务类型
        public static string CASHPAY_URL = "https://yintong.com.cn/traderapi/cardandpay.htm";  //代付请求地址
        public static string QUERYBALANCE_URL = "https://yintong.com.cn/traderapi/traderAcctQuery.htm";  //余额查询地址
        #endregion

    }
}
