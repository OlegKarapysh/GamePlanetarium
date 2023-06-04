using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public abstract class ImageSeed
{
    public abstract QuestionImage[] QuestionImages { get; }
    protected const string ImageExtension = ".png";
    protected const string ImagesDirectoryName = "Images";
    protected const string ColoredImagePostfix = "-color";
    protected const string EngPostfix = "-en";
    
    protected byte[] ConvertImageToBytes(string imagePath)
    {
        using var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        using var memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    protected string BuildPathToImage(string imageName) => ImagesDirectoryName + "\\" + imageName;
}