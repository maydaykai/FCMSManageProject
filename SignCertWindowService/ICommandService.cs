using System.ServiceModel;

namespace SignCertWindowService
{
    [ServiceContract]
    public interface ICommandService
    {
        [OperationContract]
        string ExecuteCommand(string command);
    }
}