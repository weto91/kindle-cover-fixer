using ImageMagick;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private static void ImageResizer(string sourceImage, string destinationImage, bool resize, bool toGrayScale)
        {
            using var imageMagick = new MagickImage(sourceImage);
            if (resize)
            {
                imageMagick.Resize(380, 570);
            }
            if (toGrayScale)
            {
                imageMagick.ColorType = ColorType.Grayscale;
            }
            imageMagick.Format = MagickFormat.Jpg;
            imageMagick.Write(destinationImage);
        }
    }
}
