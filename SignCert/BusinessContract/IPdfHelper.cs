namespace SignCert.BusinessContract
{
    public interface IPdfHelper
    {
        string FileName { get; }

        bool CanExecute();

        void Execute();
    }
}