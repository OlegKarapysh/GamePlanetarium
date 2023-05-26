namespace GamePlanetarium.Domain.Statistics;

public class QuestionStatisticsData
{
    public required string FirstAnswerText { get; init; }
    public required byte QuestionOrder { get; init; }
    public byte IncorrectAnswersCount { get; private set; }

    public void UpdateIncorrectAnswersCount(bool isAnswerCorrect)
    {
        if (!isAnswerCorrect)
        {
            IncorrectAnswersCount++;
        }
    }
}