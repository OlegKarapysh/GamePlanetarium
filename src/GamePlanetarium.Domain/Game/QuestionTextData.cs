namespace GamePlanetarium.Domain.Game;

public readonly struct QuestionTextData
{
    public string QuestionText { get; }
    public string[] AnswersText { get; }

    public QuestionTextData(string questionText, string[] answersText)
    {
        QuestionText = questionText;
        AnswersText = answersText;
    }
}