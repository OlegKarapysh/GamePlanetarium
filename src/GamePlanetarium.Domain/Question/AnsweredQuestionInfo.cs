namespace GamePlanetarium.Domain.Question;

public class AnsweredQuestionInfo : EventArgs
{
    public required byte QuestionNumber { get; init; }
    public required Answer.Answer FirstAnswer { get; init; }
    public required bool IsAnsweredCorrectly { get; init; }
}