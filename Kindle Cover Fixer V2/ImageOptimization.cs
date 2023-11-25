using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using ImageMagick;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {

        private void ImageResizer(string sourceImage, string destinationImage, bool toGrayScale)
        {
            using (var imageMagick = new MagickImage(sourceImage))
            {
               // imageMagick.Transparent(MagickColor.FromRgb(0, 0, 0));
               // imageMagick.FilterType = FilterType.Quadratic;
                imageMagick.Resize(380, 570);
                imageMagick.ColorType = ColorType.Grayscale;
                imageMagick.Format = MagickFormat.Jpg;
                imageMagick.Write(destinationImage);
            }
        }
    }
}
