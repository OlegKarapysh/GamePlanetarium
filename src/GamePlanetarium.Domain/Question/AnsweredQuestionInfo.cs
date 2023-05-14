using GamePlanetarium.Domain.Answer;

namespace GamePlanetarium.Domain.Question;

public class AnsweredQuestionInfo : EventArgs
{
    public required int QuestionNumber { get; init; }
    public required Answer.Answer FirstAnswer { get; init; }
    public required bool IsAnsweredCorrectly { get; init; }
}