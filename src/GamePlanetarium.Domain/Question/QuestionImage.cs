namespace GamePlanetarium.Domain.Question;

public class QuestionImage
{
    public string ImageName { get; }
    public byte[] BlackWhiteImageSource { get; }
    public byte[] ColoredImageSource { get; }
    
    public QuestionImage(string imageName, byte[] blackWhiteImageSource, byte[] coloredImageSource)
    {
        ArgumentNullException.ThrowIfNull(imageName, nameof(imageName));
        ArgumentNullException.ThrowIfNull(blackWhiteImageSource, nameof(blackWhiteImageSource));
        ArgumentNullException.ThrowIfNull(coloredImageSource, nameof(coloredImageSource));
        ImageName = imageName;
        BlackWhiteImageSource = blackWhiteImageSource;
        ColoredImageSource = coloredImageSource;
    }

    public override int GetHashCode() => HashCode.Combine(BlackWhiteImageSource, ColoredImageSource);
    public override string ToString() => ImageName;
    public override bool Equals(object? obj)
    {
        return obj is QuestionImage otherImage &&
               otherImage.ImageName == ImageName &&
               otherImage.BlackWhiteImageSource.SequenceEqual(BlackWhiteImageSource) &&
               otherImage.ColoredImageSource.SequenceEqual(ColoredImageSource);
    }
}