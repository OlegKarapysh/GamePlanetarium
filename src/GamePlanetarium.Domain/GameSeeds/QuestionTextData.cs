using System.Diagnostics.CodeAnalysis;

namespace GamePlanetarium.Domain.GameSeeds;

public class QuestionTextData
{
    public required string QuestionText { get; init; }
    public required string[] AnswersText { get; init; }

    public QuestionTextData()
    {
    }
    [SetsRequiredMembers]
    public QuestionTextData(string questionText, string[] answersText)
    {
        QuestionText = questionText;
        AnswersText = answersText;
    }
}