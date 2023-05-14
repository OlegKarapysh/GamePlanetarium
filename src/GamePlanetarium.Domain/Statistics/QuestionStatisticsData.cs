namespace GamePlanetarium.Domain.Statistics;

public class QuestionStatisticsData
{
    public required string FirstAnswerText { get; init; }
    public required int QuestionOrder { get; init; }
    public int IncorrectAnswersCount { get; private set; }

    public void UpdateIncorrectAnswersCount(bool isAnswerCorrect)
    {
        if (!isAnswerCorrect)
        {
            IncorrectAnswersCount++;
        }
    }
}