using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace ManageFcmsCommon
{
    public class ImageWatermark
    {
        public static Image CreateWatermark(
           string fileName,
           string markStr,
           Font font,
           Color color,
           float opacity,
           ContentAlignment markAlign)
        {
            return CreateWatermark(
                Image.FromFile(fileName),
                markStr,
                font,
                color,
                opacity,
                markAlign);
        }

        public static Image CreateWatermark(
            Image image,
            string markStr,
            Font font,
            Color color,
            float opacity,
            ContentAlignment markAlign)
        {
            if (image == null)
            {
                throw new ArgumentNullException("iamge");
            }

            if (font == null)
            {
                font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            }

            if (string.IsNullOrEmpty(markStr))
            {
                return image;
            }

            Rectangle textRect = new Rectangle(Point.Empty, image.Size);

            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter;

            switch (markAlign)
            {
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }

            Bitmap bmp = new Bitmap(image);
            color = Color.FromArgb((int)(255 * opacity), color);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.DrawString(
                        markStr,
                        font,
                        brush,
                        textRect,
                        sf);
                }
                g.Flush();
            }

            return bmp;
        }

        public static Image CreateWatermark(
            string fileName,
            string markFileName,
            Size markSize,
            float opacity,
            ContentAlignment markAlign)
        {
            return CreateWatermark(
                Image.FromFile(fileName),
                Image.FromFile(markFileName),
                markSize,
                opacity,
                markAlign);
        }

        public static Image CreateWatermark(
            Image image,
            Image markImage,
            Size markSize,
            float opacity,
            ContentAlignment markAlign)
        {
            if (image == null)
            {
                throw new ArgumentNullException("iamge");
            }

            if (markImage == null)
            {
                throw new ArgumentNullException("maskImage");
            }

            if (markSize == Size.Empty)
            {
                markSize = markImage.Size;
            }

            int width = image.Width;
            int height = image.Height;
            Rectangle maskRect = new Rectangle(Point.Empty, markSize);

            switch (markAlign)
            {
                case ContentAlignment.TopLeft:
                    maskRect.X = 2;
                    maskRect.Y = 2;
                    break;
                case ContentAlignment.TopCenter:
                    maskRect.X = (width - markSize.Width) / 2;
                    maskRect.Y = 2;
                    break;
                case ContentAlignment.TopRight:
                    maskRect.X = width - markSize.Width - 2;
                    maskRect.Y = 2;
                    break;
                case ContentAlignment.MiddleLeft:
                    maskRect.X = 2;
                    maskRect.Y = (height - markSize.Height) / 2;
                    break;
                case ContentAlignment.MiddleCenter:
                    maskRect.X = (width - markSize.Width) / 2;
                    maskRect.Y = (height - markSize.Height) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    maskRect.X = width - markSize.Width - 2;
                    maskRect.Y = (height - markSize.Height) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                    maskRect.X = 2;
                    maskRect.Y = height - markSize.Height - 2;
                    break;
                case ContentAlignment.BottomCenter:
                    maskRect.X = (width - markSize.Width) / 2;
                    maskRect.Y = height - markSize.Height - 2;
                    break;
                case ContentAlignment.BottomRight:
                    maskRect.X = width - markSize.Width - 2;
                    maskRect.Y = height - markSize.Height - 2;
                    break;
            }

            Bitmap bmp = new Bitmap(image);

            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = { 
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
                                                new float[] {0.0f,  0.0f,  0.0f,  opacity, 0.0f},        
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                    ColorAdjustType.Bitmap);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    using (Bitmap maskBmp = new Bitmap(markImage))
                    {
                        maskBmp.MakeTransparent();
                        g.DrawImage(
                            maskBmp,
                            maskRect,
                            0,
                            0,
                            markImage.Width,
                            markImage.Height,
                            GraphicsUnit.Pixel,
                            imageAttributes);
                    }
                    g.Flush();
                }
            }
            return bmp;
        }
        public static Image CreateWatermark(
            Image image,
            Image markImage,
            float opacity,
            ContentAlignment markAlign)
        {
            if (image == null)
            {
                throw new ArgumentNullException("iamge");
            }

            if (markImage == null)
            {
                throw new ArgumentNullException("maskImage");
            }


            var markSize = image.Size;

            int width = image.Width;
            int height = image.Height;
            Rectangle maskRect = new Rectangle(Point.Empty, markSize);

            Bitmap bmp = new Bitmap(image);
            markImage = CutImage(markImage, Point.Empty, width, height);
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = { 
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
                                                new float[] {0.0f,  0.0f,  0.0f,  opacity, 0.0f},        
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                    ColorAdjustType.Bitmap);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    using (Bitmap maskBmp = new Bitmap(markImage))
                    {
                        maskBmp.MakeTransparent();
                        g.DrawImage(
                            maskBmp,
                            maskRect,
                            0,
                            0,
                            markImage.Width,
                            markImage.Height,
                            GraphicsUnit.Pixel,
                            imageAttributes);
                    }
                    g.Flush();
                }
            }
            return bmp;
        }
        /// <summary>
        /// 截取图片区域，返回所截取的图片
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="pos"></param>
        /// <param name="cutWidth"></param>
        /// <param name="cutHeight"></param>
        /// <returns></returns>
        private static Image CutImage(Image srcImage, Point pos, int cutWidth, int cutHeight)
        {

            Image cutedImage = null;

            //先初始化一个位图对象，来存储截取后的图像
            Bitmap bmpDest = new Bitmap(cutWidth, cutHeight);
            Graphics g = Graphics.FromImage(bmpDest);

            //矩形定义,将要在被截取的图像上要截取的图像区域的左顶点位置和截取的大小
            Rectangle rectSource = new Rectangle(pos.X, pos.Y, cutWidth, cutHeight);


            //矩形定义,将要把 截取的图像区域 绘制到初始化的位图的位置和大小
            //rectDest说明，将把截取的区域，从位图左顶点开始绘制，绘制截取的区域原来大小
            Rectangle rectDest = new Rectangle(0, 0, cutWidth, cutHeight);

            //第一个参数就是加载你要截取的图像对象，第二个和第三个参数及如上所说定义截取和绘制图像过程中的相关属性，第四个属性定义了属性值所使用的度量单位
            g.DrawImage(srcImage, rectDest, rectSource, GraphicsUnit.Pixel);

            //在GUI上显示被截取的图像
            cutedImage = bmpDest;

            g.Dispose();

            return cutedImage;

        }
    }
}
