using System.Diagnostics.CodeAnalysis;

namespace GamePlanetarium.Domain.Question;

public class MultiAnswerQuestion : Question
{
    public Answer.Answer[] CorrectAnswers { get; }
        
    [SetsRequiredMembers]
    public MultiAnswerQuestion(string text, Answer.Answer[] answers, QuestionImage image)
        : base(text, answers, image)
    {
        CorrectAnswers = answers.Where(a => a.IsCorrect).ToArray();
        ThrowIfNoCorrectAnswers(CorrectAnswers);
    }
        
    public override bool CheckIfCorrect(params Answer.Answer[] answers) =>
        answers.Length == CorrectAnswers.Length && answers.All(CorrectAnswers.Contains);
}