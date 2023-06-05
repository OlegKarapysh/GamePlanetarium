using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.GameSeeds;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.UnitTests;

public class GameObservableTest
{
    private readonly Answer.Answer[] _answers1;
    private readonly Answer.Answer[] _answers2;
    private readonly IQuestion[] _questions;
    private readonly GameObservable _sut;

    public GameObservableTest()
    {
        var questionImage = new QuestionImage("QuestionImage1", new byte[] { 1, 2 }, new byte[] { 2, 1 });
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
            new SingleAnswerQuestion("question1", _answers1, questionImage),
            new SingleAnswerQuestion("question1", _answers2, questionImage)
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

    [Fact]
    public void GetCorrectAnswers_ShouldReturnArrayOfCorrectAnswers()
    {
        // Assign.
        var correctAnswers = new[] { Answers.Second, Answers.First };
        
        // Act.
        var actualAnswers = _sut.GetCorrectAnswers();
        
        // Assert.
        actualAnswers.Should()!.Equal(correctAnswers);
    }

    [Fact]
    public void GetQuestionTextSeed_ShouldReturnQuestionTextSeed()
    {
        // Act.
        var actualQuestionTextSeed = _sut.GetQuestionTextSeed();
        
        // Assert.
        actualQuestionTextSeed.Data.Should()!.HaveCount(_questions.Length);
        actualQuestionTextSeed.Data[0].QuestionText.Should()!.Be(_questions[0].Text);
        actualQuestionTextSeed.Data[1].QuestionText.Should()!.Be(_questions[1].Text);
        actualQuestionTextSeed.Data[0].AnswersText.Should()!.HaveCount(_answers1.Length);
        actualQuestionTextSeed.Data[1].AnswersText.Should()!.HaveCount(_answers2.Length);
        actualQuestionTextSeed.Data[0].AnswersText[0].Should()!.Be(_answers1[0].Text);
        actualQuestionTextSeed.Data[1].AnswersText[0].Should()!.Be(_answers2[0].Text);
    }

    [Fact]
    public void ChangeQuestionsTextBySeed_ShouldChangeQuestionsAndAnswersTextInGame()
    {
        // Assign.
        var seed = new QuestionTextSeed(new QuestionTextData[]
        {
            new("newQuestion1", new[] { "newAnswer1", "newAnswer2", "newAnswer3" }),
            new("newQuestion2", new[] { "newAnswer4", "newAnswer5", "newAnswer6" })
        });
        
        // Act.
        _sut.ChangeQuestionsTextBySeed(seed);
        
        // Assert.
        _sut.Questions[0].Text.Should()!.Be(seed.Data[0].QuestionText);
        _sut.Questions[1].Text.Should()!.Be(seed.Data[1].QuestionText);
        _sut.Questions[0].Answers[0].Text.Should()!.Be(seed.Data[0].AnswersText[0]);
        _sut.Questions[1].Answers[1].Text.Should()!.Be(seed.Data[1].AnswersText[1]);
    }
}