using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.Domain.Statistics;
using Moq;

namespace GamePlanetarium.Domain.UnitTests;

public class GameStatisticsTest
{
    private readonly Mock<IGameObservable> _gameObservableMock;
    private readonly GameStatisticsDataCollector _sut;

    public GameStatisticsTest()
    {
        _gameObservableMock = new Mock<IGameObservable>();
        _sut = new GameStatisticsDataCollector(_gameObservableMock.Object!);
    }

    [Fact]
    public void IsGameEnded_ShouldBeTrue_AfterGameEndedEventRaised()
    {
        // Act, Assert.
        _sut.IsGameEnded.Should()!.BeFalse();
        _gameObservableMock.Raise(g => { g.GameEnded += null!; }, EventArgs.Empty);
        _sut.IsGameEnded.Should()!.BeTrue();
    }

    [Fact]
    public void QuestionStatistics_ShouldBeUpdated_AfterTriedAnsweringQuestionEventRaised()
    {
        // Assign.
        const byte questionNumber = 0;
        const byte questionOrder = 1;
        const string firstAnswerText = "answer";
        var correctAnsweredQuestionInfo = new AnsweredQuestionInfo
        {
            QuestionNumber = questionNumber,
            FirstAnswer = new Answer.Answer(firstAnswerText + "wrong", Answers.First, true),
            IsAnsweredCorrectly = true
        };
        var wrongAnsweredQuestionInfo = new AnsweredQuestionInfo
        {
            QuestionNumber = questionNumber,
            FirstAnswer = new Answer.Answer(firstAnswerText, Answers.Second, false),
            IsAnsweredCorrectly = false
        };

        // Act.
        _gameObservableMock.Raise(g => { g.TriedAnsweringQuestion += null!; }, wrongAnsweredQuestionInfo);
        _gameObservableMock.Raise(g => { g.TriedAnsweringQuestion += null!; }, correctAnsweredQuestionInfo);
        
        // Assert.
        _sut.QuestionsStatistics.Count.Should()!.Be(1);
        _sut.QuestionsStatistics[questionNumber].QuestionOrder.Should()!.Be(questionOrder);
        _sut.QuestionsStatistics[questionNumber].FirstAnswerText.Should()!.Be(firstAnswerText);
        _sut.QuestionsStatistics[questionNumber].IncorrectAnswersCount.Should()!.Be(1);
    }
}