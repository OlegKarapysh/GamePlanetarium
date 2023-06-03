using System.IO;
using System.Windows.Media.Imaging;

namespace GamePlanetarium.Converters;

public class BitmapImageConverter
{
    public static byte[] ConvertImageToBytes(string imagePath)
    {
        using var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        using var memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public static BitmapImage ConvertBytesToImage(byte[] bytes)
    {
        var imageSource = new BitmapImage();
        using var memoryStream = new MemoryStream(bytes);
        imageSource.BeginInit();
        imageSource.StreamSource = memoryStream;
        imageSource.CacheOption = BitmapCacheOption.OnLoad;
        imageSource.EndInit();
        return imageSource;
    }
}