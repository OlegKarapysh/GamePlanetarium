using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Statistics;

public class GameStatisticsDataCollector
{
    public bool IsGameEnded { get; private set; }
    public Dictionary<int, QuestionStatisticsData> QuestionsStatistics { get; } = new();
    public int QuestionsAnsweredCount => QuestionsStatistics.Count;
    public DateOnly DateStamp { get; }

    public GameStatisticsDataCollector(IGameObservable game, DateOnly? dateStamp = null)
    {
        ArgumentNullException.ThrowIfNull(game);
        
        DateStamp = dateStamp ?? DateOnly.FromDateTime(DateTime.Today);
        game.GameEnded += OnGameEnded;
        game.TriedAnsweringQuestion += OnQuestionAnswered;
    }

    private void OnQuestionAnswered(object? sender, AnsweredQuestionInfo info)
    {
        int questionNumber = info.QuestionNumber;
        if (!QuestionsStatistics.ContainsKey(questionNumber))
        {
            QuestionsStatistics.Add(questionNumber, new QuestionStatisticsData
            {
                FirstAnswerText = info.FirstAnswer.Text,
                QuestionOrder = QuestionsAnsweredCount + 1,
            });
        }
        QuestionsStatistics[questionNumber].UpdateIncorrectAnswersCount(info.IsAnsweredCorrectly);
    }
    private void OnGameEnded(object? sender, EventArgs e) => IsGameEnded = true;
}