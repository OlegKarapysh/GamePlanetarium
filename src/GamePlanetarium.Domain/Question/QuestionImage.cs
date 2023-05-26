namespace GamePlanetarium.Domain.Question;

public class QuestionImage
{
    public required string ImageName { get; init; }
    public required byte[] BlackWhiteImageSource { get; init; }
    public required byte[] ColoredImageSource { get; init; }

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