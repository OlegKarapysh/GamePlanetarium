using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.UnitTests;

public class QuestionImageTest
{
    private readonly QuestionImage _sut;
    private readonly string _imageName = "firstImage";
    private readonly byte[] _blackWhiteImage = { 1, 2 };
    private readonly byte[] _coloredImage = { 2, 1 };

    public QuestionImageTest()
    {
        _sut = new QuestionImage(_imageName, _blackWhiteImage, _coloredImage);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenQuestionImagesAreEqual()
    {
        // Assign.
        var equalSut = new QuestionImage(_imageName, _blackWhiteImage, _coloredImage);

        // Act.
        var result = _sut.Equals(equalSut);
        
        // Assert.
        result.Should()!.BeTrue();
    }
    
    [Theory]
    [InlineData("differentName", new byte[] { 0 }, new byte[] { 0 })]
    [InlineData("firstImage", new byte[] { 0, 0 }, new byte[] { 0, 0 })]
    [InlineData("firstImage", new byte[] { 1, 2 }, new byte[] { 0, 0 })]
    [InlineData("firstImage", new byte[] { 1, 2 }, new byte[] { 2, 0 })]
    public void Equals_ShouldReturnFalse_WhenQuestionImagesAreDifferent(
        string imageName, byte[] blackWhiteImage, byte[] coloredImage)
    {
        // Assign.
        var differentSut = new QuestionImage(imageName, blackWhiteImage, coloredImage);

        // Act.
        var result = _sut.Equals(differentSut);
        
        // Assert.
        result.Should()!.BeFalse();
    }

    [Theory]
    [InlineData(new byte[] { 0 }, new byte[] { 0 })]
    [InlineData(new byte[] { 1, 2 }, new byte[] { 0 })]
    [InlineData(new byte[] { 1, 2 }, new byte[] { 2, 0 })]
    public void GetHashCode_ShouldReturnDifferentCode_WhenQuestionImagesAreDifferent(
        byte[] blackWhiteImage, byte[] coloredImage)
    {
        // Assign.
        var differentSut = new QuestionImage("", blackWhiteImage, coloredImage);

        // Act.
        var result = _sut.GetHashCode() != differentSut.GetHashCode();
        
        // Assert.
        result.Should()!.BeTrue();
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameCode_WhenQuestionImagesAreEqual()
    {
        // Assign.
        var equalSut = new QuestionImage(_imageName, _blackWhiteImage, _coloredImage);
        
        // Act.
        var result = _sut.GetHashCode() == equalSut.GetHashCode();
        
        // Assert.
        result.Should()!.BeTrue();
    }

    [Fact]
    public void ToString_ShouldReturnImageName()
    {
        // Assign.
        var expected = _imageName;
        
        // Act.
        var result = _sut.ToString();
        
        // Assert.
        result.Should()!.Be(expected);
    }
}