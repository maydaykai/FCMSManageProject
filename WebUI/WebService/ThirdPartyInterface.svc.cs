using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WebUI.WebService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ThirdPartyInterface”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ThirdPartyInterface.svc 或 ThirdPartyInterface.svc.cs，然后开始调试。
    [ServiceContract(Namespace = "WebUI.WebService")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ThirdPartyInterface
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST")]
        public string GetSmsBalance()
        {
            var eucpSdkService = new cn.b2m.eucp.sdk4report.SDKService();
            var smsBalance = eucpSdkService.getBalance("6SDK-EMY-6688-KEVNM", "410359");
            return "{\"smsBalance\":\"" + smsBalance + "\"}";
        }
    }
}
