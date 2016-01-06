using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SignCert.Common
{
    public class CustomFont
    {
        public static BaseFont Msyh = BaseFont.CreateFont(@"c:\windows\fonts\msyh.ttf", BaseFont.IDENTITY_H,
                                                          BaseFont.NOT_EMBEDDED);

        public static Font Msyh8;
        public static Font Msyh8UnderLine;

        public static Font Msyh10;
        public static Font Msyh10BoldUnderLine;

        public static Font Msyh10Bold;

        public static Font Msyh12Bold;

        static CustomFont()
        {
            Msyh8 = new Font(Msyh, 8);

            Msyh8UnderLine = new Font(Msyh, 8);
            Msyh8UnderLine.SetStyle(Font.UNDERLINE);

            Msyh10 = new Font(Msyh, 10);

            Msyh10BoldUnderLine = new Font(Msyh, 10);
            Msyh10BoldUnderLine.SetStyle(Font.UNDERLINE | Font.BOLD);

            Msyh10Bold = new Font(Msyh, 10);
            Msyh10Bold.SetStyle(Font.BOLD);

            Msyh12Bold = new Font(Msyh, 14);
            Msyh12Bold.SetStyle(Font.BOLD);
        }
    }
}