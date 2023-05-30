using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.UnitTests;

public class QuestionTest
{
    private readonly QuestionImage _questionImage;
    private readonly Answer.Answer[] _correctAnswers;
    private readonly Answer.Answer[] _incorrectAnswers;
    private readonly Question.Question _sut;
    
    public QuestionTest()
    {
        _questionImage = new QuestionImage("QuestionImage1", new byte[] { 1, 2 }, new byte[] { 2, 1 });
        _correctAnswers = new Answer.Answer[]
        {
            new("answer1", Answers.First, false),
            new("answer2", Answers.Second, true),
            new("answer3", Answers.Third, false)
        };
        _incorrectAnswers = new Answer.Answer[]
        {
            new("answer1", Answers.First, false),
            new("answer2", Answers.Second, false),
            new("answer3", Answers.Third, false)
        };
        _sut = new SingleAnswerQuestion("", _correctAnswers, _questionImage);
    }
    [Fact]
    public void CreatingQuestion_ShouldThrowException_WhenConstructorArgsAreNull()
    {
        // Assign.
        var createWithSecondArgNull = () => new SingleAnswerQuestion("", null!, _questionImage);
        var createWithThirdArgNull = () => new SingleAnswerQuestion("", _correctAnswers, null!);
        
        // Act, Assert.
        createWithSecondArgNull.Should()!.Throw<ArgumentException>();
        createWithThirdArgNull.Should()!.Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreatingQuestion_ShouldThrowException_WhenAnswersAreIncorrect()
    {
        // Assign.
        var createWithIncorrectAnswers = () => new SingleAnswerQuestion("", _incorrectAnswers, _questionImage);
        var createMultiQuestionWithIncorrectAnswers =
            () => new MultiAnswerQuestion("", _incorrectAnswers, _questionImage);
        var createWithEmptyAnswers = () => new SingleAnswerQuestion("", new Answer.Answer[] { null! }, _questionImage);
        
        // Act, Assert.
        createWithIncorrectAnswers.Should()!.Throw<ArgumentException>();
        createMultiQuestionWithIncorrectAnswers.Should()!.Throw<ArgumentException>();
        createWithEmptyAnswers.Should()!.Throw<ArgumentException>();
    }
    
    [Fact]
    public void AnsweringQuestion_ShouldReturnTrue_WhenAnswerIsCorrect()
    {
        // Assign.
        var correctAnswer = _correctAnswers.First(a => a.IsCorrect);
        
        // Act.
        var result = _sut.TryAnswer(correctAnswer);
        
        // Assert.
        result.Should()!.BeTrue();
        _sut.IsAnswered.Should()!.BeTrue();
    }

    [Fact]
    public void AnsweringQuestion_ShouldReturnFalse_WhenAnswerIsWrong()
    {
        // Assign.
        var wrongAnswer = _correctAnswers.First(a => !a.IsCorrect);
        
        // Act.
        var result = _sut.TryAnswer(wrongAnswer);
        
        // Assert.
        result.Should()!.BeFalse();
        _sut.IsAnswered.Should()!.BeFalse();
    }

    [Fact]
    public void IsAnswered_ShouldBeAlwaysTrue_OnlyAfterCorrectAnswerIsGiven()
    {
        // Assign.
        var correctAnswer = _correctAnswers.First(a => a.IsCorrect);
        var wrongAnswer = _correctAnswers.First(a => !a.IsCorrect);
        
        // Act.
        var resultBeforeAnyAnswers = _sut.IsAnswered;
        _sut.TryAnswer(wrongAnswer);
        var resultAfterWrongAnswer = _sut.IsAnswered;
        _sut.TryAnswer(correctAnswer);
        var resultAfterCorrectAnswer = _sut.IsAnswered;
        _sut.TryAnswer(wrongAnswer);
        var resultAfterQuestionAnswered = _sut.IsAnswered;
        
        // Assert.
        resultBeforeAnyAnswers.Should()!.BeFalse();
        resultAfterWrongAnswer.Should()!.BeFalse();
        resultAfterCorrectAnswer.Should()!.BeTrue();
        resultAfterQuestionAnswered.Should()!.BeTrue();
    }
}