using ManageFcmsCommon;
using SignCert.BusinessContract;
using SignCert.Common;

namespace SignCert.Business.CertSignHelper
{
    /// <summary>
    /// 平台公司和服务公司签名需要盖章
    /// </summary>
    public class ChapterGraphicsCertSign : ICerSigntHelper
    {
        //private PDFSeal _pdfseal;
        private string _sealPath;
        private string _pdfFile;
        private string _keyWord;

        public ChapterGraphicsCertSign Init(string sealPath, string pdfFile, string keyWord)
        {
            //_pdfseal = new PDFSeal();
            _sealPath = sealPath;
            _pdfFile = pdfFile;
            _keyWord = keyWord;
            return this;
        }

        public bool Sign()
        {
            //bool success = _pdfseal.addSeal(_sealPath, _pdfFile, _pdfFile, _keyWord);

            string command = GetCommand(_sealPath, _pdfFile, _pdfFile, _keyWord);
            bool success = SignCertServiceProxy.GetProxy().ExecuteCommand(command) == string.Empty;

            Log4NetHelper.WriteLog(string.Format("电子章{0}签章结果：{1}", _sealPath, success));

            return success;
        }

        /// <summary>
        /// 盖章模式下用户直接点击图章，故此处不提供检测
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool Verify(string content)
        {
            return true;
        }

        string GetCommand(string sealFile, string pdfFile, string outputFile, string keyText)
        {
            string libpath = @"c:\lib\";
            var command = string.Format("java -jar {0}PDFSealCore.jar sealfile={1} pdffile={2} addsealbytextposition={3} outputfile={4}", (object)libpath, (object)sealFile, (object)pdfFile, (object)keyText, (object)outputFile);
            return command;

        }

    }
}
