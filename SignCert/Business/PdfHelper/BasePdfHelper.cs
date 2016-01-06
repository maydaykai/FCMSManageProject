using System;
using SignCert.BusinessContract;
using SignCert.Common;
using iTextSharp.text;

namespace SignCert.Business.PdfHelper
{
    public abstract class BasePdfHelper : IPdfHelper
    {
        protected static Chunk NewLine = new Chunk(Environment.NewLine);
        protected readonly int MiniFileSize = 1024 * 50;//50KB

        protected readonly Font Msyh10 = CustomFont.Msyh10;
        protected readonly Font Msyh10Bold = CustomFont.Msyh10Bold;
        protected readonly Font Msyh10BolderUnderLine = CustomFont.Msyh10BoldUnderLine;
        protected readonly Font Msyh12Bold = CustomFont.Msyh12Bold;
        protected readonly Font Msyh8 = CustomFont.Msyh8;
        protected readonly Font Msyh8UnderLine = CustomFont.Msyh8UnderLine;
        public abstract string FileName { get; }

        public abstract bool CanExecute();

        public abstract void Execute();
    }
}