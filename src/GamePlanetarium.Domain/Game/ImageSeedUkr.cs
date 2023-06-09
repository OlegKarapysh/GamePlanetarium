using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public class ImageSeedUkr : ImageSeed
{
    public override QuestionImage[] QuestionImages
    {
        get
        {
            var images = new List<QuestionImage>();
            for (int i = 1; i <= GameObservable.QuestionsCount; i++)
            {
                var imageName = i + ImageExtension;
                var coloredImageName = i + ColoredImagePostfix + ImageExtension;
                var blackWhiteImage = ConvertImageToBytes(BuildPathToImage(imageName));
                var coloredImage = ConvertImageToBytes(BuildPathToImage(coloredImageName));
                var image = new QuestionImage(imageName, blackWhiteImage, coloredImage);
                images.Add(image);
            }

            return images.ToArray();
        }
    }
}