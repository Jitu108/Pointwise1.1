using Pointwise.Domain.Enums;
using System.Globalization;
using System.IO;

namespace Pointwise.Domain.Helper
{
    public static class FileHelper
    {
        public static Extension GetExtension(string fileName)
        {
            switch (Path.GetExtension(fileName).ToUpper(CultureInfo.InvariantCulture))
            {
                case "JPG":
                case "JPEG":
                    return Extension.JPG;
                case "GIF":
                    return Extension.GIF;
                case "PNG":
                    return Extension.PNG;
                case "TIFF":
                    return Extension.TIFF;

            }
            return 0;
        }
    }
}
