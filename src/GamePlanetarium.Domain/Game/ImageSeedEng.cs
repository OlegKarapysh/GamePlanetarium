using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public class ImageSeedEng : ImageSeed
{
    public override QuestionImage[] QuestionImages
    {
        get
        {
            var images = new List<QuestionImage>();
            for (int i = 1; i <= 15; i++)
            {
                var imageName = i + EngPostfix + ImageExtension;
                var coloredImageName = i + ColoredImagePostfix + EngPostfix + ImageExtension;
                var blackWhiteImage = ConvertImageToBytes(BuildPathToImage(imageName));
                var coloredImage = ConvertImageToBytes(BuildPathToImage(coloredImageName));
                var image = new QuestionImage(imageName, blackWhiteImage, coloredImage);
                images.Add(image);
            }

            return images.ToArray();
        }
    }
}