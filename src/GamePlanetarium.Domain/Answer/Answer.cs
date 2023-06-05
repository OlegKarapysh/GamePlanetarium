using System.Diagnostics.CodeAnalysis;

namespace GamePlanetarium.Domain.Answer;

public class Answer
{
    public required string Text { get; set; }
    public required Answers Number { get; init; }
    public required bool IsCorrect { get; init; }

    public Answer()
    {
    }
    [SetsRequiredMembers]
    public Answer(string text, Answers number, bool isCorrect)
    {
        Text = text;
        Number = number;
        IsCorrect = isCorrect;
    }
}