using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebUI.WebService
{
    /// <summary>
    ///     ▁▂▃▄▅▆▇█▇▆▅▄▃▂▁本WCF支持以下三种方法调用：▁▂▃▄▅▆▇█▇▆▅▄▃▂▁
    ///     1.VS直接添加服务引用
    ///     2.同域下AJAX请求(支持GET或POST)
    ///     3.WebRequest请求(支持GET或POST)
    ///     -----------------------------------------------------------------
    ///     ▁▂▃▄▅▆▇█▇▆▅▄▃▂▁注意：▁▂▃▄▅▆▇█▇▆▅▄▃▂▁
    ///     WebRequest如果传递实体对象，则对象的KEY必须和接口变量签名一致
    ///     AJAX使用POST时，需要将对象转化为对象字符串（建议使用JSON2库）
    ///     -----------------------------------------------------------------
    ///     ▁▂▃▄▅▆▇█▇▆▅▄▃▂▁兼容三种方式的开发原则：▁▂▃▄▅▆▇█▇▆▅▄▃▂▁
    ///     Get：
    ///     传入参数:CLR基本类型
    ///     返回参数:不限
    ///     Post：
    ///     传入参数：不限
    ///     返回参数：不限
    ///     -----------------------------------------------------------------
    ///     ▁▂▃▄▅▆▇█▇▆▅▄▃▂▁实践建议：▁▂▃▄▅▆▇█▇▆▅▄▃▂▁
    ///     不建议直接在前台用AJAX调用WCF。建议服务端调用WCF,AJAX与对应的服务端API通信。
    ///     获取资源一般使用GET，写入资源一般用POST。
    /// </summary>
    [ServiceContract]
    public interface ICertSignatureService
    {
        /// <summary>
        ///     新增:债权转让合同:电子签章合同生成任务
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST")]
        bool AddCreditAssignmentTask(string contractId);

        /// <summary>
        ///     设置电子证书托管服务启用或禁用
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST")]
        bool SetAutoSignStatus(string memberId, bool enable);

        /// <summary>
        ///     获取用户是否托管电子证书
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "GET")]
        bool GetAgreeAutoSignStatus(string memberId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    Method = "POST")]
        //Task AddTaskEntity(Task task);
    }

    //[DataContract]
    //public class Task
    //{
    //    [DataMember]
    //    public int LoanId { get; set; }
    //}
}