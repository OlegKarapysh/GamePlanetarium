using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.UnitTests;

public class GameObservableTest
{
    private readonly QuestionImage _questionImage;
    private readonly Answer.Answer[] _answers1;
    private readonly Answer.Answer[] _answers2;
    private readonly IQuestion[] _questions;
    private readonly GameObservable _sut;

    public GameObservableTest()
    {
        _questionImage = new QuestionImage("QuestionImage1", new byte[] { 1, 2 }, new byte[] { 2, 1 });
        _answers1 = new Answer.Answer[]
        {
            new("answer1", Answers.First, false),
            new("answer2", Answers.Second, true),
            new("answer3", Answers.Third, false)
        };
        _answers2 = new Answer.Answer[]
        {
            new("answer4", Answers.First, true),
            new("answer5", Answers.Second, false),
            new("answer6", Answers.Third, false)
        };
        _questions = new IQuestion[]
        {
            new SingleAnswerQuestion("question1", _answers1, _questionImage),
            new SingleAnswerQuestion("question1", _answers2, _questionImage)
        };
        _sut = new GameObservable(_questions);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void TryAnswerQuestion_ShouldReturnTrue_WhenAnswerIsCorrect(byte questionNumber, int answerNumber)
    {
        // Assign.
        var correctAnswer = (Answers)answerNumber;
        
        // Act.
        var result = _sut.TryAnswerQuestion(questionNumber, correctAnswer);
        
        // Assert.
        result.Should()!.BeTrue();
    }
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 2)]
    public void TryAnswerQuestion_ShouldReturnFalse_WhenAnswerIsWrong(byte questionNumber, int answerNumber)
    {
        // Assign.
        var wrongAnswer = (Answers)answerNumber;
        
        // Act.
        var result = _sut.TryAnswerQuestion(questionNumber, wrongAnswer);
        
        // Assert.
        result.Should()!.BeFalse();
    }

    [Fact]
    public void TryAnswerQuestion_ShouldSetThatGameEnded_WhenAllQuestionsAreAnsweredCorrectly()
    {
        // Assign.
        var correctAnswer1 = Answers.Second;
        var wrongAnswer1 = Answers.Third;
        var correctAnswer2 = Answers.First;
        
        // Act.
        var firstQuestionAnsweredCorrectly = _sut.TryAnswerQuestion(0, correctAnswer1);
        var resultAfterOneQuestionAnswered = _sut.IsGameEnded;
        var firstQuestionAnsweredIncorrectly = _sut.TryAnswerQuestion(0, wrongAnswer1);
        var resultAfterWrongAnswer = _sut.IsGameEnded;
        var secondQuestionAnsweredCorrectly = _sut.TryAnswerQuestion(1, correctAnswer2);
        var resultAfterLastCorrectAnswer = _sut.IsGameEnded;
        
        // Assert.
        firstQuestionAnsweredCorrectly.Should()!.BeTrue();
        resultAfterOneQuestionAnswered.Should()!.BeFalse();
        firstQuestionAnsweredIncorrectly.Should()!.BeFalse();
        resultAfterWrongAnswer.Should()!.BeFalse();
        secondQuestionAnsweredCorrectly.Should()!.BeTrue();
        resultAfterLastCorrectAnswer.Should()!.BeTrue();
    }

    [Fact]
    public void TryAnswerQuestion_ShouldThrowException_WhenTryingToAnswerAfterGameEnded()
    {
        // Assign.
        var correctAnswer1 = Answers.Second;
        var correctAnswer2 = Answers.First;
        var tryAnswerAfterGameEnded = () => _sut.TryAnswerQuestion(0, correctAnswer1);
        
        // Act.
        _sut.TryAnswerQuestion(0, correctAnswer1);
        _sut.TryAnswerQuestion(1, correctAnswer2);

        // Assert.
        _sut.IsGameEnded.Should()!.BeTrue();
        tryAnswerAfterGameEnded.Should()!.Throw<InvalidOperationException>();
    }


    [Fact]
    public void TryAnswerQuestion_ShouldRaiseTriedAnsweringQuestionEvent_WithCorrectArgs()
    {
        // Assign.
        using var monitoredSut = _sut.Monitor();

        // Act.
        _sut.TryAnswerQuestion(0, Answers.First);
        
        // Assert.
        monitoredSut!.Should()!.Raise("TriedAnsweringQuestion")!
                    .WithSender(_sut)!
                    .WithArgs<AnsweredQuestionInfo>();
    }
    
    [Fact]
    public void TryAnswerQuestion_ShouldRaiseGameEndedEvent_AfterAllQuestionsAnswered()
    {
        // Assign.
        using var monitoredSut = _sut.Monitor();
        const Answers correctAnswer1 = Answers.Second;
        const Answers correctAnswer2 = Answers.First;
        
        // Act.
        _sut.TryAnswerQuestion(0, correctAnswer1);
        _sut.TryAnswerQuestion(1, correctAnswer2);
        
        // Assert.
        monitoredSut!.Should()!.Raise("GameEnded")!
                     .WithSender(_sut)!
                     .WithArgs<EventArgs>();
    }
}