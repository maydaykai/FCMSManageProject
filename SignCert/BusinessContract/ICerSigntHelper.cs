namespace SignCert.BusinessContract
{
    public interface ICerSigntHelper
    {
        bool Sign();

        bool Verify(string content);
    }
}